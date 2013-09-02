using Autodesk.AutoCAD.ApplicationServices.Core;
using Autodesk.AutoCAD.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lazcad.Opc
{
    public class OpcContextMenu : ContextMenuExtension
    {
        public OpcContextMenu()
        {
            Title = "Lazcad OPC";

            var connectMenu = new MenuItem("Opc Connect (Lazcad)");
            connectMenu.Click += connectMenu_Click;

            MenuItems.Add(connectMenu);
        }

        void connectMenu_Click(object sender, EventArgs e)
        {
            Application.DocumentManager.MdiActiveDocument.SendStringToExecute("opcselect ", true, false, true);
        }
    }
}
