using System;
using System.Windows.Forms;


////////////////////////////////////////////////////////////////////////////////////////////////////
// namespace: Snake
//
// summary:	Main Snake Game Verison 1.1.
////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Snake
{
    /// <summary>
    /// Class Snake Program.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Window());
        }
    }
}
