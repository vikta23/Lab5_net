using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp
{
    public class ExchangeStudent : Student, IRatable, ICanReceiveScholarship, ICanParticipateInExchange
    {
        public string HomeCountry { get; private set; }
        public DateTime ExchangeEndDate { get; private set; }

        public ExchangeStudent(string fullName, string faculty, int course, string homeCountry, DateTime exchangeEndDate)
            : base(fullName, faculty, course)
        {
            HomeCountry = homeCountry;
            ExchangeEndDate = exchangeEndDate;
        }

        public override double GetRating()
        {
            return CalculateAverageGrade() + Course * 3;
        }

        public bool IsEligibleForScholarship()
        {
            return CalculateAverageGrade() >= 85;
        }

        public string GetHomeCountry() => HomeCountry;

        public DateTime GetExchangeEndDate() => ExchangeEndDate;

        public bool IsCurrentlyAbroad() => DateTime.Now < ExchangeEndDate;
    }


}
