using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sunrise.Energy.Computing;

namespace Sunrise.Energy.Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            CalculateWats calculateWats = new CalculateWats();
            int year = 2016;
            int month = 7;
            int day = 25;
            double latitude = 93;
            int hour = 8;
            DateTime data2;
            calculateWats.setLatidue(latitude);

            DateTime date = new DateTime(year, month, day, hour, 0, 0);
            calculateWats.setData(date);
       
            

                        ModelDTO modleDto = new ModelDTO();
                       // modleDto.Date = date;
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
            Console.WriteLine("Enter hour :");
            Console.Read();

        }
    }
}
