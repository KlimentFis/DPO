using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// <summary>
    /// Логика взаимодействия для DatePickerWindow.xaml
    /// </summary>
    public partial class DatePickerWindow : Window
    {
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string ImageSource { get; private set; }

        public DatePickerWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StartDate = StartDatePicker.SelectedDate.Value;
                EndDate = EndDatePicker.SelectedDate.Value;
                DialogResult = true;

                if (StartDate > EndDate)
                {
                    MessageBox.Show("Конечнач дата не может быть ранее начальной даты", "Ошибка!");
                }

            }
            catch
            {
                MessageBox.Show("Даты выбраны не корректно, выберите данные!", "Ошибка!");
            }
        }

        private void LoadPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files |*.jpg;*.png;*.gif;*.bmp;*.jpeg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string imageUrl = openFileDialog.FileName;
                ImageSource = imageUrl;
                BitmapImage bitmap = new BitmapImage(new Uri(imageUrl));
                imageView.Source = bitmap;
            }
        }
    }
}
