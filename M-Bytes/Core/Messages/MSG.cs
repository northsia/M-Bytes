using Guna.UI2.WinForms;
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

        public static void SH(string Texth1, string Texth2)
        {
            info info = new info();
            info.Label1Text = Texth1;
            info.Label2Text = Texth2;

            info.Show();
        }
    }
}
