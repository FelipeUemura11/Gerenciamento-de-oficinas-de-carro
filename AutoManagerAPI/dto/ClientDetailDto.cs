namespace AutoManagerAPI.dto;

public class ClientDetailDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string? Cpf { get; set; }
    public string? Cep { get; set; }
    public string? Estado { get; set; }
    public string? Cidade { get; set; }
    public string? Bairro { get; set; }
    public string? Logradouro { get; set; }
    public string? Senha { get; set; }
    public List<CarDto> Cars { get; set; }
    public List<OrderDto> Orders { get; set; }
}