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
    /// Logika interakcji dla klasy SupervisorNewStudentBlade.xaml
    /// </summary>
    public partial class SupervisorNewStudentBlade : UserControl
    {
        private SupervisorStudentsPage _page;

        public SupervisorNewStudentBlade(SupervisorStudentsPage page)
        {
            _page = page;
            InitializeComponent();
            GroupComboViewModel vmCombo = new GroupComboViewModel();
            GroupCombo.ItemsSource = vmCombo.Group;
        }

        private void UpdateBut_Click(object sender, RoutedEventArgs e)
        {
            using (Model.TestEntities ent = new Model.TestEntities())
            {
                Model.studenci st = new Model.studenci {imie_student = Name.Text, drugie_imie_student = LastName.Text, nazwisko_student = LastName.Text,
                    nr_album = Index.Text, miejsce_urodzenia_student = BirthPlace.Text, data_urodzenia_student = Convert.ToDateTime(BirthDate.Text), pesel_student = PESEL.Text,
                    telefon_student = Phone.Text, email_student = EMAIL.Text, id_grupy = ((Model.grupy_dziekanskie)(GroupCombo.SelectedItem)).id_grupy};
                ent.studenci.Add(st);
                
                ent.SaveChanges();
                _page.UpdateStudentsView();

            }
        }

        class GroupComboViewModel
        {
            public List<Model.grupy_dziekanskie> Group { get; set; }

            public GroupComboViewModel()
            {
                using (var entity = new Model.TestEntities())
                {
                    Group = new List<Model.grupy_dziekanskie>();
                    foreach (var item in entity.grupy_dziekanskie)
                    {
                        Group.Add(item);
                    }
                }
            }

        }
    }
}
