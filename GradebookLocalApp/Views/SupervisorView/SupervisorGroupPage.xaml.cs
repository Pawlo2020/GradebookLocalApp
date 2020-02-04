using System;
using System.Collections.Generic;
using System.Linq;
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

namespace GradebookLocalApp.Views.SupervisorView
{
    /// <summary>
    /// Logika interakcji dla klasy SupervisorGroupPage.xaml
    /// </summary>
    public partial class SupervisorGroupPage : Page
    {
        Blades.SupervisorNewGroupBlade _newGroupBlade;
        Blades.SupervisorGroupBlade _groupBlade;
        public SupervisorGroupPage()
        {
            _newGroupBlade = new Blades.SupervisorNewGroupBlade(this);
            _groupBlade = new Blades.SupervisorGroupBlade(this);
            InitializeComponent();
            TeacherComboViewModel vmCombo = new TeacherComboViewModel();
            SubjectCombo.ItemsSource = vmCombo.Teacher;

            UpdateGroupsView();
        }

        private void NewGroup_Click(object sender, RoutedEventArgs e)
        {
            SupGroupBlade.Children.Clear();
            SupGroupBlade.Children.Add(_newGroupBlade);
        }

        public void UpdateGroupsView()
        {
            GroupsView.SelectedItem = null;
            using (Model.TestEntities entity = new Model.TestEntities())
            {
                GroupsView.Items.Clear();
                foreach (var item in entity.grupy_dziekanskie)
                {
                    GroupsView.Items.Add(new Model.grupy_dziekanskie {id_grupy = item.id_grupy,nazwa_grupy = item.nazwa_grupy });
                }
            }
        }

        private void TaskView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                using (var entity = new Model.TestEntities())
                {

                    var item = (Model.grupy_dziekanskie)GroupsView.SelectedItem;
                    var group = entity.grupy_dziekanskie.Where(s => s.id_grupy == item.id_grupy).FirstOrDefault();

                    _groupBlade.SetGroup(group);

                }
            }
            catch { }

            SupGroupBlade.Children.Clear();
            SupGroupBlade.Children.Add(_groupBlade);
        }

        private void SubjectCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateViewTeacher();
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

        private void UpdateViewTeacher()
        {
            GroupsView.SelectedItem = null;
            using (Model.TestEntities entity = new Model.TestEntities())
            {
                GroupsView.Items.Clear();
                var teacher = ((Model.prowadzacy)SubjectCombo.SelectedItem).id_prow.ToString();
                var groups = entity.teacherbyid1(teacher);


                foreach (var item in groups)
                {
                    GroupsView.Items.Add(new Model.grupy_dziekanskie { id_grupy = Convert.ToInt32(item.id_grupy), nazwa_grupy = item.nazwa_grupy });
                }
            }
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            UpdateGroupsView();
        }
    }
}
