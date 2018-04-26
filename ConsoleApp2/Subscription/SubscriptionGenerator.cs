using System;
using System.Collections.Generic;

namespace Terminator.BusinessLayer.Subscription
{
    public class SubscriptionGenerator
    {
        private List<Subscription> _subscriptions;
        private readonly Random _rnd = new Random();
        private NumberOfNulls _maxNulls;
        private NumberOfNulls _actualNulls;


        public List<Subscription> Generate(SubscriptionConfiguration subscriptionConfiguration, int noOfMessages)
        {

            var operators = new List<string> { "=", "<", "<=", ">", ">=" };
            const string stringOperator = "=";


            CalculatePosibleNumbersOfNulls(subscriptionConfiguration);
            _subscriptions = new List<Subscription>();
            for (var i = 0; i < noOfMessages; i++)
            {
                var subscription = new Subscription
                {
                    Name = new Option
                    {
                        Field = "name",
                        Op = stringOperator,
                        Value = subscriptionConfiguration.Names[_rnd.Next(subscriptionConfiguration.Names.Count)]
                    },
                    Country = new Option
                    {
                        Field = "country",
                        Op = stringOperator,
                        Value = subscriptionConfiguration.Names[_rnd.Next(subscriptionConfiguration.Countries.Count)]
                    },
                    Temperature = new Option
                    {
                        Field = "temperature",
                        Op = operators[_rnd.Next(operators.Count)],
                        Value =
                        Random(subscriptionConfiguration.TemperatureMin, subscriptionConfiguration.TemperatureMax).ToString()
                    },
                    Latitude = new Option
                    {
                        Field = "latitude",
                        Op = operators[_rnd.Next(operators.Count)],
                        Value = Random(subscriptionConfiguration.LatMin, subscriptionConfiguration.LatMax).ToString()
                    },
                    Longitude = new Option
                    {
                        Field = "longitude",
                        Op = operators[_rnd.Next(operators.Count)],
                        Value = Random(subscriptionConfiguration.LongMin, subscriptionConfiguration.LongMax).ToString()
                    },
                    Precipitation = new Option
                    {
                        Field = "precipitation",
                        Op = operators[_rnd.Next(operators.Count)],
                        Value =
                        GenerateValueDoubleFromRange("Precipitation", subscriptionConfiguration.PrecipitationMin,
                            subscriptionConfiguration.PrecipitationMax).ToString()
                    },
                    WindSpeed = new Option
                    {
                        Field = "windspeed",
                        Op = operators[_rnd.Next(operators.Count)],
                        Value =
                        GenerateValueDoubleFromRange("WindSpeed", subscriptionConfiguration.WindSpeedMin,
                            subscriptionConfiguration.WindSpeedMax).ToString()
                    },
                    Pressure = new Option
                    {
                        Field = "pressure",
                        Op = operators[_rnd.Next(operators.Count)],
                        Value =
                        GenerateValueIntFromRange("Pressure", subscriptionConfiguration.PressureMin,
                            subscriptionConfiguration.PressureMax).ToString()
                    },
                };

                _subscriptions.Add(subscription);
            }


            return _subscriptions;
        }


        private int? GenerateValueIntFromRange(string propertyName, int min, int max)
        {
            var actualNullValue = (int)_actualNulls.GetType().GetProperty(propertyName).GetValue(_actualNulls);
            var maxNullValue = (int)_maxNulls.GetType().GetProperty(propertyName).GetValue(_maxNulls);

            if (actualNullValue >= maxNullValue)
                return RandomHearthRate(min, max);

            if (_rnd.Next(0, 2) == 0)
                return RandomHearthRate(min, max);

            _actualNulls.GetType().GetProperty(propertyName).SetValue(_actualNulls, actualNullValue + 1);
            return null;
        }

        private double? GenerateValueDoubleFromRange(string propertyName, double min, double max)
        {
            var actualNullValue = (int)_actualNulls.GetType().GetProperty(propertyName).GetValue(_actualNulls);
            var maxNullValue = (int)_maxNulls.GetType().GetProperty(propertyName).GetValue(_maxNulls);

            if (actualNullValue >= maxNullValue)
                return RandomHeight(min, max);

            if (_rnd.Next(0, 2) == 0)
                return RandomHeight(min, max);

            _actualNulls.GetType().GetProperty(propertyName).SetValue(_actualNulls, actualNullValue + 1);
            return null;
        }


        private void CalculatePosibleNumbersOfNulls(SubscriptionConfiguration subscriptionConfiguration)
        {
            _maxNulls = new NumberOfNulls
            {
                Precipitation =
                    subscriptionConfiguration.NoOfMessages -
                    subscriptionConfiguration.NoOfMessages * subscriptionConfiguration.PrecipitationFrequency / 100,
                Pressure =
                    subscriptionConfiguration.NoOfMessages -
                    subscriptionConfiguration.NoOfMessages * subscriptionConfiguration.PresureFrequency / 100,
                WindSpeed =
                    subscriptionConfiguration.NoOfMessages -
                    subscriptionConfiguration.NoOfMessages * subscriptionConfiguration.WindSpeedFrequency / 100

            };
            _actualNulls = new NumberOfNulls
            {
                Precipitation = 0,
                Pressure = 0,
                WindSpeed = 0
            };
        }


        private double RandomHeight(double minHearthRate, double maxHearthRate)
        {
            return Math.Round(_rnd.NextDouble() * (maxHearthRate - minHearthRate) + minHearthRate, 2);
        }

        private int RandomHearthRate(int minHearthRate, int maxHearthRate)
        {
            return _rnd.Next(minHearthRate, maxHearthRate);
        }



        private double Random(double min, double max)
        {
            return Math.Round(_rnd.NextDouble() * (max - min) + min, 2);
        }

        private int Random(int min, int max)
        {
            return _rnd.Next(min, max);
        }
    }
}