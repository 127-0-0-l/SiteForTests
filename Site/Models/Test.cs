﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.Models
{
    public class Test
    {
        public string TestName { get; set; }
        public List<Question> Questions { get; set; } = new List<Question>();
    }
}