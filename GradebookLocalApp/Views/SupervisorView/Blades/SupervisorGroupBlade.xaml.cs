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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GradebookLocalApp.Views.SupervisorView.Blades
{
    /// <summary>
    /// Logika interakcji dla klasy SupervisorGroupBlade.xaml
    /// </summary>
    public partial class SupervisorGroupBlade : UserControl
    {
        private SupervisorGroupPage _page;
        private Model.grupy_dziekanskie _group;
        public SupervisorGroupBlade(SupervisorGroupPage page)
        {
            _page = page;
            InitializeComponent();
        }

        private void updateBut_Click(object sender, RoutedEventArgs e)
        {
            using (var entity = new Model.TestEntities())
            {
                var a = entity.grupy_dziekanskie.Where<Model.grupy_dziekanskie>(x => x.id_grupy == _group.id_grupy).FirstOrDefault();

                a.nazwa_grupy = Group.Text;

                entity.SaveChanges();
                _page.UpdateGroupsView();

            }
        }

        internal void SetGroup(Model.grupy_dziekanskie group)
        {
            _group = group;

            Group.Text = group.nazwa_grupy;

        }

        private void DeleteBut_Click(object sender, RoutedEventArgs e)
        {
            using (var entity = new Model.TestEntities())
            {
                var a = entity.grupy_dziekanskie.Where<Model.grupy_dziekanskie>(x => x.id_grupy == _group.id_grupy).FirstOrDefault();

                entity.grupy_dziekanskie.Remove(a);

                entity.SaveChanges();
                _page.UpdateGroupsView();

            }
        }
    }
}
