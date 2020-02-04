using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using GradebookLocalApp.Model;

namespace GradebookLocalApp.Views.SupervisorView.Blades
{
    /// <summary>
    /// Logika interakcji dla klasy SupervisorClassBlade.xaml
    /// </summary>
    public partial class SupervisorClassBlade : UserControl
    {
        private SupervisorClassesPage _page;
        private Model.zajecia _class;
        ContentWindow _window;
        SupervisorStudentsListPage _listPage;
        public SupervisorClassBlade(SupervisorClassesPage supervisorClassesPage ,ContentWindow window)
        {
            _window = window;
            _page = supervisorClassesPage;
            InitializeComponent();

            SubjectComboViewModel vmComboSubject = new SubjectComboViewModel();

            foreach(var item in vmComboSubject.Subject)
            {
                SubjectCombo.Items.Add(item);
            }

            TeacherComboViewModel vmComboTeacher = new TeacherComboViewModel();
            TeacherCombo.ItemsSource = vmComboTeacher.Teacher;

            ClassTypeComboViewModel vmComboClassType = new ClassTypeComboViewModel();
            ClassTypeCombo.ItemsSource = vmComboClassType.ClassType;

            GroupComboViewModel vmCombo = new GroupComboViewModel();
            GroupCombo.ItemsSource = vmCombo.Group;


        }

        private void UpdateBut_Click(object sender, RoutedEventArgs e)
        {
            using (Model.TestEntities entity = new TestEntities())
            {
                var type = (ClassTypeCombo.Items[ClassTypeCombo.SelectedIndex] as typy_zajec).id_typ;

                var prow = (TeacherCombo.Items[TeacherCombo.SelectedIndex] as prowadzacy).id_prow;

                var przed = (SubjectCombo.Items[SubjectCombo.SelectedIndex] as przedmioty).id_przed;


                var a = entity.zajecia.Where(x => x.id_zajec == _class.id_zajec).FirstOrDefault();

                a.id_typ = entity.typy_zajec.Where(x => x.id_typ == type).FirstOrDefault().id_typ;
                a.data_zaj = Convert.ToDateTime(ClassDate.Text);
                a.id_prow = entity.prowadzacy.Where(x => x.id_prow == prow).FirstOrDefault().id_prow;
                a.id_przed = entity.przedmioty.Where(x => x.id_przed == przed).FirstOrDefault().id_przed;
                
                entity.SaveChanges();
                _page.UpdateClassView();

            }
        }

        private void DeleteBut_Click(object sender, RoutedEventArgs e)
        {
            using (var entity = new Model.TestEntities())
            {

                var a = entity.zajecia.Where<Model.zajecia>(x => x.id_zajec == _class.id_zajec).FirstOrDefault();
                
                entity.zajecia.Remove(a);
                _page.SupClassBlade.Children.Clear();

                entity.SaveChanges();
                _page.UpdateClassView();

            }
        }

        class GroupComboViewModel
        {
            public List<Model.grupy_dziekanskie> Group { get; set; }

            public GroupComboViewModel()
            {
                using (var entity = new Model.TestEntities())
                {
                    Group = new List<Model.grupy_dziekanskie>();
                    foreach (var item in entity.grupy_dziekanskie)
                    {
                        Group.Add(item);
                    }
                }
            }

        }

        class SubjectComboViewModel
        {
            public List<Model.przedmioty> Subject { get; set; }

            public SubjectComboViewModel()
            {
                using (var entity = new Model.TestEntities())
                {
                    Subject = new List<Model.przedmioty>();
                    foreach (var item in entity.przedmioty)
                    {
                        Subject.Add(item);
                    }
                }
            }

        }

        class ClassTypeComboViewModel
        {
            public List<Model.typy_zajec> ClassType { get; set; }

            public ClassTypeComboViewModel()
            {
                using (var entity = new Model.TestEntities())
                {
                    ClassType = new List<Model.typy_zajec>();
                    foreach (var item in entity.typy_zajec)
                    {
                        ClassType.Add(item);
                    }
                }
            }
        }

        class TeacherComboViewModel
        {
            public List<Model.prowadzacy> Teacher { get; set; }

            public TeacherComboViewModel()
            {
                using (var entity = new Model.TestEntities())
                {
                    Teacher = new List<Model.prowadzacy>();
                    foreach (var item in entity.prowadzacy)
                    {
                        Teacher.Add(item);
                    }
                }
            }

        }


        internal void SetClass(zajecia classe)
        {
            _class = classe;
            

            ClassDate.Text = classe.data_zaj.ToString();
            using (var entity  = new Model.TestEntities())
            {
                var subject = entity.przedmioty.Where(x => x.id_przed == classe.id_przed).FirstOrDefault();

                for (int i = 0; i < SubjectCombo.Items.Count; i++)
                {
                    
                    if ((SubjectCombo.Items[i] as przedmioty).id_przed==subject.id_przed)
                    {
                        SubjectCombo.SelectedIndex = i;

                    }
                }

                var teacher = entity.prowadzacy.Where(x => x.id_prow == classe.id_prow).FirstOrDefault();

                for (int i = 0; i < TeacherCombo.Items.Count; i++)
                {

                    if ((TeacherCombo.Items[i] as prowadzacy).id_prow == teacher.id_prow)
                    {
                        TeacherCombo.SelectedIndex = i;

                    }
                }

                var classType = entity.typy_zajec.Where(x => x.id_typ == classe.id_typ).FirstOrDefault();

                for (int i = 0; i < ClassTypeCombo.Items.Count; i++)
                {

                    if ((ClassTypeCombo.Items[i] as typy_zajec).id_typ == classType.id_typ)
                    {
                        ClassTypeCombo.SelectedIndex = i;

                    }
                }

                UpdateGroupView();



            }
            
        }

        private void AddGroupClick(object sender, RoutedEventArgs e)
        {
            using (var entity = new Model.TestEntities())
            {
                var gr = ((Model.grupy_dziekanskie)GroupCombo.SelectedItem).id_grupy;

                entity.zajecia.Where(x => x.id_zajec == _class.id_zajec).FirstOrDefault().grupy_dziekanskie.Add(entity.grupy_dziekanskie.Where(x=>x.id_grupy==gr).FirstOrDefault());

                entity.SaveChanges();
                UpdateGroupView();


            }
        }

        private void UpdateGroupView()
        {
            using (var entity = new TestEntities())
            {
                var groups = entity.grupy_dziekanskie.Where(x => x.zajecia.Where(y => y.id_zajec == _class.id_zajec).FirstOrDefault().id_zajec == _class.id_zajec);
                
                try
                {
                    GroupsView.Items.Clear();
                    foreach(var item in groups)
                    {
                        GroupsView.Items.Add(item);


                    }
                }
                catch (Exception)
                {

                    throw;
                }

            }
        }

        private void TaskView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void StudentsListClick(object sender, RoutedEventArgs e)
        {
            var group = ((Model.grupy_dziekanskie)GroupsView.SelectedItem);
            _listPage = new SupervisorStudentsListPage(_class, group);
            _window.ContentFrame.Content = _listPage;
        }
    }
}
