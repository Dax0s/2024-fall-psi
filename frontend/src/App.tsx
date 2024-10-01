import { Suspense, lazy } from 'react';
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
  const games = [
    { path: 'games/reaction-game', name: 'Reaction Game' },
    { path: 'games/memory-game-with-numbers', name: 'Memory Game' },
    { path: 'games/aim-trainer-game', name: 'Aim Trainer' },
    { path: 'games/dot-count-game', name: 'Dot Counting' },
    { path: 'games/sequence-game', name: 'Sequence Game' },
  ];

  return (
    <div className="app">
      <header className="app-header">
        <div className="games-grid">
          {games.map((game) => (
            <Link key={game.path} to={game.path} className="grid-item">
              {game.name}
            </Link>
          ))}
        </div>
      </header>
    </div>
  );
}

export default function App() {
  return <RouterProvider router={router} />;
}
