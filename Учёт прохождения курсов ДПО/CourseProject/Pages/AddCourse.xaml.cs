using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CourseProject.Entities;

namespace CourseProject.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddCourse.xaml
    /// </summary>
    public partial class AddCourse : Page
    {
        public AddCourse()
        {
            InitializeComponent();
        }

        private void AddCourse_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CourseNameTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите название курса");
                return;
            }
            if (string.IsNullOrWhiteSpace(OrganizationTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите название организации");

            }

            if (HoursQuantityTextBox.Text == null)
            {
                MessageBox.Show("Пожалуйста, введите количество часов курса");
            }

            // Проверяем, что ComboBox не NULL и выбраны непустые значения
            if (CourseTypeComboBox.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите вид курса");
                return;
            }

            int THATpriority;
            if (PriorityCheckBox.IsChecked == true) 
            {
                 THATpriority = 1;

            }
            else
            {
                THATpriority = 0;
            }

            bool THATinternship;
            if (InternshipCheckBox.IsChecked == true)
            {
                THATinternship = true;
            }
            else
            {
                THATinternship = false;
            }

            using (var db = new TestBaseEntities())
            {

                Courses course = new Courses()
                {
                    name = CourseNameTextBox.Text,
                    organization = OrganizationTextBox.Text,
                    kind = CourseTypeComboBox.SelectedItem.ToString().Substring(38),
                    hours_quantity = Convert.ToInt32(HoursQuantityTextBox.Text),
                    priority = THATpriority,
                    internship = THATinternship
                };
                db.Courses.Add(course);
                db.SaveChanges();
            }
            MessageBox.Show("Курс успешно добавлен!", "Внимание!");
            NavigationService.Navigate(new CoursesPage());
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string input = textBox.Text;

            if (!Regex.IsMatch(input, "^[0-9]+$"))
            {
                MessageBox.Show("Пожалуйста, введите только цифры.");
                textBox.Text = ""; // Очистить поле ввода
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Courses());
        }
    }
}
