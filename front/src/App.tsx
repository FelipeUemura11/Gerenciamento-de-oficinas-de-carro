import React, { useState } from 'react';
import { BrowserRouter, Route, Routes, Navigate } from 'react-router-dom';
import Register from './components/pages/Register';
import Login from './components/pages/Login';
import Home from './components/pages/Home';
// import { BrowserRouter, Route, Routes, Link } from 'react-router-dom';

function App() {
  const [isRegister, setIsRegister] = useState(false);

  const toggleForm = () => setIsRegister(!isRegister);
  return (
    <BrowserRouter>
      <Routes>
        <Route
          path="/"
          element={
            <div>
              <h1>Gerenciamento Automóveis</h1>
              <button onClick={toggleForm}>
                {isRegister ? 'Já tem uma conta? Fazer Login' : 'Não tem conta? Registrar'}
              </button>
              {isRegister ? <Register /> : <Login />}
            </div>
          }
        />
        <Route path="/home" element={<Home/>} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
