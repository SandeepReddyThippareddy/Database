import { format } from 'date-fns';
import { observer } from 'mobx-react-lite';
import React from 'react'
import { Link } from 'react-router-dom';
import { Button, Header, Item, Segment, Image, Label } from 'semantic-ui-react'
import { Event } from "../../../app/models/Event";
import { useStore } from '../../../app/stores/store';

const EventImageStyle = {
    filter: 'brightness(30%)'
};

const EventImageTextStyle = {
    position: 'absolute',
    bottom: '5%',
    left: '5%',
    width: '100%',
    height: 'auto',
    color: 'white'
};

interface Props {
    Event: Event
}

export default observer(function EventDetailedHeader({ Event }: Props) {
    const { EventStore: { updateAttendeance, loading, cancelEventToggle } } = useStore();
    return (
        <Segment.Group>
            <Segment basic attached='top' style={{ padding: '0' }}>
                {Event.isCancelled &&
                    <Label style={{ position: 'absolute', zIndex: 1000, left: -14, top: 20 }}
                        ribbon color='red' content='Cancelled' />}
                <Image src={`/assets/categoryImages/${Event.category}.jpg`} fluid style={EventImageStyle} />
                <Segment style={EventImageTextStyle} basic>
                    <Item.Group>
                        <Item>
                            <Item.Content>
                                <Header
                                    size='huge'
                                    content={Event.title}
                                    style={{ color: 'white' }}
                                />
                                <p>{format(Event.date!, 'dd MMM yyyy')}</p>
                                <p>
                                    Hosted by <strong><Link to={`/profiles/${Event.hostUsername}`}>{Event.hostUsername}</Link></strong>
                                </p>
                            </Item.Content>
                        </Item>
                    </Item.Group>
                </Segment>
            </Segment>
            <Segment clearing attached='bottom'>
                {Event.isHost ? (
                    <>
                        <Button
                            color={Event.isCancelled ? 'green' : 'red'}
                            floated='left'
                            basic
                            content={Event.isCancelled ? 'Re-activate Event' : 'Cancel Event'}
                            onClick={cancelEventToggle}
                            loading={loading}
                        />
                        <Button
                            as={Link}
                            to={`/manage/${Event.id}`}
                            color='orange'
                            floated='right'
                            disabled={Event.isCancelled}
                        >
                            Manage Event
                        </Button>
                    </>

                ) : Event.isGoing ? (
                    <Button onClick={updateAttendeance} 
                        loading={loading}>Cancel attendance</Button>
                ) : (
                    <Button disabled={Event.isCancelled} onClick={updateAttendeance} 
                        loading={loading} color='teal'>Join Event</Button>
                )}
            </Segment>
        </Segment.Group>
    )
})