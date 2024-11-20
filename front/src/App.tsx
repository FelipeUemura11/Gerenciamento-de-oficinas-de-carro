import React, { useState } from 'react';
import Register from './components/pages/Register';
import Login from './components/pages/Login';
// import { BrowserRouter, Route, Routes, Link } from 'react-router-dom';

function App() {
  const [isRegister, setIsRegister] = useState(false);

  const toggleForm = () => setIsRegister(!isRegister);
  return (
    <div>
      <h1> Gerenciamento Automoveis - nome </h1>
      <button onClick={toggleForm}>
        {isRegister ? "Já tem uma conta? Fazer Login" : "Não tem conta? Registrar"}
      </button>
      {isRegister ? <Register/> : <Login/>}
    </div>
  );
}

export default App;
