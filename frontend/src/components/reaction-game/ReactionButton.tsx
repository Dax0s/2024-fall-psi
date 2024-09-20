import React, { useRef, useState } from 'react';
import './ReactionButton.css';

function delay(ms: number) {
  return new Promise((resolve) => setTimeout(resolve, ms));
}

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
  const [buttonClass, setButtonClass] = useState('whiteReactionBtn');

  const [averageLabel, setAverageLabel] = useState('');

  async function handleClick() {
    switch (reactionState.current) {
      case ReactionState.BASE:
        await clickOnBaseState();
        break;
      case ReactionState.WAIT:
        reactionState.current = ReactionState.BASE;
        setButtonLabel('TOO SOON, TRY AGAIN');
        setButtonClass('orangeReactionBtn');
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
    setButtonClass('redReactionBtn');

    const startTime = performance.now();
    latestStartTime.current = startTime;

    await delay(msToWait);

    if (
      reactionState.current === ReactionState.WAIT &&
      latestStartTime.current === startTime
    ) {
      reactionState.current = ReactionState.GO;
      setButtonLabel('PRESS NOW');
      setButtonClass('greenReactionBtn');
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
    setButtonClass('whiteReactionBtn');

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
      <h1>{reactionTimeLabel}</h1>
      <button id="reactionBtn" className={buttonClass} onClick={handleClick}>
        {buttonLabel}
      </button>
      <p>{averageLabel}</p>
    </>
  );
}

export default ReactionButton;
