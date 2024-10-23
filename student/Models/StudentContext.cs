using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace student.Models
{
    public class StudentContext : DbContext
    {
        public DbSet<studentsmodel> Students { get; set; }
    }
    
}