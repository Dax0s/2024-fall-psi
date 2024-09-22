import React, { useState, useEffect } from "react";

const MemoryGame = () => {
    const [grid, setGrid] = useState([]);
    const [clickedNumbers, setClickedNumbers] = useState<number[]>([]);
    const [isGameStarted, setIsGameStarted] = useState(false);
    const [showNumbers, setShowNumbers] = useState(true);
    const [result, setResult] = useState<string | null>(null);

    // Start the game and get grid numbers from the backend
    const startGame = async () => {
        try {
            const response = await fetch("http://localhost:5252/game/start");
            const data = await response.json();
            setGrid(data);
            setIsGameStarted(true);

            // Show numbers for 3 seconds and then hide them
            setTimeout(() => {
                setShowNumbers(false);
            }, 3000);
        } catch (error) {
            console.error("Error starting game:", error);
        }
    };

    // Handle number click
    const handleClick = (number : number) => {
        if (clickedNumbers.length < 5) {
            setClickedNumbers([...clickedNumbers, number]);
        }
    };

    // Submit the clicked numbers for verification
    useEffect(() => {
        if (clickedNumbers.length === 5) {
            verifySequence();
        }
    }, [clickedNumbers]);

    // Verify the clicked sequence
    const verifySequence = async () => {
        try {
            const response = await fetch("http://localhost:5252/game/verify", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(clickedNumbers),
            });

            const data = await response.json();
            setResult(data ? "You win!" : "You lose. Try again!");
        } catch (error) {
            console.error("Error verifying sequence:", error);
        }
    };

    return (
        <div>
            <h1>Memory Game</h1>
            {!isGameStarted && <button onClick={startGame}>Start Game</button>}
            {isGameStarted && (
                <div className="grid">
                    {grid.map((number, index) => (
                        <div
                            key={index}
                            className="grid-item"
                            onClick={() => handleClick(number)}
                        >
                            {showNumbers ? number : "?"}
                        </div>
                    ))}
                </div>
            )}
            {result && <h2>{result}</h2>}
            <div>
                <h3>Clicked Numbers:</h3>
                <p>{clickedNumbers.join(", ")}</p>
            </div>
        </div>
    );
};

export default MemoryGame;
