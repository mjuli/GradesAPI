using System.ComponentModel.DataAnnotations;

namespace NotasAlunos.Entities
{
    public class Subject
    {
        public Subject()
        {
        }

        public Subject(string name)
        {
            IdSubject = Guid.NewGuid();
            SubjectName = name;
            DataInclusao = DateTime.Now;
        }
        [Key]
        public Guid IdSubject { get; set; }
        public string SubjectName { get; set; }
        public DateTime DataInclusao { get; set; }
        public int IdSessaoOperacao { get; set; }
        public int IdSessao { get; set; }
    }
}