import { MutableRefObject, useEffect, useRef, useState } from 'react';
import Difficulty from '@/components/aim-trainer-game/Difficulty';
import DifficultyPicker from '@/components/aim-trainer-game/DifficultyPicker';
import StartGameButton from '@/components/aim-trainer-game/StartGameButton';
import { delay } from '@/utils/timing';
import { BACKEND_URL } from '@/utils/contants';

type GameStartRequest = {
  difficulty: Difficulty;
  screenSize: {
    x: number;
    y: number;
  };
};

type PointSpawnElement = {
  pos: {
    x: number;
    y: number;
  };
  spawnTime: number;
};

type GameStartResponse = {
  dotInfos: PointSpawnElement[];
  amountOfDots: number;
  timeToLive: number;
};

const SIZE_OF_BALL = 96;
const BORDER = 100;

const GAME_URL = 'aim-trainer-game';

async function fetchGameStartInfo(
  difficulty: Difficulty,
  width: number,
  height: number,
) {
  try {
    const tmp = await fetch(`${BACKEND_URL}/aimtrainergame/startgame`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        difficulty: difficulty,
        screenSize: {
          x: width - SIZE_OF_BALL - BORDER,
          y: height - SIZE_OF_BALL - BORDER,
        },
      } as GameStartRequest),
    });
    return (await tmp.json()) as GameStartResponse;
  } catch (e) {
    console.error(e);
    return undefined;
  }
}

function styleElement(
  element: HTMLDivElement,
  { pos: { x, y } }: PointSpawnElement,
) {
  element.className = 'w-24 h-24 bg-sky-500 rounded-full';
  element.style.position = 'absolute';
  element.style.top = `${y + BORDER / 2}px`;
  element.style.left = `${x + BORDER / 2}px`;
}

const AimTrainerGame = () => {
  const [gameIsStarted, setGameIsStarted] = useState(false);
  const [difficulty, setDifficulty] = useState(Difficulty.EASY);
  const [isLoading, setIsLoading] = useState(false);
  const [dotsLeft, setDotsLeft] = useState(0);
  const [score, setScore] = useState(0);

  const dots: MutableRefObject<Array<HTMLDivElement>> = useRef([]);

  function removeElement(element: HTMLDivElement) {
    element.remove();
    setDotsLeft((prevDots) => prevDots - 1);
  }

  function removeAllDots() {
    dots.current.forEach(removeElement);
    dots.current = [];
  }

  async function spawnDots(gameData: GameStartResponse) {
    const parentElement = document.querySelector('body');

    if (!parentElement) return;

    for (const dotInfo of gameData.dotInfos) {
      await delay(dotInfo.spawnTime);

      if (!window.location.href.includes(GAME_URL)) {
        return;
      }

      const element = document.createElement('div');
      styleElement(element, dotInfo);
      parentElement.appendChild(element);
      dots.current.push(element);

      const elementTimeout = setTimeout(() => {
        removeElement(element);
      }, gameData.timeToLive);

      element.addEventListener('click', () => {
        clearTimeout(elementTimeout);
        setScore((prevScore) => prevScore + 1);
        removeElement(element);
      });
    }
  }

  async function startGame() {
    const { innerWidth: width, innerHeight: height } = window;

    setIsLoading(true);
    const gameData = await fetchGameStartInfo(difficulty, width, height);

    if (!gameData) {
      setIsLoading(false);
      alert('Failed to fetch game data from server');

      return;
    }

    setDotsLeft(gameData.amountOfDots);
    setScore(0);
    setIsLoading(false);

    setGameIsStarted(true);
    await spawnDots(gameData);
    setGameIsStarted(false);
  }

  useEffect(() => {
    return () => {
      removeAllDots();
    };
  }, []);

  return (
    <>
      {gameIsStarted || dotsLeft > 0 ? (
        <div className="m-4 absolute flex flex-col">
          <p>Left: {dotsLeft}</p>
        </div>
      ) : null}
      {gameIsStarted || score > 0 || dotsLeft > 0 ? (
        <div className="m-4 absolute right-0 flex flex-col">
          <p>Score: {score}</p>
        </div>
      ) : null}
      {!gameIsStarted && dotsLeft === 0 ? (
        <div className="flex flex-col-reverse items-center justify-center h-screen">
          <StartGameButton
            className={'my-4'}
            onClick={startGame}
            isLoading={isLoading}
          />
          <DifficultyPicker
            defaultDifficulty={difficulty}
            setParentDifficulty={setDifficulty}
          />
        </div>
      ) : null}
    </>
  );
};

export default AimTrainerGame;
