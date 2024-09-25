import {useState} from "react";
import Difficulty from "../components/aim-trainer-game/Difficulty";
import DifficultyPicker from "../components/aim-trainer-game/DifficultyPicker";
import difficulty from "../components/aim-trainer-game/Difficulty";

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
    return await tmp.json() as PointSpawnElement[];
}

async function spawnDots(gameData: PointSpawnElement[]) {
    const parentElement = document.querySelector("body");

    if (!parentElement)
        return;

    for (const dotInfo of gameData) {
        await delay(dotInfo.spawnTime);

        const element = document.createElement("div");
        element.addEventListener("click", () => {
            element.remove();
        })

        element.className = "w-24 h-24 bg-sky-500 rounded-full";
        element.style.position = "absolute";
        element.style.top = `${dotInfo.pos.y + BORDER / 2}px`;
        element.style.left = `${dotInfo.pos.x + BORDER / 2}px`;

        parentElement.appendChild(element);
    }
}

const AimTrainerGame = () => {
    const [gameIsStarted, setGameIsStarted] = useState(false);
    const [difficulty, setDifficulty] = useState(Difficulty.EASY);

    async function startGame() {
        const { innerWidth: width, innerHeight: height } = window;
        const gameData = await fetchGameStartInfo(difficulty, width, height);
        setGameIsStarted(true);
        await spawnDots(gameData);
        setGameIsStarted(false);
    }

    return (
        <>
            {!gameIsStarted ?
            <div className="flex flex-col-reverse items-center justify-center h-screen">
                <button
                    className="px-8 py-4 my-4 text-xl font-bold text-white bg-cyan-500 hover:bg-blue-900 rounded-full duration-300 shadow-lg hover:shadow-xl"
                    onClick={startGame}
                >
                    Start Game
                </button>
                <DifficultyPicker defaultDifficulty={difficulty} setParentDifficulty={setDifficulty} />
            </div> : null}

        </>
    )
}

export default AimTrainerGame;