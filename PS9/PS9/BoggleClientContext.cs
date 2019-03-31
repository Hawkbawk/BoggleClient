using System.Windows.Forms;

namespace PS9
{
    internal class BoggleClientContext : ApplicationContext
    {
        private static BoggleClientContext context;
        private int windowCount;

        private BoggleClientContext()
        {
        }

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
    }
}