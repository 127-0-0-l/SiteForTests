using System.Collections.Generic;

namespace Site.Models
{
    public class Test
    {
        public string TestName { get; set; }
        public List<Question> Questions { get; set; } = new List<Question>();
    }
}