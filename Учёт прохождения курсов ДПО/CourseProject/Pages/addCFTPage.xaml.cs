using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using CourseProject.Entities;
using System.Linq;
using System.Data;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;

namespace CourseProject.Pages
{

    public partial class addCFTPage : Page
    {
        public Lecturers thisLecturer;
        public bool Archive;

        public addCFTPage(Lecturers selectedLecturer)
        {
            InitializeComponent();
            CourseTypeComboBox.SelectedItem = CourseTypeComboBox.Items[0];
            thisLecturer = selectedLecturer;
            LoadData();
            MainLabel.Content = "Курсы преподавателя " + this.thisLecturer.second_name + " " + thisLecturer.first_name + " " + thisLecturer.patronymic;
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

                dt.Rows.Add(course.name, course.organization, course.hours_quantity, course.kind, correctInternship, correctPriority);
            }

            allCoursesDataGrid.ItemsSource = dt.DefaultView;
        }



        private void LoadData()
        {
            TestBaseEntities context = TestBaseEntities.GetContext();
            var courses = context.Courses.ToList();
            DataTable dtAC = new DataTable();
            dtAC.Columns.Add("CourseID");
            dtAC.Columns.Add("CourseName");
            dtAC.Columns.Add("CourseOrganization");
            dtAC.Columns.Add("CourseHours");
            dtAC.Columns.Add("CourseKind");
            dtAC.Columns.Add("CourseInternship");
            dtAC.Columns.Add("CoursePriority");

            string correctInternship = "";
            string correctPriority = "";

            foreach (var course in courses)
            {
                if (course.internship == false)
                {
                    correctInternship = "Нет";
                }
                else
                {
                    correctInternship = "Да";
                }

                if (course.priority == 0)
                {
                    correctPriority = "Не приоритетен";
                }
                else
                {
                    correctPriority = "Приоритетен";
                }

                dtAC.Rows.Add(course.ID, course.name, course.organization, course.hours_quantity, course.kind, correctInternship, correctPriority);


            }

            allCoursesDataGrid.ItemsSource = dtAC.DefaultView;

            using (var db = new TestBaseEntities())
            {
                var lecturerID = thisLecturer.ID;
                var coursesQuery = from c in db.Courses
                                   join cc in db.Completed_courses on c.ID equals cc.course
                                   where cc.lecturer == lecturerID
                                   select new
                                   {
                                       Course = c,
                                       StartDate = cc.start_date,
                                       EndDate = cc.end_date
                                   };
                var lecturerCoursesList = coursesQuery.ToList();

                DataTable dtTL = new DataTable();
                dtTL.Columns.Add("LecturerCourseID");
                dtTL.Columns.Add("LecturerCourseName");
                dtTL.Columns.Add("LecturerCourseOrganization");
                dtTL.Columns.Add("LecturerCourseHours");
                dtTL.Columns.Add("LecturerCourseKind");
                dtTL.Columns.Add("LecturerCourseInternship");
                dtTL.Columns.Add("LecturerCoursePriority");
                dtTL.Columns.Add("LecturerCourseSD");
                dtTL.Columns.Add("LecturerCourseED");

                foreach (var course in lecturerCoursesList)
                {
                    if (course.Course.internship == false)
                    {
                        correctInternship = "Нет";
                    }
                    else
                    {
                        correctInternship = "Да";
                    }

                    if (course.Course.priority == 0)
                    {
                        correctPriority = "Не приоритетен";
                    }
                    else
                    {
                        correctPriority = "Приоритетен";
                    }

                    dtTL.Rows.Add(course.Course.ID, course.Course.name, course.Course.organization, course.Course.hours_quantity, course.Course.kind, correctInternship, correctPriority, course.StartDate, course.EndDate);
                }
                lecturerCoursesDataGrid.ItemsSource = dtTL.DefaultView;
            }
        }

        private void DeleteCourse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView selectedCourse = (DataRowView)lecturerCoursesDataGrid.SelectedItem;


                if (selectedCourse != null)
                {
                    int courseId = Convert.ToInt32(selectedCourse["LecturerCourseID"]);

                    using (var db = new TestBaseEntities())
                    {
                        Completed_courses completedCourse = db.Completed_courses
                            .Where(cc => cc.course == courseId && cc.lecturer == thisLecturer.ID)
                            .FirstOrDefault();

                        if (completedCourse != null)
                        {
                            db.Completed_courses.Remove(completedCourse);
                            db.SaveChanges();
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show("Не удалось найти соответствующий курс для удаления");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Не выбран курс для удаления. Пожалуйста, выберите курс и повторите попытку");
                }
            }
            catch
            {
                MessageBox.Show("При выполнении возникла ошибка", "Это фатальная ошибка!");
            }
        }


        private void AddCourse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView selectedCourse = (DataRowView)allCoursesDataGrid.SelectedItem;
                if (selectedCourse != null)
                {
                    int courseId = Convert.ToInt32(selectedCourse["CourseID"]);

                    using (var db = new TestBaseEntities())
                    {
                        int alreadyAdded = db.Completed_courses
                                           .Count(cc => cc.course == courseId && cc.lecturer == thisLecturer.ID);
                        if (alreadyAdded > 0)
                        {
                            MessageBox.Show("Этот курс уже добавлен для этого преподавателя");
                            return;
                        }

                        DatePickerWindow datePickerWindow = new DatePickerWindow();
                        if (datePickerWindow.ShowDialog() == true)
                        {
                            DateTime startDate = datePickerWindow.StartDate;
                            DateTime endDate = datePickerWindow.EndDate;
                            string imageSource = datePickerWindow.ImageSource;

                            Completed_courses completedCourse = new Completed_courses()
                            {
                                course = courseId,
                                lecturer = thisLecturer.ID,
                                start_date = startDate,
                                end_date = endDate,
                                image = imageSource
                            };
                            db.Completed_courses.Add(completedCourse);
                            db.SaveChanges();
                        }
                    }
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Не выбран курс для добавления. Пожалуйста, выберите курс и повторите попытку");
                }
            }
            catch
            {
                MessageBox.Show("При выполнении возникла ошибка", "Ошибка!");
            }
        }

        public void GenerateWordReport()
        {
            DataTable data = ((DataView)lecturerCoursesDataGrid.ItemsSource).ToTable();

            //Cоздаем Word документ
            using (var doc = WordprocessingDocument.Create("Отчетность по преподавателю.docx", WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = doc.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body body = mainPart.Document.AppendChild(new Body());

                //Добавляем заголовок
                TableRow headerRow = new TableRow();
                foreach (DataColumn column in data.Columns)
                {
                    TableCell cell = new TableCell();
                    cell.Append(new Paragraph(new Run(new Text(column.ColumnName))));
                    headerRow.Append(cell);
                }
                body.AppendChild(headerRow);

                //Добавляем строки для данных
                foreach (DataRow row in data.Rows)
                {
                    TableRow tableRow = new TableRow();
                    foreach (var cellData in row.ItemArray)
                    {
                        TableCell cell = new TableCell();
                        cell.Append(new Paragraph(new Run(new Text(cellData.ToString()))));
                        tableRow.Append(cell);
                    }
                    body.AppendChild(tableRow);
                }
            }
        }

        public void GenerateExcelReport()
        {
            DataTable data = ((DataView)lecturerCoursesDataGrid.ItemsSource).ToTable();

            var workbook = new XLWorkbook();

            var worksheet = workbook.Worksheets.Add("Lecturer Report");

            for (int i = 0; i < data.Columns.Count; i++)
            {
                worksheet.Cell(1, i + 1).Value = data.Columns[i].ColumnName;
            }

            // Adding Data
            for (int i = 0; i < data.Rows.Count; i++)
            {
                for (int j = 0; j < data.Columns.Count; j++)
                {
                    worksheet.Cell(i + 2, j + 1).Value = data.Rows[i][j].ToString();
                }
            }

            workbook.SaveAs("Отчетность по преподавателю.xlsx");
        }

        private void ExportClick(object sender, RoutedEventArgs e)
        {
            FormatSelectionWindow FSW = new FormatSelectionWindow();
            if (FSW.ShowDialog() == true)
            {
                string selectedFormat = FormatSelectionWindow.SelectedFormat;
                if (selectedFormat == "Word")
                {
                    GenerateWordReport();
                }
                else if (selectedFormat == "Excel")
                {
                    GenerateExcelReport();
                }
                else
                {
                    MessageBox.Show("Неверный формат выбран. Пожалуйста выберите Word или Excel");
                }
            }
        }

        private void Filter_Course_Click(object sender, RoutedEventArgs e)
        {
            var selectedItemText = (CourseTypeComboBox.SelectedItem as ComboBoxItem).Content.ToString();
            List<Courses> filteredCourses;
            TestBaseEntities context = TestBaseEntities.GetContext();

            switch (selectedItemText)
            {
                case "Сначала приоритетные":
                    filteredCourses = context.Courses.Where(x => x.priority != 0).ToList();
                    break;
                case "По названию":
                    filteredCourses = context.Courses.OrderBy(x => x.name).ToList();
                    break;
                case "По названию организации":
                    filteredCourses = context.Courses.OrderBy(x => x.organization).ToList();
                    break;
                case "Со стажировкой":
                    filteredCourses = context.Courses.Where(x => x.internship == true).ToList();
                    break;
                case "Без стажировки":
                    filteredCourses = context.Courses.Where(x => x.internship == false).ToList();
                    break;
                case "Технические":
                    filteredCourses = context.Courses.Where(x => x.kind == "Технические").ToList();
                    break;
                case "Педагогические":
                    filteredCourses = context.Courses.Where(x => x.kind == "Педагогические").ToList();
                    break;
                case "По приоритету":
                    filteredCourses = context.Courses.OrderBy(x => x.priority).ToList();
                    break;
                default:
                    filteredCourses = context.Courses.ToList();
                    break;
            }
            LoadData(filteredCourses);

        }

        private void Showcertificate_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selectedRow = lecturerCoursesDataGrid.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                try
                {
                    int targetCourseID = Convert.ToInt32(selectedRow["LecturerCourseID"]);
                    int targetLecturerID = thisLecturer.ID;
                    using (var db = new TestBaseEntities())
                    {
                        var completedCourseId = db.Completed_courses
                            .Where(cc => cc.course == targetCourseID && cc.lecturer == targetLecturerID)
                            .Select(cc => cc.ID)
                            .FirstOrDefault();

                        var CCToNavigate = db.Completed_courses.FirstOrDefault(c => c.ID == completedCourseId);
                        NavigationService.Navigate(new CertificatePage(CCToNavigate));
                    }
                }
                catch
                {
                    MessageBox.Show("Для данного преподавателя не добавлен сертификат или преподавателя не существует.", "Ошибка!");
                }
            }
        }

        private void LoadData(bool showArchive)
        {
            using (var db = new TestBaseEntities())
            {
                var lecturerID = thisLecturer.ID;
                var currentDate = DateTime.Now.AddYears(-2);

                var coursesQuery = from c in db.Courses
                                   join cc in db.Completed_courses on c.ID equals cc.course
                                   where cc.lecturer == lecturerID && (showArchive ? cc.end_date < currentDate : cc.end_date >= currentDate)
                                   select new
                                   {
                                       Course = c,
                                       StartDate = cc.start_date,
                                       EndDate = cc.end_date
                                   };

                var lecturerCoursesList = coursesQuery.ToList();

                DataTable dtTL = new DataTable();
                dtTL.Columns.Add("LecturerCourseID");
                dtTL.Columns.Add("LecturerCourseName");
                dtTL.Columns.Add("LecturerCourseOrganization");
                dtTL.Columns.Add("LecturerCourseHours");
                dtTL.Columns.Add("LecturerCourseKind");
                dtTL.Columns.Add("LecturerCourseInternship");
                dtTL.Columns.Add("LecturerCoursePriority");
                dtTL.Columns.Add("LecturerCourseSD");
                dtTL.Columns.Add("LecturerCourseED");

                string correctInternship = "";
                string correctPriority = "";

                foreach (var course in lecturerCoursesList)
                {
                    if (course.Course.internship == false)
                    {
                        correctInternship = "Нет";
                    }
                    else
                    {
                        correctInternship = "Да";
                    }

                    if (course.Course.priority == 0)
                    {
                        correctPriority = "Не приоритетен";
                    }
                    else
                    {
                        correctPriority = "Приоритетен";
                    }

                    DataRow row = dtTL.NewRow();
                    row["LecturerCourseID"] = course.Course.ID;
                    row["LecturerCourseName"] = course.Course.name;
                    row["LecturerCourseOrganization"] = course.Course.organization;
                    row["LecturerCourseHours"] = course.Course.hours_quantity;
                    row["LecturerCourseKind"] = course.Course.kind;
                    row["LecturerCourseInternship"] = correctInternship;
                    row["LecturerCoursePriority"] = correctPriority;
                    row["LecturerCourseSD"] = course.StartDate;
                    row["LecturerCourseED"] = course.EndDate;

                    // Добавляем строку в DataTable
                    dtTL.Rows.Add(row);
                }

                lecturerCoursesDataGrid.ItemsSource = dtTL.DefaultView;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LecturersPage());
        }

        private bool archive = true;
        private void Archive_Click_(object sender, RoutedEventArgs e)
        {
            archive = !archive;
            LoadData(archive);
        }
    }
}