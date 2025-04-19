using LAB05_Lupo.Models;
using LAB05_Lupo.Repositories.Unit;
using Microsoft.AspNetCore.Mvc;

namespace LAB05_Lupo.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class EstudianteController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public EstudianteController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult> GetAsistencias()
    {
        var estudiantes = await _unitOfWork.GetRepository<Estudiante>().GetAll();
        return Ok(estudiantes);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] Estudiante entity)
    {
        await _unitOfWork.GetRepository<Estudiante>().Add(entity);
        await _unitOfWork.Complete();
        return Ok("Estudiante creado correctamente");
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] Estudiante entity)
    {
        var estudiante = await _unitOfWork.GetRepository<Estudiante>().GetById(id);
        if (estudiante == null)
            return NotFound("Estudiante no encontrado");

        await _unitOfWork.GetRepository<Estudiante>().Update(entity);
        await _unitOfWork.Complete();
        return Ok("Estudiante actualizado correctamente");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _unitOfWork.GetRepository<Estudiante>().Delete(id);
        await _unitOfWork.Complete();
        return Ok("Estudiante eliminado correctamente");
    }
}
