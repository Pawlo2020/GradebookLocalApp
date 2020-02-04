using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GradebookLocalApp.Views.SupervisorView
{
    /// <summary>
    /// Logika interakcji dla klasy SupervisorDashPage.xaml
    /// </summary>
    public partial class SupervisorDashPage : Page
    {
        public SupervisorDashPage()
        {
            InitializeComponent();

            UpdateNumbers();

        }



        public void UpdateNumbers()
        {
            //using (Model.TestEntities entity = new Model.TestEntities())
            //{
            //    TeacherNumber.Content = entity.prowadzacy.Local.Count;
            //    StudentsNumber.Content = entity.studenci.Local.Count;
            //    GroupsNumber.Content = entity.studenci.Local.Count;
            //}
        }
    }
}
