using System;
using Sunrise.Energy.Computing;

namespace Program1
{
    class Energy
    {
        static void Main(string[] args)
        {
            CalculateWats calculate = new CalculateWats();
            double latitude;   //szerokosć geograficzna
            int year = 2016;
            int month = 7;
            int day = 25;

            Console.WriteLine("Enter Latidue: ");
            latitude = Double.Parse(Console.ReadLine());
            calculate.setLatidue(latitude);

            Console.WriteLine("Enter hour :");
            int hour = int.Parse(Console.ReadLine());
            DateTime date = new DateTime(year, month, day,hour,0,0);
            calculate.setData(date);

            Console.WriteLine("Direct radiation (W/m2) : {0}", calculate.getWats());
            Console.ReadLine();

        }
    }
}
