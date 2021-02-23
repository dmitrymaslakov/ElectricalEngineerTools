using ElectricalEngineerTools.Framework.DAL.ViewModels;

namespace ElectricalEngineerTools.Tab.LightingAdmin.PL.ViewModels
{
    public class LightingAdminTabViewModel : ViewModelBase
    {
        public string Manufacturer { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public string LdtIesFile { get; set; }
        public string DwgFile { get; set; }

    }
}
