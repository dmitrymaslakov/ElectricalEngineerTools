using Autodesk.AutoCAD.Runtime;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: ExtensionApplication(typeof(ElectricalEngineerTools.PL.Program))]
[assembly: CommandClass(typeof(ElectricalEngineerTools.PL.Program))]

namespace ElectricalEngineerTools.PL
{
    public class Program : IExtensionApplication
    {
        [CommandMethod("qwРасчетОсвещенностиПомещения", CommandFlags.Session)]
        public void Main()
        {
            new Palette().Show();
        }
        void IExtensionApplication.Initialize()
        {
            Main();
        }

        void IExtensionApplication.Terminate()
        {
            
        }
    }
}
