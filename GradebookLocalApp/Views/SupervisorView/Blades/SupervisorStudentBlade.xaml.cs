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

namespace GradebookLocalApp.Views.SupervisorView.Blades
{
    /// <summary>
    /// Logika interakcji dla klasy SupervisorStudentBlade.xaml
    /// </summary>
    public partial class SupervisorStudentBlade : UserControl
    {
        private SupervisorStudentsPage _page;
        private Model.studenci _student;
        public SupervisorStudentBlade(SupervisorStudentsPage page)
        {
            _page = page;
            InitializeComponent();
            GroupComboViewModel vmCombo = new GroupComboViewModel();
            GroupCombo.ItemsSource = vmCombo.Group;


        }

        private void DeleteBut_Click(object sender, RoutedEventArgs e)
        {
            using (var entity = new Model.TestEntities())
            {

                var a= entity.studenci.Where<Model.studenci>(x => x.nr_album == _student.nr_album).FirstOrDefault();
                
                entity.studenci.Remove(a);
                
                try
                {
                    entity.SaveChanges();
                    _page.SupStudentBlade.Children.Clear();
                    _page.UpdateStudentsView();
                }
                catch (Exception) { Warning.Visibility = Visibility.Visible; }
                

            }
        }

        internal void SetStudent(Model.studenci student)
        {
            _student = student;

            Index.Text = student.nr_album;
            Name.Text = student.imie_student;
            SecondName.Text = student.drugie_imie_student;
            LastName.Text = student.nazwisko_student;
            BirthDate.Text = student.data_urodzenia_student.ToString();
            BirthPlace.Text = student.miejsce_urodzenia_student;
            PESEL.Text = student.pesel_student;
            EMAIL.Text = student.email_student;
            Phone.Text = student.telefon_student;

            using (var entity = new Model.TestEntities())
            {
                var group = entity.grupy_dziekanskie.Where(x => x.id_grupy == _student.id_grupy).FirstOrDefault();

                for (int i = 0; i < GroupCombo.Items.Count; i++)
                {

                    if ((GroupCombo.Items[i] as Model.grupy_dziekanskie).id_grupy == group.id_grupy)
                    {
                        GroupCombo.SelectedIndex = i;

                    }
                }

            }

        }

        private void UpdateBut_Click(object sender, RoutedEventArgs e)
        {
            using (var entity = new Model.TestEntities())
            {
                var a = entity.studenci.Where<Model.studenci>(x => x.nr_album == _student.nr_album).FirstOrDefault();
                var group = (GroupCombo.SelectedItem as Model.grupy_dziekanskie).id_grupy;
                a.nr_album = Index.Text;
                a.imie_student = Name.Text;
                a.drugie_imie_student = SecondName.Text;
                a.nazwisko_student = LastName.Text;
                a.data_urodzenia_student = Convert.ToDateTime(BirthDate.Text);
                a.miejsce_urodzenia_student = BirthPlace.Text;
                a.pesel_student = PESEL.Text;
                a.email_student = EMAIL.Text;
                a.telefon_student = Phone.Text;
                a.id_grupy = group;

                entity.SaveChanges();
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
