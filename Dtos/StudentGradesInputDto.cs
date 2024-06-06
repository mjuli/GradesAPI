using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotasAlunos.Dtos
{
    public class StudentGradesInputDto
    {
        public string StudentName { get; set; }
        public string SubjectName { get; set; }
        public int Grade1 { get; set; }
        public int Grade2 { get; set; }
        public int Grade3 { get; set; }

    }
}