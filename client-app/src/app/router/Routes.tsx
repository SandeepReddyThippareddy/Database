import { createBrowserRouter, Navigate, RouteObject } from "react-router-dom";
import EventDashboard from "../../features/events/dashboard/EventDashboard";
import EventDetails from "../../features/events/details/EventDetails";
import EventForm from "../../features/events/form/EventForm";
import NotFound from "../../features/errors/NotFound";
import ServerError from "../../features/errors/ServerError";
import TestErrors from "../../features/errors/TestError";
import ProfilePage from "../../features/profiles/ProfilePage";
import App from "../layout/App";
import RequireAuth from "./RequireAuth";
import FileUpload from "../../features/fileupload/fileupload";

export const routes: RouteObject[] = [
    {
        path: '/',
        element: <App />,
        children: [
            {element: <RequireAuth />, children: [
                {path: 'events', element: <EventDashboard />},
                {path: 'events/:id', element: <EventDetails />},
                {path: 'createEvent', element: <EventForm key='create' />},
                {path: 'manage/:id', element: <EventForm key='manage' />},
                {path: 'profiles/:username', element: <ProfilePage />},
                {path: 'errors', element: <TestErrors />},
                {path: 'fileupload', element: <FileUpload />}
            ]},
            {path: 'not-found', element: <NotFound />},
            {path: 'server-error', element: <ServerError />},
            {path: '*', element: <Navigate replace to='/not-found' />},
        ]
    }
]

export const router = createBrowserRouter(routes);