
import { useState } from 'react';

const SequenceGame = () => {
  const [sequence, setSequence] = useState<(number | null)[]>([]);
  const [isGameStarted, setIsGameStarted] = useState(false);
  
  const startGame = async () => {
    try {
      const querySuffix = sequence.length != 0 ? `?sequence=${sequence.toString()}` : ``;
      const response = await fetch(`http://localhost:5252/SequenceGame/getSequence${querySuffix}`);
      const newSequence = await response.json();
      
      setSequence(newSequence);
      console.log(newSequence);
      
    } catch (error) {
      console.error("Error starting game: ", error);
    }
    
    setIsGameStarted(true);
  }
  
  const handleClick = (order: number | null) => {
    
  }
  
  return (
    <div className="flex flex-col items-center h-screen pt-10 bg-amber-200">
      <header className="text-4xl">Remember the sequence!</header>
      <div className="flex flex-col justify-center items-center flex-grow gap-5">
        <div className="grid grid-cols-3 gap-4 mb-4">
          <div className="bg-amber-400 aspect-square w-24 h-24 cursor-pointer hover:bg-amber-300"></div>
        </div>
        {!isGameStarted && <button
          className="bg-amber-600 p-2 text-white font-bold hover:bg-amber-500"
          onClick={startGame}
        >
          Start Game
        </button>}
      </div>
    </div>
  );
}

export default SequenceGame;
