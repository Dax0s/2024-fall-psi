
import { useEffect, useRef, useState } from 'react';

function delay(ms: number) {
  return new Promise((resolve) => setTimeout(resolve, ms));
}

const SequenceGame = () => {
  const [sequence, setSequence] = useState<(number)[]>([]);
  const userSequence = useRef<number[]>([]);
  const [isGameStarted, setIsGameStarted] = useState(false);
  const [litUpButtonId, setLitUpButtonId] = useState<number>();

  useEffect(() => {
    if (sequence.length > 0) {
      showSequence().then(() => setLitUpButtonId(undefined));
    }
  }, [sequence]);
  
  const showSequence = async () => {
    for (const buttonId of sequence) {
      setLitUpButtonId(buttonId);
      await delay(200);
      setLitUpButtonId(undefined);
      await delay(100);
    }
  }
  
  const getSequence = async () => {
    try {
      const querySuffix = sequence.length != 0 ? `?sequence=${sequence.toString()}` : ``;
      const response = await fetch(`http://localhost:5252/SequenceGame/getSequence${querySuffix}`);
      const newSequence = await response.json();
      
      setSequence(newSequence);
      setIsGameStarted(true);
    } catch (error) {
      console.error("Error starting game: ", error);
    }
  }
  
  const handleClick = (buttonId: number) => {
    if (!isGameStarted) return;
    
    setLitUpButtonId(buttonId);
    delay(100).then(() => setLitUpButtonId(undefined));
    
    userSequence.current.push(buttonId);
    
    if (buttonId !== sequence[userSequence.current.length - 1]) {
      setSequence([]);
      userSequence.current = [];
      setIsGameStarted(false);
    } else if (sequence.length == userSequence.current.length) {
      delay(1000).then(() => getSequence().then(() => userSequence.current = []));
    }
  }

  return (
    <div className="flex flex-col items-center h-screen pt-10 bg-amber-200">
      <header className="text-4xl">Remember the sequence!</header>
      <div className="flex flex-col justify-center items-center flex-grow gap-5">
        <div className="grid grid-cols-3 gap-4 mb-4">
          {Array.from({ length: 9 }, (_, index) => {
            const buttonId = index + 1;
            return (
              <div
                className={`aspect-square w-24 h-24 cursor-pointer ${litUpButtonId === buttonId ? 'bg-amber-400' : 'bg-amber-600'}`}
                key={"button-" + buttonId}
                onClick={() => handleClick(buttonId)}
              ></div>
            );
          })}
        </div>
        {!isGameStarted && (
          <button
            className="bg-amber-600 p-2 text-white font-bold hover:bg-amber-500"
            onClick={getSequence}
          >
            Start Game
          </button>
        )}
      </div>
    </div>
  );

}

export default SequenceGame;
