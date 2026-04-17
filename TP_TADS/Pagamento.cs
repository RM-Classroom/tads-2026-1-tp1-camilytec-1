public class Pagamento
{
    public int Id { get; set; }

    public int AluguelId { get; set; }

    public Aluguel? Aluguel { get; set; }

    public decimal Valor { get; set; }

    public DateTime DataPagamento { get; set; }

    public string Metodo { get; set; }
}