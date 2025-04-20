using LAB05_Lupo.Models;
using LAB05_Lupo.Repositories.Unit;
using Microsoft.EntityFrameworkCore;

namespace LAB05_Lupo.Services;

public class MatriculasService
{
    private readonly IUnitOfWork _unitOfWork;

    public MatriculasService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Profesore>> GetProfesoresByMatriculaCursoAsync(int idEstudiante)
    {
        var matriculas = await _unitOfWork.GetRepository<Matricula>().GetAll();
        IEnumerable<int> idsCursos = matriculas
            .Where(m => m.IdEstudiante == idEstudiante && m.IdCurso.HasValue)
            .Select(m => m.IdCurso.Value)
            .Distinct();
        var cursos = await _unitOfWork.GetRepository<Curso>()
            .GetByIds(idsCursos, include: q => q.Include(c => c.IdProfesors));
        var profesores = cursos
            .SelectMany(c => c.IdProfesors)
            .Distinct()
            .ToList();
        return profesores;

    }
}