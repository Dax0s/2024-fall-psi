import React, { lazy, Suspense } from 'react';
import ReactDOM from 'react-dom/client';
import {
  createBrowserRouter, Route,
  RouterProvider, useParams
} from 'react-router-dom';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';

const Loader = () => {
  const { gameName } = useParams();
  
  const GamePage = lazy(() => import(`./games/${gameName}`));

  return (
    <Suspense fallback={<div>Loading...</div>}>
      <GamePage />
    </Suspense>
  );
}

const router = createBrowserRouter([
  {
    path: '/',
    element: <App />,
  },
  {
    path: '/games/:gameName',
    element: <Loader />,
  }
]);

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement,
);
root.render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>,
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
