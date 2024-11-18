import { Score, ScoreCreationInfo } from '@/utils/score';
import { useEffect, useState } from 'react';

function reloadPage() {
  window.location.reload();
}

async function getLeaderboard(gameUrl: string, numberOfScores: number): Promise<Score[]> {
  try {
    const respose = await fetch(gameUrl + `/leaderboard?numberOfScores=${numberOfScores}`);
    return (await respose.json()) as Score[];
  } catch (err) {
    console.error(err);
    return [] as Score[];
  }
}

async function updateLeaderboard(
  gameUrl: string,
  numberOfScores: number,
  setLeaderboard: (newLeaderboard: Score[]) => void,
) {
  const newLeaderboard = await getLeaderboard(gameUrl, numberOfScores);
  setLeaderboard(newLeaderboard);
}

export function ResultsPage(props: { gameUrl: string; score: number }) {
  const [username, setUsername] = useState('');
  const [leaderboard, setLeaderboard] = useState<Score[]>([]);

  console.log(props.gameUrl);

  useEffect(() => {
    updateLeaderboard(props.gameUrl, 10, (newLeaderboard: Score[]) =>
      setLeaderboard(newLeaderboard),
    );
  }, []);

  async function saveScore() {
    if (username.length < 1) {
      return;
    }

    try {
      await fetch(props.gameUrl + `/score`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ username, value: props.score } as ScoreCreationInfo),
      });
    } catch (err) {
      console.error(err);
    }

    window.location.reload();
  }

  return (
    <>
      <h1 className="font-bold">HIGHSCORES</h1>
      <table className="table-auto mb-20 text-base border-separate border-spacing-5">
        <thead>
          <tr>
            <th className="text-2xl">User</th>
            <th className="text-2xl">Date</th>
            <th className="text-2xl">Score</th>
          </tr>
        </thead>
        <tbody>
          {leaderboard.map((score) => (
            <tr>
              <td>{score.username}</td>
              <td>{new Date(score.date).toLocaleString('lt-LT')}</td>
              <td>{score.value}</td>
            </tr>
          ))}
        </tbody>
      </table>

      <h1 className="font-bold">Your Final Score is {props.score}</h1>
      <input
        type="text"
        value={username}
        onChange={(changedUsername) => setUsername(changedUsername.target.value)}
        placeholder="Enter username"
        className="w-72 mb-4 mt-5 px-4 py-1 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-sky-500 text-black"
        maxLength={20}
      />
      <div className="flex items-center gap-5">
        <button
          className="bg-green-500 text-white px-4 py-2 rounded hover:bg-green-600 mt-4"
          onClick={saveScore}
        >
          Save result
        </button>
        <button
          className="bg-red-500 text-white px-4 py-2 rounded hover:bg-red-600 mt-4"
          onClick={reloadPage}
        >
          Go back
        </button>
      </div>
    </>
  );
}
