using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace GradebookLocalApp.Views.TeacherView.Blades
{
    /// <summary>
    /// Logika interakcji dla klasy TeacherNewProjectBlade.xaml
    /// </summary>
    public partial class TeacherNewProjectBlade : UserControl
    {
        TeacherProjectsPage _teacherProjectsPage;
        Model.prowadzacy _teacher;
        public TeacherNewProjectBlade(Model.prowadzacy teacher, TeacherProjectsPage teacherProjectsPage)
        {
            _teacherProjectsPage = teacherProjectsPage;
            _teacher = teacher;
            InitializeComponent();
            ClassComboViewModel classVmModel = new ClassComboViewModel(teacher);
            ClassCombo.ItemsSource = classVmModel.Class;
        }

        public class ClassComboViewModel
        {
            public struct AdditionalInfoClasses
            {
                public int Id { get; set; }
                public string Subject { get; set; }
                public string TypeClass { get; set; }

            }
            public List<AdditionalInfoClasses> Class { get; set; }

            public ClassComboViewModel(Model.prowadzacy teacher)
            {
                using (var entity = new Model.TestEntities())
                {
                    Class = new List<AdditionalInfoClasses>();
                    foreach (var item in entity.zajecia)
                    {
                        if (item.id_prow == teacher.id_prow)
                        {
                            Class.Add(new AdditionalInfoClasses { Id = item.id_zajec, Subject = item.przedmioty.nazwa_przed, TypeClass = item.typy_zajec.nazwa_typ });
                        }
                    }
                }
            }
        }

        private void AddBut_Click(object sender, RoutedEventArgs e)
        {
            using (var entity = new Model.TestEntities())
            {
                var classe = (ClassComboViewModel.AdditionalInfoClasses)(ClassCombo.SelectedItem);
                Model.projekty project = new Model.projekty()
                {
                    nazwa_proj = Project.Text,
                    termin_wyk = Convert.ToDateTime(ProjectDate.Text),
                    id_zajec = classe.Id,
                    id_prow = _teacher.id_prow
                };

                entity.projekty.Add(project);
                entity.SaveChanges();
                _teacherProjectsPage.UpdateProjectsView();
            }
        }
    }
}
