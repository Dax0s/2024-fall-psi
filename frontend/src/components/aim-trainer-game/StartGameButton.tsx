type props = {
  className?: string;
  onClick: () => void;
  isLoading: boolean;
};

const StartGameButton = ({ onClick, isLoading, className }: props) => {
  return (
    <button
      onClick={onClick}
      disabled={isLoading}
      className={`
                ${className}
                px-6 py-3 rounded-full font-semibold text-white
                transition-all duration-300 ease-in-out
                ${
                  isLoading
                    ? 'bg-blue-400 cursor-not-allowed'
                    : 'bg-blue-600 hover:bg-blue-700 active:bg-blue-800 hover:shadow-lg'
                }
                flex items-center justify-center
            `}
    >
      {isLoading ? (
        <>
          <svg
            className="animate-spin -ml-1 mr-3 h-5 w-5 text-white"
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 24 24"
          >
            <circle
              className="opacity-25"
              cx="12"
              cy="12"
              r="10"
              stroke="currentColor"
              strokeWidth="4"
            ></circle>
            <path
              className="opacity-75"
              fill="currentColor"
              d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"
            ></path>
          </svg>
          Loading...
        </>
      ) : (
        'Start Game'
      )}
    </button>
  );
};

export default StartGameButton;
