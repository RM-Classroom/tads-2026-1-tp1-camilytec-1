using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Fabricante
{
    public int Id { get; set; }

    public string Nome { get; set; }

    [JsonIgnore]
    public List<Veiculo> Veiculos { get; set; } = new List<Veiculo>();
}