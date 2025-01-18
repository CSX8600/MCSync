using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MesaSuite.Tests
{
    [TestClass]
    public class DashboardTests
    {
        [TestMethod]
        public void AllDashboardButtonsSecured()
        {
            frmMain main = new frmMain();
            FlowLayoutPanel flowLayoutPanel = main.Controls.OfType<FlowLayoutPanel>().First();
            List<Panel> panelsInLayout = flowLayoutPanel.Controls.OfType<Panel>().ToList();
            Dictionary<Panel, frmMain.ProgramData> permissionsDictionary = main.GetProgramKeyForButton();

            StringBuilder missingSecuredButtons = new StringBuilder();
            foreach(Panel panel in panelsInLayout.Except(permissionsDictionary.Keys))
            {
                if (missingSecuredButtons.Length > 0)
                {
                    missingSecuredButtons.Append(", ");
                }

                missingSecuredButtons.Append(panel.Name);
            }

            Assert.IsTrue(missingSecuredButtons.Length == 0, "The following panels are missing security in the GetRequiredPermissionsForButton method on frmMain:\r\n\r\n" + missingSecuredButtons.ToString());
        }
    }
}
