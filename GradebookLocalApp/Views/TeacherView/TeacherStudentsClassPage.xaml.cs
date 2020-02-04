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
    /// Logika interakcji dla klasy TeacherStudentsClassPage.xaml
    /// </summary>
    public partial class TeacherStudentsClassPage : Page
    {
        Model.zajecia _classe;
        public TeacherStudentsClassPage(Model.zajecia classe)
        {
            _classe = classe;
            InitializeComponent();

            UpdateView();
        }

        private void UpdateView()
        {
            using (var entity = new Model.TestEntities())
            {
                var students = entity.studenci.Where(x => x.zajecia.Contains(entity.zajecia.Where(y => y.id_zajec == _classe.id_zajec).FirstOrDefault()));
                StudentsView.Items.Clear();
                try
                {
                    foreach (var item in students)
                    {
                        StudentsView.Items.Add(item);
                    }

                }
                catch (Exception) { }



            }
        }

        private void TaskView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
