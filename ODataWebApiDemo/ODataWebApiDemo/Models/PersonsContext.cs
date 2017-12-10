using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ODataWebApiDemo.Models
{
    public class PersonsContext : DbContext
    {
        public PersonsContext()
            : base("name=PersonsContext")
        {
        }

        public DbSet<Person> Persons { get; set; }
    }
}