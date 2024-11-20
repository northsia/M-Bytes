using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;

namespace M_Bytes
{
    internal static class Program
    {
        // Add font to system with AddFontResourceEx
        [DllImport("gdi32.dll", SetLastError = true)]
        private static extern int AddFontResourceEx(string lpszFilename, uint fl, IntPtr pdv);

        // Remove font from system
        [DllImport("gdi32.dll", SetLastError = true)]
        private static extern bool RemoveFontResourceEx(string lpszFilename, uint fl, IntPtr pdv);

        private const uint FR_PRIVATE = 0x10;  // Private font not added to system list
        private const uint FR_NOT_ENUM = 0x20; // Prevent font from being enumerated

        [STAThread]
        static void Main()
        {
            string fontPath = Path.Combine(Environment.CurrentDirectory, "font", "Gotham.otf");

            if (!File.Exists(fontPath))
            {
                MessageBox.Show("Font file not found.");
                Environment.Exit(0);
            }

            if (!InstallFont(fontPath))
            {
                Environment.Exit(0);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new index());
        }

        private static bool InstallFont(string fontPath)
        {
            // Add font to system font collection
            int result = AddFontResourceEx(fontPath, FR_NOT_ENUM, IntPtr.Zero);
            return result > 0; // If successful, result > 0
        }

        private static bool UninstallFont(string fontPath)
        {
            // Remove font from system font collection
            return RemoveFontResourceEx(fontPath, FR_NOT_ENUM, IntPtr.Zero);
        }
    }
}