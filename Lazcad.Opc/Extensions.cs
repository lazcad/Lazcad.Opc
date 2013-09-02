using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.ProcessPower.PlantInstance;
using Autodesk.ProcessPower.PnIDObjects;
using System;

namespace Lazcad.Opc
{
    public class Extensions : IExtensionApplication
    {
        private OpcContextMenu _menu = new OpcContextMenu();

        public void Initialize()
        {
            PlantApplication.CurrentProjectOpenedEvent += PlantApplication_CurrentProjectOpenedEvent;
        }

        void PlantApplication_CurrentProjectOpenedEvent()
        {
            //Disable once loaded first time
            PlantApplication.CurrentProjectOpenedEvent -= PlantApplication_CurrentProjectOpenedEvent;

            AddContext(typeof(DynamicOffPage));
            AddContext(typeof(OffPage));
        }

        public void AddContext(Type type)
        {
            RXClass rxc = Entity.GetClass(type);
            Application.AddObjectContextMenuExtension(rxc, new OpcContextMenu());
        }

        public void Terminate()
        {
        }
    }
}
