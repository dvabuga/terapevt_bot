using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data
{
    public class MedcinParam
    {
        [Key]
        public Guid Id { get; set; }
        public Guid MedcinId { get; set; }
        public Guid ParamsValueId { get; set; }
    }
}
