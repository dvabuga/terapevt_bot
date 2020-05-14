using Data.Question;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data
{
    public class Question
    {

        [Key]
        public Guid Id { get; set; }
        public QuestionType Type { get; set; }
        public string Text { get; set; }
        public Guid QuestionTreeId { get; set; }
        public Guid ParametrId { get; set; }
        public Param Param { get; set; }
        public bool IsFirst { get; set; }
        public bool IsLast { get; set; }
        public Guid ScenarioId { get; set; }
        public QuestionScenarioType ScenarioType { get; set; }
        public ResponseType ResponseType { get; set; }

        [DataType("jsonb")]
        public string Scenario { get; set; }

        [DataType("jsonb")]
        public string Answers { get; set; }
    }
}
