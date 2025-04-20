using LAB05_Lupo.Models;
using LAB05_Lupo.Repositories.Unit;
using Microsoft.AspNetCore.Mvc;

namespace LAB05_Lupo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MateriaController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public MateriaController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var materias = await _unitOfWork.GetRepository<Materia>().GetAll();
        return Ok(materias);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        var materia = await _unitOfWork.GetRepository<Materia>().GetById(id);
        if (materia == null)
            return NotFound();
        return Ok(materia);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] Materia entity)
    {
        await _unitOfWork.GetRepository<Materia>().Add(entity);
        await _unitOfWork.Complete();
        return Ok("Materia creada correctamente");
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] Materia entity)
    {
        var existing = await _unitOfWork.GetRepository<Materia>().GetById(id);
        if (existing == null)
            return NotFound();
        await _unitOfWork.GetRepository<Materia>().Update(entity);
        await _unitOfWork.Complete();
        return Ok("Materia actualizada correctamente");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _unitOfWork.GetRepository<Materia>().Delete(id);
        await _unitOfWork.Complete();
        return Ok("Materia eliminada correctamente");
    }
}
