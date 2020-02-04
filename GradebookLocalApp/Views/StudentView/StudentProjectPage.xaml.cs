using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
namespace GradebookLocalApp.Views.StudentView
{
    /// <summary>
    /// Logika interakcji dla klasy StudentProjectPage.xaml
    /// </summary>
    public partial class StudentProjectPage : Page
    {
        Model.studenci _student;
        Blades.StudentProjectBlade _projectBlade;
        public StudentProjectPage(ContentWindow _window, Model.studenci student)
        {
            _student = student;
            InitializeComponent();
            UpdateView();
        }

        public class ProjectInfo{
            public int id_student { get; set; }
            public int id_proj { get; set; }

        }
        private void UpdateView()
        {
            using (var entity = new Model.TestEntities())
            {
                var query = entity.Database.SqlQuery<ProjectInfo>($"SELECT * FROM studenci_projekty WHERE id_student={_student.id_student}");

                SubjectView.Items.Clear();
                foreach (var item in query)
                {
                    var project = entity.projekty.Where(x => x.id_proj == item.id_proj).FirstOrDefault();
                    var classe = entity.zajecia.Where(x => x.id_zajec == project.id_zajec).FirstOrDefault();
                    var subject = entity.przedmioty.Where(x => x.id_przed == classe.id_przed).FirstOrDefault();
                    SubjectView.Items.Add(new Model.ProjectViewModel {Id=item.id_proj, Project = project.nazwa_proj, Subject =subject.nazwa_przed });

                }

            }
        }

        private void SubjectView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var project = ((Model.ProjectViewModel)SubjectView.SelectedItem);
            using (var entitiy = new Model.TestEntities())
            {
                var proj = entitiy.projekty.Where(x => x.id_proj == project.Id).FirstOrDefault();
                _projectBlade = new Blades.StudentProjectBlade(proj, _student);
                SupNewProjBlade.Children.Clear();
                _projectBlade.SetData();
                SupNewProjBlade.Children.Add(_projectBlade);

            }

            
                
        }
    }
}
