import React, { useState, useEffect } from "react";

const MemoryGame = () => {
    const [grid, setGrid] = useState<(number | null)[]>([]);
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
    const handleClick = (num: number | null, index: number) => {
        if (num !== null && clickedNumbers.length < 5 && !clickedNumbers.includes(num)) {
            setClickedNumbers([...clickedNumbers, num]);
            setGrid((prevGrid) => {
                const newGrid = [...prevGrid];
                newGrid[index] = null; // Remove the number when clicked
                return newGrid;
            });
        }
    };

    // Submit the clicked numbers for verification
    useEffect(() => {
        if (clickedNumbers.length === 5) {
            verifySequence();
        }
    }, [clickedNumbers]);

    // Verify the clicked sequence
    const verifySequence = () => {
        const expectedSequence = [1, 2, 3, 4, 5];

        // Check if clicked numbers match the expected sequence
        const isWin = clickedNumbers.length === 5 && clickedNumbers.every((num, index) => num === expectedSequence[index]);

        setResult(isWin ? "You win!" : "You lose. Try again!");
    };

    return (
        <div>
            <h1>Memory Game</h1>
            {!isGameStarted && <button onClick={startGame}>Start Game</button>}
            {isGameStarted && (
                <div className="grid grid-cols-4 gap-2 mt-5">
                    {grid.map((number, index) => (
                        <div
                            key={index}
                            className="w-24 h-24 bg-gray-100 flex justify-center items-center text-2xl cursor-pointer border-2 border-gray-300 hover:bg-gray-200"
                            onClick={showNumbers ? () => null : () => handleClick(number, index)}
                        >
                            { showNumbers ? number: "?" }
                        </div>
                    ))}
                </div>
            )}
            {result && <h2>{result}</h2>}
            <div>
            </div>
        </div>
    );
};

export default MemoryGame;

