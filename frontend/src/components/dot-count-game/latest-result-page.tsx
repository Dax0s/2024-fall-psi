import { Result } from './result';

function LatestResultPage(props: {
  latestResult: Result;
  clearLatestResult: () => void;
  pushNewResult: (newResult: Result) => void;
}) {
  function getLabelText(): string {
    const answer = props.latestResult.guess;
    const correctAnswer = props.latestResult.correctAnswer;

    const absError = Math.abs(answer - correctAnswer);
    const accuracy = Math.max(0, 100 * (1 - absError / correctAnswer));

    if (accuracy === 100) {
      return 'Congratulations! You got it right!';
    } else if (accuracy >= 90) {
      return 'So close! The actual number was: ' + correctAnswer.toString();
    } else if (accuracy >= 50) {
      return 'Not bad. The actual number was: ' + correctAnswer.toString();
    } else {
      return 'Maybe next time? The actual number was: ' + correctAnswer.toString();
    }
  }

  function prepareForNextRound() {
    props.pushNewResult(props.latestResult);
    props.clearLatestResult();
  }

  return (
    <div className="app">
      <header className="app-header">
        <div className="flex flex-col space-y-4 mt-10">
          <div className="flex justify-center">
            <label className="text-lg font-medium" id="resultLabel">
              {getLabelText()}
            </label>
          </div>
          <button
            className="bg-green-500 text-white px-4 py-2 rounded hover:bg-green-600 ml-4"
            onClick={prepareForNextRound}
          >
            NEXT ROUND
          </button>
        </div>
      </header>
    </div>
  );
}

export default LatestResultPage;
