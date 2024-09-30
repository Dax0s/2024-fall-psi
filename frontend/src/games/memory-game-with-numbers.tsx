import { verify } from 'crypto';
import { useState, useEffect } from 'react';

const MemoryGameWithNumbers = () => {
  const [grid, setGrid] = useState<(number | null)[]>([]);
  const [clickedNumbers, setClickedNumbers] = useState<number[]>([]);
  const [isGameStarted, setIsGameStarted] = useState(false);
  const [showNumbers, setShowNumbers] = useState(true);
  const [result, setResult] = useState<string | null>(null);
  const [victoryList, setVictoryList] = useState<number[]>([1, 2, 3, 4, 5]);
  const [maxNumber, setMaxNumber] = useState<number>(5);
  const TIMER = 3000;

  useEffect(() => {
    if (clickedNumbers.length === victoryList.length) {
      verifySequence();
    }
    if (clickedNumbers.some((number) => number == null)) {
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
        `http://localhost:5252/MemoryGameWithNumbers/start?maxNumber=${maxNumber}`,
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
      if (
        clickedNumbers.length === victoryList.length &&
        clickedNumbers.every((val, index) => val === victoryList[index])
      ) {
        setResult('Correct!');

        setMaxNumber((prevMax) => prevMax + 1);
      } else {
        setResult('You lose. Try again! Your score: ' + victoryList.length);
        setShowNumbers(true);
      }
    } catch (error) {
      console.error('Error verifying sequence:', error);
    }
  };

  return (
    <div>
      <h1>Memory Game</h1>
      {!isGameStarted && <button onClick={startGame}>Start Game</button>}
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
      {result && <h2>{result}</h2>}
    </div>
  );
};

export default MemoryGameWithNumbers;
