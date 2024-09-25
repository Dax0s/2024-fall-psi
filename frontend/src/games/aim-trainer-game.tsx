import {useState} from "react";
import Difficulty from "../components/aim-trainer-game/Difficulty";
import DifficultyPicker from "../components/aim-trainer-game/DifficultyPicker";
import difficulty from "../components/aim-trainer-game/Difficulty";
import StartGameButton from "../components/aim-trainer-game/StartGameButton";
import startGameButton from "../components/aim-trainer-game/StartGameButton";

type GameStartRequest = {
    difficulty: Difficulty;
    screenSize: {
        x: number;
        y: number;
    }
}

type PointSpawnElement = {
    pos: {
        x: number;
        y: number;
    },
    spawnTime: number;
}

type GameStartResponse = {
    dotInfos: PointSpawnElement[];
    amountOfDots: number;
}

const SIZE_OF_BALL = 96
const BORDER = 100

function delay(ms: number) {
    return new Promise((resolve) => setTimeout(resolve, ms));
}

async function fetchGameStartInfo(difficulty: Difficulty, width: number, height: number) {
    const tmp = await fetch('http://localhost:5252/aimtrainergame/startgame', {
        method: 'POST',
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify({
            difficulty: difficulty,
            screenSize: {
                x: width - SIZE_OF_BALL - BORDER,
                y: height - SIZE_OF_BALL - BORDER
            }
        } as GameStartRequest)
    });
    return await tmp.json() as GameStartResponse;
}

function styleElement(element: HTMLDivElement, {pos: {x, y}}: PointSpawnElement) {
    element.className = "w-24 h-24 bg-sky-500 rounded-full";
    element.style.position = "absolute";
    element.style.top = `${x + BORDER / 2}px`;
    element.style.left = `${y + BORDER / 2}px`;
}

const AimTrainerGame = () => {
    const [gameIsStarted, dotsAreSpawning] = useState(false);
    const [difficulty, setDifficulty] = useState(Difficulty.EASY);
    const [isLoading, setIsLoading] = useState(false);
    const [dotsLeft, setDotsLeft] = useState(0);

    async function spawnDots(gameData: GameStartResponse) {
        const parentElement = document.querySelector("body");

        if (!parentElement)
            return;

        for (const dotInfo of gameData.dotInfos) {
            await delay(dotInfo.spawnTime);

            const element = document.createElement("div");
            element.addEventListener("click", () => {
                element.remove();
                setDotsLeft(prevDots => prevDots - 1);
            })

            styleElement(element, dotInfo)

            parentElement.appendChild(element);
        }
    }

    async function startGame() {
        const { innerWidth: width, innerHeight: height } = window;

        setIsLoading(true);
        const gameData = await fetchGameStartInfo(difficulty, width, height);
        setDotsLeft(gameData.amountOfDots)
        setIsLoading(false);

        dotsAreSpawning(true);
        await spawnDots(gameData);
        dotsAreSpawning(false);
    }

    return (
        <>
            {gameIsStarted || dotsLeft > 0 ?
            <div className="m-4 absolute">
                <p>Left: {dotsLeft}</p>
            </div> : null}
            {!gameIsStarted && dotsLeft === 0 ?
            <div className="flex flex-col-reverse items-center justify-center h-screen">
                <StartGameButton className={"my-4"} onClick={startGame} isLoading={isLoading} />
                <DifficultyPicker defaultDifficulty={difficulty} setParentDifficulty={setDifficulty} />
            </div> : null}

        </>
    )
}

export default AimTrainerGame;