import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.tsx'
import {
  createBrowserRouter,
  RouterProvider,
} from "react-router-dom";
import './index.css'
import { EncounterBuilder } from './EncounterBuilder/EncounterBuilder.tsx';
import { SplashScreen } from './SplashScreen/SplashScreen.tsx';

const router = createBrowserRouter([
  {
    path: "/",
    element: <SplashScreen />,
  },
  {
    path: "/game",
    element: <App />,
  },
  {
    path: "/encounter-builder",
    element: <EncounterBuilder/>
  }
]);

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>,
)
