import { useState } from 'react';
import Difficulty from './Difficulty';

type props = {
  defaultDifficulty: Difficulty;
  setParentDifficulty: (difficulty: Difficulty) => void;
};

const DifficultyPicker = ({ defaultDifficulty, setParentDifficulty }: props) => {
  const [difficulty, setDifficulty] = useState(defaultDifficulty);
  const [isOpen, setIsOpen] = useState(false);

  const difficulties = [
    { value: Difficulty.EASY, label: 'Easy', color: 'bg-green-500' },
    { value: Difficulty.MEDIUM, label: 'Medium', color: 'bg-yellow-500' },
    { value: Difficulty.HARD, label: 'Hard', color: 'bg-red-500' },
  ];

  const handleSelect = (value: Difficulty) => {
    setDifficulty(value);
    setParentDifficulty(value);
    setIsOpen(false);
  };

  const selectedDifficulty = difficulties.find((d) => d.value === difficulty);

  return (
    <div className="relative inline-block text-left">
      <label htmlFor="difficulty-picker" className="block text-xs font-medium text-gray-700 mb-1">
        Difficulty:
      </label>
      <button
        id="difficulty-picker"
        className={`inline-flex justify-center items-center w-full rounded-md border border-gray-300 shadow-sm px-4 py-2 bg-white text-sm font-medium text-gray-700 hover:bg-gray-50 focus:outline-none transition-colors duration-200 ease-in-out ${isOpen ? 'rounded-b-none' : ''}`}
        onClick={() => setIsOpen(!isOpen)}
      >
        <span className={`${selectedDifficulty?.color} w-3 h-3 rounded-full mr-2`}></span>
        {selectedDifficulty?.label}
        <svg
          className={`ml-2 h-5 w-5 transition-transform duration-200 ease-in-out ${isOpen ? 'transform rotate-180' : ''}`}
          xmlns="http://www.w3.org/2000/svg"
          viewBox="0 0 20 20"
          fill="currentColor"
          aria-hidden="true"
        >
          <path
            fillRule="evenodd"
            d="M5.293 7.293a1 1 0 011.414 0L10 10.586l3.293-3.293a1 1 0 111.414 1.414l-4 4a1 1 0 01-1.414 0l-4-4a1 1 0 010-1.414z"
            clipRule="evenodd"
          />
        </svg>
      </button>

      {isOpen && (
        <div className="absolute z-10 w-full rounded-b-md shadow-lg bg-white border border-t-0 border-gray-300">
          <div className="py-1">
            {difficulties.map((diff) => (
              <button
                key={diff.value}
                onClick={() => handleSelect(diff.value)}
                className={`flex items-center w-full px-4 py-2 text-sm text-gray-700 hover:bg-gray-100 hover:text-gray-900 ${difficulty === diff.value ? 'bg-gray-100' : ''}`}
              >
                <span className={`${diff.color} w-3 h-3 rounded-full mr-2`}></span>
                {diff.label}
              </button>
            ))}
          </div>
        </div>
      )}
    </div>
  );
};

export default DifficultyPicker;
