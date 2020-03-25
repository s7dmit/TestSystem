using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TestSystem.Models
{
    public class HistoryContext : DbContext
    {
        public DbSet<Class> Classes { get; set; }

        public DbSet<School> Schools { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<StudentInClass> StudentsInClasses { get; set; }

    }
    
}