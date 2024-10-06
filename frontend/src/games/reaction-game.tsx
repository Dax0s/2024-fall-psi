import { useRef, useState } from 'react';
import { delay } from 'src/utils/timing';

async function fetchWaitTime() {
  const tmp = await fetch('http://localhost:5252/reactiontime');
  const json = await tmp.json();
  return json.millisecondsToWait;
}

enum ReactionState {
  BASE,
  WAIT,
  GO,
}

function ReactionButton() {
  const goTime = useRef(0);

  const tries = useRef(0);
  const reactionTimeSum = useRef(0);

  const latestStartTime = useRef(0);
  const reactionState = useRef(ReactionState.BASE);

  const [reactionTimeLabel, setReactionTimeLabel] = useState(
    'TEST YOUR REACTION TIME',
  );

  const [buttonLabel, setButtonLabel] = useState('PRESS ME TO START');
  const [buttonClass, setButtonClass] = useState('bg-white text-black');

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
        break;
    }
  }

  const clickOnBaseState = async () => {
    const msToWait = await fetchWaitTime();

    reactionState.current = ReactionState.WAIT;
    setButtonLabel('WAIT');
    setButtonClass('bg-red-500 text-white');

    const startTime = performance.now();
    latestStartTime.current = startTime;

    await delay(msToWait);

    if (
      reactionState.current === ReactionState.WAIT &&
      latestStartTime.current === startTime
    ) {
      reactionState.current = ReactionState.GO;
      setButtonLabel('PRESS NOW');
      setButtonClass('bg-green-500 text-white');
    }

    goTime.current = performance.now();
  };

  const clickOnGoState = () => {
    const stopTime = performance.now();
    const pageDelay = 100;
    const reactionTime = stopTime - goTime.current - pageDelay;

    tries.current = tries.current + 1;
    reactionTimeSum.current = reactionTimeSum.current + reactionTime;
    const averageReactionTime = reactionTimeSum.current / tries.current;

    reactionState.current = ReactionState.BASE;
    setButtonLabel('TRY AGAIN');
    setButtonClass('bg-white text-black');

    setReactionTimeLabel('REACTION TIME: ' + Math.floor(reactionTime) + ' ms');
    setAverageLabel(
      'AVERAGE REACTION TIME: ' +
        Math.floor(averageReactionTime) +
        ' ms' +
        ' (' +
        tries.current +
        ' ' +
        (tries.current > 1 ? 'tries' : 'try') +
        ')',
    );
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

function Game() {
  return (
    <div className="app">
      <header className="app-header">
        <ReactionButton />
      </header>
    </div>
  );
}

export default Game;
