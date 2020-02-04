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
    /// Logika interakcji dla klasy SupervisorNewSubject.xaml
    /// </summary>
    public partial class SupervisorNewSubject : UserControl
    {
        SupervisorSubjectPage _page;
        public SupervisorNewSubject(SupervisorSubjectPage page)
        {
            _page = page;
            InitializeComponent();
            TeacherComboViewModel vmCombo = new TeacherComboViewModel();
            SubjectCombo.ItemsSource = vmCombo.Teacher;
        }


        class TeacherComboViewModel
        {
            public List<Model.prowadzacy> Teacher { get; set; }

            public TeacherComboViewModel()
            {
                using (var entity = new Model.TestEntities())
                {
                    Teacher = new List<Model.prowadzacy>();
                    foreach(var item in entity.prowadzacy)
                    {
                        Teacher.Add(item);
                    }
                }
            }
        }

   

        internal void UpdateBut_Click(object sender, RoutedEventArgs e)
        {
            using (Model.TestEntities ent = new Model.TestEntities())
            {
                Model.przedmioty su = new Model.przedmioty
                {
                    //id_prow = ((TeacherComboViewModel)SubjectCombo.SelectedItem).Teacher.,
                    id_prow = ((Model.prowadzacy)SubjectCombo.SelectedItem).id_prow,
                    nazwa_przed = Subject.Text

                };
                ent.przedmioty.Add(su);

                ent.SaveChanges();
                _page.UpdateSubjectsView();

            }
        }
    }
}
