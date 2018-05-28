using System.Collections.Generic;

namespace Terminator.BusinessLayer.Publications
{
    public class PublicationConfiguration
    {
        public List<string> Names { get; set; }= new List<string>()
        {
            "Iasi", "Piatra Neamt", "Bacau", "Roman", "Suceava", "Vaslui", "Husi", "Botosani"
        };

        public List<string> Countries { get; set; } = new List<string>()
        {
            "Romania", "Moldova"
        };

        public double TemperatureMin { get; set; } = -25;
        public double TemperatureMax { get; set; } = 45;

        public int PrecipitationMin { get; set; } = 0;
        public int PrecipitationMax { get; set; } = 100;

        public int PressureMin { get; set; } = 0;
        public int PressureMax { get; set; } = 2000;

        public double WindSpeedMin { get; set; } = 0;
        public double WindSpeedMax { get; set; } = 100;

        public double LatMin { get; set; } = -90;
        public double LatMax { get; set; } = 90;

        public double LongMin { get; set; } = -180;
        public double LongMax { get; set; } = 180;
    }
}
