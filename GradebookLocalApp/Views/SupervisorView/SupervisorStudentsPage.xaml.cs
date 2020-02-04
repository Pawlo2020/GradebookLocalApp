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
    /// Logika interakcji dla klasy SupervisorStudentsPage.xaml
    /// </summary>
    public partial class SupervisorStudentsPage : Page
    {
        Blades.SupervisorNewStudentBlade _newStudentBlade;
        Blades.SupervisorStudentBlade _studentBlade;
        public SupervisorStudentsPage()
        {
            _newStudentBlade = new Blades.SupervisorNewStudentBlade(this);
            _studentBlade = new Blades.SupervisorStudentBlade(this);
            InitializeComponent();

            UpdateStudentsView();
        }

        public void UpdateStudentsView()
        {
            StudentsView.SelectedItem = null;
            using (Model.TestEntities entity = new Model.TestEntities())
            {
                StudentsView.Items.Clear();
                foreach (var item in entity.studenci)
                {
                    StudentsView.Items.Add(new Model.studenci { nr_album = item.nr_album, imie_student = item.imie_student, nazwisko_student = item.nazwisko_student, pesel_student = item.pesel_student });
                }

            }
                
        }

        private void NewStudent_Click(object sender, RoutedEventArgs e)
        {
            SupStudentBlade.Children.Clear();
            SupStudentBlade.Children.Add(_newStudentBlade);
        }

        private void StudentsView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                using (var entity = new Model.TestEntities())
                {

                    var item = (Model.studenci)StudentsView.SelectedItem;
                    var student = entity.studenci.Where(s => s.nr_album == item.nr_album).FirstOrDefault();


                    _studentBlade.SetStudent(student);

                }
            }
            catch { }
                

            SupStudentBlade.Children.Clear();
            SupStudentBlade.Children.Add(_studentBlade);
        }
    }
}
