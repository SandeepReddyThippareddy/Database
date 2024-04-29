import { format } from "date-fns";
import { Link } from "react-router-dom";
import { Item, Button, Icon, Segment, Label } from "semantic-ui-react";
import { Event } from "../../../app/models/Event";
import EventListItemAttendee from "./EventListItemAttendee";

interface Props {
    Event: Event
}

export default function EventListItem({ Event }: Props) {
    return (
        <Segment.Group>
            <Segment>
                {Event.isCancelled &&
                    <Label attached='top' color='red' content='Cancelled' style={{ textAlign: 'center' }} />}
                <Item.Group>
                    <Item>
                        <Item.Image style={{marginBottom: 5}} size='tiny' circular 
                            src={Event.host?.image || '/assets/user.png'} />
                        <Item.Content>
                            <Item.Header as={Link} to={`/events/${Event.id}`}>
                                {Event.title}
                            </Item.Header>
                            <Item.Description>Hosted by <Link to={`/profiles/${Event.hostUsername}`}>{Event.host?.displayName}</Link></Item.Description>
                            {Event.isHost && (
                                <Item.Description>
                                    <Label basic color='orange'>
                                        You are hosting this Event!
                                    </Label>
                                </Item.Description>
                            )}
                            {Event.isGoing && !Event.isHost && (
                                <Item.Description>
                                    <Label basic color='green'>
                                        You are going to this Event!
                                    </Label>
                                </Item.Description>
                            )}
                        </Item.Content>
                    </Item>
                </Item.Group>
            </Segment>
            <Segment>
                <span>
                    <Icon name='clock' /> {format(Event.date!, 'dd MMM yyyy h:mm aa')}
                    <Icon name='marker' /> {Event.venue}
                </span>
            </Segment>
            <Segment secondary>
                <EventListItemAttendee attendees={Event.attendees!} />
            </Segment>
            <Segment clearing>
                <span>{Event.description}</span>
                <Button
                    as={Link}
                    to={`/events/${Event.id}`}
                    color='teal'
                    floated='right'
                    content='View'
                />
            </Segment>
        </Segment.Group>
    )
}