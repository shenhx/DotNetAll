using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ODataWebApiDemo.Models
{
    [Table("Persons")]
    public class Person
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Gender { get; set; }
        public string UserName { get; set; }
    }
}