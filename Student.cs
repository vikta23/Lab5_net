using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp
{
    // Абстрактний базовий клас
    public abstract class Student
    {
        private string fullName;
        private string faculty;
        private int course;

        protected List<int> grades = new List<int>();

        public string FullName
        {
            get { return fullName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("ПІБ не може бути порожнім.");
                fullName = value;
            }
        }

        public string Faculty
        {
            get { return faculty; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Факультет не може бути порожнім.");
                faculty = value;
            }
        }

        public int Course
        {
            get { return course; }
            set
            {
                if (value < 1 || value > 6)
                    throw new ArgumentException("Курс має бути в межах від 1 до 6.");
                course = value;
            }
        }

        public Student(string fullName, string faculty, int course)
        {
            FullName = fullName;
            Faculty = faculty;
            Course = course;
        }

        public void AddGrade(int grade)
        {
            if (grade < 0 || grade > 100)
                throw new ArgumentException("Оцінка має бути в межах від 0 до 100.");
            grades.Add(grade);
        }

        public double CalculateAverageGrade()
        {
            if (grades.Count == 0) return 0;
            return grades.Average();
        }

        public abstract double GetRating(); // абстрактний метод для нащадків
        public double Rating => GetRating();
        public double AverageGrade => CalculateAverageGrade();
        public string StudentType => this.GetType().Name;
        public string ScholarshipStatus
        {
            get
            {
                if (this is ICanReceiveScholarship eligible)
                    return eligible.IsEligibleForScholarship() ? "Так" : "Ні";
                return "Н/Д";
            }
        }

        public string ExchangeStatus
        {
            get
            {
                if (this is ICanParticipateInExchange exchangeStudent)
                    return exchangeStudent.IsCurrentlyAbroad() ? "Так" : "Ні";
                return "Н/Д";
            }
        }
    }


    
   
   
}

