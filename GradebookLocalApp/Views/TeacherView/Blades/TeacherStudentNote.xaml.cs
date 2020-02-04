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
    /// Logika interakcji dla klasy TeacherStudentNote.xaml
    /// </summary>
    public partial class TeacherStudentNote : UserControl
    {
        Model.studenci _student;
        Model.przedmioty _subject;
        Model.prowadzacy _teacher;
        SubjectPage _subjectPage;
        public TeacherStudentNote(Model.studenci student, Model.przedmioty subject, Model.prowadzacy teacher, SubjectPage subjectPage)
        {
            _subjectPage = subjectPage;
            _student = student;
            _teacher = teacher;
            _subject = subject;
            InitializeComponent();
        }

        private void AddBut_Click(object sender, RoutedEventArgs e)
        {
            using (var entity = new Model.TestEntities())
            {
                var query = 
                    "BEGIN "
                        + "IF NOT EXISTS(SELECT * FROM oceny_przedmiotow "
                        + $"WHERE id_student = {_student.id_student} "
                        + $"AND id_przed = {_subject.id_przed} "
                        + $"AND id_prow = {_subject.id_prow}) "
                        + "BEGIN "
                            + $"INSERT INTO oceny_przedmiotow (id_student, id_przed, id_prow, wartosc_pceny_przedmiot) VALUES ('{_student.id_student}', '{_subject.id_przed}', '{_teacher.id_prow}', '{NoteSub.Text}')"
                        + " END"
                        + " ELSE"
                            + " BEGIN"
                                + $" UPDATE oceny_przedmiotow SET wartosc_pceny_przedmiot = {NoteSub.Text} WHERE id_student = {_student.id_student} AND id_prow = {_teacher.id_prow} AND id_przed = {_subject.id_przed}"
                    + " END"
                    + " END";
                entity.Database.ExecuteSqlCommand(query);
                _subjectPage.UpdateView();
                

            }
        }

        internal void SetData()
        {
            Name.Text = _student.imie_student;
            LastName.Text = _student.nazwisko_student;

        }
    }
}
