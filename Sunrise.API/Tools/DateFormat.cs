using System;
using System.Globalization;
using Sunrise.API.Resources;

namespace Sunrise.API.Tools
{
    public class DateFormat
    {
        private const string _formatDate = "yyyy-MM-ddTHH:mm:ss";
        private const string _formatTime = @"hh\:mm";
        private CultureInfo _cultureInfo;
        private DateTime _dateTime;
        private string _timezone;

        public DateFormat(string date)
        {
            try
            {
                _dateTime = DateTime.ParseExact(date.Substring(0, _formatDate.Length), _formatDate,
                    new CultureInfo("en-US"));
                _timezone = date.Substring(_formatDate.Length, date.Length - _formatDate.Length);
                _cultureInfo = new CultureInfo("en-GB");
            }
            catch (Exception)
            {
                throw new FormatException(ExceptionMessages.WrongDate);
            }
        }

        public DateTime GetUtcTime()
        {
            TimeSpan timezoneSpan;
            if (_timezone[0] == '-')
            {
                timezoneSpan = TimeSpan.ParseExact(_timezone.Substring(1, _timezone.Length - 1), _formatTime, _cultureInfo);
                timezoneSpan = timezoneSpan.Negate();
            }
            else if (_timezone[0] == '+')
            {
                timezoneSpan = TimeSpan.ParseExact(_timezone.Substring(1, _timezone.Length - 1), _formatTime, _cultureInfo);
            }
            else if (_timezone[0] == 'Z')
            {
                timezoneSpan = new TimeSpan(0, 0, 0);
            }
            else if (_timezone[0] == ' ')
            {
                return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(_dateTime, _timezone.Substring(1, _timezone.Length - 1), "UTC");
            }
            else
            {
                throw new FormatException(ExceptionMessages.TimezoneFormat);
            }

            if (timezoneSpan >= new TimeSpan(13, 0, 0))
            {
                throw new OverflowException();
            }

            if (timezoneSpan < (new TimeSpan(12, 0, 1).Negate()))
            {
                throw new OverflowException();
            }

            if ((timezoneSpan.Minutes != 0) && (timezoneSpan.Minutes != 30))
            {
                throw new FormatException(ExceptionMessages.TimezoneMinutes);
            }
            return _dateTime.Subtract(timezoneSpan);

        }
    }
}