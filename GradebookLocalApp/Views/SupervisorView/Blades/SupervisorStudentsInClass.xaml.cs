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

namespace GradebookLocalApp.Views.SupervisorView.Blades
{
    /// <summary>
    /// Logika interakcji dla klasy SupervisorStudentsInClass.xaml
    /// </summary>
    public partial class SupervisorStudentsInClass : UserControl
    {
        Model.zajecia _classe;
        Model.studenci _student;
        SupervisorStudentsListPage _page;
        public SupervisorStudentsInClass(SupervisorStudentsListPage supervisorStudentsListPage, Model.studenci student, Model.zajecia classe)
        {
            _classe = classe;
            _student = student;
            _page = supervisorStudentsListPage;
            InitializeComponent();
            
            
        }

        public void SetData(Model.studenci student)
        {
            Index.Text = student.nr_album;
            Name.Text = student.imie_student;
            LastName.Text = student.nazwisko_student;
            using (var entity = new Model.TestEntities())
            {
                var group = entity.grupy_dziekanskie.Where(x => x.id_grupy == student.id_grupy).FirstOrDefault();
                Group.Text = group.nazwa_grupy;

            }

        }

        private void DeleteBut_Click(object sender, RoutedEventArgs e)
        {
            using (var entity = new Model.TestEntities())
            {
                entity.zajecia.Where(x => x.id_zajec == _classe.id_zajec).FirstOrDefault().studenci.Remove(entity.studenci.Where(y => y.id_student == _student.id_student).FirstOrDefault());
                
                entity.SaveChanges();
                _page.UpdateView();

            }
        }
    }
}
