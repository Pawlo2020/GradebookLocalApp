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
using GradebookLocalApp.Model;

namespace GradebookLocalApp.Views.SupervisorView.Blades
{
    /// <summary>
    /// Logika interakcji dla klasy SupervisorAddStudentBlade.xaml
    /// </summary>
    public partial class SupervisorAddStudentBlade : UserControl
    {
        Model.grupy_dziekanskie _group;
        Model.zajecia _classe;
        private SupervisorStudentsListPage supervisorStudentsListPage;
        private grupy_dziekanskie group;
        private zajecia classe;

        public SupervisorAddStudentBlade(Model.grupy_dziekanskie grupy, Model.zajecia classe)
        {
            _group = grupy;
            _classe = classe;
            InitializeComponent();
            UpdateView();
        }

        public SupervisorAddStudentBlade(SupervisorStudentsListPage supervisorStudentsListPage, grupy_dziekanskie group, zajecia classe)
        {
            this.supervisorStudentsListPage = supervisorStudentsListPage;
            _group = group;
            _classe = classe;
            InitializeComponent();
            UpdateView();
        }

        private void UpdateView()
        {
            using (var entity = new Model.TestEntities())
            {
                //var students = entity.studenci.Where(x => x.id_grupy == _group.id_grupy);
                var students = entity.studentsbygroup(_group.id_grupy.ToString());
                //students = entity.Database.
                try
                {
                    foreach (var item in students)
                    {
                        StudentsView.Items.Add(item);

                    }
                }
                catch (Exception)
                {

                }
                


            }
        }

        private void StudentsView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void UpdateBut_Click(object sender, RoutedEventArgs e)
        {
            var student = ((Model.studentsbygroup_Result)StudentsView.SelectedItem);
            

            using (var entity = new Model.TestEntities())
            {
                var id = entity.studenci.Where(x => x.nr_album == student.nr_album).FirstOrDefault().id_student;
                entity.zajecia.Where(x => x.id_zajec == _classe.id_zajec).FirstOrDefault().studenci.Add(entity.studenci.Where(y => y.id_student == id).FirstOrDefault());

                entity.SaveChanges();

                supervisorStudentsListPage.UpdateView();



            }

        }
    }
}
