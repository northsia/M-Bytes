using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace M_Bytes.Core.Sound
{
    internal class Player
    {
        public static void PlayClickSound(Guna2ToggleSwitch SW)
        {
            if (SW.Checked)
            {
                SoundPlayer player = new SoundPlayer(Properties.Resources.Cick);
                player.Play();
            }
            
        }

        public static void PlayNotify(Guna2ToggleSwitch SW)
        {
            if (SW.Checked)
            {
                SoundPlayer player = new SoundPlayer(Properties.Resources.Cick);
                player.Play();
            }            
        }
    }
}
