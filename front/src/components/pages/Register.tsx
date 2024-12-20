import React, { useEffect, useState } from "react";

function Register() {
    const [formData, setFormData] = useState({
        Name: "",
        Email: "",
        Cpf: "",
        Phone: "",
        Cep: "",
        Estado: "",
        Cidade: "",
        Bairro: "",
        Logradouro: "",
        Senha: ""
    });

    const [cepTimeout, setCepTimeout] = useState<NodeJS.Timeout | null>(null);

    // Atualiza o formData conforme o usuario preenche o formulario
    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setFormData((prevData) => ({
            ...prevData,
            [name]: value,
        }));

        if (name === "Cep") {
            if (cepTimeout) {
                clearTimeout(cepTimeout);
            }
            setCepTimeout(setTimeout(() => handleCepChange(value), 500));
        }
    };

    const handleCepChange = async (cep: string) => {
        if (cep) {
            try {
                const response = await fetch(`https://viacep.com.br/ws/${cep}/json`);
                const cepData = await response.json();

                if (cepData) {
                    setFormData((prevData) => ({
                        ...prevData,
                        Estado: cepData.uf,
                        Cidade: cepData.localidade,
                        Bairro: cepData.bairro,
                        Logradouro: cepData.logradouro
                    }));
                }
            } catch (error) {
                console.error("Erro ao buscar dados de CEP", error);
            }
        }
    };

    // Envia os dados do formulario para o servidor
    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        try {
            const response = await fetch("http://localhost:5072/client", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(formData),
            });

            if (response.ok) {
                alert("Cliente registrado com sucesso!");
                setFormData({
                    Name: "",
                    Email: "",
                    Cpf: "",
                    Phone: "",
                    Cep: "",
                    Estado: "",
                    Cidade: "",
                    Bairro: "",
                    Logradouro: "",
                    Senha: ""
                });
            } else {
                alert("Erro ao registrar cliente.");
            }
        } catch (error) {
            console.error("Erro ao registrar cliente", error);
        }
    };

    return (
        <div>
            <form onSubmit={handleSubmit}>
                <h1 className="titulo-registro">Registro de Cliente</h1>
                <div className="input-container">
                    <div className="input-group">
                        <input
                            type="text"
                            name="Name"
                            placeholder="Nome"
                            value={formData.Name}
                            onChange={handleChange}
                            required
                        />
                        <input
                            type="email"
                            name="Email"
                            placeholder="Email"
                            value={formData.Email}
                            onChange={handleChange}
                            required
                        />
                    </div>

                    <div className="input-group">
                        <input
                            type="text"
                            name="Cpf"
                            placeholder="CPF"
                            value={formData.Cpf}
                            onChange={handleChange}
                            required
                        />
                        <input
                            type="text"
                            name="Phone"
                            placeholder="Telefone"
                            value={formData.Phone}
                            onChange={handleChange}
                            required
                        />
                    </div>

                    <div className="input-group">
                        <input
                            type="text"
                            name="Cep"
                            placeholder="Digite o CEP"
                            value={formData.Cep}
                            onChange={handleChange}
                            required
                        />
                        <input
                            type="text"
                            name="Estado"
                            placeholder="Estado"
                            value={formData.Estado}
                            onChange={handleChange}
                            readOnly
                        />
                    </div>

                    <div className="input-group">
                        <input
                            type="text"
                            name="Cidade"
                            placeholder="Cidade"
                            value={formData.Cidade}
                            onChange={handleChange}
                            readOnly
                        />
                        <input
                            type="text"
                            name="Bairro"
                            placeholder="Bairro"
                            value={formData.Bairro}
                            onChange={handleChange}
                            readOnly
                        />
                    </div>

                    <div className="input-group">
                        <input
                            type="text"
                            name="Logradouro"
                            placeholder="Logradouro"
                            value={formData.Logradouro}
                            onChange={handleChange}
                            readOnly
                        />
                        <input
                            type="password"
                            name="Senha"
                            placeholder="Senha"
                            value={formData.Senha}
                            onChange={handleChange}
                            required
                        />
                    </div>
                </div>
                <button type="submit">Registrar Cliente</button>
            </form>
        </div>
    );
}

export default Register;
