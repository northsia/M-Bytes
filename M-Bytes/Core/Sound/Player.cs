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
        public static void PlayClickSound()
        {
            SoundPlayer player = new SoundPlayer(Properties.Resources.Cick);
            player.Play();
        }

        public static void PlayNotify()
        {
            SoundPlayer player = new SoundPlayer(Properties.Resources.Notify);
            player.Play();
        }
    }
}
