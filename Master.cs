using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp
{

    public class Master : Student, IRatable, ICanReceiveScholarship
    {
        private string thesisTopic;
        private int publicationsCount;

        public string ThesisTopic
        {
            get { return thesisTopic; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Тема роботи не може бути порожньою.");
                thesisTopic = value;
            }
        }

        public int PublicationsCount
        {
            get { return publicationsCount; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Кількість публікацій не може бути від’ємною.");
                publicationsCount = value;
            }
        }

        public Master(string fullName, string faculty, int course, string thesisTopic, int publications)
            : base(fullName, faculty, course)
        {
            ThesisTopic = thesisTopic;
            PublicationsCount = publications;
        }

        public override double GetRating()
        {
            return Math.Round(PublicationsCount * 10 + CalculateAverageGrade());
        }

        public bool IsEligibleForScholarship()
        {
            return PublicationsCount >= 5 && CalculateAverageGrade() >= 80;
        }
    }
}
