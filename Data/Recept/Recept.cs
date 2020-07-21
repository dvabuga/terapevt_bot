﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data
{
    public class Recept
    {
        [Key]
        public Guid Id { get; set; }
        public bool? ByAge { get; set; }
        public bool? ByWeight { get; set; }
        public Guid? ReceptTemplateId { get; set; }

    }
}
