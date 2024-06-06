using Microsoft.AspNetCore.Mvc;
using NotasAlunos.Data;
using NotasAlunos.Dtos;
using NotasAlunos.Entities;
using NotasAlunos.Service;

namespace NotasAlunos.Controllers;

[ApiController]
[Route("/api/notas")]
public class StudentGradesController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly StudentGradesService service = new StudentGradesService();

    public StudentGradesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<StudentGradesOutputDto>> SaveStudentGrades([FromBody] StudentGradesInputDto input)
    {
        StudentGradesOutputDto output = await service.SaveGrades(input, _context);

        return Created(nameof(SaveStudentGrades), output);
    }

    [HttpGet]
    public async Task<ActionResult<List<StudentGradesOutputDto>>> GetStudentsGrade(){
        List<StudentGradesOutputDto> students = await service.GetStudentsWithGrades(_context);

        return students;
    }

}
