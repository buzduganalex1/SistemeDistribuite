using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class Configuration
    {
        public int NoOfMessages = 10;

        public List<string> Companies { get; set; } = new List<string>()
        {
            "Google", "Apple"
        };
        public double MinValue { get; set; } = 0.0;

        public double MaxValue { get; set; } = 100.0;

        public DateTime Date { get; set; } = DateTime.Parse("2.02.2012");

        public double MinDrop { get; set; } = 0.0;

        public double MaxDrop { get; set; } = 10.0;

        public double MinVariation { get; set; } = 0.0;

        public double MaxVariation { get; set; } = 1.0;

        public double DropFrequency { get; set; } = 4;
    }
}