using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public  class PublicationGenerator
    {
        private  List<Publication> publications;
        private  readonly Random rnd = new Random();


        public  List<Publication> Generate(PublicationConfiguration publicationConfiguration, int noOfMessages)
        {
            publications = new List<Publication>();
            for (var i = 0; i < noOfMessages; i++)
            {
                var publication = new Publication
                {
                    Company = publicationConfiguration.Companies[rnd.Next(publicationConfiguration.Companies.Count)],
                    Date = publicationConfiguration.Date,
                    Drop = Random(publicationConfiguration.MinDrop, publicationConfiguration.MaxDrop),
                    Variation = Random(publicationConfiguration.MinVariation, publicationConfiguration.MaxVariation),
                    Value = Random(publicationConfiguration.MinValue, publicationConfiguration.MaxValue)
                };


                publications.Add(publication);
            }


            return publications;
        }
    
        private  double Random(double min, double max)
        {
            return Math.Round(rnd.NextDouble() * (max - min) + min, 2);
        }

        private  int Random(int min, int max)
        {
            return rnd.Next(min, max);
        }

    }
}