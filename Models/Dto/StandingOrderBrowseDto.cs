using System;
using System.Text.Json.Serialization;

namespace StandingOrders.API.Models
{
    public class StandingOrderBrowseDto
    {
        public int StandingOrderId { get; set; }
        public decimal Amount { get; set; }
        public string Name { get; set; }
        public string AccountNumber { get; set; }
        [JsonIgnore]
        public int IntervalId { get; set; }
        public string Interval { get; set; }
        [JsonIgnore]
        public int IntervalSpecification { get; set; }
        [JsonIgnore]
        public DateTime ValidFrom { get; set; }
        public DateTime NextRealizationDate { get; set; }
    

        public DateTime CalculateNextRealizationDate()
            {
                DateTime date;
                DateTime nextRealization;

                if (ValidFrom <= DateTime.Now)
                {
                    date = DateTime.Now.AddDays(1);
                }
                else
                {
                    date = ValidFrom;
                }

                var daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
                var nextMonthDate = date.AddMonths(1);
                var daysInNextMonth = DateTime.DaysInMonth(nextMonthDate.Year, nextMonthDate.Month);


                switch (IntervalId)
                {
                    case 1:
                        nextRealization = date;
                        break;
                    case 2:
                        if ((int)date.DayOfWeek <= IntervalSpecification)
                        {
                            nextRealization = date.AddDays((int)IntervalSpecification - (int)date.DayOfWeek);
                        }
                        else
                        {
                            nextRealization = date.AddDays(7 - (int)date.DayOfWeek + (int)IntervalSpecification);
                        }
                        break;
                    case 3:
                        if ((int)IntervalSpecification >= date.Day)
                        {
                            if (daysInMonth < (int)IntervalSpecification)
                            {
                                nextRealization = date.AddDays(daysInMonth - date.Day);
                            }
                            else
                            {
                                nextRealization = date.AddDays((int)IntervalSpecification - date.Day);
                            }
                        }
                        else
                        {
                            if (daysInNextMonth < (int)IntervalSpecification)
                            {
                                nextRealization = date.AddDays(daysInMonth - date.Day + daysInNextMonth);
                            }
                            else
                            {
                                nextRealization = date.AddDays(daysInMonth - date.Day + (int)IntervalSpecification);
                            }
                        }
                        break;
                    default:
                        nextRealization = date;
                        break;
                }

             TimeSpan ts = new TimeSpan(0, 0, 0);
                nextRealization = nextRealization.Date + ts;

             return nextRealization;
        }

    }
}
