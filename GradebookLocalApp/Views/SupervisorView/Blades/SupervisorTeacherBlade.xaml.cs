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
    /// Logika interakcji dla klasy SupervisorTeacherBlade.xaml
    /// </summary>
    public partial class SupervisorTeacherBlade : UserControl
    {
        private SupervisorTeacherPage _page;
        private Model.prowadzacy _teacher;

        public SupervisorTeacherBlade(SupervisorTeacherPage supervisorTeacherPage)
        {

            _page = supervisorTeacherPage;
            InitializeComponent();
            TeacherTypeComboViewModel vmComboTeacher = new TeacherTypeComboViewModel();
            TeacherTypeCombo.ItemsSource = vmComboTeacher.Type;
        }

        internal void SetTeacher(Model.prowadzacy teacher)
        {
            _teacher = teacher;

            Index.Text = teacher.haslo;
            Name.Text = teacher.imie_prowadzacy;
            SecondName.Text = teacher.drugie_imie_prowadzacy;
            LastName.Text = teacher.nazwisko_prowadzacy;
            BirthDate.Text = teacher.data_urodzenia_prowadzacy.ToString();
            BirthPlace.Text = teacher.miejsce_urodzenia_prowadzacy;
            PESEL.Text = teacher.pesel_prowadzacy;
            EMAIL.Text = teacher.email_prowadzacy;
            Phone.Text = teacher.telefon_prowadzacy;

            using (var entity = new TestEntities())
            {
                var type = entity.typy_prowadzacych.Where(x => x.id_typ_prowadzacy == teacher.id_typ_prowadzacy).FirstOrDefault();
                

                for (int i = 0; i < TeacherTypeCombo.Items.Count; i++)
                {

                    if ((TeacherTypeCombo.Items[i] as typy_prowadzacych).id_typ_prowadzacy == teacher.id_typ_prowadzacy)
                    {
                        TeacherTypeCombo.SelectedIndex = i;

                    }
                }
            }
        }

        internal class TeacherTypeComboViewModel
        {
            public List<Model.typy_prowadzacych> Type { get; set; }
            public TeacherTypeComboViewModel()
            {
                Type = new List<Model.typy_prowadzacych>();
                using (var entity = new TestEntities())
                {
                    foreach(var item in entity.typy_prowadzacych)
                    {
                        Type.Add(item);
                    }
                }
            }
        }
        
        private void AddBut_Click(object sender, RoutedEventArgs e)
        {
            using (var entity = new Model.TestEntities())   //Uchwyt
            {
                var a = entity.prowadzacy.Where<Model.prowadzacy>(x => x.id_prow == _teacher.id_prow).FirstOrDefault(); //pobieram istniejacy rekord z bazy

                //Ustawiam w danym rekordzie teksty z kolejnych pól tekstowych
                a.haslo = Index.Text;
                a.imie_prowadzacy = Name.Text;
                a.drugie_imie_prowadzacy = SecondName.Text;
                a.nazwisko_prowadzacy = LastName.Text;
                a.data_urodzenia_prowadzacy = Convert.ToDateTime(BirthDate.Text);
                a.miejsce_urodzenia_prowadzacy = BirthPlace.Text;
                a.pesel_prowadzacy = PESEL.Text;
                a.email_prowadzacy = EMAIL.Text;
                a.telefon_prowadzacy = Phone.Text;

                //Zapisuje zmiany i aktualizuje widok tabeli w innej klasie
                entity.SaveChanges();
                _page.UpdateTeacherView();
            }
        }

        private void TeacherTypeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DeleteBut_Click(object sender, RoutedEventArgs e)
        {
            using (var entity = new TestEntities())
            {
               var a=  entity.prowadzacy.Where(x => x.id_prow == _teacher.id_prow).FirstOrDefault();
                entity.prowadzacy.Remove(a);
                entity.SaveChanges();
                _page.UpdateTeacherView();

            }
        }
    }
}
