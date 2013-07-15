using Autodesk.AutoCAD.ApplicationServices.Core;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using Autodesk.ProcessPower.PnIDObjects;

namespace Lazcad.Opc
{
    public static class Commands
    {
        private static ObjectId _prevOpcId;

        [CommandMethod("lazcad", "opcselect", CommandFlags.Modal | CommandFlags.UsePickSet)]
        public static void OpcSelectCommand()
        {
            var doc = Application.DocumentManager.MdiActiveDocument;
            var db = doc.Database;
            var ed = doc.Editor;
                        
            ObjectId opcId;

            var sel = ed.SelectImplied();
            if (sel.Status != PromptStatus.OK)
            {
                var newSel = ed.GetEntity(_prevOpcId == ObjectId.Null ? "Select first OPC" : "Select next OPC");
                opcId = newSel.ObjectId;
            }
            else
            {
                opcId = sel.Value[0].ObjectId;
            }

            //Opc already connected
            var opcManager = new OffpageConnectionManager();
            if (opcManager.IsConnected(_prevOpcId) || opcManager.IsConnected(opcId))
            {
                ed.WriteMessage("OPC is already connected, disconnect it first");
                return;
            }

            if (_prevOpcId == ObjectId.Null)
            {
                _prevOpcId = opcId;
                ed.WriteMessage("Go to the next drawing and select another OPC");
                return;
            }

            opcManager.Connect(opcId, _prevOpcId);
            _prevOpcId = ObjectId.Null;
        }

        [CommandMethod("lazcad", "opcclear", CommandFlags.Modal)]
        public static void OpcClearCommand()
        {
            _prevOpcId = ObjectId.Null;
        }
    }
}
