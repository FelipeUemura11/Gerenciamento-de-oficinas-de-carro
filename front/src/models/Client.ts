interface Client {
    id: string;
    name: string;
    email: string;
    phone: string;
    cpf: string;
    cars: Car[];
    orders: Order[];
    cep?: string;
    estado?: string;
    cidade?: string;
    bairro?: string;
    logradouro?: string;
  }