import { delay } from '@/utils/timing';
import { useRef } from 'react';
import {
  defaultCanvasColor,
  defaultDotColor,
  dotCountStep,
  GAME_URL,
  maxDotCount,
  minDotCount,
} from './constants';
import { DotCanvasInfo } from './dot';
import { GameSettings } from './game-settings';
import { NumericInputElement, readNumericInputField } from './numeric-input-element';
import { Result } from './result';

async function fetchCanvasInfo(maxDots: number): Promise<DotCanvasInfo | undefined> {
  try {
    const response = await fetch(`${GAME_URL}/getcanvas`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(maxDots),
    });
    return (await response.json()) as DotCanvasInfo;
  } catch (error) {
    console.error('Error starting game:', error);
    return undefined;
  }
}

function clearCanvas(canvasRef: React.RefObject<HTMLCanvasElement>) {
  const canvas = canvasRef.current;
  if (!canvas) {
    return;
  }

  const context = canvas.getContext('2d');
  if (!context) {
    return;
  }

  context.fillStyle = defaultCanvasColor;
  context.fillRect(0, 0, canvas.width, canvas.height);
}

function generateCanvas(canvasRef: React.RefObject<HTMLCanvasElement>, canvasInfo: DotCanvasInfo) {
  const canvas = canvasRef.current;
  if (!canvas) {
    return;
  }
  canvas.width = canvasInfo.sideLength;
  canvas.height = canvasInfo.sideLength;

  const context = canvas.getContext('2d');
  if (!context) {
    return;
  }

  clearCanvas(canvasRef);

  context.beginPath();
  canvasInfo.dots.forEach((dot) => {
    context.moveTo(dot.center.x, dot.center.y);
    context.arc(dot.center.x, dot.center.y, dot.radius, 0, 2 * Math.PI);
  });
  context.fillStyle = defaultDotColor;
  context.fill();
}

function DotsPage(props: {
  gameSettings: GameSettings;
  setLatestResult: (resultToSet: Result) => void;
}) {
  const canvasRef = useRef<HTMLCanvasElement>(null);
  const correctDotCountRef = useRef<number | null>(null);

  async function startGame() {
    const canvasInfo: DotCanvasInfo = (await fetchCanvasInfo(props.gameSettings.maxDotCount)) || {
      sideLength: 0,
      dots: [],
    };
    correctDotCountRef.current = canvasInfo.dots.length;

    generateCanvas(canvasRef, canvasInfo);
    await delay(props.gameSettings.showDurationMs);
    clearCanvas(canvasRef);
  }
  startGame();

  function handleAnswer() {
    const correctAnswer = correctDotCountRef.current;
    const guess = readNumericInputField('answerInput');

    if (!correctAnswer || !guess) {
      console.error('failed to save result');
      return;
    }

    props.setLatestResult({ correctAnswer, guess });
  }

  return (
    <div className="text-center">
      <header className="bg-gray-800 min-h-screen flex flex-col items-center justify-center text-white text-xl">
        <canvas
          ref={canvasRef}
          className="border rounded"
          width={0}
          height={0}
          color={defaultCanvasColor}
        />

        <div className="flex flex-col space-y-4 mt-10">
          <NumericInputElement
            labelText="How many dots did you see?"
            inputElementId="answerInput"
            minValue={minDotCount}
            maxValue={maxDotCount}
            step={dotCountStep}
          />
          <button
            className="bg-sky-400 text-white px-4 py-2 rounded-lg transition-colors duration-300 hover:bg-sky-600 ml-4"
            onClick={handleAnswer}
          >
            Check
          </button>
        </div>
      </header>
    </div>
  );
}

export default DotsPage;
