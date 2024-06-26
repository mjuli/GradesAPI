using System.ComponentModel.DataAnnotations;

namespace NotasAlunos.Entities
{
    public class Student
    {
        public Student()
        {
        }

        public Student(string name)
        {
            IdStudent = Guid.NewGuid();
            StudentName = name;
            DataInclusao = DateTime.Now;
        }
        [Key]
        public Guid IdStudent { get; set; }
        public string StudentName { get; set; }
        public DateTime DataInclusao { get; set; }
        public int IdSessaoOperacao { get; set; }
        public int IdSessao { get; set; }
    }
}