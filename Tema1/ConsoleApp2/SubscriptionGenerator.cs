using System;
using System.Collections.Generic;
using System.Globalization;

namespace ConsoleApp2
{
    public class SubscriptionGenerator
    {
        private List<Subscription> subscriptions;
        private readonly Random rnd = new Random();
        private NumberOfNulls maxNulls;
        private NumberOfNulls actualNulls;
        
        public List<Subscription> Generate(Configuration configuration, int noOfMessages)
        {
            var operators = new List<string> {"<", "<=", ">", ">=" };

            var maxEquals = 5;

            const string stringOperator = "=";
            
            CalculatePosibleNumbersOfNulls(configuration);

            subscriptions = new List<Subscription>();

            for (var i = 0; i < noOfMessages; i++)
            {
                var subscription = new Subscription
                {
                    Company = configuration.Companies[rnd.Next(configuration.Companies.Count)],
                    Value = new Option
                    {
                        Field = "value",
                        Op = maxEquals > 0 ? stringOperator : operators[rnd.Next(operators.Count)],
                        Value =
                        Random(configuration.MinValue, configuration.MaxValue).ToString()
                    },
                     Drop = new Option
                    {
                        Field = "drop",
                        Op = maxEquals > 0 ? stringOperator : operators[rnd.Next(operators.Count)],
                         Value = GenerateValueDoubleFromRange("Drop", 0, 10).ToString()
                    },
                     Variation = new Option
                    {
                        Field = "variation",
                        Op = maxEquals > 0 ? stringOperator : operators[rnd.Next(operators.Count)],
                        Value = Random(configuration.MinVariation, configuration.MaxVariation).ToString(CultureInfo.InvariantCulture)
                    },
                     Date = configuration.Date.ToString(CultureInfo.InvariantCulture)
                };

                subscriptions.Add(subscription);

                maxEquals--;
            }


            return subscriptions;
        }
        
        private double? GenerateValueDoubleFromRange(string propertyName, double min, double max)
        {
            var actualNullValue = (double)actualNulls.GetType().GetProperty(propertyName).GetValue(actualNulls);

            var maxNullValue = (double)maxNulls.GetType().GetProperty(propertyName).GetValue(maxNulls);

            if (actualNullValue >= maxNullValue)
                return RandomHeight(min, max);

            if (rnd.Next(0, 2) == 0)
                return RandomHeight(min, max);
            
            actualNulls.GetType().GetProperty(propertyName)?.SetValue(actualNulls, actualNullValue + 1);

            return null;
        }
        
        private void CalculatePosibleNumbersOfNulls(Configuration configuration)
        {
            maxNulls = new NumberOfNulls
            {
                Drop = 
                    configuration.NoOfMessages - configuration.NoOfMessages * configuration.DropFrequency / 100
            };
            actualNulls = new NumberOfNulls
            {
                Drop = 0
            };
        }
        
        private double RandomHeight(double minHearthRate, double maxHearthRate)
        {
            return Math.Round(rnd.NextDouble() * (maxHearthRate - minHearthRate) + minHearthRate, 2);
        }

        private int RandomHearthRate(int minHearthRate, int maxHearthRate)
        {
            return rnd.Next(minHearthRate, maxHearthRate);
        }
        
        private double Random(double min, double max)
        {
            return Math.Round(rnd.NextDouble() * (max - min) + min, 2);
        }

        private int Random(int min, int max)
        {
            return rnd.Next(min, max);
        }
    }
}