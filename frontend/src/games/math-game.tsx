import { useState, useEffect } from 'react';
import { BACKEND_URL } from '@/utils/constants';

const PuzzleRush = () => {
  const [puzzle, setPuzzle] = useState('');
  const [answer, setAnswer] = useState('');
  const [feedback, setFeedback] = useState('');
  const [loading, setLoading] = useState(false);
  const [score, setScore] = useState(0);
  const [highScore, setHighScore] = useState(0);

  useEffect(() => {
    fetchNextPuzzle();
  }, []);

  const fetchNextPuzzle = async () => {
    try {
      const response = await fetch(`${BACKEND_URL}/MathGame/next`);

      if (!response.ok) {
        throw new Error('No more puzzles available');
      }

      const data = await response.text();
      setPuzzle(data);
      setAnswer('');
      setFeedback('');
    } catch (error) {
      console.error('Error fetching puzzle', error);
      setFeedback('No more puzzles to solve.');
    }
  };

  const submitAnswer = async () => {
    setLoading(true);
    try {
      const response = await fetch(`${BACKEND_URL}/MathGame/solve`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          puzzle: puzzle,
          answer: answer,
        }),
      });

      if (!response.ok) {
        throw new Error('Error checking answer');
      }

      const isCorrect = await response.json();

      if (isCorrect) {
        setFeedback('Correct!');
        setScore((prevScore) => prevScore + 1);

        setHighScore((prevHighScore) => Math.max(prevHighScore, score + 1));
      } else {
        setFeedback('Wrong answer!');
        setScore(0);
      }

      setTimeout(fetchNextPuzzle, 2000);
    } catch (error) {
      console.error('Error submitting answer', error);
      setFeedback('Error checking answer.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-100">
      <div className="bg-white p-6 rounded-lg shadow-lg max-w-md w-full">
        <h1 className="text-2xl font-bold text-gray-800 mb-4 text-center">Math Game</h1>
        <div className="puzzle">
          <h2 className="text-xl text-gray-700 font-semibold text-center mb-4">
            {puzzle || 'Loading next puzzle...'}
          </h2>
        </div>

        <div className="mb-4">
          <p className="text-center text-gray-600">
            Score: <span className="font-bold">{score}</span> | High Score:{' '}
            <span className="font-bold">{highScore}</span>
          </p>
        </div>

        <div className="mb-4">
          <input
            type="text"
            value={answer}
            onChange={(e) => setAnswer(e.target.value)}
            placeholder="Your answer"
            disabled={loading}
            className="w-full p-2 border rounded-md text-gray-700 focus:outline-none focus:ring-2 focus:ring-indigo-500"
          />
        </div>
        <div className="flex justify-center mb-4">
          <button
            onClick={submitAnswer}
            disabled={loading || !answer}
            className={`${
              loading || !answer ? 'bg-gray-400' : 'bg-indigo-600 hover:bg-indigo-700'
            } text-white font-bold py-2 px-4 rounded focus:outline-none focus:ring-2 focus:ring-indigo-500`}
          >
            {loading ? 'Submitting...' : 'Submit Answer'}
          </button>
        </div>

        {feedback && (
          <p
            className={`text-center font-medium ${feedback === 'Correct!' ? 'text-green-500' : 'text-red-500'}`}
          >
            {feedback}
          </p>
        )}
      </div>
    </div>
  );
};

export default PuzzleRush;
