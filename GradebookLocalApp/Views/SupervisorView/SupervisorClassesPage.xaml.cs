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

namespace GradebookLocalApp.Views.SupervisorView
{
    /// <summary>
    /// Logika interakcji dla klasy SupervisorClassesPage.xaml
    /// </summary>
    public partial class SupervisorClassesPage : Page
    {

        Blades.SupervisorNewClassBlade _newClassBlade;
        Blades.SupervisorClassBlade _classBlade;
        ContentWindow _window;
        public SupervisorClassesPage(ContentWindow window)
        {
            _window = window;
            _newClassBlade = new Blades.SupervisorNewClassBlade(this);
            _classBlade = new Blades.SupervisorClassBlade(this,_window);
            InitializeComponent();

            UpdateClassView();
        }

        internal void UpdateClassView()
        {
            ClassesView.SelectionChanged -= TaskView_SelectionChanged;
            ClassesView.SelectedItem = null;
            using (Model.TestEntities entity = new Model.TestEntities())
            {
                ClassesView.Items.Clear();
                foreach (var item in entity.zajecia)
                {
                    var subjectName = entity.przedmioty.Where(x => x.id_przed == item.id_przed).FirstOrDefault();
                    var teacherName = entity.prowadzacy.Where(x => x.id_prow == item.id_prow).FirstOrDefault();
                    var typeName = entity.typy_zajec.Where(x => x.id_typ == item.id_typ).FirstOrDefault();
                    ClassesView.Items.Add(new Model.ClassViewModel {SubjectName = subjectName.nazwa_przed, TeacherName = teacherName.imie_prowadzacy + " " + teacherName.nazwisko_prowadzacy, IdClass = item.id_zajec, TypeName = typeName.nazwa_typ});
                }
            }
            ClassesView.SelectionChanged += TaskView_SelectionChanged;
        }

        

        private void NewGroup_Click(object sender, RoutedEventArgs e)
        {
            SupClassBlade.Children.Clear();
            SupClassBlade.Children.Add(_newClassBlade);
        }

        private void TaskView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
                using (var entity = new Model.TestEntities())
                {

                    var item = (Model.ClassViewModel)ClassesView.SelectedItem;
                    var classe = entity.zajecia.Where(s => s.id_zajec == item.IdClass).FirstOrDefault();

                    _classBlade.SetClass(classe);

                }


            
            SupClassBlade.Children.Clear();
            SupClassBlade.Children.Add(_classBlade);
        }
    }
}
