﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data
{
    public class Scenario
    {
        [Key]
        public Guid Id { get; set; }
        //public Guid QuestionTreeId { get; set; }
        public ScenarioType Type { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public bool Finished { get; set; }
        public int UserId { get; set; }
    }
}
