// axios, biblioteca utilizada para simplificar as requisicoes do HTTP
// forma de instalacao 'npm install axios' e 'npm install --save-dev @types/axios'

import React, { useState } from 'react';
import axios from 'axios';

// formato do formulario - login
interface LoginForm {
  email: string;
  senha: string;
}
// indicacao que Login eh um componente React
const Login: React.FC = () => {
  const [formData, setFormData] = useState<LoginForm>({ email: '', senha: '' }); // recebe um objeto formato LoginForm
  const [errorMessage, setErrorMessage] = useState<string | null>(null); // caso de login nulo
  // mudanças nos campos de entrada do formulario.
  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };
  // Manipulador de submissao
  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      const response = await axios.post('http://localhost:5072/login', formData);
      if (response.status === 200) {
        console.log('Login acessado com sucesso!');
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
