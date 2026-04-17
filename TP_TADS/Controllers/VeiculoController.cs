using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class VeiculoController : ControllerBase
{
    private readonly ApplicationContext _context;

    public VeiculoController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var veiculos = _context.Veiculos
            .Include(v => v.Fabricante)
            .ToList();

        return Ok(veiculos);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var veiculo = _context.Veiculos
            .Include(v => v.Fabricante)
            .FirstOrDefault(v => v.Id == id);

        if (veiculo == null)
            return NotFound();

        return Ok(veiculo);
    }

    [HttpPost]
    public IActionResult Post(Veiculo veiculo)
    {
        if (!_context.Fabricantes.Any(f => f.Id == veiculo.FabricanteId))
            return BadRequest("Fabricante inválido");

        _context.Veiculos.Add(veiculo);
        _context.SaveChanges();

        return Ok(veiculo);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Veiculo veiculoAtualizado)
    {
        var veiculo = _context.Veiculos.Find(id);

        if (veiculo == null)
            return NotFound();

        veiculo.Modelo = veiculoAtualizado.Modelo;
        veiculo.Ano = veiculoAtualizado.Ano;
        veiculo.Quilometragem = veiculoAtualizado.Quilometragem;
        veiculo.FabricanteId = veiculoAtualizado.FabricanteId;

        _context.SaveChanges();

        return Ok(veiculo);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var veiculo = _context.Veiculos.Find(id);

        if (veiculo == null)
            return NotFound();

        _context.Veiculos.Remove(veiculo);
        _context.SaveChanges();

        return Ok();
    }
    [HttpGet("filtro/fabricante/{fabricanteId}")]
    public IActionResult VeiculosPorFabricante(int fabricanteId)
    {
        var dados = _context.Veiculos
            .Include(v => v.Fabricante)
            .Where(v => v.FabricanteId == fabricanteId)
            .ToList();

        return Ok(dados);
    }
}