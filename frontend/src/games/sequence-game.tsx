import { useEffect, useRef, useState } from 'react';
import { delay } from '@/utils/timing';
import { BACKEND_URL } from '@/utils/constants';

const SequenceGame = () => {
  const [isGameStarted, setIsGameStarted] = useState(false);
  const [litUpButtonId, setLitUpButtonId] = useState<number>();
  const [sequence, setSequence] = useState<number[]>([]);
  const isCurrentlyShowingSequence = useRef(false);
  const correctClicks = useRef(0);

  useEffect(() => {
    if (sequence.length > 0) {
      showSequence().then(() => setLitUpButtonId(undefined));
    }
  }, [sequence]);

  const showSequence = async () => {
    isCurrentlyShowingSequence.current = true;
    for (const buttonId of sequence) {
      setLitUpButtonId(buttonId);
      await delay(200);
      setLitUpButtonId(undefined);
      await delay(100);
    }
    isCurrentlyShowingSequence.current = false;
  };

  const getSequence = async () => {
    try {
      const response = await fetch(
        `${BACKEND_URL}/SequenceGame/getSequence?sequence=${sequence.toString()}`,
      );
      const newSequence = await response.json();

      setSequence(newSequence);
      setIsGameStarted(true);
    } catch (error) {
      console.error('Error starting game: ', error);
    }
  };

  const handleClick = (buttonId: number) => {
    if (!isGameStarted || isCurrentlyShowingSequence.current) {
      return;
    }

    setLitUpButtonId(buttonId);
    delay(100).then(() => setLitUpButtonId(undefined));

    if (buttonId === sequence[correctClicks.current]) {
      correctClicks.current++;

      if (sequence.length === correctClicks.current) {
        delay(1000).then(() => getSequence().then(() => (correctClicks.current = 0)));
      }
    } else {
      setSequence([]);
      correctClicks.current = 0;
      setIsGameStarted(false);
    }
  };

  return (
    <div className="text-center">
      <header className="bg-gray-800 min-h-screen flex flex-col items-center justify-center text-white text-xl">
        <header className="text-4xl m-6">Remember the sequence!</header>
        <div className="flex flex-col justify-center items-center flex-grow gap-5">
          <div className="grid grid-cols-3 gap-4 mb-4">
            {Array.from({ length: 9 }, (_, index) => {
              const buttonId = index + 1;
              return (
                <div
                  className={`aspect-square w-24 h-24 cursor-pointer ${litUpButtonId === buttonId ? 'bg-sky-250' : 'bg-sky-400'}`}
                  key={'button-' + buttonId}
                  onClick={() => handleClick(buttonId)}
                ></div>
              );
            })}
          </div>
          {!isGameStarted && (
            <button
              className="bg-sky-400 p-3 text-white font-semibold rounded-lg transition-colors duration-300 hover:bg-sky-600"
              onClick={getSequence}
            >
              Start Game
            </button>
          )}
        </div>
      </header>
    </div>
  );
};

export default SequenceGame;
