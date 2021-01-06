using System.Collections.Generic;

namespace Site.Models
{
    public class Question
    {
        public string QuestionText { get; set; }
        public int RightAnswerId { get; set; }
        public List<Answer> Answers { get; set; } = new List<Answer>();
    }
}