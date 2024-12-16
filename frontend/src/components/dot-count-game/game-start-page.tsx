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
    <div className="text-center">
      <header className="bg-gray-800 min-h-screen flex flex-col items-center justify-center text-white text-xl">
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
            className="bg-sky-400 font-semibold text-white px-4 py-2 rounded-lg transition-colors duration-300 hover:bg-sky-600 mt-4"
            onClick={handleClick}
          >
            Start Game
          </button>
        </div>
      </header>
    </div>
  );
}
