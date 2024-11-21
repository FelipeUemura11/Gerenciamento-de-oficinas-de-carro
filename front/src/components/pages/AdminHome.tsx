import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';

const AdminHome: React.FC = () => {
  const [activeComponent, setActiveComponent] = useState<string>('Cars');

  const renderComponent = () => {
    switch (activeComponent) {
      case 'Cars':
        return <Cars />;
      case 'Clients':
        return <Clients />;
      case 'Services':
        return <Services />;
      case 'Orders':
        return <Orders />;
      default:
        return <Cars />;
    }
  };

  return (
    <div className="container-fluid">
      <div className="row">
        <nav className="col-md-2 d-none d-md-block bg-light sidebar">
          <div className="sidebar-sticky">
            <ul className="nav flex-column">
              <li className="nav-item">
                <Link className="nav-link active" to="#" onClick={() => setActiveComponent('Cars')}>
                  Carros
                </Link>
              </li>
              <li className="nav-item">
                <Link className="nav-link" to="#" onClick={() => setActiveComponent('Clients')}>
                  Clientes
                </Link>
              </li>
              <li className="nav-item">
                <Link className="nav-link" to="#" onClick={() => setActiveComponent('Services')}>
                  Serviços
                </Link>
              </li>
              <li className="nav-item">
                <Link className="nav-link" to="#" onClick={() => setActiveComponent('Orders')}>
                  Orders
                </Link>
              </li>
            </ul>
          </div>
        </nav>
        <main role="main" className="col-md-9 ml-sm-auto col-lg-10 px-4">
          {renderComponent()}
        </main>
      </div>
    </div>
  );
};

const Cars: React.FC = () => {
  return <div>Carros</div>;
};

const Clients: React.FC = () => {
  return <div>Clientes</div>;
};

const Services: React.FC = () => {
  return <div>Serviços</div>;
};

const Orders: React.FC = () => {
  return <div>Orders</div>;
};

export default AdminHome;