using System;
using System.Windows.Forms;

namespace PS9
{
    internal static class Launch
    {
        #region Private Methods

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var context = BoggleClientContext.GetContext();
            BoggleClientContext.GetContext().RunNew();

            Application.Run(context);
        }

        #endregion Private Methods
    }
}