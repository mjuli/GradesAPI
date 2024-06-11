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
            Student? student = await context.Students.SingleOrDefaultAsync(s => input.StudentName == s.StudentName);
            Subject? subject = await context.Subjects.SingleOrDefaultAsync(s => s.SubjectName == input.SubjectName);
            Grade grade = null;

            if (student != null && subject != null){
                grade = await context.Grades.SingleOrDefaultAsync(g => g.IdStudent == student.IdStudent && g.IdSubject == subject.IdSubject);
                grade.Values = new List<int> { input.Grade1, input.Grade2, input.Grade3 };
                grade.Media = grade.GetMedia();
            } 
            
            if (student == null) {
                student = new Student(input.StudentName);
                await context.Students.AddAsync(student);
            } 
            
            if(subject == null) {
                subject = new Subject(input.SubjectName);
                await context.Subjects.AddAsync(subject);
            }

            if (grade == null){
                grade = new Grade(input.Grade1, input.Grade2, input.Grade3, subject.IdSubject, student.IdStudent);
                await context.Grades.AddAsync(grade);
            }

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