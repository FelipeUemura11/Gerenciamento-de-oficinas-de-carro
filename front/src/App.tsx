import React, { useState } from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css'; // Import Bootstrap CSS
import Register from './components/pages/Register';
import Login from './components/pages/Login';
import AdminHome from './components/pages/AdminHome';
import ProtectedRoute from './components/ProtectedRoute';
import ClientHome from './components/pages/ClientHome'; // Assuming ClientHome is another component

function App() {
  const [isRegister, setIsRegister] = useState(false);
  const [isAuthenticated, setIsAuthenticated] = useState(false); // Add authentication state
  const [isAdmin, setIsAdmin] = useState(false); // Add admin state
  const [clientId, setClientId] = useState<string | null>(null); // Add client ID state

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
    </BrowserRouter>
  );
}

export default App;