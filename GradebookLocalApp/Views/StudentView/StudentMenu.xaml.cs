using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GradebookLocalApp.Views.StudentView
{
    /// <summary>
    /// Logika interakcji dla klasy StudentMenu.xaml
    /// </summary>
    public partial class StudentMenu : UserControl
    {
        StudentClasses _classesPage;
        ContentWindow _window;
        Model.studenci _student;
        StudentNotes _notes;
        StudentProjectPage _projectpage;
        public StudentMenu(ContentWindow contentWindow, Model.studenci student)
        {
            _window = contentWindow;
            _student = student;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _classesPage = new StudentClasses(_window, _student);
            _window.ContentFrame.Content = _classesPage;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _notes = new StudentNotes(_window, _student);
            _window.ContentFrame.Content = _notes;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            _projectpage = new StudentProjectPage(_window, _student);
            _window.ContentFrame.Content = _projectpage;
        }
    }
}
