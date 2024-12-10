import { GAME_URL, TRY_COUNT_PER_GAME } from './constants';
import { average } from '@/utils/math';
import { delay, Timer } from '@/utils/timing';
import { useRef, useState } from 'react';

enum ReactionState {
  BASE,
  WAIT,
  GO,
}

async function fetchWaitTime(): Promise<number | undefined> {
  try {
    const response = await fetch(`${GAME_URL}/start`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
    });
    return (await response.json()).millisecondsToWait as number;
  } catch (err) {
    console.error(err);
    return undefined;
  }
}

export function ReactionTimeButton(props: {
  tries: number[];
  setTries: (newTries: number[]) => void;
}) {
  const reactionTimer = useRef(new Timer());

  const latestStartTime = useRef(0);
  const reactionState = useRef(ReactionState.BASE);

  const [reactionTimeLabel, setReactionTimeLabel] = useState('TEST YOUR REACTION TIME');

  const [buttonLabel, setButtonLabel] = useState('PRESS ME TO START');
  const [buttonClass, setButtonClass] = useState('bg-sky-400 text-white');

  const [averageLabel, setAverageLabel] = useState('');

  async function handleClick() {
    switch (reactionState.current) {
      case ReactionState.BASE:
        await clickOnBaseState();
        break;

      case ReactionState.WAIT:
        reactionState.current = ReactionState.BASE;
        setButtonLabel('TOO SOON, TRY AGAIN');
        setButtonClass('bg-orange-500 text-white hover:brightness-80');
        break;

      case ReactionState.GO:
        clickOnGoState();
        break;

      default:
        console.error('Unknown game state encountered');
        break;
    }
  }

  const clickOnBaseState = async () => {
    const msToWait = await fetchWaitTime();
    if (!msToWait) {
      alert('Something went wrong. Try again.');
      return;
    }

    reactionState.current = ReactionState.WAIT;
    setButtonLabel('WAIT');
    setButtonClass('bg-red-500 text-white');

    const startTime = performance.now();
    latestStartTime.current = startTime;

    await delay(msToWait);

    if (reactionState.current === ReactionState.WAIT && latestStartTime.current === startTime) {
      reactionState.current = ReactionState.GO;
      setButtonLabel('PRESS NOW');
      setButtonClass('bg-green-500 text-white');
    }

    reactionTimer.current.start();
  };

  const clickOnGoState = () => {
    reactionTimer.current.stop();
    const pageDelay = 100;
    const reactionTime = reactionTimer.current.getTime() - pageDelay;

    if (props.tries.length === TRY_COUNT_PER_GAME) {
      props.setTries([]);
    }

    const updatedTries = [...props.tries, reactionTime];
    const averageReactionTime = average(updatedTries);

    reactionState.current = ReactionState.BASE;
    setButtonLabel('TRY AGAIN');
    setButtonClass('bg-sky-400 text-white');

    setReactionTimeLabel('REACTION TIME: ' + Math.floor(reactionTime) + ' ms');
    setAverageLabel('AVERAGE REACTION TIME: ' + Math.floor(averageReactionTime) + ' ms');

    props.setTries(updatedTries);
  };

  return (
    <>
      <h1 className="text-2xl font-bold mb-4">{reactionTimeLabel}</h1>
      <button
        id="reactionBtn"
        className={`${buttonClass} w-[500px] h-[500px] border-0 rounded-full text-[30pt] font-bold hover:brightness-75`}
        onClick={handleClick}
      >
        {buttonLabel}
      </button>
      <p className="mt-4 text-lg">{averageLabel}</p>
    </>
  );
}
