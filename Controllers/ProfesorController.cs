using LAB05_Lupo.Models;
using LAB05_Lupo.Repositories.Unit;
using Microsoft.AspNetCore.Mvc;

namespace LAB05_Lupo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfesorController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public ProfesorController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var profesores = await _unitOfWork.GetRepository<Profesore>().GetAll();
        return Ok(profesores);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        var profesor = await _unitOfWork.GetRepository<Profesore>().GetById(id);
        if (profesor == null)
            return NotFound();
        return Ok(profesor);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] Profesore entity)
    {
        await _unitOfWork.GetRepository<Profesore>().Add(entity);
        await _unitOfWork.Complete();
        return Ok("Profesor creado correctamente");
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] Profesore entity)
    {
        var existing = await _unitOfWork.GetRepository<Profesore>().GetById(id);
        if (existing == null)
            return NotFound();
        await _unitOfWork.GetRepository<Profesore>().Update(entity);
        await _unitOfWork.Complete();
        return Ok("Profesor actualizado correctamente");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _unitOfWork.GetRepository<Profesore>().Delete(id);
        await _unitOfWork.Complete();
        return Ok("Profesor eliminado correctamente");
    }
}
