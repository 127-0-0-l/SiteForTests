using System.Collections.Generic;

namespace Site.Models
{
    public class Question
    {
        public string QuestionText { get; set; }
        public int RightAnswerId { get; set; }
        public Dictionary<int, string> Answers { get; set; } = new Dictionary<int, string>();
    }
}