using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GradebookLocalApp
{
    /// <summary>
    /// Logika interakcji dla klasy ContentWindow.xaml
    /// </summary>
    public partial class ContentWindow : Window
    {
        public ContentWindow(int role, int id)
        {
            InitializeComponent();
            if (role == 0)
            {
                MenuStack.Children.Add(new Views.SupervisorView.SupervisorMenu(this));
                ContentFrame.Content = new Views.SupervisorView.SupervisorDashPage();
            }
            else if(role == 1)
            {
                using (var entity = new Model.TestEntities())
                {
                    var teacher = entity.prowadzacy.Where(x => x.id_prow == id).FirstOrDefault();
                    UsernamesLabel.Content = teacher.imie_prowadzacy + " " + teacher.nazwisko_prowadzacy;
                    MenuStack.Children.Add(new Views.TeacherView.TeacherMenu(this, teacher));
                    ContentFrame.Content = new Views.TeacherView.TeacherDashPage();
                }
                
            }
            else if (role == 2)
            {
                using (var entity = new Model.TestEntities())
                {
                    var student = entity.studenci.Where(x => x.id_student == id).FirstOrDefault();
                    MenuStack.Children.Add(new Views.StudentView.StudentMenu(this, student));
                    ContentFrame.Content = new Views.StudentView.StudentDashPage();

                }
                   
            }
        }

        
    }
}
