import { useState, useEffect } from 'react';
import { BACKEND_URL } from '@/utils/constants';

const MemoryGameWithNumbers = () => {
  const [grid, setGrid] = useState<(number | null)[]>([]);
  const [clickedNumbers, setClickedNumbers] = useState<number[]>([]);
  const [isGameStarted, setIsGameStarted] = useState(false);
  const [showNumbers, setShowNumbers] = useState(true);
  const [result, setResult] = useState<string | null>(null);
  const [victoryList, setVictoryList] = useState<number[]>([1]);
  const [lost, setLost] = useState(false);
  const [maxNumber, setMaxNumber] = useState<number>(1);
  const TIMER = 3000;

  useEffect(() => {
    if (clickedNumbers.length === victoryList.length) {
      verifySequence();
    }
  }, [clickedNumbers]);

  useEffect(() => {
    if (result === 'Correct!') {
      const newNumber = maxNumber;
      setVictoryList((prevList) => [...prevList, newNumber]);
      setTimeout(() => {}, TIMER);
      startGame();
    }
  }, [maxNumber]);

  const startGame = async (initialMaxNumber?: number) => {
    const numberToUse = initialMaxNumber ?? maxNumber;
    try {
      const response = await fetch(
        `${BACKEND_URL}/MemoryGameWithNumbers/start?maxNumber=${numberToUse}`,
      );
      const data = await response.json();

      setGrid(data);
      setIsGameStarted(true);
      setClickedNumbers([]);
      setShowNumbers(true);

      setTimeout(() => {
        setShowNumbers(false);
        setResult('');
      }, TIMER);
    } catch (error) {
      console.error('Error starting game:', error);
    }
  };

  const restartGame = async () => {
    try {
      await fetch(`${BACKEND_URL}/MemoryGameWithNumbers/restart`, { method: 'POST' });
      setGrid([]);
      setLost(false);
      setClickedNumbers([]);
      setShowNumbers(true);
      setResult(null);
      setVictoryList([1]);
      setMaxNumber(1);
      await startGame(1);
    } catch (error) {
      console.error('Error restarting game:', error);
      setResult('Failed to restart. Please try again.');
    }
  };

  const handleClick = (num: number | null) => {
    if (num === null) {
      setResult('You lose. Try again! Your score: ' + (victoryList.length - 1));
      setLost(true);
      setShowNumbers(true);
      return;
    }

    const currentIndex = clickedNumbers.length;
    if (num !== victoryList[currentIndex]) {
      setResult('You lose. Try again! Your score: ' + (victoryList.length - 1));
      setLost(true);
      setShowNumbers(true);
      return;
    }

    setClickedNumbers([...clickedNumbers, num]);
    setGrid((prevGrid) => [...prevGrid]);
  };

  const verifySequence = async () => {
    try {
      const response = await fetch(`${BACKEND_URL}/MemoryGameWithNumbers/attempt`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(clickedNumbers),
      });

      const isCorrect = await response.json();
      if (isCorrect) {
        setResult('Correct!');
        setMaxNumber((prevMax) => prevMax + 1);
      } else {
        setResult('You lose. Try again! Your score: ' + (victoryList.length - 1));
        setLost(true);
        setShowNumbers(true);
      }
    } catch (error) {
      console.error('Error verifying sequence:', error);
    }
  };

  return (
    <div className="flex flex-col items-center min-h-screen bg-gray-800 text-gray-100 p-6">
      <h1 className="text-4xl font-extrabold mb-8">Memory Game</h1>
      {!isGameStarted && (
        <button
          className="bg-gray-600 text-white text-lg font-semibold py-3 px-6 rounded-lg shadow-lg hover:bg-gray-500 transition duration-300"
          onClick={() => startGame(1)}
        >
          Start Game
        </button>
      )}
      {isGameStarted && (
        <div className="grid grid-cols-4 gap-4 mt-5 w-full max-w-lg">
          {grid.map((number, index) => (
            <div
              key={index}
              className={`w-16 h-16 flex justify-center items-center text-xl font-bold rounded-lg shadow-md border-2 ${
                clickedNumbers.includes(number)
                  ? 'bg-gray-500 text-gray-400 cursor-not-allowed'
                  : showNumbers
                    ? 'bg-gray-700 text-gray-100'
                    : 'bg-gray-600 text-gray-100 hover:bg-gray-500 cursor-pointer'
              } transition-all duration-300`}
              onClick={
                showNumbers || clickedNumbers.includes(number)
                  ? undefined
                  : () => handleClick(number)
              }
            >
              {showNumbers ? number : '?'}
            </div>
          ))}
        </div>
      )}
      {result && (
        <h2
          className={`text-2xl font-semibold mt-6 ${
            result === 'Correct!' ? 'text-green-400' : 'text-red-400'
          }`}
        >
          {result}
        </h2>
      )}
      {lost && (
        <button
          className="mt-6 bg-gray-600 text-white text-lg font-semibold py-3 px-6 rounded-lg shadow-lg hover:bg-gray-500 transition duration-300"
          onClick={restartGame}
        >
          Restart Game
        </button>
      )}
    </div>
  );
};

export default MemoryGameWithNumbers;
