using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data
{
    public class QuestionTree
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
