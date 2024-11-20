// axios, biblioteca utilizada para simplificar as requisicoes do HTTP
// forma de instalacao 'npm install axios' e 'npm install --save-dev @types/axios'

import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

const Login: React.FC = () => {
  const [formData, setFormData] = useState<LoginForm>({ email: '', senha: '' });
  const [errorMessage, setErrorMessage] = useState<string | null>(null);
  const navigate = useNavigate();

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      const response = await axios.post('http://localhost:5072/login', formData);
      if (response.status === 200) {
        console.log('Login acessado com sucesso!');
        navigate('/home');
      }
    } catch (error) { 
      setErrorMessage('Login Inválido, tente novamente!');
    }
  };

  return (
    <div>
      <h2>Login</h2>
      <form onSubmit={handleSubmit}>
        <label>
          Email:
          <input type="email" name="email" value={formData.email} onChange={handleChange} />
        </label>
        <label>
          Senha:
          <input type="password" name="senha" value={formData.senha} onChange={handleChange} />
        </label>
        <button type="submit">Login</button>
      </form>
      {errorMessage && <p style={{ color: 'red' }}>{errorMessage}</p>}
    </div>
  );
};

export default Login;
