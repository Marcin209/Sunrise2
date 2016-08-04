using System;
using System.Globalization;
using System.Resources;

namespace Sunrise.Energy.Computing
{
    public class CalculateWats
    {
        private double _latitude;
        private DateTime _dateTime;
        
        public void setData(DateTime date)
        {
            _dateTime = date;
        }

        public void setLatidue(double latitude)
        {
            _latitude = latitude;
        }

        private double Declination(double c)
        {
            return 23.45 * Math.Sin(ToRad(360.0 / 365.0 * (c - 81)));
        }

        private double ToRad(double c)
        {
            return c * Math.PI / 180;

        }

        private double ToDeg(double c)
        {
            return 180 * c / Math.PI;
        }

        private double AM(double c, double e, double f)
        {
            double dec = ToRad(Declination(e));
            double HRA = ToRad(15 * (c - 12));
            double elevation = Math.Asin(Math.Sin(dec) * Math.Sin(f) + Math.Cos(dec) * Math.Cos(f) * Math.Cos(HRA));
            double declination = ToRad(90) - elevation;
            return 1.0 / (0.0001 + Math.Cos(declination));
        }

        private double sunrise(int jjday, double lat) // calculates the time of sunrise
        {
            double dec = ToRad(Declination(jjday));
            lat = ToRad(lat);
            double x = -(Math.Sin(lat) * Math.Sin(dec));
            x = x / (Math.Cos(lat) * Math.Cos(dec));
            if (x > 1.0)
                x = 1.0;
            if (x < -1.0)
                x = -1.0;
            double f = Math.Acos(x);
            double H = ToDeg(f * 1 / 15.0);
            
            return 12.0 - H;
        }

        private double sunset(int jday, double lat) //calculates the time of sunset
        {
            double dec = ToRad(Declination(jday));

            lat = ToRad(lat);
            double x = -(Math.Sin(lat) * Math.Sin(dec));
            x = x / (Math.Cos(lat) * Math.Cos(dec));
            if (x > 1.0)
                x = 1.0;
            if (x < -1.0)
                x = -1.0;
            double f = Math.Acos(x);
            double H = ToDeg(f * 1.0 / 15.0);
            return 12.0 + H;
        }

        public double getWats()
        {
            double dec = ToRad(Declination(_dateTime.DayOfYear));
            double lat = ToRad(_latitude);
            double x = -(Math.Sin(lat) * Math.Sin(dec));
            x = x / (Math.Cos(lat) * Math.Cos(dec));
            if (x > 1.0)
                x = 1.0;
            if (x < -1.0)
                x = -1.0;
            double f = Math.Acos(x);
            double H = ToDeg(f * 1.0 / 15.0);

            double Stot = 0.0;
            double start = sunrise(_dateTime.DayOfYear, lat);
            double stop = sunset(_dateTime.DayOfYear, lat);
            if (_dateTime.Hour < start)
                return 0.0;
            if (_dateTime.Hour > stop)
                return 0.0;
            double am = AM(_dateTime.Hour, _dateTime.DayOfYear, lat);
            double x1 = Math.Pow(0.7, am);
            Stot = Stot + 1.353 * Math.Pow(x1, 0.678);
            return Stot;
        }
    }
}
