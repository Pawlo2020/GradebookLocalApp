using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GradebookLocalApp.Views.TeacherView.Blades
{
    /// <summary>
    /// Logika interakcji dla klasy TeacherProjectBlade.xaml
    /// </summary>
    public partial class TeacherProjectBlade : UserControl
    {
        Model.prowadzacy _teacher;
        Model.projekty _project;
        public TeacherProjectBlade(Model.prowadzacy teacher, Model.projekty project)
        {
            _teacher = teacher;
            _project = project;
            InitializeComponent();
            UpdateStagesView();
        }

        private void DeleteBut_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddBut_Click(object sender, RoutedEventArgs e)
        {
            using (var entity = new Model.TestEntities())
            {
                Model.etapy stage = new Model.etapy()
                {
                    id_proj = _project.id_proj,
                    nazwa_etapu = StageName.Text,
                    termin_etapu = DateStage.SelectedDate
                    

                };

                entity.etapy.Add(stage);
                entity.SaveChanges();
                UpdateStagesView();


            }
        }

        private void UpdateStagesView()
        {

            using (var entity = new Model.TestEntities())
            {
                var stages = entity.etapy.Where(x => x.id_proj == _project.id_proj);
                try
                {
                    StagesView.Items.Clear();
                    foreach(var item in stages)
                    {
                        StagesView.Items.Add(new Model.StageViewModel {Id=item.id_etap, Date = item.termin_etapu.ToString(),Stage = item.nazwa_etapu });
                    }
                }
                catch (Exception) { }
            }
        }

        internal void SetProject()
        {
            ProjectDate.Text = _project.termin_wyk.ToString();
            Project.Text = _project.nazwa_proj;
            StagesView.Items.Clear();
            using (var entity = new Model.TestEntities())
            {
                var stages = entity.etapy.Where(x => x.id_proj == _project.id_proj);
                try
                {
                    foreach(var item in stages)
                    {
                        StagesView.Items.Add(new Model.StageViewModel {Date = item.termin_etapu.ToString(), Id = item.id_etap, Stage = item.nazwa_etapu, });
                    }
                }
                catch (Exception)
                {}
            }
        }

        private void TaskView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
