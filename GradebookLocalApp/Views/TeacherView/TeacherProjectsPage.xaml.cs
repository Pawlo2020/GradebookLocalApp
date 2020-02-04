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

namespace GradebookLocalApp.Views.TeacherView
{
    /// <summary>
    /// Logika interakcji dla klasy TeacherProjectsPage.xaml
    /// </summary>
    public partial class TeacherProjectsPage : Page
    {
        Blades.TeacherNewProjectBlade _newProjectTeacher;   //Tutaj deklarujemy obiekt klasy UserControla
        Model.prowadzacy _teacher;
        ContentWindow _window;
        ProjectPage _projectPage;
        public TeacherProjectsPage(Model.prowadzacy teacher, ContentWindow window)
        {
            _teacher = teacher;
            _window = window;
            _newProjectTeacher = new Blades.TeacherNewProjectBlade(teacher, this);  //Tutaj inicjalizacja
            InitializeComponent();
            UpdateProjectsView();
        }

        private void TaskView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (var entity = new Model.TestEntities())
            {
                var id = ((Model.ProjectViewModel)ProjectsView.SelectedItem).Id;
                var project = entity.projekty.Where(x => x.id_proj == id).FirstOrDefault();
                _projectPage = new ProjectPage(project);
                _projectPage.SetData(_teacher,project);
                _projectPage.ProjectNameLbl.Content = project.nazwa_proj;
                _window.ContentFrame.Content = _projectPage;
            }
        }

        private void NewGroup_Click(object sender, RoutedEventArgs e)
        {
            //SupNewProjBlade - nazwa stackpanela
            //_newProjectTeacher - nazwa usercontrola
            SupNewProjBlade.Children.Add(_newProjectTeacher);   //Tutaj definicja - ustawiamy userControl w stackPanelu w zdarzeniu
        }

        public void UpdateProjectsView()
        {
            using (var entity = new Model.TestEntities())
            {
                var temp = entity.projectsbyTeacher1(_teacher.id_prow.ToString());
                var projects = entity.projekty.Where(x => x.id_prow == _teacher.id_prow);

                try
                {
                    ProjectsView.Items.Clear();
                    foreach (var item in temp)
                    {
                        ProjectsView.Items.Add(new Model.ProjectViewModel { Project = item.nazwa_proj, Subject = item.nazwa_przed, Id = item.id_proj });
                    }
                }
                catch (Exception e) { }
            }
        }
    }
}
