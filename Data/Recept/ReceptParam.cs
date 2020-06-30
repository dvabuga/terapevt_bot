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
        public Guid ParamValueId { get; set; }
        public Guid ReceptRowId { get; set; }
        public DateTimeOffset? DateTimeCreate { get; set; }

    }
}
