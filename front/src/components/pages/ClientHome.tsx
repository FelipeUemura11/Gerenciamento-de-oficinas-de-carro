import React, { useEffect, useState } from 'react';

interface Car {
  id: string;
  brand: string;
  model: string;
  year: string;
  plate: string;
  color: string;
  clientId: string;
  ordersHistoric: Order[];
}

interface Order {
  id: string;
  name: string;
  description: string;
  totalPrice: number;
  createdDate: string;
  status: boolean;
  carServiceId: string;
}

interface CarService {
  id: string;
  name: string;
  price: number;
}

interface ClientHomeProps {
  clientId: string;
}

const ClientHome: React.FC<ClientHomeProps> = ({ clientId }) => {
  const [cars, setCars] = useState<Car[]>([]);
  const [orders, setOrders] = useState<Order[]>([]);
  const [selectedCar, setSelectedCar] = useState<Car | null>(null);
  const [newCar, setNewCar] = useState<Omit<Car, 'id'>>({ brand: '', model: '', year: '2024', plate: '', color: '', clientId, ordersHistoric: [] });
  const [carServices, setCarServices] = useState<CarService[]>([]);
  const [newOrder, setNewOrder] = useState<Order>({ id: '', name: '', description: '', totalPrice: 0, createdDate: '', status: false , carServiceId: ''});

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

    fetchCars();
    fetchOrders();
    fetchCarServices();
  }, [clientId]);

  const handleAddCar = async () => {
    try {
      console.log("cliente id: ", clientId);
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
    console.log("SIIII: ", carId);
    try {
      const response = await fetch(`http://localhost:5072/car/${carId}`, {
        method: 'DELETE',
      });
      console.log("SIIII: ", carId);
      if (!response.ok) {
        const data = await response.json();
        console.error('Erro ao remover carro', data);
        return;
      }
      setCars(cars.filter(car => car.id !== carId));
    } catch (error) {
      console.error('Erro ao remover carro', error);
    }
  };

  const handleSelectCar = (car: Car) => {
    setSelectedCar(car);
  };

  const handleAddOrder = async () => {
    if (!selectedCar) {
      alert('Selecione um carro primeiro.');
      return;
    }

    try {
      const response = await fetch('http://localhost:5072/order', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          ...newOrder,
          carId: selectedCar.id,
          clientId: clientId,
          carServiceId: newOrder.carServiceId,
        }),
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
      setNewOrder({ id: '', name: '', description: '', totalPrice: 0, createdDate: '', status: false , carServiceId: ''});
    } catch (error) {
      console.error('Erro ao adicionar ordem', error);
    }
  };

  return (
    <div className="container my-4">
    <h2 className="mb-4 text-primary">Meus Carros</h2>
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

    <h2 className="mb-4 text-success">Adicionar Novo Carro</h2>
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

    {selectedCar && (
      <div className="card mb-4">
        <div className="card-body">
          <h3 className="card-title">Detalhes do Carro</h3>
          <p className="card-text">
            {selectedCar.brand} {selectedCar.model} ({selectedCar.year}) -{' '}
            {selectedCar.plate} - {selectedCar.color}
          </p>
          <h4>Histórico de Ordens</h4>
          <ul className="list-group mb-3">
            {selectedCar.ordersHistoric.map((order) => (
              <li key={order.id} className="list-group-item">
                {order.name} - {order.description} - {order.totalPrice} -{' '}
                {order.createdDate} -{' '}
                {order.status ? (
                  <span className="text-success">Concluída</span>
                ) : (
                  <span className="text-danger">Pendente</span>
                )}
              </li>
            ))}
          </ul>
          <h4>Adicionar Nova Ordem</h4>
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
                    {service.name} - {service.price}
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

    <h2 className="mb-4 text-warning">Minhas Ordens</h2>
    <ul className="list-group">
      {orders.map((order) => (
        <li
          key={order.id}
          className="list-group-item d-flex justify-content-between align-items-center"
        >
          <span>
            {order.name} - {order.description} - {order.totalPrice} -{' '}
            {order.createdDate} -{' '}
            {order.status ? (
              <span className="text-success">Concluída</span>
            ) : (
              <span className="text-danger">Pendente</span>
            )}
          </span>
        </li>
      ))}
    </ul>
  </div>
  );
};

export default ClientHome;