using System;
using System.Collections.Generic;

namespace Terminator.BusinessLayer.Publications
{
    public  class PublicationGenerator
    {
        private  List<Publication> _publications;
        private  readonly Random _rnd = new Random();


        public  List<Publication> Generate(PublicationConfiguration publicationConfiguration, int noOfMessages)
        {
            _publications = new List<Publication>();
            for (var i = 0; i < noOfMessages; i++)
            {
                var publication = new Publication
                {
                    Name = publicationConfiguration.Names[_rnd.Next(publicationConfiguration.Names.Count)],
                    Country = publicationConfiguration.Names[_rnd.Next(publicationConfiguration.Countries.Count)],
                    Temperature = Random(publicationConfiguration.TemperatureMin, publicationConfiguration.TemperatureMax),
                    Precipitation = Random(publicationConfiguration.PrecipitationMin, publicationConfiguration.PrecipitationMax),
                    Pressure = Random(publicationConfiguration.PressureMin, publicationConfiguration.PressureMax),
                    WindSpeed = Random(publicationConfiguration.WindSpeedMin, publicationConfiguration.WindSpeedMax),
                    Latitude = Random(publicationConfiguration.LatMin, publicationConfiguration.LatMax),
                    Longitude = Random(publicationConfiguration.LongMin, publicationConfiguration.LongMax),
                };


                _publications.Add(publication);
            }


            return _publications;
        }
    
        private  double Random(double min, double max)
        {
            return Math.Round(_rnd.NextDouble() * (max - min) + min, 2);
        }

        private  int Random(int min, int max)
        {
            return _rnd.Next(min, max);
        }

    }
}