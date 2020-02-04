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

namespace GradebookLocalApp.Views.TeacherView
{
    /// <summary>
    /// Logika interakcji dla klasy TeacherMenu.xaml
    /// </summary>
    public partial class TeacherMenu : UserControl
    {
        ContentWindow _window;
        TeacherSubjectsPage _subjectsTeacher;
        TeacherClassesPage _classesTeacher;
        TeacherProjectsPage _projectsTeacher;
        Model.prowadzacy _teacher;
        public TeacherMenu(ContentWindow window, Model.prowadzacy teacher)
        {
            _window = window;
            _teacher = teacher;
            
            
            
            InitializeComponent();
        }

        private void SubBut_Click(object sender, RoutedEventArgs e)
        {
            _subjectsTeacher = new TeacherSubjectsPage(_teacher, _window);
            _window.ContentFrame.Content = _subjectsTeacher;
        }

        private void ClassBut_Click(object sender, RoutedEventArgs e)
        {
            _classesTeacher = new TeacherClassesPage(_teacher, _window);
            _window.ContentFrame.Content = _classesTeacher; 
        }

        private void ProjBut_Click(object sender, RoutedEventArgs e)
        {
            _projectsTeacher = new TeacherProjectsPage(_teacher, _window);
            _window.ContentFrame.Content = _projectsTeacher;
        }

        private void AppBut_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
