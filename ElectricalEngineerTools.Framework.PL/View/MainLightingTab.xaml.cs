using ElectricalEngineerTools.Framework.PL.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace ElectricalEngineerTools.Framework.PL.View
{
    /// <summary>
    /// Interaction logic for MainWindowLighting.xaml
    /// </summary>
    public partial class MainLightingTab : UserControl
    {
        public MainLightingTab(object dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
        }
    }
}
