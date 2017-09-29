using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;


namespace SqliteApplication
{
    /// <summary>
    /// Person实体
    /// </summary>
    [Table("p_person")]
    public class PersonEntity
    {
        [Key]
        [Display(Name = "ID")]
        [Required(ErrorMessage = "ID为必选项")]
        [Column("fid")]
        public string Id { get; set; }

        [Display(Name = "名字")]
        [Column("fname")]
        public string Name { get; set; }

        [Display(Name = "年龄")]
        [Column("fage")]
        public int Age { get; set; }

        [Display(Name = "性别")]
        [Column("fsex")]
        public string Sex { get; set; }
    }
}
