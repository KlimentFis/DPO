using CourseProject.Entities;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace CourseProject.Pages
{
    public partial class AddLecturer : Page
    {
        public AddLecturer()
        {
            InitializeComponent();
        }

        private void AddLecturer_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFirstName.Text))
            {
                MessageBox.Show("Введите имя преподавателя!", "Ошибка!");
                return;
            }

            if (string.IsNullOrEmpty(txtLastName.Text))
            {
                MessageBox.Show("Введите фамилию преподавателя!", "Ошибка!");
                return;
            }

            if (string.IsNullOrEmpty(txtMiddleName.Text))
            {
                MessageBox.Show("Введите отчество преподавателя!", "Ошибка!");
                return;
            }

            using (var db = new TestBaseEntities())
            {
               Lecturers lecturer = new Lecturers()
               {
                   first_name = txtFirstName.Text,
                   second_name = txtLastName.Text,
                   patronymic = txtMiddleName.Text, 
               };
               db.Lecturers.Add(lecturer);
               db.SaveChanges();
            }
            MessageBox.Show("Добавление прошло успешно!", "Внимание!");
            NavigationService.Navigate(new LecturersPage());
        }

        private void patronymicInputCheck(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[а-яА-Я]");

            if (!regex.IsMatch(e.Text))
            {
                e.Handled = true; 
            }
        }

        private void firstnameInputCheck(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[а-яА-Я]");

            if (!regex.IsMatch(e.Text))
            {
                e.Handled = true; 
            }
        }

        private void secondnameInputCheck(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[а-яА-Я]");

            if (!regex.IsMatch(e.Text))
            {
                e.Handled = true; 
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Lecturers());
        }
    }
}
