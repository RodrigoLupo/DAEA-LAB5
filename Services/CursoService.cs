using LAB05_Lupo.Models;
using LAB05_Lupo.Repositories.Unit;

namespace LAB05_Lupo.Services;

public class CursoService
{
    private readonly IUnitOfWork _unitOfWork;

    public CursoService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Estudiante>> ObtenerEstudiantesPorCurso(int idCurso)
    {
        var matriculas = await _unitOfWork.GetRepository<Matricula>()
            .GetAll();

        IEnumerable<int> idsEstudiantes = matriculas
            .Where(m => m.IdCurso == idCurso && m.IdEstudiante.HasValue)
            .Select(m => m.IdEstudiante.Value)
            .Distinct()
            .ToList();
        
        var estudiantes = await _unitOfWork.GetRepository<Estudiante>()
            .GetByIds(idsEstudiantes);

        return estudiantes;
    }
}