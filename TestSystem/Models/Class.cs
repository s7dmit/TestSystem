using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestSystem.Models
{
    public class Class:BaseEntity
    {
        public Class() //Связь один ко многим по отношению к классу Студенты в классе (1 класс много студентов в классе)
        {
            StudentsInClasses = new List<StudentInClass>();
        }

        public int Number { get; set; }

        public string Litera { get; set; }

        public int? SchoolId { get; set; }//Связь один ко многим по отношению к классу Школа (1 школа много классов)

        public School School { get; set; }

        [JsonIgnore]

        public virtual ICollection<StudentInClass> StudentsInClasses { get; set; }
    }
}