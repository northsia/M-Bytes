using M_Bytes.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M_Bytes.Core.EngineCL
{
    internal class CheckNetwork
    {
        public static bool IsNetworkAvailable()
        {
            try
            {
                using (var ping = new Ping())
                {
                    PingReply reply = ping.Send("8.8.8.8", 1000);
                    return reply.Status == IPStatus.Success;
                }
            }
            catch
            {
                return false; 
            }
        }

        public static void CheckConnection()
        {
            if (!IsNetworkAvailable())
            {
                MSG.SH("Connection Error", "Check Your Network & Try Again.");
                Thread.Sleep(5000);


                // exit the app if No internet

                Environment.Exit(0);

            }
            else
            {
                
            }
        }
    }
}
