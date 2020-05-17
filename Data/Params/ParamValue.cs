using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Data
{
    public class ParamValue
    {
        [Key]
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public Guid ParamId { get; set; }
        public string Value { get; set; }
        public string Unit { get; set; }
        public Guid MedcinId { get; set; }
    }
}
