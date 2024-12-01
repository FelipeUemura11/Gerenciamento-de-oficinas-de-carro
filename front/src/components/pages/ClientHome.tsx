import React, { useEffect, useState } from 'react';
import Car from '../../models/Car';
import CarService from '../../models/CarService';
import Client from '../../models/Client';
import ClientHomeProps from '../../models/ClientHomeProps';
import Order from '../../models/Order';

const ClientHome: React.FC<ClientHomeProps> = ({ clientId }) => {
  const [cars, setCars] = useState<Car[]>([]);
  const [orders, setOrders] = useState<Order[]>([]);
  const [selectedCar, setSelectedCar] = useState<Car | null>(null);
  const [newCar, setNewCar] = useState<Omit<Car, 'id'>>({ brand: '', model: '', year: '2024', plate: '', color: '', clientId, ordersHistoric: [] });
  const [carServices, setCarServices] = useState<CarService[]>([]);
  const [filteredOrders, setFilteredOrders] = useState<Order[]>([]);
  const [newOrder, setNewOrder] = useState<Omit<Order, 'id'>>({ 
    name: '', 
    description: '', 
    totalPrice: 0, 
    createdDate: '', 
    status: false, 
    carServiceId: '', 
    car: {
      id: '',
      brand: '',
      model: '',
      year: '',
      plate: '',
      color: '',
      clientId: '',
      ordersHistoric: []
    }
  });
  const [showAddCar, setShowAddCar] = useState(false);
  const [showOrders, setShowOrders] = useState(false);
  const [clientName, setClientName] = useState<string>('Cliente');

  useEffect(() => {
    const fetchCars = async () => {
      try {
        const response = await fetch('http://localhost:5072/cars');
        const data = await response.json();
        const filteredCars = data.filter((car: Car) => car.clientId === clientId);
        setCars(filteredCars);
      } catch (error) {
        console.error('Erro ao buscar carros', error);
      }
    };

    const fetchOrders = async () => {
      try {
        const response = await fetch(`http://localhost:5072/orders/client/${clientId}`);
        const data = await response.json();
        const ordersWithCarDetails = data.map((order: any) => {
          if (order.Car) {
            return {
              ...order,
              car: {
                id: order.Car.Id,
                brand: order.Car.Brand,
                model: order.Car.Model,
                year: order.Car.Year,
                plate: order.Car.Plate,
                color: order.Car.Color,
                clientId: order.Car.ClientId,
                ordersHistoric: order.Car.OrdersHistoric,
              },
            };
          } else {
            return order;
          }
        })
        setOrders(ordersWithCarDetails);
        console.log("ORDERS: ", ordersWithCarDetails);
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

    const fetchClientName = async () => {
      try {
        const response = await fetch(`http://localhost:5072/client/${clientId}`);
        const data = await response.json();
        setClientName(data.name);
      } catch (error) {
        console.error('Erro ao buscar nome do cliente', error);
      }
    };

    fetchCars();
    fetchOrders();
    fetchCarServices();
    fetchClientName();
  }, [clientId]);

  const handleAddCar = async () => {
    if (!newCar.brand || !newCar.model || !newCar.year || !newCar.plate || !newCar.color) {
      alert('Por favor, preencha todas as informações do carro.');
      return;
    }
  
    try {
      const carToAdd = { ...newCar, clientId };
      const response = await fetch('http://localhost:5072/car', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(carToAdd),
      });
      const data = await response.json();
      if (!response.ok) {
        console.error('Erro ao adicionar carro', data);
        return;
      }
      setCars([...cars, data]);
      setNewCar({ brand: '', model: '', year: '2024', plate: '', color: '', clientId, ordersHistoric: [] });
    } catch (error) {
      console.error('Erro ao adicionar carro', error);
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
      if (selectedCar && selectedCar.id === carId) {
        setSelectedCar(null);
      }
    } catch (error) {
      console.error('Erro ao remover carro', error);
    }
  };

  const handleSelectCar = (car: Car) => {
    if (selectedCar && selectedCar.id === car.id) {
      setSelectedCar(null);
    } else {
      setSelectedCar(car);
    }
  };

  const handleAddOrder = async () => {
    if (!selectedCar) {
      alert('Selecione um carro primeiro.');
      return;
    }
  
    if (!newOrder.name || !newOrder.description || !newOrder.carServiceId) {
      alert('Por favor, preencha todas as informações da ordem.');
      return;
    }
  
    try {
      console.log(newOrder);
      const orderToAdd = {
        ...newOrder,
        carId: selectedCar.id,
        clientId: clientId,
        carServiceId: newOrder.carServiceId,
        createdDate: new Date().toISOString(),
      };
      console.log(orderToAdd);
      const response = await fetch('http://localhost:5072/order', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(orderToAdd),
      });
      const data = await response.json();
      if (!response.ok) {
        console.error('Erro ao adicionar ordem', data);
        return;
      }
      setOrders([...orders, data]);
      setSelectedCar({
        ...selectedCar,
        ordersHistoric: [...selectedCar.ordersHistoric, data],
      });
      setNewOrder({ 
        name: '', 
        description: '', 
        totalPrice: 0, 
        createdDate: '', 
        status: false, 
        carServiceId: '', 
        car: {
          id: '',
          brand: '',
          model: '',
          year: '',
          plate: '',
          color: '',
          clientId: '',
          ordersHistoric: []
        }
      });
    } catch (error) {
      console.error('Erro ao adicionar ordem', error);
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

  const totalPendingPrice = orders
    .filter(order => !order.status)
    .reduce((total, order) => total + order.totalPrice, 0);

  return (
    <div className="home-container">
      <nav>
        <div className="container-fluid">
          <h1>Bem vindo, {clientName}.</h1>
          <span className="navbar-text">
            <i className="bi bi-person-circle" style={{ fontSize: '1.5rem' }}></i>
          </span>
        </div>
      </nav>
      <div className="container my-4">
        <h2 className="mb-4 text-primary title-nav">Meus Carros</h2>
        <ul className="list-group mb-4">
          {cars.map((car) => (
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
                  onClick={() => handleSelectCar(car)}
                >
                  Detalhes
                </button>
              </div>
            </li>
          ))}
        </ul>

        {selectedCar && (
          <div className="card mb-4">
            <div className="card-body">
              <h3 className="card-title title-nav">Detalhes do Carro</h3>
              <p className="card-text">
                <h5>{selectedCar.brand} {selectedCar.model} ({selectedCar.year})</h5>
                {selectedCar.plate} - {selectedCar.color}
              </p>
              <h4 className="title-nav">Histórico de Ordens</h4>
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
                    </span>
                  </li>
                ))}
              </ul>
              <h4 className="title-nav">Adicionar Nova Ordem</h4>
              <div className="row g-3">
                <div className="col-md-4">
                  <input
                    type="text"
                    className="form-control"
                    placeholder="Nome"
                    value={newOrder.name}
                    onChange={(e) =>
                      setNewOrder({ ...newOrder, name: e.target.value })
                    }
                  />
                </div>
                <div className="col-md-4">
                  <input
                    type="text"
                    className="form-control"
                    placeholder="Descrição"
                    value={newOrder.description}
                    onChange={(e) =>
                      setNewOrder({ ...newOrder, description: e.target.value })
                    }
                  />
                </div>
                <div className="col-md-4">
                  <select
                    className="form-select"
                    onChange={(e) =>
                      setNewOrder({ ...newOrder, carServiceId: e.target.value })
                    }
                  >
                    <option value="">Selecione um serviço</option>
                    {carServices.map((service) => (
                      <option key={service.id} value={service.id}>
                        {service.name} - R${service.price}
                      </option>
                    ))}
                  </select>
                </div>
              </div>
              <button
                className="btn btn-success mt-3"
                onClick={handleAddOrder}
              >
                Adicionar Ordem
              </button>
            </div>
          </div>
        )}

<h2 className="mb-4 text-success title-nav" onClick={() => setShowAddCar(!showAddCar)} style={{ cursor: 'pointer' }}>
          Adicionar Novo Carro {showAddCar ? '▼' : '►'}
        </h2>
        {showAddCar && (
          <div className="row g-3 mb-4">
            <div className="col-md-4">
              <input
                type="text"
                className="form-control"
                placeholder="Marca"
                value={newCar.brand}
                onChange={(e) =>
                  setNewCar({ ...newCar, brand: e.target.value })
                }
              />
            </div>
            <div className="col-md-4">
              <input
                type="text"
                className="form-control"
                placeholder="Modelo"
                value={newCar.model}
                onChange={(e) =>
                  setNewCar({ ...newCar, model: e.target.value })
                }
              />
            </div>
            <div className="col-md-4">
              <input
                type="number"
                className="form-control"
                placeholder="Ano"
                value={newCar.year}
                onChange={(e) =>
                  setNewCar({ ...newCar, year: e.target.value })
                }
              />
            </div>
            <div className="col-md-4">
              <input
                type="text"
                className="form-control"
                placeholder="Placa"
                value={newCar.plate}
                onChange={(e) =>
                  setNewCar({ ...newCar, plate: e.target.value })
                }
              />
            </div>
            <div className="col-md-4">
              <input
                type="text"
                className="form-control"
                placeholder="Cor"
                value={newCar.color}
                onChange={(e) =>
                  setNewCar({ ...newCar, color: e.target.value })
                }
              />
            </div>
            <div className="col-md-4">
              <button className="btn btn-primary w-100" onClick={handleAddCar}>
                Adicionar Carro
              </button>
            </div>
          </div>
        )}

        <h2 className="mb-4 text-warning title-nav" onClick={() => setShowOrders(!showOrders)} style={{ cursor: 'pointer' }}>
          Minhas Ordens {showOrders ? '▼' : '►'}
        </h2>
        {showOrders && (
          <>
            <ul className="list-group">
              {orders.map((order) => (
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
                  </span>
                </li>
              ))}
            </ul>
            <h3 className="mt-4">Total a Pagar: <span className="badge bg-danger fs-4">R${totalPendingPrice}</span></h3>
          </>
        )}
      </div>
    </div>
  );
};

export default ClientHome;