using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestSystem.Models
{
    public class StudentInClass:BaseEntity
    {
        public bool Relevance { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndingDate { get; set; }

        public Class Class { get; set; }

        public Student Student { get; set; }

        public int? StudentId { get; set; }//Связь один ко многим по отношению к классу Ученик (1 ученик много СтудентовВКлассе)

        public int? ClassId { get; set; }//Связь один ко многим по отношению к классу Класс (1 ученик много СтудентовВКлассе)
    }
}