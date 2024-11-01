using M_Bytes.Core.Account;
using M_Bytes.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M_Bytes.Core.EngineCL
{
    internal class ProtecterEngine
    {

        public static void BLOCK_USER()
        {

            
            
        }


        public async static void SESSION_LGN(int session_lgn)
        {
            if (session_lgn == 0)
            {
                MSG.SH("Login First", "You Must Login, Please Login First");

                BLOCK_USER();

                await Task.Delay(3000);

                Environment.Exit(0);

            }
            else if (session_lgn == 1) 
            { 
                /*        :)       */ 
            }
        }
    }
}
