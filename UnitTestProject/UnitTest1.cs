using System;
using System.Collections.Generic;
using FluentValidation.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sunrise.Energy.Computing;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Shouldgetwats()
        {
            CalculateWats calculateWats = new CalculateWats();
            int year = 2016;
            int month = 7;
            int day = 25;
            double latitude = 93;
            int hour = 8;
            //  latitude = Double.Parse(Console.ReadLine());
            calculateWats.setLatidue(latitude);
            // Console.WriteLine("Enter hour :");
            //  int hour = int.Parse(Console.ReadLine());
            DateTime date = new DateTime(year, month, day, hour, 0, 0);
            calculateWats.setData(date);


            ModelDTO modleDto = new ModelDTO();
            modleDto.Date = date;
            modleDto.Latidute = latitude;
            modleDto.Wats = calculateWats.getWats();

            var validator = new CalculateValidator();
            var validationresult = validator.Validate(modleDto);
            // bool validationSucceeded = validationresult.IsValid;
            // IList<ValidationFailure> failures = validationresult.Errors;
            foreach (var result in validationresult.Errors)
            {
                Console.WriteLine("Property name: " + result.PropertyName);
                Console.WriteLine("Error: " + result.ErrorMessage);
                Console.WriteLine("");
            }
        }
    }
}