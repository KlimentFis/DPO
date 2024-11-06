using CourseProject.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseProject.Pages
{
    public partial class CoursesPage : Page
    {
        private TestBaseEntities _context;

        public CoursesPage()
        {
            InitializeComponent();
            CourseTypeComboBox.SelectedItem = CourseTypeComboBox.Items[0];
            _context = new TestBaseEntities();
            LoadData(_context.Courses.ToList());
        }

        private void LoadData(List<Courses> courses)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CourseID");
            dt.Columns.Add("CourseName");
            dt.Columns.Add("CourseOrganization");
            dt.Columns.Add("CourseHours");
            dt.Columns.Add("CourseKind");
            dt.Columns.Add("CourseInternship");
            dt.Columns.Add("CoursePriority");

            string correctInternship;
            string correctPriority;

            foreach (var course in courses)
            {
                if (course.internship == true)
                {
                    correctInternship = "Да";
                }
                else
                {
                    correctInternship = "Нет";
                }

                correctPriority = course.priority == 0 ? "Не приоритетен" : "Приоритетен";

                dt.Rows.Add(course.ID, course.name, course.organization, course.hours_quantity, course.kind, correctInternship, correctPriority);
            }

            coursesDataGrid.ItemsSource = dt.DefaultView;
        }

        private void AddCourse_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddCourse());
        }

        private void CourseDel_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = coursesDataGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                int selectedCourseID = Convert.ToInt32(selectedRow["CourseID"]);

                using (var db = new TestBaseEntities())
                {
                    var courseToDelete = db.Courses.FirstOrDefault(c => c.ID == selectedCourseID);

                    if (courseToDelete != null)
                    {
                        db.Courses.Remove(courseToDelete);
                        try
                        {
                            db.SaveChanges();
                        }
                        catch
                        {
                            NavigationService.Navigate(new CoursesPage());
                            MessageBox.Show("Не удалось удалить курс из базы данных так как он пройден некоторыми преподавателями, пожалуйста сначала удалите его из таблицы завершенных курсов.", "Ошибка!");
                        }
                        LoadData(_context.Courses.ToList());
                    }
                    else
                    {
                        MessageBox.Show("Выбранный курс не найден в базе данных.", "Ошибка удаления");
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите курс для удаления.", "Ошибка выбора");
            }
        }

        private void Filter_Course_Click(object sender, RoutedEventArgs e)
        {
            var selectedItemText = (CourseTypeComboBox.SelectedItem as ComboBoxItem).Content.ToString();
            List<Courses> filteredCourses;

            switch (selectedItemText)
            {
                case "Топ-50":
                    filteredCourses = _context.Courses.Where(x => x.priority != 0).ToList();
                    break;
                case "Не приоритетные":
                    filteredCourses = _context.Courses.Where(x => x.priority == 0).ToList();
                    break;
                case "По названию":
                    filteredCourses = _context.Courses.OrderBy(x => x.name).ToList();
                    break;
                case "По организации":
                    filteredCourses = _context.Courses.OrderBy(x => x.organization).ToList();
                    break;
                case "Со стажировкой":
                    filteredCourses = _context.Courses.Where(x => x.internship == true).ToList();
                    break;
                case "Без стажировки":
                    filteredCourses = _context.Courses.Where(x => x.internship == false).ToList();
                    break;
                case "Технический":
                    filteredCourses = _context.Courses.Where(x => x.kind == "Технический").ToList();
                    break;
                case "Педагогический":
                    filteredCourses = _context.Courses.Where(x => x.kind == "Педагогический").ToList();
                    break;
                default:
                    filteredCourses = _context.Courses.ToList();
                    break;
            }
            LoadData(filteredCourses);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string input = textBox.Text;

            if (!Regex.IsMatch(input, "^[0-9]+$"))
            {
                MessageBox.Show("Пожалуйста, введите только цифры.");
                textBox.Text = ""; 
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }

        private void CoursesSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = CoursesSearch.Text.Trim().ToLower(); // Переведем поисковый текст в нижний регистр

            List<Courses> filteredCourses;

            if (string.IsNullOrWhiteSpace(searchText))
            {
                // Если строка поиска пуста, загружаем все записи
                filteredCourses = _context.Courses.ToList();
            }
            else
            {
                // Поиск лекторов, второе имя которых начинается с searchText, без учета регистра
                filteredCourses = _context.Courses
                .Where(x => x.name.ToLower().StartsWith(searchText))
                .ToList();
            }

            LoadData(filteredCourses);
        }
    }
}