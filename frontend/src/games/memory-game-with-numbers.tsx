import { useState, useEffect } from 'react';
import { BACKEND_URL } from '@/utils/consts';

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

  const startGame = async () => {
    try {
      const response = await fetch(
        `${BACKEND_URL}/MemoryGameWithNumbers/start?maxNumber=${maxNumber}`,
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
  const restartGame = () => {
    setGrid([]);
    setLost(false);
    setClickedNumbers([]);
    setIsGameStarted(true);
    setShowNumbers(true);
    setResult(null);
    setVictoryList([1]);
    setMaxNumber(1);
    startGame();
  };

  const handleClick = (num: number | null) => {
    if (num !== null && !clickedNumbers.includes(num)) {
      setClickedNumbers([...clickedNumbers, num]);

      setGrid((prevGrid) => {
        const newGrid = [...prevGrid];
        return newGrid;
      });
    }
    if (num == null) {
      verifySequence();
    }
  };

  const verifySequence = async () => {
    try {
      const response = await fetch(
        `${BACKEND_URL}/MemoryGameWithNumbers/attempt`,
        {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(clickedNumbers),
        },
      );

      const isCorrect = await response.json();
      if (isCorrect) {
        setResult('Correct!');
        setMaxNumber((prevMax) => prevMax + 1);
      } else {
        setResult(
          'You lose. Try again! Your score: ' + (victoryList.length - 1),
        );
        setLost(true);
        setShowNumbers(true);
      }
    } catch (error) {
      console.error('Error verifying sequence:', error);
    }
  };

  return (
    <div className="flex flex-col items-center mt-10">
      <h1 className="text-3xl font-bold text-gray-800 mb-6">Memory Game</h1>
      {!isGameStarted && (
        <button
          className="bg-green-500 text-white font-semibold py-2 px-4 rounded-lg shadow-md hover:bg-green-600 transition duration-300"
          onClick={startGame}
        >
          Start Game
        </button>
      )}
      {isGameStarted && (
        <div className="grid grid-cols-4 gap-2 mt-5 w-80">
          {grid.map((number, index) => (
            <div
              key={index}
              className="w-12 h-12 bg-gray-100 flex justify-center items-center text-2xl cursor-pointer border-2 border-gray-300 hover:bg-gray-200"
              onClick={showNumbers ? undefined : () => handleClick(number)}
            >
              {showNumbers ? number : '?'}
            </div>
          ))}
        </div>
      )}
      {result && (
        <>
          <h2 className="text-2xl font-medium mt-6 text-gray-800">{result}</h2>
        </>
      )}
      {lost && (
        <button
          className="mt-6 bg-red-500 text-white font-semibold py-2 px-4 rounded-lg shadow-md hover:bg-red-600 transition duration-300"
          onClick={restartGame}
        >
          Restart Game
        </button>
      )}
    </div>
  );
};

export default MemoryGameWithNumbers;
