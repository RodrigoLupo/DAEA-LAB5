using LAB05_Lupo.Models;
using LAB05_Lupo.Repositories.Unit;
using Microsoft.AspNetCore.Mvc;

namespace LAB05_Lupo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AsistenciaController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public AsistenciaController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var escuelas = await _unitOfWork.GetRepository<Asistencia>().GetAll();
        return Ok(escuelas);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        var escuela = await _unitOfWork.GetRepository<Asistencia>().GetById(id);
        if (escuela == null)
            return NotFound();
        return Ok(escuela);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] Asistencia entity)
    {
        await _unitOfWork.GetRepository<Asistencia>().Add(entity);
        await _unitOfWork.Complete();
        return Ok("Escuela creada correctamente");
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] Asistencia entity)
    {
        var existing = await _unitOfWork.GetRepository<Asistencia>().GetById(id);
        if (existing == null)
            return NotFound();
        await _unitOfWork.GetRepository<Asistencia>().Update(entity);
        await _unitOfWork.Complete();
        return Ok("Escuela actualizada correctamente");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _unitOfWork.GetRepository<Asistencia>().Delete(id);
        await _unitOfWork.Complete();
        return Ok("Escuela eliminada correctamente");
    }
}
