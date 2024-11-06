using CourseProject.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.ViewModels
{
    public class TeacherCoursesViewModel
    {
        public ObservableCollection<Courses> CoursesList { get; set; }

        public TeacherCoursesViewModel(int lecturerID)
        {
            using (var context = new TestBaseEntities())
            {
                var courses = context.Completed_courses
                    .Where(c => c.Lecturers.ID == lecturerID)
                    .Select(c => c.Courses)
                    .ToList();

                CoursesList = new ObservableCollection<Courses>(courses);
            }
        }
            
    }
}
