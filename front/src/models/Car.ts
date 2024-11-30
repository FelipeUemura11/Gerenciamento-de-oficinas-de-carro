import Order from "./Order";

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

  export default Car;