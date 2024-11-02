using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace M_Bytes
{
    internal static class Program
    {
        /// <summary>
        /// Load The Font
        /// </summary>
        /// <param name="lpFileName"></param>
        /// <returns></returns>
        [DllImport("gdi32.dll")]
        private static extern int AddFontResource(string lpFileName);

        [DllImport("gdi32.dll")]
        private static extern bool RemoveFontResource(string lpFileName);
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "Fonts/Gotham.otf");

            int result = AddFontResource(path);

            if (result > 0)
            {
                // OK 
            }
            else
            {
                MessageBox.Show("Failed to load font" + result, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }

            if (Environment.OSVersion.Version.Major >= 6)
            {
                SetProcessDPIAware();
            }


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new index());
        }


        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }
}
