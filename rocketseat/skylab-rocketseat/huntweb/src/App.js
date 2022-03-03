import React from 'react'
import Header from './components/Header';
import Main from './pages/main'
import './styles.css';

import Routes from './routes';

const App = () => (
  <div className="App">
    <Header />
    <Routes />
  </div>
);

export default App;
