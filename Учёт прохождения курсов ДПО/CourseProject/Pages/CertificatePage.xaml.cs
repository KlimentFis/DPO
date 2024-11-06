using CourseProject.Entities;
using System;
using System.Collections.Generic;
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

namespace CourseProject.Pages
{

    public partial class CertificatePage : Page
    {

        public CertificatePage(Completed_courses thisCC)
        {
            InitializeComponent();
            string imageURL = thisCC.image;
            BitmapImage bitmap = new BitmapImage(new Uri(imageURL));
            imageView.Source = bitmap;
        }
    }
}
