import { useState } from 'react';
import { average } from '@/utils/math';
import { ReactionTimeButton } from '@/components/reaction-time-game/reaction-time-button';
import { ResultsPage } from '@/components/reaction-time-game/results-page';

function Game() {
  const [tries, setTries] = useState<number[]>([]);

  return (
    <div className="app">
      <header className="app-header">
        {0 <= tries.length && tries.length < 5 ? (
          <ReactionTimeButton tries={tries} setTries={(newTries: number[]) => setTries(newTries)} />
        ) : (
          <ResultsPage score={tries.length > 0 ? Math.floor(average(tries)) : 0} />
        )}
      </header>
    </div>
  );
}

export default Game;
