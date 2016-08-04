using System;
using System.Web.Http;
using Sunrise.API.Models;
using Sunrise.API.Resources;
using Sunrise.API.Tools;
using Sunrise.Energy.Computing;

namespace Sunrise.API.Controllers
{
    /// <summary>
    /// Sunrise.Energy controller
    /// </summary>
    public class SunEnergyController : ApiController
    {
        /// <summary>
        /// Returns solar insulation
        /// </summary>
        /// <remarks>Return energy in Wats/m^2.</remarks>
        /// <param name="date">Date in ISO DateTime format "yyyy-MM-ddTHH:mm:sszzz" or "yyyy-MM-ddTHH:mm:ss" [Timezone name]. Ex1: 2016-08-01T08:11:12+02:00. Ex2: 2016-08-01T08:11:12 GMT Standard Time. Ex3: 2016-08-01T08:11:12Z ('Z' goes for +00:00). More info: 'https://www.w3.org/TR/NOTE-datetime'</param>
        /// <param name="latitude">Latitude in degrees. For South latitude add "-". Min=-90. Max=90</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request. Invalid parameters</response>
        public SunEnergyModel GetSunEnergy(string date, double latitude)
        {

            if (Math.Abs(latitude) > 90)
            {
                throw new ArgumentException(ExceptionMessages.LatitudeException);
            }
            DateFormat dateFormat = new DateFormat(date);

            SunEnergyModel energyModel = new SunEnergyModel();
            CalculateWats calculateWats = new CalculateWats();
            calculateWats.setData(dateFormat.GetUtcTime());
            calculateWats.setLatidue(latitude);
            energyModel.Wats = Math.Round(calculateWats.getWats(), 2);
            return energyModel;
        }
    }
}