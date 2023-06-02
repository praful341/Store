using BLL;
using BLL.FunctionClasses.Master;
using BLL.FunctionClasses.Utility;
using STORE.Class;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace STORE
{
    public partial class FrmLogin : Form
    {
        Validation Val = new Validation();

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Shown(object sender, EventArgs e)
        {
            string appPath = Application.StartupPath.ToString();
            ClassINI iniCl = new ClassINI(appPath + "\\Store.CONFIG");
            if (!File.Exists(appPath + "\\Store.CONFIG"))
            {
                // iniCl.IniWriteValue("LOGIN", "ServerHostName", GlobalDec.Encrypt(".", true));
                //iniCl.IniWriteValue("LOGIN", "DBName", GlobalDec.Encrypt("STORE", true));
                //iniCl.IniWriteValue("LOGIN", "ServerUserName", GlobalDec.Encrypt("Praful\\Praful-it", true));
                //iniCl.IniWriteValue("LOGIN", "ServerPassWord", GlobalDec.Encrypt("", true));
                //iniCl.IniWriteValue("LOGIN", "ServerHostName", GlobalDec.Encrypt("MADHURAM-PC", true));
                iniCl.IniWriteValue("LOGIN", "ServerHostName", GlobalDec.Encrypt("192.168.1.11", true));
                iniCl.IniWriteValue("LOGIN", "DBName", GlobalDec.Encrypt("Store", true));
                iniCl.IniWriteValue("LOGIN", "ServerUserName", GlobalDec.Encrypt("sa", true));
                iniCl.IniWriteValue("LOGIN", "ServerPassWord", GlobalDec.Encrypt("admin@123", true));
            }

            //string gStrHostName = GlobalDec.Decrypt(iniCl.IniReadValue("LOGIN", "ServerHostName"), true);
            //string gStrDBName = GlobalDec.Decrypt(iniCl.IniReadValue("LOGIN", "DBName"), true);
            //string gStrUserName = GlobalDec.Decrypt(iniCl.IniReadValue("LOGIN", "ServerUserName"), true);
            //string gStrPasssword = GlobalDec.Decrypt(iniCl.IniReadValue("LOGIN", "ServerPassWord"), true);
            string gStrHostName = iniCl.IniReadValue("LOGIN", "ServerHostName");
            string gStrDBName = iniCl.IniReadValue("LOGIN", "DBName");
            string gStrUserName = iniCl.IniReadValue("LOGIN", "ServerUserName");
            string gStrPasssword = iniCl.IniReadValue("LOGIN", "ServerPassWord");

            //BLL.DBConnections.ConnectionString = @"Data Source=SOFT-IT;Initial Catalog=STORE;Integrated Security=True";
            //BLL.DBConnections.ConnectionString = @"Data Source=193.167.2.9;Initial Catalog=STORE;User ID=sa;Password=admin@123";
            //BLL.DBConnections.ConnectionString = @"Data Source=MADHURAM-PC;Initial Catalog=STORE;User ID=sa;Password=admin@123";
            //BLL.DBConnections.ConnectionString = @"Data Source=PRASHANT-PC;Initial Catalog=STORE;Integrated Security=True";
            //BLL.DBConnections.ConnectionString = @"Data Source=(local);Initial Catalog=STORE1;Integrated Security=True";

            BLL.DBConnections.ConnectionString = "Data Source=" + gStrHostName + ";Initial Catalog=" + gStrDBName + ";User ID=" + gStrUserName + ";Password=" + gStrPasssword + ";";

            BLL.DBConnections.ProviderName = "System.Data.SqlClient";

            //txtUserName.Text = "PRAFUL";
            //txtPassword.Text = "123";
            //btnLogin_Click(null, null);

            // Comment By Praful On 08092017

            ////string appPath = Application.StartupPath.ToString();
            ////ClassINI iniCl = new ClassINI(appPath + "\\Store.CONFIG"); ;
            ////if (!File.Exists(appPath + "\\Store.CONFIG"))
            ////{
            ////    //iniCl.IniWriteValue("LOGIN", "ServerHostName", GlobalDec.Encrypt(".", true));
            ////    //iniCl.IniWriteValue("LOGIN", "DBName", GlobalDec.Encrypt("STORE", true));
            ////    //iniCl.IniWriteValue("LOGIN", "ServerUserName", GlobalDec.Encrypt("SOFT-IT\\Soft", true));
            ////    //iniCl.IniWriteValue("LOGIN", "ServerPassWord", GlobalDec.Encrypt("", true));
            ////    iniCl.IniWriteValue("LOGIN", "ServerHostName", GlobalDec.Encrypt("192.168.2.81", true));
            ////    iniCl.IniWriteValue("LOGIN", "DBName", GlobalDec.Encrypt("STORE", true));
            ////    iniCl.IniWriteValue("LOGIN", "ServerUserName", GlobalDec.Encrypt("sa", true));
            ////    iniCl.IniWriteValue("LOGIN", "ServerPassWord", GlobalDec.Encrypt("admin@123", true));
            ////}

            ////string gStrHostName = GlobalDec.Decrypt(iniCl.IniReadValue("LOGIN", "ServerHostName"), true);
            ////string gStrDBName = GlobalDec.Decrypt(iniCl.IniReadValue("LOGIN", "DBName"), true);
            ////string gStrUserName = GlobalDec.Decrypt(iniCl.IniReadValue("LOGIN", "ServerUserName"), true);
            ////string gStrPasssword = GlobalDec.Decrypt(iniCl.IniReadValue("LOGIN", "ServerPassWord"), true);

            //////BLL.DBConnections.ConnectionString = @"Data Source=SOFT-IT;Initial Catalog=STORE;Integrated Security=True";
            //////BLL.DBConnections.ConnectionString = @"Data Source=192.168.2.217;Initial Catalog=STORE;User ID=sa;Password=admin@123";
            ////BLL.DBConnections.ConnectionString = @"Data Source=192.168.2.81;Initial Catalog=STORE;User ID=sa;Password=admin@123";
            //////BLL.DBConnections.ConnectionString = @"Data Source=PRASHANT-PC;Initial Catalog=STORE;Integrated Security=True";
            ////// BLL.DBConnections.ConnectionString = @"Data Source=(local);Initial Catalog=STORE;Integrated Security=True";

            //////    BLL.DBConnections.ConnectionString = "Data Source=" + gStrHostName + ";Initial Catalog=" + gStrDBName + ";User ID=" + gStrUserName + ";Password=" + gStrPasssword + ";";

            ////BLL.DBConnections.ProviderName = "System.Data.SqlClient";

            ////txtUserName.Text = "PRAFUL";
            ////txtPassword.Text = "123";
            ////btnLogin_Click(null, null);

            // End By Praful On 08092017
        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text.Length == 0)
            {
                Global.Confirm("Please Enter UserName");
                txtUserName.Focus();
                return;
            }
            if (txtPassword.Text.Length == 0)
            {
                Global.Confirm("Please Enter Password");
                txtPassword.Focus();
                return;
            }

            //if (txtUserName.Text == GlobalDec.gEmployeeProperty.UserName)
            //{
            //    MARGO.Class.Global.Message("Your are already Loged In");
            //    txtUserName.Focus();
            //    return;
            //}

            this.Cursor = Cursors.WaitCursor;

            Login objLogin = new Login();
            int IntRes = objLogin.CheckLogin(txtUserName.Text, GlobalDec.Encrypt(txtPassword.Text, true));

            this.Cursor = Cursors.Default;
            if (IntRes == -1)
            {
                Global.Confirm("Enter Valid UserName And Password");
                txtUserName.Focus();
                //panelControl1.Enabled = false;
                return;
            }
            else
            {
                FinancialYearMaster ObjFinancial = new FinancialYearMaster();
                DataTable tdt = ObjFinancial.GetData();
                GlobalDec.gEmployeeProperty.gFinancialYear = Val.ToString(tdt.Rows[0]["FINANCIAL_YEAR"]);
                GlobalDec.gEmployeeProperty.gFinancialYear_Code = Val.ToInt64(tdt.Rows[0]["FIN_YEAR_CODE"]);

                //this.Close();
                //txtUserName.Text = "";
                //txtPassword.Text = "";
                //panelControl1.Enabled = true;
                //FrmMain Log = new FrmMain();
                //Log.ShowDialog();

                FrmMain MainForm = new FrmMain();
                BLL.FormPer ObjPer = new BLL.FormPer();
                Global.gMainFormRef = MainForm;
                this.Hide();
                MainForm.ShowDialog();
                //FrmHomePage MainForm = new FrmHomePage();
                //BLL.FormPer ObjPer = new BLL.FormPer();
                //Global.gMainFormRef = MainForm;
                //this.Hide();
                //MainForm.ShowDialog();
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }

        private void txtUserName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
            }
        }
    }
}
