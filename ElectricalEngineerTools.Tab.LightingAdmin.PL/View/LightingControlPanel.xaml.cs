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

namespace ElectricalEngineerTools.Tab.LightingAdmin.PL.View
{
    /// <summary>
    /// Interaction logic for LightingControlPanel.xaml
    /// </summary>
    public partial class LightingControlPanel : Window
    {
        public LightingControlPanel(object dataContext)
        {
            InitializeComponent();
            //dataGrid.Items.Clear();
            DataContext = dataContext;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Visibility = Visibility.Hidden;
        }
    }
}
