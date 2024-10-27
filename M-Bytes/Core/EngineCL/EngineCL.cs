using M_Bytes.Core.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M_Bytes.Core.EngineCL
{
    internal class EngineCL
    {
        /// engine CL: is the engine that is used for M-Bytes functions.
        /// By Meed for M-Bytes.
        /// Engine CL 

        /// CL Means : CL ==> Clean
        /// All functions is public static 

        public static async Task CleanTemp()
        {
            string tempPath = Path.GetTempPath();
            await RemoveAllFiles(tempPath);
        }

        public static async Task RemoveAllFiles(string path)
        {
            if (Directory.Exists(path))
            {
                try
                {
                    string[] files = Directory.GetFiles(path);

                    await Task.Run(() =>
                    {
                        foreach (string file in files)
                        {
                            File.Delete(file);
                        }
                    });
                }
                catch (Exception ex) { MSG.SH("Error", ex.Message); }
            }
        }
    }
}
