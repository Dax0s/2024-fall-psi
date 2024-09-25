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

function delay(ms: number) {
    return new Promise((resolve) => setTimeout(resolve, ms));
}

async function fetchGameStartInfo() {
    const tmp = await fetch('http://localhost:5252/aimtrainergame/startgame', {
        method: 'POST',
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify({
            difficulty: Difficulty.EASY,
            screenSize: {
                x: 500,
                y: 500
            }
        } as GameStartRequest)
    });
    const json = await tmp.json() as PointSpawnElement[];
    return json;
}

const AimTrainerGame = () => {
    fetchGameStartInfo();

    async function startGame() {
        const gameData = await fetchGameStartInfo();

        for (const dotInfo of gameData) {
            await delay(dotInfo.spawnTime);

            const parentElement = document.querySelector("#game-area")
            const element = document.createElement("div");
            element.className = "w-24 h-24 bg-sky-500 rounded-full";
            element.style.position = "absolute";
            element.style.top = `${dotInfo.pos.y}px`
            element.style.left = `${dotInfo.pos.x}px`

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
            <div id="game-area" style={{width: "100%", height: "100%", position: "relative"}}></div>
        </>
    )
}

export default AimTrainerGame;