using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class Param
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool HasUnit { get; set; }
        public ParamType Type { get; set; }

    }
}
