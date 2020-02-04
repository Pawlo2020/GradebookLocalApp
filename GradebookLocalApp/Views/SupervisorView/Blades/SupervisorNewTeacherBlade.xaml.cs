using System;
using System.Collections.Generic;
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

namespace GradebookLocalApp.Views.SupervisorView.Blades
{
    /// <summary>
    /// Logika interakcji dla klasy SupervisorNewTeacherBlade.xaml
    /// </summary>
    public partial class SupervisorNewTeacherBlade : UserControl
    {
        private SupervisorTeacherPage _page;
        public SupervisorNewTeacherBlade(SupervisorTeacherPage page)
        {
            _page = page;
            InitializeComponent();
        }

        private void TeacherTypeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddBut_Click(object sender, RoutedEventArgs e)
        {
            using (Model.TestEntities ent = new Model.TestEntities())
            {
                Model.prowadzacy st = new Model.prowadzacy
                {
                    imie_prowadzacy = Name.Text,
                    drugie_imie_prowadzacy = LastName.Text,
                    nazwisko_prowadzacy = LastName.Text,
                    haslo = Index.Text,
                    miejsce_urodzenia_prowadzacy = BirthPlace.Text,
                    data_urodzenia_prowadzacy = Convert.ToDateTime(BirthDate.Text),
                    pesel_prowadzacy = PESEL.Text,
                    telefon_prowadzacy = Phone.Text,
                    email_prowadzacy = EMAIL.Text,
                    id_typ_prowadzacy = Convert.ToInt32(((ComboBoxItem)TeacherTypeCombo.SelectedItem).Tag)
                };
                ent.prowadzacy.Add(st);

                ent.SaveChanges();
                _page.UpdateTeacherView();
            }
        }
    }
}
