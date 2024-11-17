import {
  minDotCount,
  maxDotCount,
  dotCountStep,
  minShowDurationMs,
  maxShowDurationMs,
  showDurationMsStep,
} from './constants';
import { GameSettings } from './game-settings';
import { NumericInputElement, readNumericInputField } from './numeric-input-element';

export function GameStartPage(props: { setSettings: (newSettings: GameSettings) => void }) {
  function handleClick() {
    const maxDotCount = readNumericInputField('maxDotsInput') || 0;
    const showDurationMs = readNumericInputField('durationInput') || 0;
    props.setSettings({ maxDotCount, showDurationMs });
  }

  return (
    <div className="app">
      <header className="app-header">
        <div className="flex flex-col items-center justify-center h-screen">
          <h1 className="text-3xl font-bold mb-4">GUESS THE DOT COUNT</h1>

          <div className="flex flex-col space-y-4">
            <NumericInputElement
              labelText="Max. number of dots:"
              inputElementId="maxDotsInput"
              minValue={minDotCount}
              maxValue={maxDotCount}
              step={dotCountStep}
            />
            <NumericInputElement
              labelText="Dot show duration (ms):"
              inputElementId="durationInput"
              minValue={minShowDurationMs}
              maxValue={maxShowDurationMs}
              step={showDurationMsStep}
            />
          </div>

          <button
            className="bg-green-500 text-white px-4 py-2 rounded hover:bg-green-600 mt-4"
            onClick={handleClick}
          >
            GO
          </button>
        </div>
      </header>
    </div>
  );
}
