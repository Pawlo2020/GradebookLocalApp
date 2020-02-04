using System.Windows;
using System.Windows.Controls;

namespace GradebookLocalApp.Views.SupervisorView
{
    /// <summary>
    /// Logika interakcji dla klasy SupervisorMenu.xaml
    /// </summary>
    public partial class SupervisorMenu : UserControl
    {
        ContentWindow _window;
        SupervisorDashPage _dashboard;
        SupervisorTeacherPage _teachersSupervisor;
        SupervisorStudentsPage _studentsSupervisor;
        SupervisorSubjectPage _subjectsSupervisor;
        SupervisorGroupPage _groupsSupervisor;
        SupervisorClassesPage _classesSupervisor;

        public SupervisorMenu(ContentWindow window)
        {
            _window = window;
            
            
            
            
            
            
            
            InitializeComponent();
        }

        private void TeacherSupBut_Click(object sender, RoutedEventArgs e)
        {
            _teachersSupervisor = new SupervisorTeacherPage();
            _window.ContentFrame.Content = _teachersSupervisor;
        }

        private void StudentsSupBut_Click(object sender, RoutedEventArgs e)
        {
            _studentsSupervisor = new SupervisorStudentsPage();
            _studentsSupervisor.UpdateStudentsView();
            _window.ContentFrame.Content = _studentsSupervisor;
            
        }

        private void SubjectSupBut_Click(object sender, RoutedEventArgs e)
        {
            _subjectsSupervisor = new SupervisorSubjectPage();
            _window.ContentFrame.Content = _subjectsSupervisor;
        }

        private void GroupsSupBut_Click(object sender, RoutedEventArgs e)
        {
            _groupsSupervisor = new SupervisorGroupPage();
            _window.ContentFrame.Content = _groupsSupervisor;
        }

        private void DashBut_Click(object sender, RoutedEventArgs e)
        {
            _dashboard = new SupervisorDashPage();
            _window.ContentFrame.Content = _dashboard;
            _dashboard.UpdateNumbers();
        }

        private void ClassesBut_Click(object sender, RoutedEventArgs e)
        {
            _classesSupervisor = new SupervisorClassesPage(_window);
            _window.ContentFrame.Content = _classesSupervisor;
        }
    }
}
