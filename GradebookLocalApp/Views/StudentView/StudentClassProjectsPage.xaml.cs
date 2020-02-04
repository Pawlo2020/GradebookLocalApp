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

namespace GradebookLocalApp.Views.StudentView
{
    /// <summary>
    /// Logika interakcji dla klasy StudentClassProjectsPage.xaml
    /// </summary>
    public partial class StudentClassProjectsPage : Page
    {
        Model.zajecia _classe;
        Model.studenci _student;
        public StudentClassProjectsPage(Model.zajecia classe, Model.studenci student)
        {
            _student = student;
            _classe = classe;
            InitializeComponent();

            UpdateView();
        }

        private void UpdateView()
        {
            using (var entity = new Model.TestEntities())
            {
                //var projects = entity.projectsbyteacherandclass(_classe.id_zajec.ToString(),_classe.id_prow.ToString());

                try
                {
                    var projects = entity.projekty.Where(x => x.id_zajec == _classe.id_zajec);

                    foreach (var item in projects)
                    {
                        ProjectsView.Items.Add(item);
                    }


                }
                catch (Exception)
                {

                }
            }
        }

        private void ApplyBut(object sender, RoutedEventArgs e)
        {
            
            using (var entity = new Model.TestEntities())
            {
                var project = (Model.projekty)ProjectsView.SelectedItem;

                //entity.projekty.Where(x => x.id_proj == project.id_proj).FirstOrDefault().studenci.Add(entity.studenci.Where(a => a.id_student == _student.id_student).FirstOrDefault());
                entity.Database.ExecuteSqlCommand($"INSERT INTO dbo.zgloszenia (id_student, id_proj, data_zgloszenia) VALUES({_student.id_student},{project.id_proj}, '{DateTime.Now.ToString()}')");
                
                entity.SaveChanges();
            }
        }

        private void TaskView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
