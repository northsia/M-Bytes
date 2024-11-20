using Guna.UI2.WinForms;
using M_Bytes.Core.Sound;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M_Bytes.Core.Messages
{
    internal class MSG
    {

        public static void SH(string Texth1, string Texth2, Guna2ToggleSwitch Switch = null)
        {
            if (Switch != null && Switch.Checked)
            {
                Player.PlayNotify();
            }

            info info = new info();

            info.Label1Text = Texth1;
            info.Label2Text = Texth2;

            info.Show();
            info.Opacity = 1;
        }

    }
}
