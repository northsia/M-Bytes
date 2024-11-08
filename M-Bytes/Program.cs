using System;
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

        [DllImport("gdi32.dll")]
        private static extern int GetFontResourceInfo(string lpFileName, IntPtr lpBuffer, uint nBufferSize);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string path = Path.Combine(Environment.CurrentDirectory, "Fonts/Gotham.otf");

            // Check if the font is already loaded
            if (!IsFontLoaded(path))
            {
                int result = AddFontResource(path);

                if (result > 0)
                {
                    // Font successfully loaded
                }
                else
                {
                    Environment.Exit(0);
                }
            }
            else
            {
                    // Font is already loaded//
            }

            if (Environment.OSVersion.Version.Major >= 6)
            {
                SetProcessDPIAware();
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new index());
        }

        private static bool IsFontLoaded(string fontPath)
        {
            try
            {
                // Attempt to create a font object using the font file
                var font = new System.Drawing.Font(fontPath, 10);
                return font.Name != null;
            }
            catch
            {
                return false;
            }
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }
}
