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

namespace GradebookLocalApp.Views.SupervisorView.Blades
{
    /// <summary>
    /// Logika interakcji dla klasy SupervisorNewClassBlade.xaml
    /// </summary>
    public partial class SupervisorNewClassBlade : UserControl
    {
        private SupervisorClassesPage _page;
        public SupervisorNewClassBlade(SupervisorClassesPage supervisorClassesPage)
        {
            _page = supervisorClassesPage;
            InitializeComponent();

            SubjectComboViewModel vmComboSubject = new SubjectComboViewModel();
            SubjectCombo.ItemsSource = vmComboSubject.Subject;

            TeacherComboViewModel vmComboTeacher = new TeacherComboViewModel();
            TeacherCombo.ItemsSource = vmComboTeacher.Teacher;

            ClassTypeComboViewModel vmComboClassType = new ClassTypeComboViewModel();
            ClassTypeCombo.ItemsSource = vmComboClassType.ClassType;

            
        }

        private void UpdateBut_Click(object sender, RoutedEventArgs e)
        {
            using (Model.TestEntities ent = new Model.TestEntities())
            {
                Model.zajecia st = new Model.zajecia
                {
                    data_zaj = Convert.ToDateTime(ClassDate.Text),
                    id_przed = ((Model.przedmioty)SubjectCombo.SelectedItem).id_przed,
                    id_prow = ((Model.prowadzacy)TeacherCombo.SelectedItem).id_prow,
                    id_typ = ((Model.typy_zajec)ClassTypeCombo.SelectedItem).id_typ
                };
                ent.zajecia.Add(st);

                ent.SaveChanges();
                _page.UpdateClassView();
            }
        }

        class SubjectComboViewModel
        {
            public List<Model.przedmioty> Subject { get; set; }

            public SubjectComboViewModel()
            {
                using (var entity = new Model.TestEntities())
                {
                    Subject = new List<Model.przedmioty>();
                    foreach (var item in entity.przedmioty)
                    {
                        Subject.Add(item);
                    }
                }
            }

        }

        class ClassTypeComboViewModel
        {
            public List<Model.typy_zajec> ClassType { get; set; }

            public ClassTypeComboViewModel()
            {
                using (var entity = new Model.TestEntities())
                {
                    ClassType = new List<Model.typy_zajec>();
                    foreach (var item in entity.typy_zajec)
                    {
                        ClassType.Add(item);
                    }
                }
            }
        }

        

        class TeacherComboViewModel
        {
            public List<Model.prowadzacy> Teacher { get; set; }

            public TeacherComboViewModel()
            {
                using (var entity = new Model.TestEntities())
                {
                    Teacher = new List<Model.prowadzacy>();
                    foreach (var item in entity.prowadzacy)
                    {
                        Teacher.Add(item);
                    }
                }
            }

        }

        private void AddGroupClick(object sender, RoutedEventArgs e)
        {

        }

        private void GroupCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
