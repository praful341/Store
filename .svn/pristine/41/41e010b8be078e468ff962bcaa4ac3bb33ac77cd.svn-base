using BLL.FunctionClasses.Master;
using BLL.PropertyClasses.Master;
using STORE.Class;
using System;
using System.Data;
using System.Windows.Forms;

namespace STORE
{
    public partial class FrmCapitalEntryMaster : Form
    {
        BLL.FormEvents objBOFormEvents = new BLL.FormEvents();
        BLL.Validation Val = new BLL.Validation();
        CapitalMaster objCapital = new CapitalMaster();

        public FrmCapitalEntryMaster()
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
            txtCapitalEntryCode.Text = "0";
            LookupPartyName.EditValue = null;
            txtRemark.Text = "";
            LookupCapitalName.EditValue = null;
            DTPEntryDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPEntryDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPEntryDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPEntryDate.Properties.CharacterCasing = CharacterCasing.Upper;
            DTPEntryDate.EditValue = DateTime.Now;
            LookupPartyName.Focus();
            txtAmount.Text = "";
        }

        #region Validation

        private bool ValSave()
        {
            //if (txtpa.Text.Length == 0)
            //{
            //    Global.Confirm("City Name is Required");
            //    txtCityName.Focus();
            //    return false;
            //}            

            //if (!objCity.ISExists(txtCityName.Text, Val.ToInt64(txtCapitalEntryCode.EditValue)).ToString().Trim().Equals(string.Empty))
            //{
            //    Global.Confirm("City Name Already Exist.");
            //    txtCityName.Focus();
            //    txtCityName.SelectAll();
            //    return false;
            //}

            return true;
        }

        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValSave() == false)
            {
                return;
            }

            Capital_MasterProperty CapitalMasterEntryProperty = new Capital_MasterProperty();
            Int64 Code = Val.ToInt64(txtCapitalEntryCode.Text);
            CapitalMasterEntryProperty.Capital_Entry_Code = Val.ToInt64(Code);
            CapitalMasterEntryProperty.Party_Code = Val.ToInt64(LookupPartyName.EditValue);
            CapitalMasterEntryProperty.Capital_Code = Val.ToInt64(LookupCapitalName.EditValue);
            CapitalMasterEntryProperty.Amount = Val.ToDecimal(txtAmount.Text);
            CapitalMasterEntryProperty.Remark = txtRemark.Text;
            CapitalMasterEntryProperty.Entry_Date = Val.DBDate(System.DateTime.Now.ToShortDateString());
            CapitalMasterEntryProperty.Insert_Date = Val.DBDate(DTPEntryDate.Text);

            int IntRes = objCapital.CapitalEntry_Save(CapitalMasterEntryProperty);
            if (IntRes == -1)
            {
                Global.Confirm("Error In Save City Details");
                LookupPartyName.Focus();
            }
            else
            {
                if (Code == 0)
                {
                    Global.Confirm("Capital Entry Master Details Data Save Successfully");
                }
                else
                {
                    Global.Confirm("Capital Entry Master Details Data Update Successfully");
                }
                GetData();
                btnClear_Click(sender, e);
            }
            CapitalMasterEntryProperty = null;
        }

        public void GetData()
        {
            DataTable DTab = objCapital.Capital_GetData_Search();
            grdCapitalEntryMaster.DataSource = DTab;
            dgvCapitalEntryMaster.BestFitColumns();
        }

        private void FrmCountryMaster_Load(object sender, EventArgs e)
        {
            Global.LOOKUPCapital(LookupCapitalName);
            Global.LOOKUPParty(LookupPartyName);

            DTPEntryDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPEntryDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPEntryDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPEntryDate.Properties.CharacterCasing = CharacterCasing.Upper;
            DTPEntryDate.EditValue = DateTime.Now;

            GetData();
            btnClear_Click(btnClear, null);
        }

        private void dgvCountryMaster_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (e.Clicks == 2)
                {
                    DataRow Drow = dgvCapitalEntryMaster.GetDataRow(e.RowHandle);
                    txtCapitalEntryCode.Text = Val.ToString(Drow["ID"]);
                    LookupCapitalName.EditValue = Val.ToInt64(Drow["Capital_Code"]);
                    LookupPartyName.EditValue = Val.ToInt64(Drow["Party_code"]);
                    txtRemark.Text = Val.ToString(Drow["Remark"]);
                    DTPEntryDate.EditValue = Val.DBDate(Drow["Insert_Date"].ToString());
                    txtAmount.Text = Val.ToDecimal(Drow["Amount"]).ToString();
                }
            }
        }

        private void LookupPartyName_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmLedgerMaster frmCnt = new FrmLedgerMaster();
                frmCnt.ShowDialog();
                Global.LOOKUPParty(LookupPartyName);
            }
        }

        private void LookupCapitalName_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmCapitalMaster frmCnt = new FrmCapitalMaster();
                frmCnt.ShowDialog();
                Global.LOOKUPCapital(LookupCapitalName);
            }
        }
    }
}
