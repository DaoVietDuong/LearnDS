import React from 'react';
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import Nav from './Nav';
import MainContainer from './MainContainer';
import Banner from './Banner';

function App() {
  return (
    <div className="App">
      <Nav />
      {/* <Banner/> */}
      <MainContainer />
    </div>
  );
}

export default App;
