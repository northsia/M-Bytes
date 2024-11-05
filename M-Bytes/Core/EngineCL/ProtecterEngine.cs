using M_Bytes.Core.Account;
using M_Bytes.Core.Firebase_data;
using M_Bytes.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M_Bytes.Core.EngineCL
{
    internal class ProtecterEngine
    {

        public static int SESSION_LGN;



        public async static void PROTECTER_ENGINE_PROTECT_()
        {

            if (SESSION_LGN == 0)
            {
                MSG.SH("You Are Banned", "You Are Banned From Using This Software");

                await Task.Delay(2000);

                Environment.Exit(0);

                return;
            }
       
        }



        public async static void SESSEION_PR_ENGINE(int SESSION_LGN)
        {
            if (SESSION_LGN == 0)
            {
                MSG.SH("Login First", "You Must Login, Please Login First");

                await Task.Delay(3000);

                Environment.Exit(0);

            }
            else if (SESSION_LGN == 1) 
            { 
                /*        :)       */ 
            }
        }
    }
}
