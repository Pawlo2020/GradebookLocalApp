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

namespace GradebookLocalApp.Views.TeacherView
{
    /// <summary>
    /// Logika interakcji dla klasy TeacherSubjectsPage.xaml
    /// </summary>
    public partial class TeacherSubjectsPage : Page
    {
        SubjectPage _subjectPage;
        ContentWindow _window;
        Model.prowadzacy _teacher;
        public TeacherSubjectsPage(Model.prowadzacy teacher , ContentWindow window)
        {
            _teacher = teacher;
            _window = window;
            InitializeComponent();

            using (var entity = new Model.TestEntities())
            {
                var subjectView = entity.przedmioty.Where(x => x.id_prow == teacher.id_prow);

                try
                {
                    foreach (var item in subjectView)
                    {
                        SubjectView.Items.Add(new Model.SubjectViewModel { IdSubject = item.id_przed, SubjectName = item.nazwa_przed, TeacherName = item.prowadzacy.imie_prowadzacy, TeacherLastName = item.prowadzacy.nazwisko_prowadzacy });

                    }
                }
                catch (Exception e)
                {}

            }


        }

        private void SubjectView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = ((Model.SubjectViewModel)SubjectView.SelectedItem);
            using (var entity = new Model.TestEntities())
            {
                var subject = entity.przedmioty.Where(x => x.id_przed == item.IdSubject).FirstOrDefault();
                _subjectPage = new SubjectPage(subject, _teacher);
                SupSubjectBlade.Children.Clear();
                _window.ContentFrame.Content = _subjectPage;
            }
            
        }
    }
}
