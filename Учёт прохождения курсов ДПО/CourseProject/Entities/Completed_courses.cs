//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CourseProject.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Completed_courses
    {
        public int ID { get; set; }
        public Nullable<int> lecturer { get; set; }
        public Nullable<int> course { get; set; }
        public Nullable<System.DateTime> start_date { get; set; }
        public Nullable<System.DateTime> end_date { get; set; }
        public string image { get; set; }
    
        public virtual Courses Courses { get; set; }
        public virtual Lecturers Lecturers { get; set; }
    }
}
