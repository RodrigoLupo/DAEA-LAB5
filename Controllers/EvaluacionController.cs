using LAB05_Lupo.Models;
using LAB05_Lupo.Repositories.Unit;
using Microsoft.AspNetCore.Mvc;

namespace LAB05_Lupo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EvaluacionController:ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public EvaluacionController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var evaluaciones = await _unitOfWork.GetRepository<Evaluacione>().GetAll();
        return Ok(evaluaciones);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        var evaluacion = await _unitOfWork.GetRepository<Evaluacione>().GetById(id);
        if (evaluacion == null)
            return NotFound();
        return Ok(evaluacion);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] Evaluacione entity)
    {
        await _unitOfWork.GetRepository<Evaluacione>().Add(entity);
        await _unitOfWork.Complete();
        return Ok("Evaluación creada correctamente");
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] Evaluacione entity)
    {
        var existing = await _unitOfWork.GetRepository<Evaluacione>().GetById(id);
        if (existing == null)
            return NotFound();
        await _unitOfWork.GetRepository<Evaluacione>().Update(entity);
        await _unitOfWork.Complete();
        return Ok("Evaluación actualizada correctamente");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _unitOfWork.GetRepository<Evaluacione>().Delete(id);
        await _unitOfWork.Complete();
        return Ok("Evaluación eliminada correctamente");
    }
}