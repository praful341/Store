using BLL.FunctionClasses.Master;
using BLL.PropertyClasses.Master;
using STORE.Class;
using System;
using System.Data;
using System.Windows.Forms;

namespace STORE
{
    public partial class FrmCapitalMaster : Form
    {
        BLL.FormEvents objBOFormEvents = new BLL.FormEvents();
        BLL.Validation Val = new BLL.Validation();
        CapitalMaster objCapital = new CapitalMaster();

        public FrmCapitalMaster()
        {
            InitializeComponent();
        }
        public void ShowForm()
        {
            Val.frmGenSet(this);
            AttachFormEvents();
            this.Show();
        }
        private void AttachFormEvents()
        {
            objBOFormEvents.CurForm = this;
            objBOFormEvents.FormKeyPress = true;
            objBOFormEvents.FormKeyDown = true;
            objBOFormEvents.FormResize = true;
            objBOFormEvents.FormClosing = true;
            //objBOFormEvents.ObjToDispose.Add(ObjGroup);
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCapitalCode.Text = "0";
            txtCapitalName.Text = "";
            txtRemark.Text = "";
            txtCapitalName.Focus();
        }

        #region Validation

        private bool ValSave()
        {
            if (txtCapitalName.Text.Length == 0)
            {
                Global.Confirm("Capital Name is Required");
                txtCapitalName.Focus();
                return false;
            }

            if (!objCapital.ISExists(txtCapitalName.Text, Val.ToInt64(txtCapitalCode.EditValue)).ToString().Trim().Equals(string.Empty))
            {
                Global.Confirm("Capital Name Already Exist.");
                txtCapitalName.Focus();
                txtCapitalName.SelectAll();
                return false;
            }

            return true;
        }

        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValSave() == false)
            {
                return;
            }

            Capital_MasterProperty CapitalMasterProperty = new Capital_MasterProperty();
            Int64 Code = Val.ToInt64(txtCapitalCode.Text);
            CapitalMasterProperty.Capital_Code = Val.ToInt64(Code);
            CapitalMasterProperty.Capital_Name = txtCapitalName.Text;
            CapitalMasterProperty.Remark = txtRemark.Text;

            int IntRes = objCapital.Save(CapitalMasterProperty);
            if (IntRes == -1)
            {
                Global.Confirm("Error In Save Capital Details");
                txtCapitalName.Focus();
            }
            else
            {
                if (Code == 0)
                {
                    Global.Confirm("Capital Details Data Save Successfully");
                }
                else
                {
                    Global.Confirm("Capital Details Data Update Successfully");
                }
                GetData();
                btnClear_Click(sender, e);
            }
            CapitalMasterProperty = null;
        }

        public void GetData()
        {
            DataTable DTab = objCapital.GetData_Search();
            grdCapitalMaster.DataSource = DTab;
        }

        private void FrmCountryMaster_Load(object sender, EventArgs e)
        {
            GetData();
            btnClear_Click(btnClear, null);
        }

        private void dgvCountryMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (e.Clicks == 2)
                {
                    DataRow Drow = dgvCapitalMaster.GetDataRow(e.RowHandle);
                    txtCapitalCode.Text = Val.ToString(Drow["Capital_Code"]);
                    txtCapitalName.Text = Val.ToString(Drow["Capital_Name"]);
                    txtRemark.Text = Val.ToString(Drow["Remark"]);
                }
            }
        }
    }
}
