using System;

namespace Azusa.WeatherApi.UnitySharedDomain.Premitives
{
    public struct DateOnly : IEquatable<DateOnly>
    {
        public static DateOnly Now => new DateOnly(DateTime.Now);
        
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public DayOfWeek DayOfWeek => ToDateTime().DayOfWeek;
        
        public DateOnly(int year, int month, int day)
        {
            Year = year;
            Month = month;
            Day = day;
        }

        public DateOnly(DateTime dateTime)
        {
            Year = dateTime.Year;
            Month = dateTime.Month;
            Day = dateTime.Day;
        }

        public static implicit operator DateOnly(DateTime dateTime) => new DateOnly(dateTime);
        
        public override bool Equals(object obj)
        {
            return obj is DateOnly other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Year;
                hashCode = (hashCode * 397) ^ Month;
                hashCode = (hashCode * 397) ^ Day;
                return hashCode;
            }
        }

        public DateTime ToDateTime() => new DateTime(Year, Month, Day);
        
        public bool Equals(DateOnly other)
        {
            return Year == other.Year && Month == other.Month && Day == other.Day;
        }

        public static bool operator ==(DateOnly lhs, DateOnly rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(DateOnly lhs, DateOnly rhs)
        {
            return !(lhs == rhs);
        }
    }
}