namespace Sunrise.API.Models
{
    /// <summary>
    /// Sunrise.Vector model
    /// </summary>
    public class SunVectorModel
    {
        /// <summary>
        /// Azimut angle [Degree]
        /// </summary>
        public decimal Azimut { get; set; }
        /// <summary>
        /// Zenith angle [Degree]
        /// </summary>        
        public decimal Zenith { get; set; }
        /// <summary>
        /// Elevation angle [Degree]
        /// </summary>        
        public decimal Elevation { get; set; }
    }
}