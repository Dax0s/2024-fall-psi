import { useState } from 'react';
import { delay } from 'src/utils/timing';
import { Vec2 } from 'src/utils/vec2';

type Dot = {
  center: Vec2;
  radius: number;
};

type DotCanvasInfo = {
  sideLength: number;
  dots: Dot[];
};

const noLastDotCountValue = 0;

const defaultCanvasColor = 'aliceblue';
const defaultDotColor = 'hotpink';

const minDotCount = 1;
const maxDotCount = 1000;
const dotCountStep = 1;

// In milliseconds
const minDuration = 50;
const maxDuration = 10000;
const durationStep = 50;

const DotCountGame = () => {
  const [lastDotCount, setLastDotCount] = useState(noLastDotCountValue);

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
      return canvasInfo;
    } catch (error) {
      console.error('Error starting game:', error);
    }
  };

  const displayDots = (canvasInfo: DotCanvasInfo) => {
    const canvas = document.getElementById('dotCanvas') as HTMLCanvasElement;
    canvas.width = canvasInfo.sideLength;
    canvas.height = canvasInfo.sideLength;
    clearCanvas();

    const context = canvas.getContext('2d');
    if (!context) return;

    context.beginPath();
    canvasInfo.dots.forEach((dot) => {
      context.moveTo(dot.center.x, dot.center.y);
      context.arc(dot.center.x, dot.center.y, dot.radius, 0, 2 * Math.PI);
    });

    context.fillStyle = defaultDotColor;
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

    setLastDotCount(noLastDotCountValue);
    displayDots(canvasInfo);
    await delay(duration);
    clearCanvas();

    setLastDotCount(canvasInfo.dots.length);
    const resultLabel = document.getElementById(
      'resultLabel',
    ) as HTMLLabelElement;
    resultLabel.textContent = '';
  };

  const handleAnswer = () => {
    const answerElement = document.getElementById(
      'answerInput',
    ) as HTMLInputElement;
    const resultLabel = document.getElementById(
      'resultLabel',
    ) as HTMLLabelElement;

    const answer = parseInt(answerElement.value);
    if (
      !answer ||
      lastDotCount === noLastDotCountValue ||
      resultLabel.textContent !== ''
    )
      return;

    const correctAnswer = lastDotCount;
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
  };

  return (
    <div className="flex flex-col items-center justify-center h-screen">
      <h1 className="text-3xl font-bold mb-4">GUESS THE DOT COUNT</h1>

      <div className="flex flex-col space-y-4">
        <div className="flex items-center">
          <label htmlFor="maxDotsInput" className="text-lg font-medium mr-2">
            Max. number of dots:
          </label>
          <input
            id="maxDotsInput"
            className="border rounded px-4 py-2"
            type="number"
            min={minDotCount}
            max={maxDotCount}
            step={dotCountStep}
          />
        </div>

        <div className="flex items-center">
          <label htmlFor="durationInput" className="text-lg font-medium mr-2">
            Dot show duration (ms):
          </label>
          <input
            id="durationInput"
            className="border rounded px-4 py-2"
            type="number"
            min={minDuration}
            max={maxDuration}
            step={durationStep}
          />
        </div>
      </div>

      <button
        className="bg-green-500 text-white px-4 py-2 rounded hover:bg-green-600 mt-4"
        onClick={handleClick}
      >
        GO
      </button>

      <canvas
        id="dotCanvas"
        className={
          lastDotCount !== noLastDotCountValue
            ? 'invisible h-10'
            : 'border rounded mt-4'
        }
        color={defaultCanvasColor}
      />

      <div
        className={
          lastDotCount !== noLastDotCountValue
            ? 'flex flex-col space-y-4'
            : 'invisible'
        }
      >
        <div className="flex items-center">
          <label htmlFor="answerInput" className="text-lg font-medium mr-2">
            How many dots did you see?
          </label>
          <input
            type="number"
            id="answerInput"
            className="border rounded px-4 py-2"
            min={minDotCount}
            max={maxDotCount}
            step={dotCountStep}
          />
          <button
            id="submitButton"
            className="bg-green-500 text-white px-4 py-2 rounded hover:bg-green-600 ml-4"
            onClick={handleAnswer}
          >
            CHECK
          </button>
        </div>

        <div className="flex justify-center">
          <label className="text-lg font-medium" id="resultLabel">
            THer label
          </label>
        </div>
      </div>
    </div>
  );
};

export default DotCountGame;
