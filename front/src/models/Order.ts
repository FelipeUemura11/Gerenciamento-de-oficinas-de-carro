import Car from './Car';

interface Order {
    id: string;
    name: string;
    description: string;
    totalPrice: number;
    createdDate: string;
    status: boolean;
    carServiceId: string;
    car: Car;
  }

export default Order;