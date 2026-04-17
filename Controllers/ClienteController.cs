using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly ApplicationContext _context;

    public ClienteController(ApplicationContext context)
    {
        _context = context;
    }

    // GET - listar todos clientes
    [HttpGet]
    public IActionResult Get()
    {
        var clientes = _context.Clientes.ToList();
        return Ok(clientes);
    }

    // GET por id
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var cliente = _context.Clientes
            .FirstOrDefault(c => c.Id == id);

        if (cliente == null)
            return NotFound();

        return Ok(cliente);
    }

    // POST
    [HttpPost]
    public IActionResult Post(Cliente cliente)
    {
        _context.Clientes.Add(cliente);
        _context.SaveChanges();
        return Ok(cliente);
    }

    // PUT
    [HttpPut("{id}")]
    public IActionResult Put(int id, Cliente clienteAtualizado)
    {
        var cliente = _context.Clientes.Find(id);

        if (cliente == null)
            return NotFound();

        cliente.Nome = clienteAtualizado.Nome;
        cliente.CPF = clienteAtualizado.CPF;
        cliente.Email = clienteAtualizado.Email;

        _context.SaveChanges();

        return Ok(cliente);
    }

    // DELETE
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var cliente = _context.Clientes.Find(id);

        if (cliente == null)
            return NotFound();

        _context.Clientes.Remove(cliente);
        _context.SaveChanges();

        return Ok("Cliente removido com sucesso");
    }

    // FILTRO (JOIN): clientes com alugueis
    [HttpGet("com-alugueis")]
    public IActionResult GetClientesComAlugueis()
    {
        var clientes = _context.Clientes
            .Include(c => c.Alugueis)
                .ThenInclude(a => a.Veiculo)
            .ToList();

        return Ok(clientes);
    }
}