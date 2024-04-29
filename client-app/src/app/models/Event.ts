import { Profile } from "./profile";

export interface Event {
    id: string;
    title: string;
    description: string;
    category: string;
    date: Date | null;
    city: string;
    venue: string;
    hostUsername?: string;
    isCancelled?: boolean;
    isGoing?: boolean;
    isHost?: boolean
    attendees: Profile[]
    host?: Profile;
}

export class EventFormValues
  {
    id?: string = undefined;
    title: string = '';
    category: string = '';
    description: string = '';
    date: Date | null = null;
    city: string = '';
    venue: string = '';

	  constructor(Event?: EventFormValues) {
      if (Event) {
        this.id = Event.id;
        this.title = Event.title;
        this.category = Event.category;
        this.description = Event.description;
        this.date = Event.date;
        this.venue = Event.venue;
        this.city = Event.city;
      }
    }

  }

  export class Event implements Event {
    constructor(init?: EventFormValues) {
      Object.assign(this, init);
    }
  }
