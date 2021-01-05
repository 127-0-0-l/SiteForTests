using System.Collections.Generic;

namespace Site.Models
{
    public class Question
    {
        public string QuestionText { get; set; }
        public int RightAnswerId { get; set; }
        public Dictionary<string, string> Answers { get; set; } = new Dictionary<string, string>();
    }
}