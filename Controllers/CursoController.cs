using LAB05_Lupo.Models;
using LAB05_Lupo.Repositories.Unit;
using LAB05_Lupo.Services;
using Microsoft.AspNetCore.Mvc;

namespace LAB05_Lupo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CursoController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly CursoService _service;
    public CursoController(IUnitOfWork unitOfWork, CursoService service)
    {
        _unitOfWork = unitOfWork;
        _service = service;
    }
    [HttpGet("{idCurso}/estudiantes")]
    public async Task<IActionResult> GetEstudiantes(int idCurso)
    {
        var estudiantes = await _service.ObtenerEstudiantesPorCurso(idCurso);
        return Ok(estudiantes);
    }
    
    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var cursos = await _unitOfWork.GetRepository<Curso>().GetAll();
        return Ok(cursos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        var curso = await _unitOfWork.GetRepository<Curso>().GetById(id);
        if (curso == null)
            return NotFound();
        return Ok(curso);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] Curso entity)
    {
        await _unitOfWork.GetRepository<Curso>().Add(entity);
        await _unitOfWork.Complete();
        return Ok("Curso creado correctamente");
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] Curso entity)
    {
        var existing = await _unitOfWork.GetRepository<Curso>().GetById(id);
        if (existing == null)
            return NotFound();
        await _unitOfWork.GetRepository<Curso>().Update(entity);
        await _unitOfWork.Complete();
        return Ok("Curso actualizado correctamente");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _unitOfWork.GetRepository<Curso>().Delete(id);
        await _unitOfWork.Complete();
        return Ok("Curso eliminado correctamente");
    }
}
