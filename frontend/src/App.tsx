import React, { useState, Suspense, lazy } from 'react';
import {
  createBrowserRouter,
  RouterProvider,
  useParams,
} from 'react-router-dom';
import { Link } from 'react-router-dom';
import logo from './logo.svg';
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
  const [weather, setWeather] = useState('');
  const getWeather = async () => {
    try {
      const tmp = await fetch('http://localhost:5252/weatherforecast');
      const json = await tmp.json();
      console.log(json[0]);
      setWeather(json[0].summary);
    } catch (e) {
      console.log(e);
    }
  };

  return (
    <div className="app">
      <header className="app-header">
        <img src={logo} className="app-logo" alt="logo" />
        <p>
          Edit <code>src/App.tsx</code> and save to reload.
        </p>
        <a
          className="app-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
        <button onClick={getWeather}>Get weather</button>
        <p>{weather}</p>
        <hr style={{ width: '100%', color: '#61dbfb' }} />
        <div className="games-grid">
          <Link to={'games/reaction-game'} className="grid-item">
            Reaction Game
          </Link>
          <Link to={'games/memory-game-with-numbers'} className="grid-item">
            Memory Game
          </Link>
        </div>
      </header>
    </div>
  );
}

export default function App() {
  return <RouterProvider router={router} />;
}
