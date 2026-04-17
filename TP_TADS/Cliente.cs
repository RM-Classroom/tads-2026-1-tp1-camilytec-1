using System;
using System.Collections.Generic;

public class Cliente
{
    public int Id { get; set; }

    public string Nome { get; set; }

    public string CPF { get; set; }

    public string Email { get; set; }

    public List<Aluguel> Alugueis { get; set; } = new();
}