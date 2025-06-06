﻿using LAB05_Lupo.Models;
using LAB05_Lupo.Repositories.Unit;
using LAB05_Lupo.Services;
using Microsoft.AspNetCore.Mvc;

namespace LAB05_Lupo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MatriculaController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly MatriculasService _service;
    public MatriculaController(IUnitOfWork unitOfWork, MatriculasService service)
    {
        _unitOfWork = unitOfWork;
        _service = service;
    }
    
    [HttpGet("{idMatricula}/profesores")]
    public async Task<IActionResult> GetProfesores(int idMatricula)
    {
        var profesores = await _service.GetProfesoresByMatriculaCursoAsync(idMatricula);
        return Ok(profesores);
    }

    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var matriculas = await _unitOfWork.GetRepository<Matricula>().GetAll();
        return Ok(matriculas);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        var matricula = await _unitOfWork.GetRepository<Matricula>().GetById(id);
        if (matricula == null)
            return NotFound();
        return Ok(matricula);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] Matricula entity)
    {
        await _unitOfWork.GetRepository<Matricula>().Add(entity);
        await _unitOfWork.Complete();
        return Ok("Matrícula creada correctamente");
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] Matricula entity)
    {
        var existing = await _unitOfWork.GetRepository<Matricula>().GetById(id);
        if (existing == null)
            return NotFound();
        await _unitOfWork.GetRepository<Matricula>().Update(entity);
        await _unitOfWork.Complete();
        return Ok("Matrícula actualizada correctamente");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _unitOfWork.GetRepository<Matricula>().Delete(id);
        await _unitOfWork.Complete();
        return Ok("Matrícula eliminada correctamente");
    }
}
