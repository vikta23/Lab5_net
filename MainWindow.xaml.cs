using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace StudentApp
{
    public partial class MainWindow : Window
    {
        private List<Student> students = new List<Student>();

        public MainWindow()
        {
            InitializeComponent();
            RefreshStudentTable();
        }

        private void StudentTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CreditsTextBox == null) return;

            var selected = (StudentTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (selected == "Bachelor")
            {
                CreditsLabel.Visibility = Visibility.Visible;
                CreditsTextBox.Visibility = Visibility.Visible;

                ThesisTopicLabel.Visibility = Visibility.Collapsed;
                ThesisTopicTextBox.Visibility = Visibility.Collapsed;

                PublicationsLabel.Visibility = Visibility.Collapsed;
                PublicationsTextBox.Visibility = Visibility.Collapsed;

                HomeCountryLabel.Visibility = Visibility.Collapsed;
                HomeCountryTextBox.Visibility = Visibility.Collapsed;
                ExchangeEndDateLabel.Visibility = Visibility.Collapsed;
                ExchangeEndDateTextBox.Visibility = Visibility.Collapsed;
            }
            else if (selected == "Master")
            {
                CreditsLabel.Visibility = Visibility.Collapsed;
                CreditsTextBox.Visibility = Visibility.Collapsed;

                ThesisTopicLabel.Visibility = Visibility.Visible;
                ThesisTopicTextBox.Visibility = Visibility.Visible;
                PublicationsLabel.Visibility = Visibility.Visible;
                PublicationsTextBox.Visibility = Visibility.Visible;

                HomeCountryLabel.Visibility = Visibility.Collapsed;
                HomeCountryTextBox.Visibility = Visibility.Collapsed;
                ExchangeEndDateLabel.Visibility = Visibility.Collapsed;
                ExchangeEndDateTextBox.Visibility = Visibility.Collapsed;
            }
            else if (selected == "ExchangeStudent")
            {
                CreditsTextBox.Visibility = Visibility.Collapsed;
                CreditsLabel.Visibility = Visibility.Collapsed;

                ThesisTopicTextBox.Visibility = Visibility.Collapsed;
                ThesisTopicLabel.Visibility = Visibility.Collapsed;
                PublicationsTextBox.Visibility = Visibility.Collapsed;
                PublicationsLabel.Visibility = Visibility.Collapsed;

                HomeCountryLabel.Visibility = Visibility.Visible;
                HomeCountryTextBox.Visibility = Visibility.Visible;
                ExchangeEndDateLabel.Visibility = Visibility.Visible;
                ExchangeEndDateTextBox.Visibility = Visibility.Visible;
            }
        }

        private void CreateStudentButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string fullName = FullNameTextBox.Text;
                string faculty = FacultyTextBox.Text;
                int course = int.Parse(CourseTextBox.Text);

                var selected = (StudentTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

                Student student = null;

                if (selected == "Bachelor")
                {
                    int credits = int.Parse(CreditsTextBox.Text);
                    student = new Bachelor(fullName, faculty, course, credits);
                }
                else if (selected == "Master")
                {
                    string thesis = ThesisTopicTextBox.Text;
                    int pubs = int.Parse(PublicationsTextBox.Text);
                    student = new Master(fullName, faculty, course, thesis, pubs);
                }
                else if (selected == "ExchangeStudent")
                {
                    string country = HomeCountryTextBox.Text;
                    DateTime endDate;
                    if (!DateTime.TryParse(ExchangeEndDateTextBox.Text, out endDate))
                    {
                        MessageBox.Show("Невірний формат дати. Використовуйте yyyy-mm-dd.");
                        return;
                    }

                    student = new ExchangeStudent(fullName, faculty, course, country, endDate);
                }
                students.Add(student);
                RefreshStudentTable();
                OutputTextBlock.Text = $"Додано студента: {student.FullName}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка: {ex.Message}");
            }
        }

        private void AddGradeButton_Click(object sender, RoutedEventArgs e)
        {
            if (StudentsDataGrid.SelectedItem is Student selectedStudent)
            {
                try
                {
                    int grade = int.Parse(GradeTextBox.Text);
                    selectedStudent.AddGrade(grade);
                    OutputTextBlock.Text = $"Оцінка {grade} додана студенту {selectedStudent.FullName}";
                    RefreshStudentTable();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Помилка: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Оберіть студента у таблиці.");
            }
        }

        private void ShowTopStudentButton_Click(object sender, RoutedEventArgs e)
        {
            var top = StudentHelper.GetTopStudent(students);
            if (top != null)
            {
                OutputTextBlock.Text = $"Найкращий студент: {top.FullName}, рейтинг: {top.GetRating():F2}";
            }
            else
            {
                OutputTextBlock.Text = "Немає студентів.";
            }
        }
        private void RefreshStudentTable()
        {
            // Анонімний клас для відображення рейтингу
            StudentsDataGrid.ItemsSource = null;
            StudentsDataGrid.ItemsSource = students;
        }
    }
}