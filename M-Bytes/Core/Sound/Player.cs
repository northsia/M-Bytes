using Guna.UI2.WinForms;
using M_Bytes.Core.Settings;
using System;
using System.Collections.Generic;
using System.IO;
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
            string path = Environment.CurrentDirectory + "/Settings/settings.json";

            bool Player = Json.GET_TRUE_F_CONTENT_FROM_JSON_(path, "NotificationsEnabled");

            if (Player)
            {
                SoundPlayer player = new SoundPlayer(Properties.Resources.Notify);
                player.Play();
            }
        }
    }
}
