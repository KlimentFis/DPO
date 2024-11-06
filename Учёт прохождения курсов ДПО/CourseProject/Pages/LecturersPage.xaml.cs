using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
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
    public partial class LecturersPage : Page
    {
        private TestBaseEntities _context;
        public ObservableCollection<Lecturers> Lecturer { get; set; }

        public LecturersPage()
        {
            InitializeComponent();
            _context = new TestBaseEntities();
            var lecturers = _context.Lecturers.OrderBy(x => x.patronymic).OrderBy(x => x.first_name).OrderBy(x => x.second_name).ToList();
            DataTable dt = new DataTable();
            dt.Columns.Add("LecturerID");
            dt.Columns.Add("LecturerLastName");
            dt.Columns.Add("LecturerFirstName");
            dt.Columns.Add("LecturerPatronymic");
            foreach (var lecturer in lecturers)
            {
                dt.Rows.Add(lecturer.ID, lecturer.second_name, lecturer.first_name, lecturer.patronymic);
            }
            lecturersDataGrid.ItemsSource = dt.DefaultView;
        }

        private void LoadData(List<Lecturers> lecturers)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("LecturerID");
            dt.Columns.Add("LecturerLastName");
            dt.Columns.Add("LecturerFirstName");
            dt.Columns.Add("LecturerPatronymic");

            foreach (var lecturer in lecturers)
            {
                dt.Rows.Add(lecturer.ID, lecturer.second_name, lecturer.first_name, lecturer.patronymic);
            }
            lecturersDataGrid.ItemsSource = dt.DefaultView;

            lecturersDataGrid.ItemsSource = dt.DefaultView;
        }

        private void AddLecturer_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddLecturer());
        }

        private void LecturerButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = lecturersDataGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                int lecturerID = Convert.ToInt32(selectedRow["LecturerID"]);

                using (var db = new TestBaseEntities())
                {
                    var selectedLecturer = db.Lecturers.FirstOrDefault(l => l.ID == lecturerID);

                    if (selectedLecturer != null)
                    {
                        NavigationService.Navigate(new addCFTPage(selectedLecturer));
                    }
                    else
                    {
                        MessageBox.Show("Выбранный преподаватель не найден в базе данных.", "Ошибка");
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите преподавателя.", "Ошибка выбора");
            }
        }

        private void DelLecturer_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = lecturersDataGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                try
                {
                    int lecturerId = Convert.ToInt32(selectedRow["LecturerID"]);

                    using (var db = new TestBaseEntities())
                    {
                        var lecturerToDelete = db.Lecturers.FirstOrDefault(c => c.ID == lecturerId);

                        if (lecturerToDelete != null)
                        {
                            (lecturersDataGrid.ItemsSource as DataView).Table.Rows.Remove(selectedRow.Row);

                            db.Lecturers.Remove(lecturerToDelete);
                            try
                            {
                                db.SaveChanges();
                            }
                            catch
                            {
                                NavigationService.Navigate(new LecturersPage());
                                MessageBox.Show("Не удалось удалить преподавателя из базы данных так как он имеет пройденные курсы, пожалуйста сначала удалите их.", "Ошибка!");
                            }

                        }
                        else
                        {
                            MessageBox.Show("Выбранный преподаватель не найден в базе данных.", "Ошибка удаления");
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Возникла ошибка!", "Фатальная ошибка!");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите преподавателя для удаления.", "Ошибка выбора");
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = LecturersSearch.Text.Trim().ToLower(); // Переведем поисковый текст в нижний регистр

            List<Lecturers> filteredLecturers;

            if (string.IsNullOrWhiteSpace(searchText))
            {
                // Если строка поиска пуста, загружаем все записи
                filteredLecturers = _context.Lecturers.ToList();
            }
            else
            {
                // Поиск лекторов, второе имя которых начинается с searchText, без учета регистра
                filteredLecturers = _context.Lecturers
                .Where(x => x.second_name.ToLower().StartsWith(searchText))
                .ToList();
            }

            LoadData(filteredLecturers);
        }
    }
}
