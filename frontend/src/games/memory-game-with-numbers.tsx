import { useState, useEffect } from 'react';
import { BACKEND_URL } from '@/utils/constants';

const MemoryGameWithNumbers = () => {
  const [grid, setGrid] = useState<(number | null)[]>([]);
  const [clickedNumbers, setClickedNumbers] = useState<(number | null)[]>([]);
  const [isGameStarted, setIsGameStarted] = useState(false);
  const [showNumbers, setShowNumbers] = useState(true);
  const [victoryList, setVictoryList] = useState<number[]>([1]);
  const [lost, setLost] = useState(false);
  const [maxNumber, setMaxNumber] = useState<number>(1);
  const [score, setScore] = useState<number>(0); // State to track the score
  const TIMER = 3000;

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
      setVictoryList([1]);
      setMaxNumber(1);
      setScore(0); // Reset score on restart
      await startGame(1);
    } catch (error) {
      console.error('Error restarting game:', error);
    }
  };

  const handleClick = async (num: number | null) => {
    const currentIndex = clickedNumbers.length;
    if (num === null || num !== victoryList[currentIndex]) {
      setLost(true);
      setShowNumbers(true);
      setScore(victoryList.length - 1); // Update the score when the player loses
      return;
    }

    const newClickedNumbers = [...clickedNumbers, num];
    setClickedNumbers(newClickedNumbers);

    if (newClickedNumbers.length === victoryList.length) {
      try {
        const response = await fetch(`${BACKEND_URL}/MemoryGameWithNumbers/attempt`, {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify(newClickedNumbers),
        });

        const isCorrect = await response.json();
        if (isCorrect) {
          const newMaxNumber = maxNumber + 1;
          setVictoryList((prevList) => [...prevList, newMaxNumber]);
          setMaxNumber(newMaxNumber);
          setScore(victoryList.length); // Increment score for correct sequence
          startGame(newMaxNumber);
        } else {
          setLost(true);
          setShowNumbers(true);
          setScore(victoryList.length - 1); // Update the score when the player loses
        }
      } catch (error) {
        console.error('Error verifying sequence:', error);
      }
    }
  };

  return (
    <div className="flex flex-col items-center justify-center min-h-screen bg-gray-800 text-white p-6">
      <h1 className="text-4xl font-extrabold mb-8">Memory Game</h1>
      {!isGameStarted && (
        <button
          className="bg-sky-400 text-white text-lg font-semibold py-3 px-6 rounded-lg shadow-lg hover:bg-sky-600 transition-colors duration-300"
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
      {lost && (
        <h2 className="text-2xl font-semibold mt-6 text-red-400">You lose! Your score: {score}</h2>
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
