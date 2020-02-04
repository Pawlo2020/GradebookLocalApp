using System;
using System.Linq;
using System.Windows.Controls;

namespace GradebookLocalApp.Views.StudentView
{
    /// <summary>
    /// Logika interakcji dla klasy StudentClasses.xaml
    /// </summary>
    public partial class StudentClasses : Page
    {
        Model.studenci _student;
        StudentClassProjectsPage _projectPage;
        ContentWindow _window;
        public StudentClasses(ContentWindow window, Model.studenci student)
        {
            _student = student;
            _window = window;
            InitializeComponent();
            UpdateView();
        }

        private void UpdateView()
        {
            using (var entity = new Model.TestEntities())
            {
                var stud_class = entity.zajecia.Where(x => x.studenci.Contains(entity.studenci.Where(u => u.id_student == _student.id_student).FirstOrDefault()));

                ClassesView.Items.Clear();
                
                try
                {
                    foreach (var item in stud_class)
                    {
                        ClassesView.Items.Add(new Model.ClassViewModel
                        {
                            IdClass = item.id_zajec,
                            SubjectName = entity.przedmioty.Where(x => x.id_przed == item.id_przed).FirstOrDefault().nazwa_przed,
                            TeacherName = entity.prowadzacy.Where(a => a.id_prow == item.id_prow).FirstOrDefault().imie_prowadzacy + " " + entity.prowadzacy.Where(a => a.id_prow == item.id_prow).FirstOrDefault().nazwisko_prowadzacy,
                            TypeName = entity.typy_zajec.Where(x => x.id_typ == item.id_typ).FirstOrDefault().nazwa_typ
                            
                        });
                    }
                }
                catch (System.Exception)
                {


                }


            }
        }

        private void TaskView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var id = ((Model.ClassViewModel)ClassesView.SelectedItem).IdClass;
            using (var entity = new Model.TestEntities())
            {
                var classe = entity.zajecia.Where(x => x.id_zajec == id).FirstOrDefault();
                _projectPage = new StudentClassProjectsPage(classe, _student);
                _window.ContentFrame.Content = _projectPage;
               
            }

        }
    }
}
