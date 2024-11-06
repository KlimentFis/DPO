using CourseProject.Pages;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace CourseProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void WindowClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены что хотите выйти из приложения?", "Выход", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }



        private void MainFrameNavigated(object sender, NavigationEventArgs e)
        {
            if (!(e.Content is Page page)) return;
            this.Title = "Учет прохождения курсов ДПО";
        }

        private void My_Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new CoursesPage());
            Button1.Background = new SolidColorBrush(Color.FromRgb(198, 198, 198));
            Button.Background = new SolidColorBrush(Color.FromRgb(112, 112, 112));
            Button2.Background = new SolidColorBrush(Color.FromRgb(198, 198, 198));
        }

        private void My_Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new LecturersPage());
            Button.Background = new SolidColorBrush(Color.FromRgb(198, 198, 198));
            Button2.Background = new SolidColorBrush(Color.FromRgb(198, 198, 198));
            Button1.Background = new SolidColorBrush(Color.FromRgb(112, 112, 112));
        }

        private void My_Button_Click_3(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new CoursesPage());
            Button1.Background = new SolidColorBrush(Color.FromRgb(198, 198, 198));
            Button2.Background = new SolidColorBrush(Color.FromRgb(112, 112, 112));
            Button.Background = new SolidColorBrush(Color.FromRgb(198, 198, 198));
        }
    }
}
