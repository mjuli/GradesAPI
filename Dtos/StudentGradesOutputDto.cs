namespace NotasAlunos.Dtos
{
    public class StudentGradesOutputDto
    {
        public StudentGradesOutputDto()
        {
        }

        public StudentGradesOutputDto(string name, List<SubjectGradesDto> grades)
        {
            StudentName = name;
            SubjectGrades = grades;
        }

        public string StudentName { get; set; }
        public List<SubjectGradesDto> SubjectGrades { get; set; }
    }
}