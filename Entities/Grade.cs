using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotasAlunos.Entities
{
    public class Grade
    {
        public Grade()
        {
        }
        public Grade(int grade1, int grade2, int grade3, Guid idSubject, Guid idStudent)
        {
            IdGrade = Guid.NewGuid();
            Values = new List<int> { grade1, grade2, grade3 };
            Media = GetMedia();
            IdSubject = idSubject;
            IdStudent = idStudent;
            DataInclusao = DateTime.Now;
        }
        [Key]
        public Guid IdGrade { set; get; }
        [NotMapped]
        public List<int> Values { get; set; }
        public string Media { get; set; }
        public Guid IdSubject { get; set; }
        public Guid IdStudent { get; set; }
        public DateTime DataInclusao { get; set; }
        public int IdSessaoOperacao { get; set; }
        public int IdSessao { get; set; }

        public string GetMedia()
        {
            string result = "A";

            int sum = this.Values.Aggregate(0, (total, grade) => total + grade);
            double media = (double)sum / this.Values.Count;

            switch (media)
            {
                case var _ when media < 3:
                    result = "E";
                    break;
                case var _ when media < 5:
                    result = "D";
                    break;
                case var _ when media < 7:
                    result = "C";
                    break;
                case var _ when media < 9:
                    result = "B";
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}