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
    /// Logika interakcji dla klasy TeacherClassesPage.xaml
    /// </summary>
    public partial class TeacherClassesPage : Page
    {
        TeacherStudentsClassPage _teacherStudentsClassPage;
        ContentWindow _window;
        public TeacherClassesPage(Model.prowadzacy teacher, ContentWindow window)
        {
            _window = window;
            InitializeComponent();

            using (var entity = new Model.TestEntities())
            {
                var classes = entity.zajecia.Where(x => x.id_prow == teacher.id_prow);
                try
                {
                    foreach (var item in classes)
                    {
                        ClassesView.Items.Add(new Model.ClassViewModel { IdClass = item.id_zajec, SubjectName = item.przedmioty.nazwa_przed, 
                            TeacherName = item.prowadzacy.imie_prowadzacy + " " + item.prowadzacy.nazwisko_prowadzacy, TypeName = item.typy_zajec.nazwa_typ });
                    }
                }
                catch (Exception e) { }
            }
        }

        private void TaskView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var id = ((Model.ClassViewModel)(ClassesView.SelectedItem)).IdClass;
            Model.zajecia classe;
            using (var entity = new Model.TestEntities())
            {
                classe = entity.zajecia.Where(x=>x.id_zajec==id).FirstOrDefault();

            }

                _teacherStudentsClassPage = new TeacherStudentsClassPage(classe);
            _window.ContentFrame.Content = _teacherStudentsClassPage;
                
        }
    }
}
