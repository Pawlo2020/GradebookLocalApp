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

namespace GradebookLocalApp.Views.SupervisorView.Blades
{
    /// <summary>
    /// Logika interakcji dla klasy SupervisorNewGroupBlade.xaml
    /// </summary>
    public partial class SupervisorNewGroupBlade : UserControl
    {
        private SupervisorGroupPage _page;
        private Model.grupy_dziekanskie _group;
        public SupervisorNewGroupBlade(SupervisorGroupPage page)
        {
            _page = page;
            InitializeComponent();
        }



        private void AddBut_Click(object sender, RoutedEventArgs e)
        {
            using (Model.TestEntities ent = new Model.TestEntities())
            {
                Model.grupy_dziekanskie gr = new Model.grupy_dziekanskie
                {
                    nazwa_grupy = Group.Text
                };
                ent.grupy_dziekanskie.Add(gr);

                ent.SaveChanges();
                _page.UpdateGroupsView();

            }
        }
    }
}
