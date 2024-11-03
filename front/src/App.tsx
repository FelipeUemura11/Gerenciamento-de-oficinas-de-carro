import React, { useState } from 'react';
import Register from './Register';
import Login from './Login';

function App() {
  const [isRegister, setIsRegister] = useState(false);

  const toggleForm = () => setIsRegister(!isRegister);
  return (
    <div>
      <h1> AutoManeger </h1>
      <button onClick={toggleForm}>
        {isRegister ? "Já tem uma conta? Fazer Login" : "Não tem conta? Registrar"}
      </button>
      {isRegister ? <Register/> : <Login/>}
    </div>
  );
}

export default App;
