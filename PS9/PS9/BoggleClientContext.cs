using System.Windows.Forms;

namespace PS9
{
    internal class BoggleClientContext : ApplicationContext
    {
        #region Private Fields

        private static BoggleClientContext context;
        private int windowCount;

        #endregion Private Fields

        #region Private Constructors

        private BoggleClientContext()
        {
        }

        #endregion Private Constructors

        #region Public Methods

        public static BoggleClientContext GetContext()
        {
            if (context == null)
            {
                context = new BoggleClientContext();
            }
            return context;
        }

        public void RunNew()
        {
            BoggleClient view = new BoggleClient();
            new BoggleClientController(view);
            windowCount++;

            view.FormClosed += (o, e) => { if (--windowCount <= 0) ExitThread(); };
            view.Show();
        }

        #endregion Public Methods
    }
}