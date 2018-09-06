﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareNews.Lib.Objects
{
    public class NewsApiSourceObject
    {
        public string Status { get; set; }
        public NewsApiSourceItem[] Sources { get; set; }

    }

    public class NewsApiSourceItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Category { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }

    }
}
