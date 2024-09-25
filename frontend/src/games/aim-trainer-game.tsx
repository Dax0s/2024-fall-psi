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
    const json = await tmp.json() as PointSpawnElement[];
    return json;
}

const AimTrainerGame = () => {
    async function startGame() {
        const { innerWidth: width, innerHeight: height } = window;

        const gameData = await fetchGameStartInfo(width, height);

        for (const dotInfo of gameData) {
            await delay(dotInfo.spawnTime);

            const parentElement = document.querySelector("body")
            const element = document.createElement("div");
            element.className = "w-24 h-24 bg-sky-500 rounded-full";
            element.style.position = "absolute";
            element.style.top = `${dotInfo.pos.y + BORDER / 2}px`
            element.style.left = `${dotInfo.pos.x + BORDER / 2}px`

            element.addEventListener("click", () => {
                element.remove()
            })

            if (parentElement)
                parentElement.appendChild(element)
        }
    }

    return (
        <>
            <button onClick={startGame}>Start game</button>
        </>
    )
}

export default AimTrainerGame;