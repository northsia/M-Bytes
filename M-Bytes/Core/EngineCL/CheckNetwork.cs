using M_Bytes.Core.Messages;
using NLog;
using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M_Bytes.Core.EngineCL
{
    internal class CheckNetwork
    {
        public static readonly Logger logger = LogManager.GetCurrentClassLogger();

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

        public async static void CheckConnection()
        {
            if (!IsNetworkAvailable())
            {
                // Log the error
                logger.Log(LogLevel.Error, "No Internet Connection.");

                // Show the message and wait for it to be closed
                MSG.SH("Connection Error", "Check Your Network & Try Again.");

                // Wait for 2 seconds before exiting (if you want to give users time to read the message)
                await Task.Delay(2000);

                // Exit the app
                Environment.Exit(0);
            }
            else
            {
                // Additional logic for when the network is available (if needed)
            }
        }
    }
}
