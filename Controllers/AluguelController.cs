using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class AluguelController : ControllerBase
{
    private readonly ApplicationContext _context;

    public AluguelController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var alugueis = _context.Alugueis
            .Include(a => a.Cliente)
            .Include(a => a.Veiculo)
            .ThenInclude(v => v.Fabricante)
            .ToList();

        return Ok(alugueis);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var aluguel = _context.Alugueis
            .Include(a => a.Cliente)
            .Include(a => a.Veiculo)
            .ThenInclude(v => v.Fabricante)
            .FirstOrDefault(a => a.Id == id);

        if (aluguel == null)
            return NotFound();

        return Ok(aluguel);
    }

    [HttpPost]
    public IActionResult Post(Aluguel aluguel)
    {
        if (!_context.Clientes.Any(c => c.Id == aluguel.ClienteId))
            return BadRequest("Cliente inválido");

        if (!_context.Veiculos.Any(v => v.Id == aluguel.VeiculoId))
            return BadRequest("Veículo inválido");

        _context.Alugueis.Add(aluguel);
        _context.SaveChanges();

        var result = _context.Alugueis
            .Include(a => a.Cliente)
            .Include(a => a.Veiculo)
            .ThenInclude(v => v.Fabricante)
            .FirstOrDefault(a => a.Id == aluguel.Id);

        return Ok(result);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Aluguel aluguelAtualizado)
    {
        var aluguel = _context.Alugueis.Find(id);

        if (aluguel == null)
            return NotFound();

        aluguel.ClienteId = aluguelAtualizado.ClienteId;
        aluguel.VeiculoId = aluguelAtualizado.VeiculoId;
        aluguel.DataInicio = aluguelAtualizado.DataInicio;
        aluguel.DataFim = aluguelAtualizado.DataFim;
        aluguel.DataDevolucao = aluguelAtualizado.DataDevolucao;
        aluguel.QuilometragemInicial = aluguelAtualizado.QuilometragemInicial;
        aluguel.QuilometragemFinal = aluguelAtualizado.QuilometragemFinal;
        aluguel.ValorDiaria = aluguelAtualizado.ValorDiaria;
        aluguel.ValorTotal = aluguelAtualizado.ValorTotal;

        _context.SaveChanges();

        return Ok(aluguel);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var aluguel = _context.Alugueis.Find(id);

        if (aluguel == null)
            return NotFound();

        _context.Alugueis.Remove(aluguel);
        _context.SaveChanges();

        return Ok();
    }

    [HttpGet("por-cliente/{clienteId}")]
    public IActionResult GetPorCliente(int clienteId)
    {
        var dados = _context.Alugueis
            .Include(a => a.Cliente)
            .Include(a => a.Veiculo)
            .Where(a => a.ClienteId == clienteId)
            .ToList();

        return Ok(dados);
    }

    [HttpGet("ativos")]
    public IActionResult GetAtivos()
    {
        var dados = _context.Alugueis
            .Include(a => a.Cliente)
            .Include(a => a.Veiculo)
            .Where(a => a.DataDevolucao == null)
            .ToList();

        return Ok(dados);
    }
    [HttpGet("filtro/cliente/{clienteId}")]
    public IActionResult AlugueisPorCliente(int clienteId)
    {
        var dados = _context.Alugueis
            .Include(a => a.Cliente)
            .Include(a => a.Veiculo)
            .Where(a => a.ClienteId == clienteId)
            .ToList();

        return Ok(dados);
    }
    [HttpGet("filtro/historico")]
    public IActionResult HistoricoCompleto()
    {
        var dados = _context.Alugueis
            .Include(a => a.Cliente)
            .Include(a => a.Veiculo)
                .ThenInclude(v => v.Fabricante)
            .ToList();

        return Ok(dados);
    }

}