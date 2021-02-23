using ElectricalEngineerTools.PL.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace ElectricalEngineerTools.PL.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowLighting : UserControl
    {
        public MainWindowLighting()
        {
            InitializeComponent();

            DataContext = new MainViewModel();
        }
    }
}
