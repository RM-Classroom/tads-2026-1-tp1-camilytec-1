using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class PagamentoController : ControllerBase
{
    private readonly ApplicationContext _context;

    public PagamentoController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var pagamentos = _context.Pagamentos
            .Include(p => p.Aluguel)
                .ThenInclude(a => a.Cliente)
            .Include(p => p.Aluguel)
                .ThenInclude(a => a.Veiculo)
            .ToList();

        return Ok(pagamentos);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var pagamento = _context.Pagamentos
            .Include(p => p.Aluguel)
                .ThenInclude(a => a.Cliente)
            .Include(p => p.Aluguel)
                .ThenInclude(a => a.Veiculo)
            .FirstOrDefault(p => p.Id == id);

        if (pagamento == null)
            return NotFound();

        return Ok(pagamento);
    }

    [HttpPost]
    public IActionResult Post(Pagamento pagamento)
    {
        if (!_context.Alugueis.Any(a => a.Id == pagamento.AluguelId))
            return BadRequest("Aluguel inválido");

        _context.Pagamentos.Add(pagamento);
        _context.SaveChanges();

        var result = _context.Pagamentos
            .Include(p => p.Aluguel)
                .ThenInclude(a => a.Cliente)
            .Include(p => p.Aluguel)
                .ThenInclude(a => a.Veiculo)
            .FirstOrDefault(p => p.Id == pagamento.Id);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var pagamento = _context.Pagamentos.Find(id);

        if (pagamento == null)
            return NotFound();

        _context.Pagamentos.Remove(pagamento);
        _context.SaveChanges();

        return Ok();
    }
    [HttpGet("filtro/cliente/{clienteId}")]
    public IActionResult PagamentosPorCliente(int clienteId)
    {
        var dados = _context.Pagamentos
            .Include(p => p.Aluguel)
                .ThenInclude(a => a.Cliente)
            .Where(p => p.Aluguel.ClienteId == clienteId)
            .ToList();

        return Ok(dados);
    }
}