using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using StockBiz;

namespace WinMain
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            Application.ThreadException += Application_ThreadException;
            //Thread.
            Application.Run(new MainForm());
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            ExceptionHelper.Handler(e.Exception);
        }

        private static void CurrentDomainOnUnhandledException(object sender
            , UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        {
            ExceptionHelper.Handler((Exception)unhandledExceptionEventArgs.ExceptionObject);
        }
    }
}
