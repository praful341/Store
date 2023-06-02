using SoftwareLocker;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace STORE
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static bool OpenDetailFormOnClose { get; set; }

        [STAThread]
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [STAThread]
        static void Main(String[] Args)
        {
            //TrialMaker t = new TrialMaker("Store", Application.StartupPath + "\\RegFile.reg", ConfigurationManager.AppSettings["P"].ToString() + ":" + "\\TMSetDMIT.dbf", "Company Name: Tech Rhombus\nPhone: +91 6351631301, Mobile: +91 9687046432", 10, 500000, "143");

            //byte[] MyOwnKey = { 97, 250, 1, 5, 84, 21, 7, 63,
            //     4, 54, 87, 56, 123, 10, 3, 62,
            //     7, 9, 20, 36, 37, 21, 101, 57};
            //t.TripleDESKey = MyOwnKey;

            //TrialMaker.RunTypes RT = t.ShowDialog();
            //bool is_trial;

            //if (RT != TrialMaker.RunTypes.Expired)
            //{
            //    if (RT == TrialMaker.RunTypes.Full)
            //        is_trial = false;
            //    else
            //        is_trial = true;

            //Application.Run(new FrmMain());
            //}

            //            Application.EnableVisualStyles();
            //            Application.SetCompatibleTextRenderingDefault(false);

            //            Application.EnableVisualStyles();
            //            Application.SetCompatibleTextRenderingDefault(false);

            //            Thread.CurrentThread.SetApartmentState(ApartmentState.STA);
            //            TrialMaker t = new TrialMaker("Process_BR", Application.StartupPath + "\\RegFile.reg",
            //"D:" + "\\TMSetStore.dbf",
            //"Company Name: Tech Rhombus\nPhone: +91 6351631301, Mobile: +91 9687046432",
            //10, 500000, "143");

            //            byte[] MyOwnKey = { 97, 250, 1, 5, 84, 21, 7, 63,
            //                 4, 54, 87, 56, 123, 10, 3, 62,
            //                 7, 9, 20, 36, 37, 21, 101, 57};
            //            t.TripleDESKey = MyOwnKey;

            //            TrialMaker.RunTypes RT = t.ShowDialog();
            //            bool is_trial;
            //            if (RT != TrialMaker.RunTypes.Expired)
            //            {
            //                if (RT == TrialMaker.RunTypes.Full)
            //                    is_trial = false;
            //                else
            //                    is_trial = true;

            Application.Run(new FrmLogin());

            //                //if (OpenDetailFormOnClose)
            //                //{
            //                Application.Run(new FrmMain());
            //                //}
            //            }

        }
    }
}
