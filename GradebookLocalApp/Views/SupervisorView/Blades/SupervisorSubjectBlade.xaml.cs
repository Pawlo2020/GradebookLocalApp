using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using GradebookLocalApp.Model;
using System.Linq;
using System;

namespace GradebookLocalApp.Views.SupervisorView.Blades
{
    /// <summary>
    /// Logika interakcji dla klasy SupervisorSubjectBlade.xaml
    /// </summary>
    public partial class SupervisorSubjectBlade : UserControl
    {
        private SupervisorSubjectPage _page;
        private Model.przedmioty _subject;
        public SupervisorSubjectBlade(SupervisorSubjectPage supervisorSubjectPage)
        {
            _page = supervisorSubjectPage;
            InitializeComponent();
            TeacherComboViewModel vmCombo = new TeacherComboViewModel();
            TeacherCombo.ItemsSource = vmCombo.Teacher;
        }

        private void UpdateSubBut_Click(object sender, RoutedEventArgs e)
        {
            using (Model.TestEntities entity = new TestEntities())
            {
                var a = entity.przedmioty.Where(x => x.id_przed == _subject.id_przed).FirstOrDefault();
                var prow = (TeacherCombo.Items[TeacherCombo.SelectedIndex] as prowadzacy).id_prow;
                a.id_prow = prow;
                a.nazwa_przed = Subject.Text;

                entity.SaveChanges();
                _page.UpdateSubjectsView();
            }
        }

        class TeacherComboViewModel
        {
            public List<Model.prowadzacy> Teacher { get; set; }

            public TeacherComboViewModel()
            {
                using (var entity = new Model.TestEntities())
                {
                    Teacher = new List<Model.prowadzacy>();
                    foreach (var item in entity.prowadzacy)
                    {
                        Teacher.Add(item);
                    }
                }
            }
        }

        internal void SetSubject(przedmioty subject)
        {
            _subject = subject;

            Subject.Text = subject.nazwa_przed;
            using (var entity = new Model.TestEntities())
            {
                var prow = entity.prowadzacy.Where(x => x.id_prow == _subject.id_prow).FirstOrDefault();
                for (int i = 0; i < TeacherCombo.Items.Count; i++)
                {
                    if ((TeacherCombo.Items[i] as prowadzacy).id_prow == prow.id_prow)
                    {
                        TeacherCombo.SelectedIndex = i;
                    }
                }
            }
        }

        private void DeleteBut_Click(object sender, RoutedEventArgs e)
        {
            using (var entity = new Model.TestEntities())
            {

                var a = entity.przedmioty.Where<Model.przedmioty>(x => x.id_przed==_subject.id_przed).FirstOrDefault();

                entity.przedmioty.Remove(a);

                try
                {
                    entity.SaveChanges();
                    _page.SupSubjectBlade.Children.Clear();
                    _page.UpdateSubjectsView();
                }
                catch (Exception) { Warning.Visibility = Visibility.Visible; }
            }
        }
    }
}
