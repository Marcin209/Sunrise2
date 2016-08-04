using System;
using System.Web.Http;
using Sunrise.API.Models;
using Sunrise.API.Resources;
using Sunrise.API.Tools;
using Sunrise.Vector;
using Sunrise.Vector.Computing;

namespace Sunrise.API.Controllers
{
    /// <summary>
    /// Sunrise.Vector controller
    /// </summary>
    public class SunVectorController : ApiController
    {
        /// <summary>
        /// Returns vector of sun position
        /// </summary>
        /// <remarks>Returns Azimut, Zenith and Elevation in degrees</remarks>
        /// <param name="date">Date in ISO DateTime format "yyyy-MM-ddTHH:mm:sszzz" or "yyyy-MM-ddTHH:mm:ss" [Timezone name]. Ex1: 2016-08-01T08:11:12+02:00. Ex2: 2016-08-01T08:11:12 GMT Standard Time. Ex3: 2016-08-01T08:11:12Z ('Z' goes for +00:00). More info: 'https://www.w3.org/TR/NOTE-datetime'</param>
        /// <param name="latitude">Latitude in degrees. For South latitude add "-". Min=-90. Max=90</param>
        /// <param name="longitude">Longitude in degress. For West longitude add "-". Min=-180. Max=180</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request. Invalid parameters</response>
        public SunVectorModel GetSunVector(string date, double latitude, double longitude)
        {
            if (Math.Abs(latitude) > 90)
            {
                throw new ArgumentException(ExceptionMessages.LatitudeException);
            }

            if (Math.Abs(longitude) > 180)
            {
                throw new ArgumentException(ExceptionMessages.LongitudeException);
            }
            DateFormat dateFormat = new DateFormat(date);
            PositionCoordinate positionCoordinate = new PositionCoordinate()
            {
                UdtLocationdLatitude = latitude,
                UdtLocationdLongitude = longitude
            };
            ComputeVector computeVector = new ComputeVector();
            SunVector sunVector = computeVector.SunPos(dateFormat.GetUtcTime(), positionCoordinate);
            SunVectorModel vectorModel = new SunVectorModel();
            vectorModel.Azimut = sunVector.AzimutDecimal;
            vectorModel.Zenith = sunVector.ZenithDecimal;
            vectorModel.Elevation = sunVector.ElevationDecimal;
            return vectorModel;
        }
    }
}