using Microsoft.EntityFrameworkCore;
using NotasAlunos.Data;
using NotasAlunos.Dtos;
using NotasAlunos.Entities;

namespace NotasAlunos.Service
{
    public class StudentGradesService
    {

        public async Task<StudentGradesOutputDto> SaveGrades(StudentGradesInputDto input, AppDbContext context)
        {
            Student student = new Student(input.StudentName);
            Subject subject = new Subject(input.SubjectName);
            Grade grade = new Grade(input.Grade1, input.Grade2, input.Grade3, subject.IdSubject, student.IdStudent);

            await context.Students.AddAsync(student);
            await context.Subjects.AddAsync(subject);
            await context.Grades.AddAsync(grade);
            await context.SaveChangesAsync();

            SubjectGradesDto subjectGrades = new SubjectGradesDto(subject.SubjectName, grade.Media);
            List<SubjectGradesDto> subjectGradesList = new List<SubjectGradesDto> { subjectGrades };
            StudentGradesOutputDto output = new StudentGradesOutputDto(student.StudentName, subjectGradesList);

            return output;
        }

        public async Task<List<StudentGradesOutputDto>> GetStudentsWithGrades(AppDbContext context)
        {
            var resultado = await (from student in context.Students
                                   join grade in context.Grades on student.IdStudent equals grade.IdStudent
                                   join subject in context.Subjects on grade.IdSubject equals subject.IdSubject
                                   group new { Grade = grade, Subject = subject } by student.StudentName into g
                                   select new StudentGradesOutputDto(
                                       g.Key,
                                       g.Select(x => new SubjectGradesDto(x.Subject.SubjectName, x.Grade.Media)).ToList()
                                   )).ToListAsync();

            return resultado;
        }
    }
}