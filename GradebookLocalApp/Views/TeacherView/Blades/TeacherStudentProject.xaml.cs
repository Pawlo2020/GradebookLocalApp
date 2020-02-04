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

namespace GradebookLocalApp.Views.TeacherView.Blades
{
    /// <summary>
    /// Logika interakcji dla klasy TeacherStudentProject.xaml
    /// </summary>
    public partial class TeacherStudentProject : UserControl
    {
        Model.NotesViewModel.ProjectsNotes _projStud;
        Model.StageViewModel currentStage;
        Model.prowadzacy _teacher;
        ProjectPage _projectPage;


        public TeacherStudentProject(Model.NotesViewModel.ProjectsNotes projStud, Model.prowadzacy teacher, ProjectPage projectPage)
        {
            _projectPage = projectPage;
            _teacher = teacher;
            _projStud = projStud;
            InitializeComponent();

        }

        public void SetData()
        {
            try
            {
                NameLbl.Content = _projStud.StudentName;
                LastNameLbl.Content = _projStud.StudentLastName;
                Project.Text = _projStud.Note;

                UpdateView();
            }
            catch (Exception) { }

        }

        public void UpdateView()
        {
            using (var entity = new Model.TestEntities())
            {
                var stages = entity.etapy.Where(x => x.id_proj == _projStud.id_proj);
                //var notebyStage = entity.notesbyStage(_projStud.id_student.ToString(), _projStud.id_proj.ToString());
                try
                {
                    StagesView.Items.Clear();
                    foreach (var item in stages)
                    {
                        var note = entity.Database.SqlQuery<string>($"SELECT wartosc FROM oceny_etapow WHERE id_etap = {item.id_etap} AND id_prow = {_teacher.id_prow} AND id_student = {_projStud.id_student}").ToList();
                        StagesView.Items.Add(new Model.StageViewModel { Id = item.id_etap, Date = item.termin_etapu.ToString(), Stage = item.nazwa_etapu, Note = note.Count>0?note[0]:"" });
                    }
                }
                catch (Exception) { }
            }

        }


        private void TaskView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            currentStage = ((Model.StageViewModel)StagesView.SelectedItem);

        }

        private void AddNote(object sender, RoutedEventArgs e)
        {
            var note = StageNote.Text;

            using (var entity = new Model.TestEntities())
            {
                Model.oceny_etapow stageNote = new Model.oceny_etapow()
                {
                    id_prow = _teacher.id_prow,
                    wartosc = note,
                    id_student = _projStud.id_student,
                    id_etap = currentStage.Id
                };


                var query =
                    "BEGIN "
                        + "IF NOT EXISTS(SELECT * FROM oceny_etapow "
                        + $"WHERE id_student = {_projStud.id_student} "
                        + $"AND id_etap = {currentStage.Id} "
                        + $"AND id_prow = {_teacher.id_prow}) "
                        + "BEGIN "
                            + $"INSERT INTO dbo.oceny_etapow (id_prow, id_student,id_etap, wartosc) VALUES({_teacher.id_prow},{_projStud.id_student},{currentStage.Id},'{note}')"
                        + " END"
                        + " ELSE"
                            + " BEGIN"
                                + $" UPDATE oceny_etapow SET wartosc = {note} WHERE id_student = {_projStud.id_student} AND id_etap = {currentStage.Id} AND id_prow = {_teacher.id_prow}"
                    + " END"
                    + " END";


                entity.Database.ExecuteSqlCommand(query);

                
                UpdateView();
            }
        }

        private void UpdateClick(object sender, RoutedEventArgs e)
        {
            using (var entity = new Model.TestEntities())
            {
                var note = Project.Text;

                var query =
                    "BEGIN "
                        + "IF NOT EXISTS(SELECT * FROM oceny_projektow "
                        + $"WHERE id_student = {_projStud.id_student} "
                        + $"AND id_proj = {_projStud.id_proj} "
                        + $"AND id_prow = {_teacher.id_prow}) "
                        + "BEGIN "
                            + $"INSERT INTO dbo.oceny_projektow (id_prow, id_student, id_proj, wartosc_oceny_proj) VALUES({_teacher.id_prow},{_projStud.id_student},{_projStud.id_proj},'{note}')"
                        + " END"
                        + " ELSE"
                            + " BEGIN"
                                + $" UPDATE oceny_projektow SET wartosc_oceny_proj = {note} WHERE id_student = {_projStud.id_student} AND id_proj = {_projStud.id_proj} AND id_prow = {_teacher.id_prow}"
                    + " END"
                    + " END";

                entity.Database.ExecuteSqlCommand(query);
                _projectPage.UpdateView();
            }
        }
    }
}
