using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace GradebookLocalApp.Views.TeacherView
{
    /// <summary>
    /// Logika interakcji dla klasy SubjectPage.xaml
    /// </summary>
    public partial class SubjectPage : Page
    {
        Blades.TeacherStudentNote _studentNote;
        Model.przedmioty _subject;
        Model.prowadzacy _teacher;
        public SubjectPage(Model.przedmioty subject, Model.prowadzacy teacher)
        {
            _teacher = teacher;
            _subject = subject;
            InitializeComponent();
            UpdateView();
            SubjectNameLbl.Content = _subject.nazwa_przed;
        }

        public void UpdateView()
        {
            using (var entity = new Model.TestEntities())
            {
                var students = entity.studenci;
                StudentsView.Items.Clear();
               
                        try
                        {
                            StudentsView.Items.Clear();
                            foreach (var stud in students)
                            {
                                var student = entity.studenci.Where(x => x.id_student == stud.id_student).FirstOrDefault();
                                
                                try
                                {  }
                                catch (Exception) { }

                                var note = entity.Database.SqlQuery<string>($"Select wartosc_pceny_przedmiot FROM oceny_przedmiotow WHERE id_student = {student.id_student} AND id_przed = {_subject.id_przed} AND id_prow = {_subject.id_prow}").ToList();
                        
                            StudentsView.Items.Add(new Model.NotesViewModel.SubjectsNotes
                            {
                                id_student = stud.id_student,
                                id_przed = _subject.id_przed,
                                SubjectName = _subject.nazwa_przed,
                                StudentName = student.imie_student,
                                StudentLastName = student.nazwisko_student,
                                Note = note.Count>0? note[0]:""
                            });
                        }
                        }
                        catch (Exception)
                        {
                        }
                    }
        }

        private void TaskView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = ((Model.NotesViewModel.SubjectsNotes)StudentsView.SelectedItem);
            using (var entity = new Model.TestEntities())
            {
                try
                {
                    var student = entity.studenci.Where(x => x.id_student == item.id_student).FirstOrDefault();
                    _studentNote = new Blades.TeacherStudentNote(student, _subject, _teacher, this);
                    SubBlade.Children.Clear();
                    _studentNote.SetData();
                    SubBlade.Children.Add(_studentNote);
                }
                catch (Exception) { }
            }
        }
    }
}
