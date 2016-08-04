using System;
using System.Resources;
using Sunrise.Vector.Computing;

//http://www.pveducation.org/pvcdrom/suns-position-high-accuracy

namespace Sunrise.Vector
{
    public class ComputeVector
    {
        private const double Pi = 3.14159265358979323846;
        private const double Twopi = 2 * Pi;
        private const double Rad = Pi / 180;
        private const double DEarthMeanRadius = 6371.01;
        private const double DAstronomicalUnit = 149597890;
        double _dDecimalHours;
        double _udtSunCoordinatesdAzimuth;
        public static SunVector PrintSunVector = new SunVector();
        private ResourceManager _resourceManager = new ResourceManager("Sunrise.API.Resources.ExceptionMessages", System.Reflection.Assembly.GetExecutingAssembly());

        public SunVector SunPos(DateTime date, PositionCoordinate positionCoordinate)
        {
            double udtTimeiYear = date.Year;
            double udtTimeiMonth = date.Month;
            double udtTimeiDay = date.Day;
            double udtTimedHours = date.Hour;
            double udtTimedMinutes = date.Minute;
            double udtTimedSeconds = date.Second;
            double udtLocationdLatitude = positionCoordinate.UdtLocationdLatitude;
            double udtLocationdLongitude = positionCoordinate.UdtLocationdLongitude;

            _dDecimalHours = udtTimedHours + (udtTimedMinutes + udtTimedSeconds / 60) / 60;
            double liAux1 = Math.Truncate((udtTimeiMonth - 14) / 12);
            double liAux2 = Math.Truncate(1461 * (udtTimeiYear + 4800 + liAux1) / 4) + Math.Truncate(367 * (udtTimeiMonth - 2 - 12 * liAux1) / 12) - Math.Truncate(3 * Math.Truncate((udtTimeiYear + 4900 + liAux1) / 100) / 4) + udtTimeiDay - 32075;
            double dJulianDate = liAux2 - 0.5 + _dDecimalHours / 24;
            double dElapsedJulianDays = dJulianDate - 2451545.0;
            double dOmega = 2.1429 - 0.0010394594 * dElapsedJulianDays;
            double dMeanLongitude = 4.8950630 + 0.017202791698 * dElapsedJulianDays;
            double dMeanAnomaly = 6.2400600 + 0.0172019699 * dElapsedJulianDays;
            double dEclipticLongitude = dMeanLongitude + 0.03341607 * Math.Sin(dMeanAnomaly) + 0.00034894 * Math.Sin(2 * dMeanAnomaly) - 0.0001134 - 0.0000203 * Math.Sin(dOmega);
            double dEclipticObliquity = 0.4090928 - 6.2140e-9 * dElapsedJulianDays + 0.0000396 * Math.Cos(dOmega);
            double dSinEclipticLongitude = Math.Sin(dEclipticLongitude);
            double dY = Math.Cos(dEclipticObliquity) * dSinEclipticLongitude;
            double dX = Math.Cos(dEclipticLongitude);
            double dRightAscension = Math.Atan2(dY, dX);
            if (0.0 > dRightAscension)
            {
                dRightAscension += Twopi;
            }
            double dDeclination = Math.Asin(Math.Sin(dEclipticObliquity) * dSinEclipticLongitude);
            double dGreenwichMeanSiderealTime = 6.6974243242 + 0.0657098283 * dElapsedJulianDays + _dDecimalHours;
            double dLocalMeanSiderealTime = (15 * dGreenwichMeanSiderealTime + udtLocationdLongitude) * Rad;
            double dHourAngle = dLocalMeanSiderealTime - dRightAscension;
            double dLatitudeInRadians = udtLocationdLatitude * Rad;
            double dCos_Latitude = Math.Cos(dLatitudeInRadians);
            double dSin_Latitude = Math.Sin(dLatitudeInRadians);
            double dCos_HourAngle = Math.Cos(dHourAngle);
            double udtSunCoordinatesdZenithAngle = Math.Acos(dCos_Latitude * dCos_HourAngle * Math.Cos(dDeclination) + Math.Sin(dDeclination) * dSin_Latitude);
            dY = -Math.Sin(dHourAngle);
            dX = Math.Tan(dDeclination) * dCos_Latitude - dSin_Latitude * dCos_HourAngle;
            _udtSunCoordinatesdAzimuth = Math.Atan2(dY, dX);
            if (0.0 > _udtSunCoordinatesdAzimuth)
            {
                _udtSunCoordinatesdAzimuth += Twopi;
            }
            _udtSunCoordinatesdAzimuth /= Rad;
            double dParallax = DEarthMeanRadius / DAstronomicalUnit * Math.Sin(udtSunCoordinatesdZenithAngle);
            udtSunCoordinatesdZenithAngle = (udtSunCoordinatesdZenithAngle + dParallax) / Rad;


            PrintSunVector.AzimutDecimal = Convert.ToDecimal(Math.Round(_udtSunCoordinatesdAzimuth, 4));
            PrintSunVector.ZenithDecimal = Convert.ToDecimal(Math.Round(udtSunCoordinatesdZenithAngle, 4));
            PrintSunVector.ElevationDecimal = Convert.ToDecimal(Math.Round(90 - udtSunCoordinatesdZenithAngle, 4));


            return PrintSunVector;
        }


    }

    public class SunVector
    {
        public decimal AzimutDecimal { get; set; }
        public decimal ZenithDecimal { get; set; }
        public decimal ElevationDecimal { get; set; }


    }
}
