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

namespace GradebookLocalApp.Views.StudentView.Blades
{
    /// <summary>
    /// Logika interakcji dla klasy StudentProjectBlade.xaml
    /// </summary>
    public partial class StudentProjectBlade : UserControl
    {
        Model.projekty _project;
        Model.studenci _student;
        public StudentProjectBlade(Model.projekty project, Model.studenci student)
        {
            _project = project;
            _student = student;
            InitializeComponent();
            
        }

        public void SetData()
        {
            using (var entity = new Model.TestEntities())
            {
                var stages = entity.etapy.Where(x => x.id_proj == _project.id_proj);
                try
                {
                    StagesView.Items.Clear();
                    foreach (var item in stages)
                    {
                        StagesView.Items.Add(new Model.StageViewModel { Id = item.id_etap, Date = item.termin_etapu.ToString(), Stage = item.nazwa_etapu });
                    }

                    Project.Text = entity.oceny_projektow.Where(x => x.id_student == _student.id_student && x.id_proj == _project.id_proj).FirstOrDefault().wartosc_oceny_proj;
                }
                catch (Exception) { }
            }



        }

        private void TaskView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentStage = ((Model.StageViewModel)StagesView.SelectedItem);

            using (var entity = new Model.TestEntities())
            {
                Stage.Text = entity.oceny_etapow.Where(x => x.id_etap == currentStage.Id && x.id_student == _student.id_student).FirstOrDefault().wartosc;


            }

                
        }
    }
}
