import { makeAutoObservable, reaction, runInAction } from "mobx";
import { Event, EventFormValues } from "../models/Event";
import agent from "../api/agent";
import { store } from "./store";
import { Profile } from "../models/profile";
import { Pagination, PagingParams } from "../models/pagination";

export default class EventStore {
    EventRegistry = new Map<string, Event>();
    selectedEvent?: Event = undefined;
    editMode = false;
    loading = false;
    loadingInitial = false;
    pagination: Pagination | null = null;
    pagingParams = new PagingParams();
    predicate = new Map().set('all', true);

    constructor() {
        makeAutoObservable(this);

        reaction(
            () => this.predicate.keys(),
            () => {
                this.pagingParams = new PagingParams();
                this.EventRegistry.clear();
                this.loadEvents();
            }
        )
    }

    setPagingParams = (pagingParams: PagingParams) => {
        this.pagingParams = pagingParams;
    }

    setPredicate = (predicate: string, value: string | Date) => {
        const resetPredicate = () => {
            this.predicate.forEach((value, key) => {
                if (key !== 'startDate') this.predicate.delete(key);
            })
        }
        switch (predicate) {
            case 'all':
                resetPredicate();
                this.predicate.set('all', true);
                break;
            case 'isGoing':
                resetPredicate();
                this.predicate.set('isGoing', true);
                break;
            case 'isHost':
                resetPredicate();
                this.predicate.set('isHost', true);
                break;
            case 'startDate':
                this.predicate.delete('startDate');
                this.predicate.set('startDate', value);
                break;
        }
    }

    get axiosParams() {
        const params = new URLSearchParams();
        params.append('pageNumber', this.pagingParams.pageNumber.toString());
        params.append('pageSize', this.pagingParams.pageSize.toString())
        this.predicate.forEach((value, key) => {
            if (key === 'startDate') {
                params.append(key, (value as Date).toISOString())
            } else {
                params.append(key, value);
            }
        })
        return params;
    }

    get groupedEvents() {
        return Object.entries(
            this.eventsByDate.reduce((events, Event) => {
                const date = Event.date!.toISOString().split('T')[0];
                events[date] = events[date] ? [...events[date], Event] : [Event];
                return events;
            }, {} as { [key: string]: Event[] })
        )
    }

    get eventsByDate() {
        return Array.from(this.EventRegistry.values()).sort((a, b) =>
            a.date!.getTime() - b.date!.getTime());
    }

    loadEvents = async () => {
        this.loadingInitial = true;
        try {
            const result = await agent.Events.list(this.axiosParams);
            result.data.forEach(Event => {
                this.setEvent(Event);
            })
            this.setPagination(result.pagination);
            this.setLoadingInitial(false);
        } catch (error) {
            console.log(error);
            this.setLoadingInitial(false);
        }
    }

    setPagination = (pagination: Pagination) => {
        this.pagination = pagination;
    }

    loadEvent = async (id: string) => {
        let Event = this.getEvent(id);
        if (Event) {
            this.selectedEvent = Event;
            return Event;
        }
        else {
            this.setLoadingInitial(true);
            try {
                Event = await agent.Events.details(id);
                this.setEvent(Event);
                runInAction(() => this.selectedEvent = Event);
                this.setLoadingInitial(false);
                return Event;
            } catch (error) {
                console.log(error);
                this.setLoadingInitial(false);
            }
        }
    }

    private setEvent = (Event: Event) => {
        const user = store.userStore.user;
        if (user) {
            Event.isGoing = Event.attendees!.some(
                a => a.username === user.username
            );
            Event.isHost = Event.hostUsername === user.username;
            Event.host = Event.attendees?.find(x => x.username === Event.hostUsername);
        }
        Event.date = new Date(Event.date!);
        this.EventRegistry.set(Event.id, Event);
    }

    private getEvent = (id: string) => {
        return this.EventRegistry.get(id);
    }

    setLoadingInitial = (state: boolean) => {
        this.loadingInitial = state;
    }

    createEvent = async (event: EventFormValues) => {
        const user = store.userStore!.user;
        const profile = new Profile(user!);
        try {
            await agent.Events.create(event);
            const newEvent = new Event(event);
            newEvent.hostUsername = user!.username;
            newEvent.attendees = [profile];
            this.setEvent(newEvent);
            runInAction(() => this.selectedEvent = newEvent);
        } catch (error) {
            console.log(error);
        }
    }

    updateEvent = async (Event: EventFormValues) => {
        try {
            await agent.Events.update(Event);
            runInAction(() => {
                if (Event.id) {
                    let updatedEvent = { ...this.getEvent(Event.id), ...Event };
                    this.EventRegistry.set(Event.id, updatedEvent as Event);
                    this.selectedEvent = updatedEvent as Event;
                }
            })
        } catch (error) {
            console.log(error);
        }
    }

    deleteEvent = async (id: string) => {
        this.loading = true;
        try {
            await agent.Events.delete(id);
            runInAction(() => {
                this.EventRegistry.delete(id);
                this.loading = false;
            })
        } catch (error) {
            console.log(error);
            runInAction(() => {
                this.loading = false;
            })
        }
    }

    updateAttendeance = async () => {
        const user = store.userStore.user;
        this.loading = true;
        try {
            await agent.Events.attend(this.selectedEvent!.id);
            runInAction(() => {
                if (this.selectedEvent?.isGoing) {
                    this.selectedEvent.attendees = this.selectedEvent.attendees?.filter(a => a.username !== user?.username);
                    this.selectedEvent.isGoing = false;
                } else {
                    const attendee = new Profile(user!);
                    this.selectedEvent?.attendees?.push(attendee);
                    this.selectedEvent!.isGoing = true;
                }
                this.EventRegistry.set(this.selectedEvent!.id, this.selectedEvent!);
            })
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => this.loading = false);
        }
    }

    cancelEventToggle = async () => {
        this.loading = true;
        try {
            await agent.Events.attend(this.selectedEvent!.id);
            runInAction(() => {
                this.selectedEvent!.isCancelled = !this.selectedEvent!.isCancelled;
                this.EventRegistry.set(this.selectedEvent!.id, this.selectedEvent!);
            })
        } catch (error) {
            console.log(error);
        } finally {
            runInAction(() => this.loading = false);
        }
    }

    clearSelectedEvent = () => {
        this.selectedEvent = undefined;
    }

    updateAttendeeFollowing = (username: string) => {
        this.EventRegistry.forEach(Event => {
            Event.attendees.forEach((attendee: Profile) => {
                if (attendee.username === username) {
                    attendee.following ? attendee.followersCount-- : attendee.followersCount++;
                    attendee.following = !attendee.following;
                }
            })
        })
    }
}