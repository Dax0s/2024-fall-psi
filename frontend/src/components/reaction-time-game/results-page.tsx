import { useState } from 'react';
import { Link } from 'react-router-dom';

export function ResultsPage(props: { score: number }) {
  const [username, setUsername] = useState('');
  return (
    <>
      <h1 className="font-bold">Your Final Result: {props.score}</h1>
      <input
        type="text"
        value={username}
        onChange={(changedUsername) => setUsername(changedUsername.target.value)}
        placeholder="Enter username"
        className="w-72 mb-4 mt-5 px-4 py-1 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-sky-500 text-black"
        maxLength={20}
      />
      <div className="flex items-center gap-5">
        <button className="bg-green-500 text-white px-4 py-2 rounded hover:bg-green-600 mt-4">
          Save result
        </button>
        <Link to=".">
          <button className="bg-red-500 text-white px-4 py-2 rounded hover:bg-red-600 mt-4">
            Go back
          </button>
        </Link>
      </div>
    </>
  );
}
