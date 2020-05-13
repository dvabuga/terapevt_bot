using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class Question
    {
        public Guid Id { get; set; }
        public QuestionType Type { get; set; }
        public string Text { get; set; }
        public Guid QuestionTreeId { get; set; }
        public Guid ParametrId { get; set; }
        public Param Param { get; set; }
        public bool IsFirst { get; set; }
        public bool IsLast { get; set; }
        public Guid ScenarioId { get; set; }
        public string Answers { get; set; }
    }
}
