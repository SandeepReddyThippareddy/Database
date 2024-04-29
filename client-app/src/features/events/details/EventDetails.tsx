import { Grid } from "semantic-ui-react";
import { useStore } from '../../../app/stores/store';
import { observer } from 'mobx-react-lite';
import LoadingComponent from '../../../app/layout/LoadingComponent';
import { useParams } from "react-router-dom";
import { useEffect } from "react";
import EventDetailedChat from "./EventDetailedChat";
import EventDetailedHeader from "./EventDetailedHeader";
import EventDetailedInfo from "./EventDetailedInfo";
import EventDetailedSidebar from "./EventDetailedSidebar";
import Feedback from "./Feedback";

export default observer(function EventDetails() {
    const { EventStore } = useStore();
    const { selectedEvent: Event, loadEvent, loadingInitial, clearSelectedEvent } = EventStore;
    const { id } = useParams();

    useEffect(() => {
        if (id) loadEvent(id);
        return () => clearSelectedEvent();
    }, [id, loadEvent, clearSelectedEvent]);

    if (loadingInitial || !Event) return <LoadingComponent />

    return (
        <Grid>
            <Grid.Column width='10'>
                <EventDetailedHeader Event={Event} />
                <EventDetailedInfo Event={Event} />
                <EventDetailedChat EventId={Event.id} />
            </Grid.Column>
            <Grid.Column width='6'>
                <EventDetailedSidebar Event={Event}/>
                <Feedback EventId={Event.id} />
            </Grid.Column>
        </Grid>
    )
})