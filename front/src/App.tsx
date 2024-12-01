import React, { useState } from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css'; // Import Bootstrap CSS
import Register from './components/pages/Register';
import Login from './components/pages/Login';
import AdminHome from './components/pages/AdminHome';
import ProtectedRoute from './components/ProtectedRoute';
import ClientHome from './components/pages/ClientHome'; // Assuming ClientHome is another component
import './components/styles/style.css';  // Import CSS

function App() {
  const [isRegister, setIsRegister] = useState(false);
  const [isAuthenticated, setIsAuthenticated] = useState(false); // Add authentication state
  const [isAdmin, setIsAdmin] = useState(false); // Add admin state
  const [clientId, setClientId] = useState<string | null>(null); // Add client ID state

  const toggleForm = () => setIsRegister(!isRegister);

  return (
    <BrowserRouter>
      <div>
        {/* Barra superior */}
        <header className="header">
          <div className="logo-container">
            <img src="images/logosite.jpg" alt="Logo" className="logo" />
          </div>
          <div className="title-container">
            <h1>Gerenciamento de Automóveis</h1>
          </div>
        </header>

        <Routes>
          <Route
            path="/"
            element={
              <div>
                <button onClick={toggleForm} className={isRegister ? 'btn-login' : 'btn-cadastro'}>
                  {isRegister ? 'Já tem uma conta? Acesse' : 'Ainda não tem conta? Registre-se'}
                </button>
                {isRegister ? <Register /> : <Login setIsAuthenticated={setIsAuthenticated} setIsAdmin={setIsAdmin} setClientId={setClientId} />}
              </div>
            }
          />
          <Route
            path="/admin"
            element={
              <ProtectedRoute isAuthenticated={isAuthenticated && isAdmin}>
                <AdminHome />
              </ProtectedRoute>
            }
          />
          <Route
            path="/home"
            element={
              <ProtectedRoute isAuthenticated={isAuthenticated && !isAdmin}>
                <ClientHome clientId={clientId!} />
              </ProtectedRoute>
            }
          />
        </Routes>
      </div>
    </BrowserRouter>
  );
}

export default App;
