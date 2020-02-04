using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GradebookLocalApp.Views.SupervisorView.Blades
{
    /// <summary>
    /// Logika interakcji dla klasy SupervisorStudentsListPage.xaml
    /// </summary>
    public partial class SupervisorStudentsListPage : Page
    {
        Model.grupy_dziekanskie _group;
        Model.zajecia _classe;
        Blades.SupervisorAddStudentBlade _addStudentBlade;
        Blades.SupervisorStudentsInClass _studentInClass;
        public SupervisorStudentsListPage(Model.zajecia _class, Model.grupy_dziekanskie group)
        {
            _classe = _class;
            _group = group;
            InitializeComponent();
            UpdateView();
            ClassNameLbl.Content = "Lista studentów";
        }

        private void NewGroup_Click(object sender, RoutedEventArgs e)
        {
            _addStudentBlade = new SupervisorAddStudentBlade(this,_group, _classe);
            ClassBlade.Children.Clear();
            ClassBlade.Children.Add(_addStudentBlade);
        }

        private void TaskView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var student = ((Model.studenci)StudentsView.SelectedItem);
                _studentInClass = new SupervisorStudentsInClass(this, student, _classe);
                _studentInClass.SetData(student);
                ClassBlade.Children.Clear();
                ClassBlade.Children.Add(_studentInClass);
            }
            catch (Exception) { }
        }

        internal void UpdateView()
        {
            using (var entity = new Model.TestEntities())
            {
                StudentsView.Items.Clear();
                try
                {
                    var students = entity.studenci.Where(x => x.zajecia.Contains(entity.zajecia.Where(y=>y.id_zajec==_classe.id_zajec).FirstOrDefault()));
                    StudentsView.Items.Clear();
                    foreach (var item in students)
                    {
                        StudentsView.Items.Add(item);
                    }
                }
                catch (Exception) { }
            }
        }
    }
}
