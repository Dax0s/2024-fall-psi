import { useState } from 'react';
import { average, inRange } from '@/utils/math';
import { ReactionTimeButton } from '@/components/reaction-time-game/reaction-time-button';
import { GAME_URL } from '@/components/reaction-time-game/constants';
import { ResultsPage } from '@/components/global/results-page';

function Game() {
  const [tries, setTries] = useState<number[]>([]);

  return (
    <div className="app">
      <header className="app-header">
        {inRange(tries.length, 0, 5) ? (
          <ReactionTimeButton tries={tries} setTries={(newTries: number[]) => setTries(newTries)} />
        ) : (
          <ResultsPage
            gameUrl={GAME_URL}
            score={tries.length > 0 ? Math.floor(average(tries)) : 0}
          />
        )}
      </header>
    </div>
  );
}

export default Game;