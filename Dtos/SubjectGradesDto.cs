namespace NotasAlunos.Dtos
{
    public class SubjectGradesDto
    {
        public SubjectGradesDto(string name, string grade)
        {
            SubjectName = name;
            SubjectGrade = grade;
        }

        public string SubjectName { get; set; }
        public string SubjectGrade { get; set; }
    }
}