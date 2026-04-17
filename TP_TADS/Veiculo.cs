using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;

public class Veiculo
{
    public int Id { get; set; }

    public string Modelo { get; set; }

    public int Ano { get; set; }

    public double Quilometragem { get; set; }

    public int FabricanteId { get; set; }


    public Fabricante? Fabricante { get; set; }

    public List<Aluguel> Alugueis { get; set; } = new List<Aluguel>();
}