using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestSystem.Models
{
    public class Student:BaseEntity
    {
        public Student()//Связь один ко многим по отношению к классу СтудентыВКлассе (1 ученик много СтудентовВКлассе)
        {
            StudentsInClasses = new List<StudentInClass>();
        }

        public string Firstname { get; set; }

        public string Secondname { get; set; }

        public string Thirdname { get; set; }

        public DateTime BirthDay { get; set; }

        public string Address { get; set; }

        [JsonIgnore]
        public virtual ICollection<StudentInClass> StudentsInClasses { get; set; }
    }
}