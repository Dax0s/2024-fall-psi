import { useRef } from 'react';

export type Dot = {
  x: number;
  y: number;
};

export type DotCanvasInfo = {
  canvasWidth: number;
  numberOfDots: number;
  dots: Dot[];
  dotRadius: number;
};

const defaultCanvasColor = 'aliceblue';
const defaultCanvasDotColor = 'hotpink';

const minDotCount = 1;
const maxDotCount = 1000;
const dotCountStep = 1;

// In milliseconds
const minDuration = 50;
const maxDuration = 10000;
const durationStep = 50;

function delay(ms: number) {
  return new Promise((resolve) => setTimeout(resolve, ms));
}

const DotCountGame = () => {
  const lastDotCount = useRef(0);

  const clearCanvas = () => {
    const canvas = document.getElementById('dotCanvas') as HTMLCanvasElement;
    const context = canvas.getContext('2d');
    if (!context) return;

    context.fillStyle = defaultCanvasColor;
    context.fillRect(0, 0, canvas.width, canvas.height);
  };

  const fetchInfo = async (maxDots: number) => {
    try {
      const response = await fetch(
        `http://localhost:5252/DotCountGame?maxDots=${maxDots}`,
      );
      const canvasInfo = (await response.json()) as DotCanvasInfo;
      console.log(canvasInfo);
      return canvasInfo;
    } catch (error) {
      console.error('Error starting game:', error);
    }
  };

  const displayDots = (canvasInfo: DotCanvasInfo) => {
    const canvas = document.getElementById('dotCanvas') as HTMLCanvasElement;
    canvas.width = canvasInfo.canvasWidth;
    canvas.height = canvasInfo.canvasWidth;
    clearCanvas();

    const context = canvas.getContext('2d');
    if (!context) return;

    context.beginPath();
    canvasInfo.dots.forEach((dot) => {
      context.moveTo(dot.x, dot.y);
      context.arc(dot.x, dot.y, canvasInfo.dotRadius, 0, 2 * Math.PI);
    });

    context.fillStyle = defaultCanvasDotColor;
    context.fill();
  };

  const handleClick = async () => {
    const maxDotsElement = document.getElementById(
      'maxDotsInput',
    ) as HTMLInputElement;
    const durationElement = document.getElementById(
      'durationInput',
    ) as HTMLInputElement;

    const maxDots = parseInt(maxDotsElement.value);
    const duration = parseInt(durationElement.value);
    if (!maxDots || !duration) return;

    const canvasInfo = (await fetchInfo(maxDots)) as DotCanvasInfo;
    lastDotCount.current = canvasInfo.dots.length;

    displayDots(canvasInfo);
    await delay(duration);
    clearCanvas();
  };

  const handleAnswer = () => {
    const answerElement = document.getElementById(
      'answerInput',
    ) as HTMLInputElement;
    const resultLabel = document.getElementById(
      'resultLabel',
    ) as HTMLLabelElement;

    resultLabel.textContent = '';
    const answer = parseInt(answerElement.value);
    if (!answer || lastDotCount.current === 0) return;

    const correctAnswer = lastDotCount.current;
    const absDifference = Math.abs(answer - correctAnswer);

    const accuracy =
      absDifference > correctAnswer
        ? 0
        : 100 * (1 - absDifference / correctAnswer);

    if (accuracy === 100) {
      resultLabel.textContent = 'Congratulations! You got it right!';
    } else if (accuracy >= 90) {
      resultLabel.textContent =
        'So close! The actual number was: ' +
        correctAnswer.toString() +
        ' (' +
        Math.floor(accuracy) +
        '% accuracy)';
    } else if (accuracy >= 50) {
      resultLabel.textContent =
        'Not bad. The actual number was: ' +
        correctAnswer.toString() +
        ' (' +
        Math.floor(accuracy) +
        '% accuracy)';
    } else {
      resultLabel.textContent =
        'Maybe next time? The actual number was: ' +
        correctAnswer.toString() +
        ' (' +
        Math.floor(accuracy) +
        '% accuracy)';
    }

    lastDotCount.current = 0;
  };

  return (
    <>
      <div className="grid grid-cols-9 gap-5 gap-y-10 place-items-center-h-screen">
        <label className="col-start-3 col-span-1">Max. # of dots:</label>
        <input
          id="maxDotsInput"
          className="col-start-4 col-span-1"
          type="number"
          min={minDotCount}
          max={maxDotCount}
          step={dotCountStep}
        />
        <label className="col-start-5 col-span-1">Duration (ms):</label>
        <input
          id="durationInput"
          className="col-start-6 col-span-1"
          type="number"
          min={minDuration}
          max={maxDuration}
          step={durationStep}
        />
        <button className="col-start-7 col-span-1" onClick={handleClick}>
          GO
        </button>
      </div>
      <div>
        <canvas
          id="dotCanvas"
          className="m-auto rounded"
          color={defaultCanvasColor}
        />
      </div>
      <div className="grid grid-cols-5 gap-5 gap-y-10 place-items-center-h-screen">
        <label className="col-start-2 col-end-4">
          How many dots did you see?
        </label>
        <input
          id="answerInput"
          className="col-start-2 col-span-2"
          type="number"
          min={minDotCount}
          max={maxDotCount}
          step={dotCountStep}
        />
        <button className="col-start-4 col-span-1" onClick={handleAnswer}>
          CHECK
        </button>
        <label id="resultLabel" className="col-start-2 col-end-4"></label>
      </div>
    </>
  );
};

export default DotCountGame;
