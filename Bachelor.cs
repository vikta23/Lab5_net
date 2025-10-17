using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp
{
    public class Bachelor : Student, IRatable, ICanReceiveScholarship
    {
        private int creditsPerSemester;

        public int CreditsPerSemester
        {
            get { return creditsPerSemester; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Кредити мають бути більше за 0.");
                creditsPerSemester = value;
            }
        }

        public Bachelor(string fullName, string faculty, int course, int credits)
            : base(fullName, faculty, course)
        {
            CreditsPerSemester = credits;
        }

        public override double GetRating()
        {
            //рейтинг = середній бал * кредити / 30
            return (int)Math.Round(CalculateAverageGrade() * CreditsPerSemester / 30.0);
        }

        public bool IsEligibleForScholarship()
        {
            return CalculateAverageGrade() >= 80;
        }
    }
}
