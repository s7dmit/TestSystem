using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestSystem.Models 
{
    public class School:BaseEntity
    {
        public School()
        {
            Classes = new List<Class>();//Связь один ко многим по отношению к классу Класс (1 школа много классов)
        }

        public string Name { get; set; }

        public string Address { get; set; }

        [JsonIgnore]

        public virtual ICollection<Class> Classes { get; set; }
    }
}