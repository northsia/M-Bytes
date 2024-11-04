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
        public static bool IS_BLOCKED { get; private set; }

        public static string macs { get; private set; }

        public static string MAC { get; private set; }


        public async static void CHECK_IF_BLOCKED()
        {
            if (IS_BLOCKED)
            {
                MSG.SH("You Are Banned", "You Are Banned From Using This Software.\nFor Unethic Purpose !");

                await Task.Delay(2000);

                Environment.Exit(0);
            }
            else
            {
                // Nothing
            }
        }




        public async static void BLOCK_USER_ENGINE()
        {
            try
            {
                string MAC = GetMacAddresses().First();


                string macs = await FIREBASE_DATA_API.SEND_GET_DATA_("ADDR");


                if (MAC != null && macs.Contains(MAC))
                {
                    IS_BLOCKED = true;
                }
                else
                {
                    IS_BLOCKED = false;
                }
            }
            finally
            {
                MSG.SH("Banned !", "Your Device Has Been Banned.\nFor Unethic Purpose !");

            }

        }

        public async static void PROTECTER_ENGINE_PROTECT_ONLOAD_()
        {

            string macs = await FIREBASE_DATA_API.SEND_GET_DATA_("ADDR");

            if (macs.Contains(GetMacAddresses().First()))
            {
                IS_BLOCKED = true;
            }
            else
            {
                IS_BLOCKED = false;
            }
        }


        public async static void PROTECTER_ENGINE_PROTECT_()
        {
            string MAC = GetMacAddresses().First(); 

            if (SESSION_LGN == 0 || macs.Contains(MAC))
            {
                MSG.SH("You Are Banned", "You Are Banned From Using This Software");

                await Task.Delay(2000);

                Environment.Exit(0);

                return;
            }
       
        }

        static string[] GetMacAddresses()
        {
            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            var macAddresses = new List<string>();

            foreach (NetworkInterface networkInterface in networkInterfaces)
            {
                var physicalAddress = networkInterface.GetPhysicalAddress();
                if (physicalAddress != null && physicalAddress.ToString() != "")
                {
                    macAddresses.Add(physicalAddress.ToString());
                }
            }

            return macAddresses.ToArray();
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
