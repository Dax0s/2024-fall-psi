import React, { useState, Suspense, lazy } from 'react';
import {
  createBrowserRouter,
  RouterProvider,
  useParams,
} from 'react-router-dom';
import { Link } from 'react-router-dom';
import './App.css';

const Loader = () => {
  const { gameName } = useParams();

  const GamePage = lazy(() => import(`./games/${gameName}`));

  return (
    <Suspense fallback={<div>Loading...</div>}>
      <GamePage />
    </Suspense>
  );
};

const router = createBrowserRouter([
  {
    path: '/',
    element: <HomePage />,
  },
  {
    path: '/games/:gameName',
    element: <Loader />,
  },
]);

function HomePage() {
  return (
    <div className="app">
      <header className="app-header">
        <div className="games-grid">
          <Link to={'games/reaction-game'} className="grid-item">
            Reaction Game
          </Link>
          <Link to={'games/memory-game-with-numbers'} className="grid-item">
            Memory Game
          </Link>
          <Link to={'games/aim-trainer-game'} className="grid-item">
            Aim Trainer
          </Link>
        </div>
      </header>
    </div>
  );
}

export default function App() {
  return <RouterProvider router={router} />;
}
