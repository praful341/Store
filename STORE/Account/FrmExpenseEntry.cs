using BLL.FunctionClasses.Master;
using BLL.PropertyClasses.Master;
using STORE;
using STORE.Class;
using System;
using System.Data;
using System.Windows.Forms;

namespace STORE
{
    public partial class FrmExpenseEntry : Form
    {
        BLL.FormEvents objBOFormEvents = new BLL.FormEvents();
        BLL.Validation Val = new BLL.Validation();
        ExpenseEntryMaster objExpenseEntry = new ExpenseEntryMaster();

        public FrmExpenseEntry()
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
            txtExpenseEntryCode.Text = "0";
            LookupPartyName.EditValue = null;
            txtRemark.Text = "";
            CmbCashBankType.SelectedIndex = 0;
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

            ExpenseEntry_MasterProperty ExpenseEntryMasterProperty = new ExpenseEntry_MasterProperty();
            Int64 Code = Val.ToInt64(txtExpenseEntryCode.Text);
            ExpenseEntryMasterProperty.Expense_ID = Val.ToInt64(Code);
            ExpenseEntryMasterProperty.Party_Code = Val.ToInt64(LookupPartyName.EditValue);
            ExpenseEntryMasterProperty.Cash_Type = Val.ToString(CmbCashBankType.EditValue);
            ExpenseEntryMasterProperty.Amount = Val.ToDecimal(txtAmount.Text);
            ExpenseEntryMasterProperty.Remark = txtRemark.Text;
            ExpenseEntryMasterProperty.Entry_Date = Val.DBDate(System.DateTime.Now.ToShortDateString());
            ExpenseEntryMasterProperty.Insert_Date = Val.DBDate(DTPEntryDate.Text);

            int IntRes = objExpenseEntry.ExpenseEntry_Save(ExpenseEntryMasterProperty);
            if (IntRes == -1)
            {
                Global.Confirm("Error In Save Expense Entry Details");
                LookupPartyName.Focus();
            }
            else
            {
                if (Code == 0)
                {
                    Global.Confirm("Expense Entry Master Details Data Save Successfully");
                }
                else
                {
                    Global.Confirm("Expense Entry Master Details Data Update Successfully");
                }
                GetData();
                btnClear_Click(sender, e);
            }
            ExpenseEntryMasterProperty = null;
        }

        public void GetData()
        {
            DataTable DTab = objExpenseEntry.Expense_Entry_GetData_Search();
            grdIncomeEntryMaster.DataSource = DTab;
            dgvIncomeEntryMaster.BestFitColumns();
        }

        private void FrmCountryMaster_Load(object sender, EventArgs e)
        {
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
                    DataRow Drow = dgvIncomeEntryMaster.GetDataRow(e.RowHandle);
                    txtExpenseEntryCode.Text = Val.ToString(Drow["Expense_ID"]);
                    LookupPartyName.EditValue = Val.ToInt64(Drow["Party_code"]);
                    CmbCashBankType.EditValue = Convert.ToString(Drow["Cash_Type"]);
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
    }
}
