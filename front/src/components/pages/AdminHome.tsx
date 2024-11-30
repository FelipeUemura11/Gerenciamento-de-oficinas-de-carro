import React, { useEffect, useState } from 'react';
import Car from '../../models/Car';
import CarService from '../../models/CarService';
import Client from '../../models/Client';
import ClientHomeProps from '../../models/ClientHomeProps';
import Order from '../../models/Order';

const AdminHome: React.FC = () => {
  const [clients, setClients] = useState<Client[]>([]);
  const [cars, setCars] = useState<Car[]>([]);
  const [orders, setOrders] = useState<Order[]>([]);
  const [carServices, setCarServices] = useState<CarService[]>([]);
  const [selectedClient, setSelectedClient] = useState<Client | null>(null);
  const [selectedCar, setSelectedCar] = useState<Car | null>(null);
  const [newService, setNewService] = useState<Omit<CarService, 'id'>>({ name: '', price: 0, description: '', category: '' });
  const [showClients, setShowClients] = useState(true);
  const [showCars, setShowCars] = useState(true);
  const [showOrders, setShowOrders] = useState(true);
  const [showCarServices, setShowCarServices] = useState(true);
  const [searchService, setSearchService] = useState<string>('');
  const [editClient, setEditClient] = useState<Client | null>(null);

  useEffect(() => {
    const fetchClients = async () => {
      try {
        const response = await fetch('http://localhost:5072/clients');
        const data = await response.json();
        setClients(data);
      } catch (error) {
        console.error('Erro ao buscar clientes', error);
      }
    };

    const fetchCars = async () => {
      try {
        const response = await fetch('http://localhost:5072/cars');
        const data = await response.json();
        setCars(data);
      } catch (error) {
        console.error('Erro ao buscar carros', error);
      }
    };

    const fetchOrders = async () => {
      try {
        const response = await fetch('http://localhost:5072/orders');
        const data = await response.json();
        setOrders(data);
      } catch (error) {
        console.error('Erro ao buscar ordens', error);
      }
    };

    const fetchCarServices = async () => {
      try {
        const response = await fetch('http://localhost:5072/carservices');
        const data = await response.json();
        setCarServices(data);
      } catch (error) {
        console.error('Erro ao buscar serviços de carro', error);
      }
    };

    fetchClients();
    fetchCars();
    fetchOrders();
    fetchCarServices();
  }, []);

  const fetchCarServices = async () => {
    try {
      const response = await fetch('http://localhost:5072/carservices');
      const data = await response.json();
      setCarServices(data);
    } catch (error) {
      console.error('Erro ao buscar serviços de carro', error);
    }
  };

  const handleRemoveClient = async (clientId: string) => {
    try {
      const response = await fetch(`http://localhost:5072/clients/${clientId}`, {
        method: 'DELETE',
      });
      if (!response.ok) {
        const data = await response.json();
        console.error('Erro ao remover cliente', data);
        return;
      }
      setClients(clients.filter(client => client.id !== clientId));
    } catch (error) {
      console.error('Erro ao remover cliente', error);
    }
  };

  const handleRemoveCar = async (carId: string) => {
    try {
      const response = await fetch(`http://localhost:5072/car/${carId}`, {
        method: 'DELETE',
      });
      if (!response.ok) {
        const data = await response.json();
        console.error('Erro ao remover carro', data);
        return;
      }
      setCars(cars.filter(car => car.id !== carId));
      setOrders(orders.filter(order => order.car.id !== carId));
      if (selectedClient) {
        setSelectedClient({
          ...selectedClient,
          cars: selectedClient.cars.filter(car => car.id !== carId),
        });
      }
      if (selectedCar && selectedCar.id === carId) {
        setSelectedCar(null);
      }
    } catch (error) {
      console.error('Erro ao remover carro', error);
    }
  };

  const handleAddService = async () => {
    if (!newService.name || newService.price <= 0 || !newService.description || !newService.category) {
      alert('Por favor, preencha todas as informações do serviço.');
      return;
    }

    try {
      const response = await fetch('http://localhost:5072/carservice', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(newService),
      });
      const data = await response.json();
      if (!response.ok) {
        console.error('Erro ao adicionar serviço', data);
        return;
      }
      setCarServices([...carServices, data]);
      setNewService({ name: '', price: 0, description: '', category: '' });
    } catch (error) {
      console.error('Erro ao adicionar serviço', error);
    }
  };

  const handleCompleteOrder = async (orderId: string) => {
    try {
      const response = await fetch(`http://localhost:5072/orders/status/${orderId}`, {
        method: 'PUT',
      });
      if (!response.ok) {
        const data = await response.json();
        console.error('Erro ao concluir ordem', data);
        return;
      }
      setOrders(orders.map(order => order.id === orderId ? { ...order, status: true } : order));
      if (selectedClient) {
        setSelectedClient({
          ...selectedClient,
          orders: selectedClient.orders.map(order => order.id === orderId ? { ...order, status: true } : order),
        });
      }
      if (selectedCar) {
        setSelectedCar({
          ...selectedCar,
          ordersHistoric: selectedCar.ordersHistoric.map(order => order.id === orderId ? { ...order, status: true } : order),
        });
      }
    } catch (error) {
      console.error('Erro ao concluir ordem', error);
    }
  };

  const handleSearchService = async () => {
    try {
      const response = await fetch(`http://localhost:5072/carservice/${searchService}`);
      const data = await response.json();
      if (!response.ok) {
        console.error('Erro ao buscar serviço', data);
        setCarServices([]);
        return;
      }
      setCarServices(data);
    } catch (error) {
      console.error('Erro ao buscar serviço', error);
      setCarServices([]);
    }
  };

  const handleResetServiceSearch = async () => {
    setSearchService('');
    await fetchCarServices();
  };

  const handleToggleClientDetails = (client: Client) => {
    if (selectedClient && selectedClient.id === client.id) {
      setSelectedClient(null);
    } else {
      setSelectedClient(client);
    }
  };

  const handleToggleCarDetails = (car: Car) => {
    if (selectedCar && selectedCar.id === car.id) {
      setSelectedCar(null);
    } else {
      setSelectedCar(car);
    }
  };

  const handleUpdateClient = async () => {
    if (!editClient) return;
  
    try {
      const response = await fetch(`http://localhost:5072/clients/${editClient.id}`, {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(editClient),
      });
  
      if (!response.ok) {
        console.error('Erro ao atualizar cliente');
        return;
      }
  
      setClients(clients.map(client => client.id === editClient.id ? editClient : client));
      setEditClient(null);
      alert('Cliente atualizado com sucesso!');
    } catch (error) {
      console.error('Erro ao atualizar cliente', error);
    }
  };

  const handleRemoveCarService = async (serviceId: string) => {
    try {
      const response = await fetch(`http://localhost:5072/carservices/${serviceId}`, {
        method: 'DELETE',
      });
      if (!response.ok) {
        const data = await response.json();
        console.error('Erro ao remover serviço', data);
        return;
      }
      setCarServices(carServices.filter(service => service.id !== serviceId));
    } catch (error) {
      console.error('Erro ao remover serviço', error);
    }
  };

  const formatDate = (dateString: string) => {
    const options: Intl.DateTimeFormatOptions = {
      hour: '2-digit',
      minute: '2-digit',
      day: '2-digit',
      month: '2-digit',
      year: 'numeric',
    };
    return new Date(dateString).toLocaleString('pt-BR', options);
  };

  return (
    <div>
      <nav className="navbar navbar-light bg-light">
        <div className="container-fluid">
          <span className="navbar-brand mb-0 h1">Admin</span>
          <span className="navbar-text">
            <i className="bi bi-person-circle" style={{ fontSize: '1.5rem' }}></i>
          </span>
        </div>
      </nav>
      <div className="container my-4">
        <h2 className="mb-4 text-primary" onClick={() => setShowClients(!showClients)} style={{ cursor: 'pointer' }}>
          Clientes {showClients ? '▼' : '►'}
        </h2>
        {showClients && (
          <ul className="list-group mb-4">
            {clients.length === 0 ? (
              <li className="list-group-item">Nenhum cliente encontrado</li>
            ) : (
              clients.map((client) => (
                <li
                  key={client.id}
                  className="list-group-item d-flex justify-content-between align-items-center"
                >
                  <>
                    <div>
                      {client.name} - {client.email} - {client.phone}
                    </div>
                    <div>
                      <button
                        className="btn btn-danger btn-sm me-2"
                        onClick={() => handleRemoveClient(client.id)}
                      >
                        Remover
                      </button>
                      <button
                        className="btn btn-info btn-sm"
                        onClick={() => handleToggleClientDetails(client)}
                      >
                        Detalhes
                      </button>
                    </div>
                  </>
                </li>
              ))
            )}
          </ul>
        )}

        {selectedClient && (
          <div className="card mb-4">
            <div className="card-body">
              <h3 className="card-title">Detalhes do Cliente</h3>
              <p className="card-text">
                <h5>{selectedClient.name}</h5>
                <strong>Email:</strong> {selectedClient.email} <br />
                <strong>Telefone:</strong> {selectedClient.phone} <br />
                <strong>CPF:</strong> {selectedClient.cpf} <br />
                <strong>CEP:</strong> {selectedClient.cep} <br />
                <strong>Estado:</strong> {selectedClient.estado} <br />
                <strong>Cidade:</strong> {selectedClient.cidade} <br />
                <strong>Bairro:</strong> {selectedClient.bairro} <br />
                <strong>Logradouro:</strong> {selectedClient.logradouro}
              </p>
              <button className="btn btn-primary mb-3" onClick={() => setEditClient(selectedClient)}>Editar Cliente</button>
              <h4>Carros</h4>
              <ul className="list-group mb-3">
                {selectedClient.cars.length === 0 ? (
                  <li className="list-group-item">Nenhum carro encontrado</li>
                ) : (
                  selectedClient.cars.map((car) => (
                    <li key={car.id} className="list-group-item d-flex justify-content-between align-items-center">
                      <span>
                        {car.brand} {car.model} ({car.year}) - {car.plate} - {car.color}
                      </span>
                      <button
                        className="btn btn-danger btn-sm"
                        onClick={() => handleRemoveCar(car.id)}
                      >
                        Remover
                      </button>
                    </li>
                  ))
                )}
              </ul>
              <h4>Ordens</h4>
              <ul className="list-group mb-3">
                {selectedClient.orders.length === 0 ? (
                  <li className="list-group-item">Nenhuma ordem encontrada</li>
                ) : (
                  selectedClient.orders.map((order) => (
                    <li key={order.id} className="list-group-item d-flex justify-content-between align-items-center">
                      <span>
                        <strong>Nome:</strong> {order.name} - <strong>Descrição:</strong> {order.description} -{' '}
                        {formatDate(order.createdDate)}
                      </span>
                      <span>
                        <span className="badge bg-success fs-5 me-2">R${order.totalPrice}</span>
                        {order.status ? (
                          <span className="badge bg-success fs-5">Concluída</span>
                        ) : (
                          <span className="badge bg-danger fs-5">Pendente</span>
                        )}
                        {!order.status && (
                          <button
                            className="btn btn-success btn-sm ms-2"
                            onClick={() => handleCompleteOrder(order.id)}
                          >
                            Concluir
                          </button>
                        )}
                      </span>
                    </li>
                  ))
                )}
              </ul>
            </div>
          </div>
        )}

        {editClient && (
          <div className="card mb-4">
            <div className="card-body">
              <h3 className="card-title">Editar Cliente</h3>
              <div className="mb-3">
                <label className="form-label">Nome</label>
                <input
                  type="text"
                  className="form-control"
                  value={editClient.name}
                  onChange={(e) => setEditClient({ ...editClient, name: e.target.value })}
                />
              </div>
              <div className="mb-3">
                <label className="form-label">Email</label>
                <input
                  type="email"
                  className="form-control"
                  value={editClient.email}
                  onChange={(e) => setEditClient({ ...editClient, email: e.target.value })}
                />
              </div>
              <div className="mb-3">
                <label className="form-label">Telefone</label>
                <input
                  type="text"
                  className="form-control"
                  value={editClient.phone}
                  onChange={(e) => setEditClient({ ...editClient, phone: e.target.value })}
                />
              </div>
              <div className="mb-3">
                <label className="form-label">CEP</label>
                <input
                  type="text"
                  className="form-control"
                  value={editClient.cep}
                  onChange={(e) => setEditClient({ ...editClient, cep: e.target.value })}
                />
              </div>
              <div className="mb-3">
                <label className="form-label">Estado</label>
                <input
                  type="text"
                  className="form-control"
                  value={editClient.estado}
                  onChange={(e) => setEditClient({ ...editClient, estado: e.target.value })}
                />
              </div>
              <div className="mb-3">
                <label className="form-label">Cidade</label>
                <input
                  type="text"
                  className="form-control"
                  value={editClient.cidade}
                  onChange={(e) => setEditClient({ ...editClient, cidade: e.target.value })}
                />
              </div>
              <div className="mb-3">
                <label className="form-label">Bairro</label>
                <input
                  type="text"
                  className="form-control"
                  value={editClient.bairro}
                  onChange={(e) => setEditClient({ ...editClient, bairro: e.target.value })}
                />
              </div>
              <div className="mb-3">
                <label className="form-label">Logradouro</label>
                <input
                  type="text"
                  className="form-control"
                  value={editClient.logradouro}
                  onChange={(e) => setEditClient({ ...editClient, logradouro: e.target.value })}
                />
              </div>
              <button className="btn btn-success" onClick={handleUpdateClient}>Salvar</button>
              <button className="btn btn-secondary ms-2" onClick={() => setEditClient(null)}>Cancelar</button>
            </div>
          </div>
        )}

        <h2 className="mb-4 text-success">Adicionar Novo Serviço</h2>
        <div className="row g-3 mb-4">
          <div className="col-md-3">
            <input
              type="text"
              className="form-control"
              placeholder="Nome do Serviço"
              value={newService.name}
              onChange={(e) =>
                setNewService({ ...newService, name: e.target.value })
              }
            />
          </div>
          <div className="col-md-3">
            <input
              type="number"
              className="form-control"
              placeholder="Preço"
              value={newService.price}
              onChange={(e) =>
                setNewService({ ...newService, price: parseFloat(e.target.value) })
              }
            />
          </div>
          <div className="col-md-3">
            <input
              type="text"
              className="form-control"
              placeholder="Descrição"
              value={newService.description}
              onChange={(e) =>
                setNewService({ ...newService, description: e.target.value })
              }
            />
          </div>
          <div className="col-md-3">
            <input
              type="text"
              className="form-control"
              placeholder="Categoria"
              value={newService.category}
              onChange={(e) =>
                setNewService({ ...newService, category: e.target.value })
              }
            />
          </div>
          <div className="col-md-12">
            <button className="btn btn-primary w-100" onClick={handleAddService}>
              Adicionar Serviço
            </button>
          </div>
        </div>

        <h2 className="mb-4 text-info" onClick={() => setShowCarServices(!showCarServices)} style={{ cursor: 'pointer' }}>
          Serviços de Carro {showCarServices ? '▼' : '►'}
        </h2>
        {showCarServices && (
          <>
            <div className="input-group mb-3">
              <input
                type="text"
                className="form-control"
                placeholder="Pesquisar serviço por nome"
                value={searchService}
                onChange={(e) => setSearchService(e.target.value)}
              />
              <button className="btn btn-outline-secondary" onClick={handleSearchService}>
                Pesquisar
              </button>
              <button className="btn btn-outline-secondary" onClick={handleResetServiceSearch}>
                Resetar
              </button>
            </div>
            <ul className="list-group mb-4">
              {carServices.length === 0 ? (
                <li className="list-group-item">Nenhum serviço encontrado</li>
              ) : (
                carServices.map((service) => (
                  <li
                    key={service.id}
                    className="list-group-item d-flex justify-content-between align-items-center"
                  >
                    <div>
                      {service.name} - R${service.price} - {service.category} - {service.description}
                    </div>
                    <button
                      className="btn btn-danger btn-sm"
                      onClick={() => handleRemoveCarService(service.id)}
                    >
                      Remover
                    </button>
                  </li>
                ))
              )}
            </ul>
          </>
        )}

        <h2 className="mb-4 text-warning" onClick={() => setShowCars(!showCars)} style={{ cursor: 'pointer' }}>
          Carros {showCars ? '▼' : '►'}
        </h2>
        {showCars && (
          <ul className="list-group mb-4">
            {cars.length === 0 ? (
              <li className="list-group-item">Nenhum carro encontrado</li>
            ) : (
              cars.map((car) => (
                <li
                  key={car.id}
                  className="list-group-item d-flex justify-content-between align-items-center"
                >
                  <div>
                    {car.brand} {car.model} ({car.year}) - {car.plate} - {car.color}
                  </div>
                  <div>
                    <button
                      className="btn btn-danger btn-sm me-2"
                      onClick={() => handleRemoveCar(car.id)}
                    >
                      Remover
                    </button>
                    <button
                      className="btn btn-info btn-sm"
                      onClick={() => handleToggleCarDetails(car)}
                    >
                      Detalhes
                    </button>
                  </div>
                </li>
              ))
            )}
          </ul>
        )}

        {selectedCar && (
          <div className="card mb-4">
            <div className="card-body">
              <h3 className="card-title">Detalhes do Carro</h3>
              <p className="card-text">
                <h5>{selectedCar.brand} {selectedCar.model} ({selectedCar.year})</h5>
                {selectedCar.plate} - {selectedCar.color}
              </p>
              <h4>Histórico de Ordens</h4>
              <ul className="list-group mb-3">
                {selectedCar.ordersHistoric.map((order) => (
                  <li key={order.id} className="list-group-item d-flex justify-content-between align-items-center">
                    <span>
                      <strong>Nome:</strong> {order.name} - <strong>Descrição:</strong> {order.description} -{' '}
                      {formatDate(order.createdDate)}
                    </span>
                    <span>
                      <span className="badge bg-success fs-5 me-2">R${order.totalPrice}</span>
                      {order.status ? (
                        <span className="badge bg-success fs-5">Concluída</span>
                      ) : (
                        <span className="badge bg-danger fs-5">Pendente</span>
                      )}
                      {!order.status && (
                        <button
                          className="btn btn-success btn-sm ms-2"
                          onClick={() => handleCompleteOrder(order.id)}
                        >
                          Concluir
                        </button>
                      )}
                    </span>
                  </li>
                ))}
              </ul>
            </div>
          </div>
        )}

        <h2 className="mb-4 text-warning" onClick={() => setShowOrders(!showOrders)} style={{ cursor: 'pointer' }}>
          Ordens {showOrders ? '▼' : '►'}
        </h2>
        {showOrders && (
          <ul className="list-group mb-4">
            {orders.length === 0 ? (
              <li className="list-group-item">Nenhuma ordem encontrada</li>
            ) : (
              orders.map((order) => (
                <li
                  key={order.id}
                  className="list-group-item d-flex justify-content-between align-items-center"
                >
                  <span>
                    <strong>Nome:</strong> {order.name} - <strong>Descrição:</strong> {order.description} -{' '}
                    {formatDate(order.createdDate)} - {order.car.brand} {order.car.model} ({order.car.year})
                  </span>
                  <span>
                    <span className="badge bg-success fs-5 me-2">R${order.totalPrice}</span>
                    {order.status ? (
                      <span className="badge bg-success fs-5">Concluída</span>
                    ) : (
                      <span className="badge bg-danger fs-5">Pendente</span>
                    )}
                    {!order.status && (
                      <button
                        className="btn btn-success btn-sm ms-2"
                        onClick={() => handleCompleteOrder(order.id)}
                      >
                        Concluir
                      </button>
                    )}
                  </span>
                </li>
              ))
            )}
          </ul>
        )}
      </div>
    </div>
  );
};

export default AdminHome;
