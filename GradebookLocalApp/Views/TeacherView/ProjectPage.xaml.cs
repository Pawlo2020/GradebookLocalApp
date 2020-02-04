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
    /// Logika interakcji dla klasy ProjectPage.xaml
    /// </summary>
    public partial class ProjectPage : Page
    {
        Blades.TeacherProjectBlade _projectBlade;
        Blades.TeacherApplicationBlade _applicationBlade;
        Blades.TeacherStudentProject _teacherStudentProject;
        Model.prowadzacy _teacher;
        Model.projekty _project;
        public ProjectPage(Model.projekty project)
        {

            _project = project;
            InitializeComponent();
            UpdateView();
            
        }

        internal void SetData(Model.prowadzacy teacher, Model.projekty project)
        {
            _teacher = teacher;
            _project = project;
            ProjectNameLbl.Content = project.nazwa_proj;
            ProjectDateLbl.Content = project.termin_wyk;
        }

        private void TaskView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var projStud = ((Model.NotesViewModel.ProjectsNotes)StudentsView.SelectedItem);
            _teacherStudentProject = new Blades.TeacherStudentProject(projStud, _teacher, this);
            _teacherStudentProject.SetData();
            ProjBlade.Children.Clear();
            ProjBlade.Children.Add(_teacherStudentProject);



        }

        private void EditBut_Click(object sender, RoutedEventArgs e)
        {
            ProjBlade.Children.Clear();
            _projectBlade = new Blades.TeacherProjectBlade(_teacher, _project);
            ProjBlade.Children.Clear();
            _projectBlade.SetProject();
            ProjBlade.Children.Add(_projectBlade);


        }

        private void AddStudentClick(object sender, RoutedEventArgs e)
        {
            ProjBlade.Children.Clear();
            _applicationBlade = new Blades.TeacherApplicationBlade(_project, this);
            ProjBlade.Children.Add(_applicationBlade);
        }

        class StudentsSchema
        {
            public int id_student { get; set; }

            public int id_proj { get; set; }


        }

        public void UpdateView()
        {
            StudentsView.Items.Clear();
            using (var entity = new Model.TestEntities())
            {
                //var students = entity.studenci.Where(x => x.projekty.Where(y => y.id_proj == _project.id_proj).FirstOrDefault().id_proj == _project.id_proj);

                //var students = entity.projekty.Where(e => e.id_proj == _project.id_proj).Select(e => e.studenci.All(x=>x.id_student));

                
                try
                {
                    var ids = entity.Database.SqlQuery<StudentsSchema>($"SELECT * FROM dbo.studenci_projekty WHERE id_proj='{_project.id_proj}'").ToList();
                    //var students = entity.projectsbyclass(_project.id_proj.ToString());
                    foreach (var item in ids)
                    {
                        
                        
                            var student = entity.studenci.Where(x => x.id_student == item.id_student).FirstOrDefault();
                            Model.oceny_projektow note = null;
                            note = entity.oceny_projektow.Where(x => x.id_student == item.id_student && x.id_proj == item.id_proj).FirstOrDefault();

                        StudentsView.Items.Add(new Model.NotesViewModel.ProjectsNotes
                        {
                            id_student = item.id_student,
                            id_proj = item.id_proj,
                            StudentName = student.imie_student,
                            StudentLastName = student.nazwisko_student,
                            Note = note != null ? note.wartosc_oceny_proj : ""
                        }) ;
                        
                        

                        

                    }
                }
                catch (Exception)
                {
                }

            }
        }
    }
}
