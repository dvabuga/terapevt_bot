using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class ParamValue
    {
        public Guid Id { get; set; }
        public Guid QuestionId { get; set; }
        public Guid ParamId { get; set; }
        public string Value { get; set; }
        public UnitType Unit { get; set; }
    }
}
