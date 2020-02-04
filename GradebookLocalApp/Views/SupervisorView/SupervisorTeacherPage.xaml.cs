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
    /// Logika interakcji dla klasy SupervisorTeacherPage.xaml
    /// </summary>
    public partial class SupervisorTeacherPage : Page
    {
        Blades.SupervisorNewTeacherBlade _newTeacherBlade;
        Blades.SupervisorTeacherBlade _TeacherBlade;
        public SupervisorTeacherPage()
        {
            _newTeacherBlade = new Blades.SupervisorNewTeacherBlade(this);
            _TeacherBlade = new Blades.SupervisorTeacherBlade(this);
            InitializeComponent();

            UpdateTeacherView();
        }

        internal void UpdateTeacherView()
        {
            TeacherView.SelectedItem = null;
            using (Model.TestEntities entity = new Model.TestEntities())
            {
                TeacherView.Items.Clear();
                
                foreach (var item in entity.prowadzacy)
                {
                    var type = entity.typy_prowadzacych.Where(x => x.id_typ_prowadzacy == item.id_typ_prowadzacy).FirstOrDefault();
                    TeacherView.Items.Add(new Model.TeacherViewModel {Id = item.id_prow, Imie = item.imie_prowadzacy, Nazwisko = item.nazwisko_prowadzacy, Pesel = item.pesel_prowadzacy, Typ = type.typ_prowadzacy});
                }

            }
        }

        private void NewSubject_Click(object sender, RoutedEventArgs e)
        {
            SupTeacherBlade.Children.Clear();
            SupTeacherBlade.Children.Add(_newTeacherBlade);
        }

        private void TeacherView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                using (var entity = new Model.TestEntities())
                {

                    var item = (Model.TeacherViewModel)TeacherView.SelectedItem;
                    var teacher = entity.prowadzacy.Where(s => s.id_prow == item.Id).FirstOrDefault();

                    _TeacherBlade.SetTeacher(teacher);

                }
            }
            catch { }


            SupTeacherBlade.Children.Clear();
            SupTeacherBlade.Children.Add(_TeacherBlade);
        }
    }
}
