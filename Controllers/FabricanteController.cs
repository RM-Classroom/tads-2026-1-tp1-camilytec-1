using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class FabricanteController : ControllerBase
{
    private readonly ApplicationContext _context;

    public FabricanteController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var fabricantes = _context.Fabricantes
            .Include(f => f.Veiculos)
            .ToList();

        return Ok(fabricantes);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var fabricante = _context.Fabricantes
            .Include(f => f.Veiculos)
            .FirstOrDefault(f => f.Id == id);

        if (fabricante == null)
            return NotFound();

        return Ok(fabricante);
    }

    [HttpPost]
    public IActionResult Post(Fabricante fabricante)
    {
        _context.Fabricantes.Add(fabricante);
        _context.SaveChanges();
        return Ok(fabricante);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var fabricante = _context.Fabricantes.Find(id);

        if (fabricante == null)
            return NotFound();

        _context.Fabricantes.Remove(fabricante);
        _context.SaveChanges();

        return Ok();
    }

    [HttpGet("com-veiculos")]
    public IActionResult FabricantesComVeiculos()
    {
        var dados = _context.Fabricantes
            .Include(f => f.Veiculos)
            .ToList();

        return Ok(dados);
    }
}