import { MutableRefObject, useEffect, useRef, useState } from 'react';
import Difficulty from '@/components/aim-trainer-game/Difficulty';
import DifficultyPicker from '@/components/aim-trainer-game/DifficultyPicker';
import StartGameButton from '@/components/aim-trainer-game/StartGameButton';
import { delay } from '@/utils/timing';
import { BACKEND_URL } from '@/utils/constants';

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
  dotCount: number;
  timeToLive: number;
};

type Highscore = {
  id: string;
  username: string;
  score: number;
  date: Date;
};

const SIZE_OF_BALL = 96;
const BORDER = 100;

const GAME_URL = 'aim-trainer-game';
const NUMBER_OF_HIGHSCORES = 10;

async function fetchGameStartInfo(difficulty: Difficulty, width: number, height: number) {
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

function styleElement(element: HTMLDivElement, { pos: { x, y } }: PointSpawnElement) {
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
  const dotsLeftRef = useRef(0);
  const [score, setScore] = useState(0);
  const scoreRef = useRef(0);
  const [username, setUsername] = useState('');
  const [highscores, setHighscores] = useState([] as Highscore[]);
  const [loadingHighscores, setLoadingHighscores] = useState(false);

  const dots: MutableRefObject<Array<HTMLDivElement>> = useRef([]);

  async function fetchHighscores() {
    setLoadingHighscores(true);
    const tmp = await fetch(`${BACKEND_URL}/aimtrainergame/highscores?=${NUMBER_OF_HIGHSCORES}`);
    const highscores = (await tmp.json()) as Highscore[];
    setHighscores(highscores);
    setLoadingHighscores(false);
  }

  function removeElement(element: HTMLDivElement) {
    dots.current = dots.current.filter((el) => el.id !== element.id);
    element.remove();

    setDotsLeft((prevDots) => {
      dotsLeftRef.current = prevDots - 1;
      return prevDots - 1;
    });
  }

  function removeAllDots() {
    dots.current.forEach(removeElement);
    dots.current = [];
  }

  async function spawnDots(gameData: GameStartResponse) {
    const parentElement = document.querySelector('body');

    if (!parentElement) {
      return;
    }

    let i = 0;
    for (const dotInfo of gameData.dotInfos) {
      await delay(dotInfo.spawnTime);

      if (!window.location.href.includes(GAME_URL)) {
        return;
      }

      const element = document.createElement('div');
      styleElement(element, dotInfo);
      element.id = `${i++}`;
      parentElement.appendChild(element);
      dots.current.push(element);

      const elementTimeout = setTimeout(() => {
        removeElement(element);
      }, gameData.timeToLive);

      element.addEventListener('click', () => {
        clearTimeout(elementTimeout);
        setScore((prevScore) => {
          scoreRef.current = prevScore + 1;
          return prevScore + 1;
        });
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

    setDotsLeft(gameData.dotCount);
    setScore(0);
    setIsLoading(false);

    setGameIsStarted(true);
    await spawnDots(gameData);
  }

  useEffect(() => {
    // if dots array has dots, game is not started or there are dots left
    if (dots.current.length > 0 || !gameIsStarted || dotsLeftRef.current) return;

    setGameIsStarted(false);

    void endGame();
  }, [dots.current]);

  async function endGame() {
    if (!username) return;

    if (scoreRef.current !== 0) {
      await fetch(`${BACKEND_URL}/aimtrainergame/highscores`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          username,
          score: scoreRef.current,
        }),
      });
    }

    void fetchHighscores();
  }

  useEffect(() => {
    void fetchHighscores();
    return () => {
      removeAllDots();
    };
  }, []);

  return (
    <>
      {!gameIsStarted ? (
        <div className="m-4 absolute flex flex-col">
          <p>Highscores: </p>
          {loadingHighscores && 'Loading...'}
          {highscores.map((h, i) => (
            <p key={h.id}>
              {i + 1}. {h.username} scored {h.score} on {new Date(h.date).toLocaleString('lt-LT')}
            </p>
          ))}
        </div>
      ) : null}
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
        <div className="flex flex-col items-center justify-center h-screen">
          <DifficultyPicker
            className={'my-4'}
            defaultDifficulty={difficulty}
            setParentDifficulty={setDifficulty}
          />
          <input
            type="text"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
            placeholder="Enter username"
            className="mb-4 px-4 py-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-sky-500"
            maxLength={20}
          />
          <StartGameButton onClick={startGame} isLoading={isLoading} />
        </div>
      ) : null}
    </>
  );
};

export default AimTrainerGame;
