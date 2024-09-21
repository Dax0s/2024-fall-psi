import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import logo from './logo.svg';
import './App.css';

import ReactionButton from './components/reaction-game/ReactionButton';

function App() {
  const [weather, setWeather] = useState('');
  const getWeather = async () => {
    try {
      const tmp = await fetch('http://localhost:5252/weatherforecast');
      const json = await tmp.json();
      console.log(json[0]);
      setWeather(json[0].summary);
    } catch (e) {
      console.log(e);
    }
  };

  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.tsx</code> and save to reload.
        </p>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
        <button onClick={getWeather}>Get weather</button>
        <p>{weather}</p>
        <hr style={{ width: '100%', color: '#61dbfb' }} />
        <div className="Games-grid">
          <Link to={'games/reaction-game'} className="Grid-item">
            Reaction Game
          </Link>
        </div>
      </header>
    </div>
  );
}

export default App;
