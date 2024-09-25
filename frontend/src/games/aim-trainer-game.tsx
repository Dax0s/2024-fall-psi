import {useState} from "react";

enum Difficulty {
    EASY,
    MEDIUM,
    HARD
}

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

async function fetchGameStartInfo(width: number, height: number) {
    const tmp = await fetch('http://localhost:5252/aimtrainergame/startgame', {
        method: 'POST',
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify({
            difficulty: Difficulty.HARD,
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

    async function startGame() {
        const { innerWidth: width, innerHeight: height } = window;
        const gameData = await fetchGameStartInfo(width, height);
        setGameIsStarted(true);
        await spawnDots(gameData);
        setGameIsStarted(false);
    }

    return (
        <>
            {!gameIsStarted ? <button onClick={startGame}>Start game</button> : null}
        </>
    )
}

export default AimTrainerGame;