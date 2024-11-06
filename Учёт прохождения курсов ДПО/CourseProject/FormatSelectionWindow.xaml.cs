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
using System.Windows.Shapes;

namespace CourseProject
{
    public partial class FormatSelectionWindow : Window
    {
        public static string SelectedFormat { get; private set; }

        public FormatSelectionWindow()
        {
            InitializeComponent();
        }

        private void WordButtonClick(object sender, RoutedEventArgs e)
        {
            SelectedFormat = "Word";
            this.DialogResult = true;
            this.Close();
        }

        private void ExcelButtonClick(object sender, RoutedEventArgs e)
        {
            SelectedFormat = "Excel";
            this.DialogResult = true;
            this.Close();
        }
    }
}