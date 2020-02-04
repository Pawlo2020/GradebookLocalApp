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

namespace GradebookLocalApp.Views.TeacherView.Blades
{
    /// <summary>
    /// Logika interakcji dla klasy TeacherApplicationBlade.xaml
    /// </summary>
    public partial class TeacherApplicationBlade : UserControl
    {
        Model.projekty _project;
        ProjectPage _projectPage;
        public TeacherApplicationBlade(Model.projekty project, ProjectPage projectPage)
        {
            _projectPage = projectPage;
            _project = project;
            InitializeComponent();
            UpdateView();
        }

        internal class ApplicationSchema
        {
            public int id_proj { get; set; }
            public int id_student { get; set; }
            public DateTime data_zgloszenia { get; set; }

        }

        private void UpdateView()
        {
            using (var entity = new Model.TestEntities())
            {
                var query = entity.Database.SqlQuery<ApplicationSchema>($"SELECT * FROM dbo.zgloszenia WHERE id_proj = {_project.id_proj}").ToList();
                
                try
                {
                    foreach(var item in query)
                    {
                        var student = entity.studenci.Where(x => x.id_student == item.id_student).FirstOrDefault();
                        ApplicationsView.Items.Add(new Model.ApplicationViewModel { projId = item.id_proj, studId = item.id_student, Date = item.data_zgloszenia.ToString(), StudentName = student.imie_student, StudentLastName = student.nazwisko_student });

                    }

                }
                catch (Exception) { }
            }
        }

        private void TaskView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddBut_Click(object sender, RoutedEventArgs e)
        {
            var app = ((Model.ApplicationViewModel)ApplicationsView.SelectedItem);

            using (var entity = new Model.TestEntities())
            {
                entity.projekty.Where(x => x.id_proj == app.projId).FirstOrDefault().studenci.Add(entity.studenci.Where(a => a.id_student ==app.studId).FirstOrDefault());

                entity.SaveChanges();

                entity.Database.ExecuteSqlCommand($"DELETE FROM dbo.zgloszenia WHERE id_proj ={app.projId} and id_student = {app.studId}");

                UpdateView();
                _projectPage.UpdateView();



            }
        }
    }
}
