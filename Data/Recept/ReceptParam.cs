using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data
{
    public class ReceptParam
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ReceptId { get; set; }
        public Guid ParamId { get; set; }
        public Guid ReceptRowId { get; set; }

    }
}
