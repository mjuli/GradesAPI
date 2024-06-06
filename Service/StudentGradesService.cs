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

        internal async Task<List<StudentGradesOutputDto>> GetStudentsWithGrades(AppDbContext context)
        {
            List<Grade> grades = await context.Grades.ToListAsync();
            List<Subject> subjects = await context.Subjects.ToListAsync();

            Student students = context.Students
                                .Select(s => {
                                    StudentGradesOutputDto output = new StudentGradesOutputDto();
                                    output.StudentName = s.StudentName;

                                    List<Grade> studentGrades = grades
                                                                    .Where(grade => grade.IdStudent == s.IdStudent)
                                                                    .ToList();
                                    
                                    //SubjectGradesDto subjectGrades = new SubjectGradesDto();
                                   // grades.Find(s.IdStudent)
                                }).ToListAsync();
            
        }
    }
}