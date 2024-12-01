// src/components/pages/Login.tsx
import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import LoginForm from '../../models/LoginForm';
import '../styles/style.css';



interface LoginProps {
  setIsAuthenticated: (isAuthenticated: boolean) => void;
  setIsAdmin: (isAdmin: boolean) => void;
  setClientId: (clientId: string) => void;
}

interface LoginResponse {
  isAdmin: boolean;
  clientId: string;
}

const Login: React.FC<LoginProps> = ({ setIsAuthenticated, setIsAdmin, setClientId }) => {
  const [formData, setFormData] = useState<LoginForm>({ email: '', senha: '' });
  const [errorMessage, setErrorMessage] = useState<string | null>(null);
  const navigate = useNavigate();

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      const response = await axios.post<LoginResponse>('http://localhost:5072/login', formData);
      console.log('Response:', response);
      if (response.status === 200) {
        console.log('Login acessado com sucessowwww!');
        setIsAuthenticated(true);
        setClientId(response.data.clientId);
        console.log("CLIENTE ID: ", response.data);
        if (response.data.isAdmin) {
          setIsAdmin(true);
          navigate('/admin');
        } else {
          setIsAdmin(false);
          navigate('/home');
        }
      }
    } catch (error) {
      setErrorMessage('Login falhou. Verifique suas credenciais.');
    }
  };

  return (
      <form className="formulario-login" onSubmit={handleSubmit}>
        <h2>Email : </h2>
        <input type="email" name="email" value={formData.email} onChange={handleChange} />
        <h2>Senha : </h2>
        <input type="password" name="senha" value={formData.senha} onChange={handleChange} />
        <button type="submit">Login</button>
        {errorMessage && <p>{errorMessage}</p>}
      </form>
  );
};

export default Login;