import { Suspense, lazy } from 'react';
import { createBrowserRouter, RouterProvider, useParams } from 'react-router-dom';
import { Link } from 'react-router-dom';

const Loader = () => {
  const { gameName } = useParams();

  const GamePage = lazy(() => import(`./games/${gameName}.tsx`));

  return (
    <Suspense fallback={<div className="bg-gray-800 min-h-screen" />}>
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
    { path: 'games/reaction-time-game', name: 'Reaction Time' },
    { path: 'games/memory-game-with-numbers', name: 'Memory Game' },
    { path: 'games/aim-trainer-game', name: 'Aim Trainer' },
    { path: 'games/dot-count-game', name: 'Dot Counting' },
    { path: 'games/sequence-game', name: 'Sequence Game' },
    { path: 'games/math-game', name: 'Math Game' },
  ];

  return (
    <div className="text-center">
      <header className="bg-gray-800 min-h-screen flex flex-col items-center justify-center text-white text-xl">
        <div className="grid grid-cols-3 gap-6">
          {games.map((game) => (
            <Link
              key={game.path}
              to={game.path}
              className="p-5 text-center bg-sky-400 rounded-lg no-underline text-white transition-colors duration-300 hover:bg-sky-600"
            >
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
