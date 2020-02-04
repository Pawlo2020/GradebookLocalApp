using System;
using System.Linq;
using System.Windows;


namespace GradebookLocalApp
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        ContentWindow _window;
        int mode;
        public LoginPage()
        {
            InitializeComponent();
            StudentRadio.IsChecked = true;
        }

        private void signinBut_Click(object sender, RoutedEventArgs e)
        {
            using (var entity = new Model.TestEntities())
            {
                var admins = entity.administrators;
                var students = entity.studenci;
                var teachers = entity.prowadzacy;

                if (mode == 0)
                {
                    try
                    {
                        var admin = admins.Where(x => x.login == loginBox.Text && x.pass == passBox.Password).FirstOrDefault();
                        if(admin != null)
                        {
                            _window = new ContentWindow(0,admin.id_admin);
                            _window.UsernamesLabel.Content = "admin";
                            _window.RoleLabel.Content = "Administrator";
                            this.Close();
                            _window.Show();
                        }
                        
                    }
                    catch (Exception) { }

                }else if (mode == 1)
                {
                    try
                    {
                        var teacher = teachers.Where(x => x.haslo == loginBox.Text).FirstOrDefault();
                        if (teacher != null)
                        {
                            _window = new ContentWindow(1, teacher.id_prow);
                            _window.UsernamesLabel.Content = teacher.imie_prowadzacy + " " + teacher.nazwisko_prowadzacy;
                            _window.RoleLabel.Content = "Prowadzący";
                            this.Close();
                            _window.Show();
                        }

                    }
                    catch (Exception) { }
                }else if (mode == 2)
                {
                    try
                    {
                        var student = students.Where(x => x.nr_album == loginBox.Text).FirstOrDefault();
                        if (student != null)
                        {
                            _window = new ContentWindow(2, student.id_student);
                            _window.UsernamesLabel.Content = student.imie_student + " " + student.nazwisko_student;
                            _window.RoleLabel.Content = "Student";
                            this.Close();
                            _window.Show();
                        }

                    }
                    catch (Exception) { }
                }
            }   
        }

        private void AdminRadio_Checked(object sender, RoutedEventArgs e)
        {
            mode = 0;
            TeacherRadio.IsChecked = false;
            StudentRadio.IsChecked = false;
        }

        private void TeacherRadio_Checked(object sender, RoutedEventArgs e)
        {
            mode = 1;
            StudentRadio.IsChecked = false;
            AdminRadio.IsChecked = false;
        }

        private void StudentRadio_Checked(object sender, RoutedEventArgs e)
        {
            mode = 2;
            AdminRadio.IsChecked = false;
            TeacherRadio.IsChecked = false;
        }
    }
}
