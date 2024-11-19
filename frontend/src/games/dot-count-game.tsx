import { useState } from 'react';
import { GameStartPage } from '@/components/dot-count-game/game-start-page';
import { GameSettings } from '@/components/dot-count-game/game-settings';
import { Result } from '@/components/dot-count-game/result';
import {
  GAME_URL,
  maxDotCount,
  maxShowDurationMs,
  minDotCount,
  minShowDurationMs,
} from '@/components/dot-count-game/constants';
import DotsPage from '@/components/dot-count-game/dots-page';
import LatestResultPage from '@/components/dot-count-game/latest-result-page';
import { inRange } from '@/utils/math';
import { ResultsPage } from '@/components/global/results-page';

function validGameSettings(gameSettings: GameSettings): boolean {
  return (
    inRange(gameSettings.maxDotCount, minDotCount, maxDotCount) &&
    inRange(gameSettings.showDurationMs, minShowDurationMs, maxShowDurationMs)
  );
}

function calculateScore(results: Result[], maxDotCount: number): number {
  const totalError = results.reduce(
    (accumulator, result) => accumulator + Math.abs(result.guess - result.correctAnswer),
    0,
  );
  const scoreCoefficient = Math.max(0.0, 1.0 - totalError / maxDotCount);

  return Math.floor(maxDotCount * scoreCoefficient);
}

function DotCountGame() {
  const [gameSettings, setGameSettings] = useState<GameSettings>({
    maxDotCount: 0,
    showDurationMs: 0,
  });
  const [results, setResults] = useState<Result[]>([]);
  const [latestResult, setLatestResult] = useState<Result | null>(null);

  if (!validGameSettings(gameSettings)) {
    return <GameStartPage setSettings={setGameSettings} />;
  }

  if (latestResult != null) {
    return (
      <LatestResultPage
        latestResult={latestResult}
        clearLatestResult={() => setLatestResult(null)}
        pushNewResult={(newResult: Result) => setResults([...results, newResult])}
      />
    );
  }

  if (inRange(results.length, 0, 4)) {
    return (
      <DotsPage
        gameSettings={gameSettings}
        setLatestResult={(newLatestResult) => setLatestResult(newLatestResult)}
      />
    );
  }

  return (
    <div className="app">
      <header className="app-header">
        <ResultsPage gameUrl={GAME_URL} score={calculateScore(results, gameSettings.maxDotCount)} />
      </header>
    </div>
  );
}

export default DotCountGame;
