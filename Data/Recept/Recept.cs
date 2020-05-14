using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data
{
    public class Recept
    {
        [Key]
        public Guid Id { get; set; }
        public string Template { get; set; }
    }
}
