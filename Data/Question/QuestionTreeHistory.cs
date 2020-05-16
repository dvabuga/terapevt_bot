using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data
{
    public class QuestionTreeHistory
    {
        [Key]
        public Guid Id { get; set; }
        //public Guid QuestionTreeId { get; set; }
        public Guid QuestionId { get; set; }
        public Question Question { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public int UserId { get; set; }
        public Guid ScenarioId { get; set; }
        public Guid MedcinId { get; set; }
    }
}
