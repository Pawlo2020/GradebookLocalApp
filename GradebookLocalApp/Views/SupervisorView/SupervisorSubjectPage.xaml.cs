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
    /// Logika interakcji dla klasy SupervisorSubjectPage.xaml
    /// </summary>
    public partial class SupervisorSubjectPage : Page
    {
        Blades.SupervisorNewSubject _newSubjectBlade;
        Blades.SupervisorSubjectBlade _subjectBlade;
        public SupervisorSubjectPage()
        {
            _newSubjectBlade = new Blades.SupervisorNewSubject(this);
            _subjectBlade = new Blades.SupervisorSubjectBlade(this);
            InitializeComponent();

            UpdateSubjectsView();
        }

        internal void UpdateSubjectsView()
        {
            SubjectView.SelectedItem = null;
            using (Model.TestEntities entity = new Model.TestEntities())
            {
                SubjectView.Items.Clear();
                foreach (var item in entity.przedmioty)
                {
                    var teacher = entity.prowadzacy.Where(x => x.id_prow == item.id_prow).FirstOrDefault();
                    SubjectView.Items.Add(new Model.SubjectViewModel { IdSubject = item.id_przed, SubjectName=item.nazwa_przed, TeacherName = teacher.imie_prowadzacy, TeacherLastName = teacher.nazwisko_prowadzacy});
                }

            }
        }

        private void NewSubject_Click(object sender, RoutedEventArgs e)
        {
            SupSubjectBlade.Children.Clear();
            SupSubjectBlade.Children.Add(_newSubjectBlade);
        }

        private void SubjectView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
                using (var entity = new Model.TestEntities())
                {
                try
                {
                    var item = (Model.SubjectViewModel)SubjectView.SelectedItem;
                    var subject = entity.przedmioty.Where(s => s.id_przed == item.IdSubject).FirstOrDefault();
                    _subjectBlade.SetSubject(subject);
                }
                catch (Exception) { }

                    

                }
            


            SupSubjectBlade.Children.Clear();
            SupSubjectBlade.Children.Add(_subjectBlade);
        }
    }
}
