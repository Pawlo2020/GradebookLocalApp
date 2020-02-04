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

namespace GradebookLocalApp.Views.StudentView
{
    /// <summary>
    /// Logika interakcji dla klasy StudentNotes.xaml
    /// </summary>
    public partial class StudentNotes : Page
    {
        Model.studenci _student;
        public StudentNotes(ContentWindow _window, Model.studenci student)
        {
            _student = student;
            InitializeComponent();
            UpdateView();
        }

        private void UpdateView()
        {
            using (var entity = new Model.TestEntities())
            {
                var query = entity.Database.SqlQuery<Model.NotesViewModel.SubjectsNotes>($"SELECT * FROM oceny_przedmiotow WHERE id_student={_student.id_student}").ToList();

                
                    foreach(var item in query)
                    {
                        var subjectName = entity.przedmioty.Where(x => x.id_przed == item.id_przed).FirstOrDefault().nazwa_przed;
                        var note = entity.Database.SqlQuery<string>($"SELECT wartosc_pceny_przedmiot FROM oceny_przedmiotow WHERE id_przed = {item.id_przed} AND id_student = {item.id_student}").ToList();
                        SubjectView.Items.Add(new Model.NotesViewModel.SubjectsNotes {
                            id_przed=item.id_przed, 
                            id_student=item.id_student, 
                            Note=note.Count>0?note[0]:"", 
                            SubjectName=subjectName });
                    }

            }
        }

        private void SubjectView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
