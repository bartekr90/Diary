using System;
using System.Data.Entity;
using System.Linq;

namespace Diary
{
    public class ApplicationDbContext : DbContext
    {      
        public ApplicationDbContext()
            : base("name=ApplicationDbContext")
        {

        }
               
    }
        
}