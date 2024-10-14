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
    if (!isGameStarted || isCurrentlyShowingSequence.current) return;

    setLitUpButtonId(buttonId);
    delay(100).then(() => setLitUpButtonId(undefined));

    if (buttonId === sequence[correctClicks.current]) {
      correctClicks.current++;

      if (sequence.length === correctClicks.current) {
        delay(1000).then(() =>
          getSequence().then(() => (correctClicks.current = 0)),
        );
      }
    } else {
      setSequence([]);
      correctClicks.current = 0;
      setIsGameStarted(false);
    }
  };

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
                key={'button-' + buttonId}
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
};

export default SequenceGame;
