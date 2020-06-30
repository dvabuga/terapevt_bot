using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data
{
    public class ReceptRow
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ReceptId { get; set; }
        public string Text { get; set; }
        public DateTimeOffset? DateTimeCreate { get; set; }
    }
}
