using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp
{
    public static class StudentHelper
    {
        public static Student GetTopStudent(List<Student> students)
        {
            if (students == null || students.Count == 0)
                return null;

            return students.OrderByDescending(s => s.CalculateAverageGrade()).First();
        }
    }
}
