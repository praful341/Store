using BLL;
using BLL.FunctionClasses.Account;
using BLL.FunctionClasses.Transaction;
using BLL.PropertyClasses.Master;
using BLL.PropertyClasses.Transaction;
using DevExpress.XtraEditors;
using STORE.Class;
using STORE.Report;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace STORE.Transaction
{
    public partial class FrmItemSaleRetMaster : Form
    {
        BLL.FormEvents objBOFormEvents = new BLL.FormEvents();
        BLL.Validation Val = new BLL.Validation();
        ItemPurchase ObjItemPurchase = new ItemPurchase();
        ItemPurchaseMaster ObjPurchase = new ItemPurchaseMaster();
        Invoice_Entry ObjInvoiceEntry = new Invoice_Entry();
        string Form_Type = "";

        public FrmItemSaleRetMaster(string Type_)
        {
            InitializeComponent();
            Form_Type = Type_;
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtInvoiceNo.Text.Trim()))
            {
                Global.Confirm("Invoice No Is Required");
                txtInvoiceNo.Focus();
                return;
            }

            if (string.IsNullOrEmpty(DTPInvoiceDate.Text.Trim()))
            {
                Global.Confirm("Invoice Date Is Required");
                DTPInvoiceDate.Focus();
                return;
            }

            if (string.IsNullOrEmpty(LookupFromParty.Text.Trim()))
            {
                Global.Confirm("Party Name Is Required");
                LookupFromParty.Focus();
                return;
            }

            PanelShow.Enabled = false;
            PanelSave.Enabled = true;
            GrdPurchase.Enabled = true;
            GetPurchaseDetail();
            dgvPurchase.BestFitColumns();
            dgvPurchase.Focus();
        }

        public Invoice_EntryProperty SaveItemPurchaseMaster()
        {
            Invoice_EntryProperty Invoice_EntryProperty = new Invoice_EntryProperty();

            Invoice_EntryProperty.Purchase_Master_ID = Val.ToInt64(txtPurchaseMasterID.Text);
            Invoice_EntryProperty.Financial_Year = GlobalDec.gEmployeeProperty.gFinancialYear;
            Invoice_EntryProperty.Invoice_No = Val.ToString(txtInvoiceNo.Text);
            Invoice_EntryProperty.Invoice_Date = Val.DBDate(DTPInvoiceDate.Text);
            Invoice_EntryProperty.Transaction_Date = Val.DBDate(DTPTranDate.Text);
            Invoice_EntryProperty.Payment_Mode = Val.ToString(CmbPaymentMode.SelectedItem);
            Invoice_EntryProperty.Payment_Days = Val.ToString(txtPaymentDays.Text);
            Invoice_EntryProperty.Payment_Date = Val.DBDate(DTPPaymentDate.Text);
            Invoice_EntryProperty.From_Party_Code = Val.ToInt64(LookupFromParty.EditValue);
            Invoice_EntryProperty.IS_Reverse = Val.ToInt64(CHKReverse.EditValue);
            Invoice_EntryProperty.Remark = txtRemark.Text;

            Invoice_EntryProperty.CGST = Val.Val(txtCGST.Text);
            Invoice_EntryProperty.IGST = Val.Val(txtIGST.Text);
            Invoice_EntryProperty.SGST = Val.Val(txtSGST.Text);
            Invoice_EntryProperty.Gross_Amt = Val.Val(txtGrossAmtLocal.Text);
            Invoice_EntryProperty.Add_Amt = Val.Val(txtTotalAddAmount.Text);
            Invoice_EntryProperty.Less_Amt = Val.Val(txtTotalLessAmount.Text);
            Invoice_EntryProperty.Net_Amt = Val.Val(txtNetAmtLocal.Text);
            Invoice_EntryProperty.Challan_No = Val.ToString(txtChallanNo.Text);

            return Invoice_EntryProperty;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValSave() == false)
            {
                return;
            }

            if (Global.Confirm("Are You Sure To Save ?", "STORE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }
            Int64 IntRes = 0;
            Invoice_EntryProperty Invoice_EntryPropertyNew = new Invoice_EntryProperty();

            Invoice_EntryPropertyNew = SaveItemPurchaseMaster();
            Invoice_EntryPropertyNew = ObjInvoiceEntry.SaveInvoiceEntryMaster(Invoice_EntryPropertyNew, Form_Type);
            Int64 Newmstid = Val.ToInt64(Invoice_EntryPropertyNew.Purchase_Master_ID.ToString());

            Invoice_EntryPropertyNew = null;

            ArrayList AL = new ArrayList();

            DataTable DTab = (System.Data.DataTable)GrdPurchase.DataSource;
            DTab.AcceptChanges();

            foreach (DataRow DRow in DTab.Rows)
            {
                if (Val.Val(DRow["ITEM_CODE"]) == 0)
                {
                    continue;
                }

                Invoice_EntryProperty Invoice_EntryProperty = new Invoice_EntryProperty();

                Invoice_EntryProperty.SGST = 0;
                Invoice_EntryProperty.SGST_Amt = 0;
                Invoice_EntryProperty.CGST = 0;
                Invoice_EntryProperty.CGST_Amt = 0;
                Invoice_EntryProperty.IGST = Val.Val(DRow["IGST_Amt"]);
                Invoice_EntryProperty.IGST_Amt = Val.Val(DRow["IGST_Amt"]);
                Int64 ItemSaleRetMasterID = Val.ToInt64(DRow["ItemSaleReturnMasterID"]);
                if (ItemSaleRetMasterID == 0)
                {
                    Invoice_EntryProperty.ItemSaleReturnMasterID = Val.ToInt64(Newmstid);
                    Invoice_EntryProperty.ItemSaleReturnDtlID = Val.ToInt64(DRow["ItemSaleReturnDtlID"]);
                }
                else
                {
                    Invoice_EntryProperty.ItemSaleReturnMasterID = Val.ToInt64(DRow["ItemSaleReturnMasterID"]);
                    Invoice_EntryProperty.ItemSaleReturnDtlID = Val.ToInt64(DRow["ItemSaleReturnDtlID"]);
                }

                Invoice_EntryProperty.HSN_ID = Val.ToInt64(DRow["HSN_ID"]);
                Invoice_EntryProperty.Item_Code = Val.ToInt64(DRow["Item_Code"]);
                Invoice_EntryProperty.Unit_Type = Val.ToString(DRow["Unit_Name"]);
                Invoice_EntryProperty.Quantity = Val.Val(DRow["Quantity"]);
                Invoice_EntryProperty.Rate_Dollar = Val.Val(DRow["Rate"]);
                Invoice_EntryProperty.Gross_Amt = Val.Val(DRow["Gross_Amt"]);
                Invoice_EntryProperty.Discount_Per = Val.Val(DRow["Discount"]);
                Invoice_EntryProperty.Net_Amt = Val.Val(DRow["NetAmount"]);
                Invoice_EntryProperty.Remark = Val.ToString(DRow["Remarks"]);

                AL.Add(Invoice_EntryProperty);
            }
            IntRes = ObjInvoiceEntry.SavePurchaseDetail(AL, Form_Type);

            if (IntRes != 0)
            {
                Global.Confirm("Save Data Successfully");
                GetData();
                btnClear_Click(null, null);
            }
            else
            {
                Global.Confirm("Error in Data Save");
                txtInvoiceNo.Focus();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPurchaseMasterID.Text = "";
            DTPInvoiceDate.Text = "";
            LookupFromParty.EditValue = null;
            CmbPaymentMode.Text = "";
            txtPaymentDays.Text = "";
            txtRemark.Text = "";

            //txtInvoiceNo.Text = "";
            string Invoice_No = Val.ToString(ObjInvoiceEntry.GEtMaximumID_New("Sales_Return"));
            txtInvoiceNo.Text = Invoice_No.ToString();
            txtInvoiceNo.Enabled = true;

            string Challan_No = Val.ToString(ObjInvoiceEntry.GEtMaximumID_New("Challan"));
            txtChallanNo.Text = Challan_No.ToString();

            txtSGST.Text = "";
            txtCGST.Text = "";
            txtIGST.Text = "";
            txtTotalAddAmount.Text = "0";
            txtTotalLessAmount.Text = "0";
            txtGrossAmtLocal.Text = "0";
            txtNetAmtLocal.Text = "0";
            CHKReverse.EditValue = 0;
            //DTPTranDate.Text = DateTime.Now.ToShortDateString();
            //DTPInvoiceDate.Text = DateTime.Now.ToShortDateString();
            DTPTranDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPTranDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPTranDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPTranDate.Properties.CharacterCasing = CharacterCasing.Upper;

            DTPInvoiceDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPInvoiceDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPInvoiceDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPInvoiceDate.Properties.CharacterCasing = CharacterCasing.Upper;

            DTPTranDate.EditValue = DateTime.Now;
            DTPInvoiceDate.EditValue = DateTime.Now;

            CmbPaymentMode.SelectedIndex = 0;
            CalculateGridAmount(dgvPurchase.FocusedRowHandle);
            txtPurchaseMasterID.Text = ObjInvoiceEntry.FindNewID(Form_Type).ToString();
            //txtChallanNo.Text = "";

            PanelShow.Enabled = true;
            GrdPurchase.Enabled = false;
            PanelSave.Enabled = false;
            GrdPurchase.DataSource = null;
            DTPInvoiceDate.Focus();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region functions

        private bool ValSave()
        {
            if (string.IsNullOrEmpty(txtInvoiceNo.Text.Trim()))
            {
                Global.Confirm("Invoice No Is Required");
                txtInvoiceNo.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(DTPInvoiceDate.Text.Trim()))
            {
                Global.Confirm("Invoice Date Is Required");
                DTPInvoiceDate.Focus();
                return false;
            }

            if (Val.ToString(LookupFromParty.Text.Trim()) == "")
            {
                Global.Confirm("From Party Is Required");
                LookupFromParty.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(DTPTranDate.Text.Trim()))
            {
                Global.Confirm("Party Tansaction Date Is Required");
                DTPTranDate.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(CmbPaymentMode.Text.Trim()))
            {
                Global.Confirm("Payment Type Is Required");
                CmbPaymentMode.Focus();
                return false;
            }

            DataTable DTab = (DataTable)GrdPurchase.DataSource;
            if (DTab != null)
            {
                if (DTab.Rows.Count <= 0)
                {
                    Global.Confirm("Fill Purchase Items list");
                    txtInvoiceNo.Focus();
                    return false;
                }
            }
            return true;
        }

        private void GetPurchaseDetail()
        {
            this.Cursor = Cursors.WaitCursor;
            GrdPurchase.DataSource = null;
            DataTable DTab = ObjInvoiceEntry.GetPurchaseDetail(Form_Type, Val.ToInt64(txtPurchaseMasterID.Text));
            GrdPurchase.DataSource = DTab;
            this.Cursor = Cursors.Default;
        }

        #endregion

        private void LookupFromParty_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmLedgerMaster frmCnt = new FrmLedgerMaster();
                frmCnt.ShowDialog();
                Ledger_MasterProperty Party = new Ledger_MasterProperty();
                Party.Party_Type = "";
                Global.LOOKUPParty(LookupFromParty, Party);
                Party = null;
            }
        }

        private void FrmItemPurchaseMaster_Shown(object sender, EventArgs e)
        {
            Global.LOOKUPItemRep(RepLookUpItem);
            Global.LOOKUPItemHSNRep(RepHSNCode);
            btnClear_Click(btnClear, null);
            Ledger_MasterProperty Party = new Ledger_MasterProperty();
            Party.Party_Type = "";
            Global.LOOKUPParty(LookupFromParty, Party);
            Party = null;

            this.Text = "Sales Return Invoice";
            lblTerms.Visible = false;
            DTPPaymentDate.Visible = false;
            txtPaymentDays.Visible = false;
            labelControl1.Visible = false;
            CmbPaymentMode.Visible = false;

            //dtpSearchFromDate.Text = Val.DBDate(System.DateTime.Now.ToShortDateString());
            //dtpSearchToDate.Text = Val.DBDate(System.DateTime.Now.ToShortDateString());
            dtpSearchFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            dtpSearchFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            dtpSearchFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            dtpSearchFromDate.Properties.CharacterCasing = CharacterCasing.Upper;

            dtpSearchToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            dtpSearchToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            dtpSearchToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            dtpSearchToDate.Properties.CharacterCasing = CharacterCasing.Upper;

            dtpSearchFromDate.EditValue = DateTime.Now;
            dtpSearchToDate.EditValue = DateTime.Now;

            GetData();
            btnClear_Click(btnClear, null);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            PanelShow.Enabled = true;
            GrdPurchase.Enabled = false;
            PanelSave.Enabled = false;
            GrdPurchase.DataSource = null;
        }

        private Boolean ValDelete()
        {
            if (Val.Val(txtInvoiceNo.Text) == 0)
            {
                Global.Message("Invoice No Is Required");
                txtInvoiceNo.Focus();
                return false;
            }
            return true;
        }

        private void gridView2_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                if (e.Clicks == 2)
                {
                    DataRow DRow = gridView2.GetDataRow(e.RowHandle);

                    txtPurchaseMasterID.Text = Val.ToString(DRow["ItemSaleReturnMasterID"]);
                    txtInvoiceNo.Text = Val.ToString(DRow["Inovice_No"]);
                    DTPTranDate.EditValue = Val.DBDate(DRow["Trans_Date"].ToString());
                    DTPInvoiceDate.EditValue = Val.DBDate(DRow["Invoice_Date"].ToString());
                    LookupFromParty.EditValue = Val.ToInt64(DRow["Ledger_Code"]);
                    CmbPaymentMode.Text = Val.ToString(DRow["Payment_Type"]);
                    CHKReverse.EditValue = Val.ToInt64(DRow["Is_Reverse"]);
                    txtPaymentDays.Text = Val.ToString(DRow["Terms"]);
                    DTPPaymentDate.EditValue = Val.DBDate(DRow["Term_Date"].ToString());
                    txtRemark.Text = Val.ToString(DRow["Remark"]);
                    txtCGST.Text = Val.ToString(DRow["CGST_Amt"]);
                    txtIGST.Text = Val.ToString(DRow["IGST_Amt"]);
                    txtSGST.Text = Val.ToString(DRow["SGST_Amt"]);
                    txtGrossAmtLocal.Text = Val.ToString(DRow["Gross_Amt"]);
                    txtNetAmtLocal.Text = Val.ToString(DRow["Net_Amt"]);
                    txtTotalAddAmount.Text = Val.ToString(DRow["Add_Amt"]);
                    txtTotalLessAmount.Text = Val.ToString(DRow["Less_Amt"]);
                    txtChallanNo.Text = Val.ToString(DRow["Challan_No"]);

                    GetPurchaseDetail();
                    PanelShow.Enabled = true;
                    GrdPurchase.Enabled = true;
                    PanelSave.Enabled = true;
                    CalculateGridAmount(dgvPurchase.FocusedRowHandle);
                    txtInvoiceNo.Focus();
                }
            }
        }

        private void RepLookUpItem_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmItemMaster frmCnt = new FrmItemMaster();
                frmCnt.ShowDialog();
                Global.LOOKUPItemRep(RepLookUpItem);
            }
        }

        public DataTable GetData()
        {
            Invoice_EntryProperty Invoice_EntryProperty = new Invoice_EntryProperty();
            Invoice_EntryProperty.From_Date = Val.DBDate(dtpSearchFromDate.Text);
            Invoice_EntryProperty.To_Date = Val.DBDate(dtpSearchToDate.Text);
            DataTable DTab = ObjInvoiceEntry.GetData(Invoice_EntryProperty, Form_Type);
            gridControl2.DataSource = DTab;
            gridControl2.RefreshDataSource();
            gridView2.BestFitColumns();
            Invoice_EntryProperty = null;
            GetPurchaseDetail();
            return DTab;
        }

        private void RepLookUpItem_Validating(object sender, CancelEventArgs e)
        {
            LookUpEdit type = sender as LookUpEdit;

            dgvPurchase.SetRowCellValue(dgvPurchase.FocusedRowHandle, "Unit_Name", type.GetColumnValue("UNIT_NAME"));
            dgvPurchase.SetRowCellValue(dgvPurchase.FocusedRowHandle, "HSN_ID", type.GetColumnValue("HSN_ID"));
            dgvPurchase.SetRowCellValue(dgvPurchase.FocusedRowHandle, "Rate", type.GetColumnValue("LAST_PURCHASE_RATE"));
            dgvPurchase.SetRowCellValue(dgvPurchase.FocusedRowHandle, "SGST_Rate", type.GetColumnValue("SGST_RATE"));
            dgvPurchase.SetRowCellValue(dgvPurchase.FocusedRowHandle, "CGST_Rate", type.GetColumnValue("CGST_RATE"));
            dgvPurchase.SetRowCellValue(dgvPurchase.FocusedRowHandle, "IGST_Rate", type.GetColumnValue("IGST_RATE"));
        }

        private void LookupFromParty_EditValueChanged(object sender, EventArgs e)
        {
            if (LookupFromParty.Text.Trim().Length > 0)
            {
                txtPartyState.Text = LookupFromParty.GetColumnValue("Party_State_Code").ToString();
                string gst = LookupFromParty.GetColumnValue("GSTIN").ToString();
                if (gst.Length > 0) { CHKReverse.EditValue = 1; } else { CHKReverse.EditValue = 0; }
                dgvPurchase.PostEditor();
            }
            else
            {
                txtPartyState.Text = ""; CHKReverse.EditValue = 0;
            }
            chkGST();
        }

        private void chkGST()
        {
            if (txtPartyState.Text == GlobalDec.gEmployeeProperty.State_Code.ToString())
            {
                txtSGST.Visible = true;
                lblSGST.Visible = true;
                txtCGST.Visible = true;
                lblCGST.Visible = true;
                txtIGST.Visible = false;
                lblIGST.Visible = false;

                dgvPurchase.Columns["SGST_Rate"].Visible = true;
                dgvPurchase.Columns["SGST_Amt"].Visible = true;
                dgvPurchase.Columns["CGST_Rate"].Visible = true;
                dgvPurchase.Columns["CGST_Amt"].Visible = true;
                dgvPurchase.Columns["IGST_Rate"].Visible = false;
                dgvPurchase.Columns["IGST_Amt"].Visible = false;
            }
            else
            {
                txtSGST.Visible = false;
                lblSGST.Visible = false;
                txtCGST.Visible = false;
                lblCGST.Visible = false;
                txtIGST.Visible = true;
                lblIGST.Visible = true;
                dgvPurchase.Columns["SGST_Rate"].Visible = false;
                dgvPurchase.Columns["SGST_Amt"].Visible = false;
                dgvPurchase.Columns["CGST_Rate"].Visible = false;
                dgvPurchase.Columns["CGST_Amt"].Visible = false;
                dgvPurchase.Columns["IGST_Rate"].Visible = true;
                dgvPurchase.Columns["IGST_Amt"].Visible = true;
            }
        }

        private void txtPaymentDays_EditValueChanged(object sender, EventArgs e)
        {
            dgvPurchase.PostEditor();
            if (DTPInvoiceDate.Text.Length <= 0 || txtPaymentDays.Text == "")
            {
                txtPaymentDays.Text = "";
                DTPPaymentDate.EditValue = null;
            }
            else
            {
                DateTime Date = Convert.ToDateTime(DTPInvoiceDate.EditValue).AddDays(Val.ToDouble(txtPaymentDays.Text));
                DTPPaymentDate.EditValue = Val.DBDate(Date.ToShortDateString());
            }
        }

        private void RepHSNCode_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmItemHSNMaster frmCnt = new FrmItemHSNMaster();
                frmCnt.ShowDialog();
                Global.LOOKUPItemHSNRep(RepHSNCode);
            }
        }

        private void CalculateGridAmount(int rowindex)
        {
            try
            {
                dgvPurchase.SetRowCellValue(rowindex, "Gross_Amt", Math.Round(Val.ToDouble(dgvPurchase.GetRowCellValue(rowindex, "Rate")) * Val.ToDouble(dgvPurchase.GetRowCellValue(rowindex, "Quantity")), 2));
                double GrossAmt = Val.ToDouble(dgvPurchase.Columns["NetAmount"].SummaryText);

                if (txtPartyState.Text == GlobalDec.gEmployeeProperty.State_Code.ToString())
                {
                    dgvPurchase.SetRowCellValue(rowindex, "SGST_Amt", (Math.Round(Val.ToDouble(dgvPurchase.GetRowCellValue(rowindex, "SGST_Rate")) * Val.ToDouble(dgvPurchase.GetRowCellValue(rowindex, "Gross_Amt")), 2)) / 100);
                    dgvPurchase.SetRowCellValue(rowindex, "IGST_Amt", (Math.Round(Val.ToDouble("0"), 2)));
                    dgvPurchase.SetRowCellValue(rowindex, "CGST_Amt", (Math.Round(Val.ToDouble(dgvPurchase.GetRowCellValue(rowindex, "CGST_Rate")) * Val.ToDouble(dgvPurchase.GetRowCellValue(rowindex, "Gross_Amt")), 2)) / 100);
                    dgvPurchase.SetRowCellValue(rowindex, "NetAmount", Math.Round(Val.ToDouble(dgvPurchase.GetRowCellValue(rowindex, "Gross_Amt")) - Val.ToDouble(dgvPurchase.GetRowCellValue(rowindex, "Discount")) + Val.ToDouble(dgvPurchase.GetRowCellValue(rowindex, "SGST_Amt")) + Val.ToDouble(dgvPurchase.GetRowCellValue(rowindex, "IGST_Amt")) + Val.ToDouble(dgvPurchase.GetRowCellValue(rowindex, "CGST_Amt")), 2));

                    txtGrossAmtLocal.Text = GrossAmt.ToString();
                }
                else
                {
                    dgvPurchase.SetRowCellValue(rowindex, "SGST_Amt", (Math.Round(Val.ToDouble("0"), 2)));
                    dgvPurchase.SetRowCellValue(rowindex, "IGST_Amt", (Math.Round(Val.ToDouble(dgvPurchase.GetRowCellValue(rowindex, "IGST_Rate")) * Val.ToDouble(dgvPurchase.GetRowCellValue(rowindex, "Gross_Amt")), 2)) / 100);
                    dgvPurchase.SetRowCellValue(rowindex, "CGST_Amt", (Math.Round(Val.ToDouble("0"), 2)));
                    dgvPurchase.SetRowCellValue(rowindex, "NetAmount", Math.Round(Val.ToDouble(dgvPurchase.GetRowCellValue(rowindex, "Gross_Amt")) - Val.ToDouble(dgvPurchase.GetRowCellValue(rowindex, "Discount")) + Val.ToDouble(dgvPurchase.GetRowCellValue(rowindex, "SGST_Amt")) + Val.ToDouble(dgvPurchase.GetRowCellValue(rowindex, "IGST_Amt")) + Val.ToDouble(dgvPurchase.GetRowCellValue(rowindex, "CGST_Amt")), 2));

                    txtGrossAmtLocal.Text = GrossAmt.ToString();
                }
            }
            catch (Exception)
            {
            }
        }

        private void dgvPurchase_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            CalculateGridAmount(dgvPurchase.FocusedRowHandle);
            GetSummary();
        }

        private void dgvPurchase_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            CalculateGridAmount(e.PrevFocusedRowHandle);
            GetSummary();
        }

        private void dgvPurchase_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            CalculateGridAmount(dgvPurchase.FocusedRowHandle);
            GetSummary();
        }

        private void GetSummary()
        {
            double IGST = Val.ToDouble(dgvPurchase.Columns["IGST_Amt"].SummaryText);
            double CGST = Val.ToDouble(dgvPurchase.Columns["CGST_Amt"].SummaryText);
            double SGST = Val.ToDouble(dgvPurchase.Columns["SGST_Amt"].SummaryText);
            double GrossAmt = Val.ToDouble(dgvPurchase.Columns["NetAmount"].SummaryText);
            txtIGST.Text = IGST.ToString();
            txtCGST.Text = CGST.ToString();
            txtSGST.Text = SGST.ToString();

            if (txtPartyState.Text == GlobalDec.gEmployeeProperty.State_Code.ToString())
            {
                txtGrossAmtLocal.Text = GrossAmt.ToString();
            }
            else
            {
                txtGrossAmtLocal.Text = GrossAmt.ToString();
            }
        }

        private void txtTotalAddAmount_EditValueChanged(object sender, EventArgs e)
        {
            double GrsAmt = Val.ToDouble(txtGrossAmtLocal.Text);
            double AddAmt = Val.ToDouble(txtTotalAddAmount.Text);
            double LessAmt = Val.ToDouble(txtTotalLessAmount.Text);
            txtNetAmtLocal.Text = Val.ToDouble(GrsAmt + AddAmt - LessAmt).ToString();
        }

        private void txtTotalLessAmount_EditValueChanged(object sender, EventArgs e)
        {
            double GrsAmt = Val.ToDouble(txtGrossAmtLocal.Text);
            double AddAmt = Val.ToDouble(txtTotalAddAmount.Text);
            double LessAmt = Val.ToDouble(txtTotalLessAmount.Text);
            txtNetAmtLocal.Text = Val.ToDouble(GrsAmt + AddAmt - LessAmt).ToString();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Invoice_EntryProperty Invoice_EntryPropertyNew = new Invoice_EntryProperty();
            Invoice_EntryPropertyNew.Invoice_Date = Val.DBDate(DTPInvoiceDate.Text);
            Invoice_EntryPropertyNew.Invoice_No = Val.ToString(txtInvoiceNo.Text);
            Invoice_EntryPropertyNew.Trn_Id = Val.ToInt64(txtPurchaseMasterID.Text);
            Invoice_EntryPropertyNew.Type = Val.ToString(Form_Type);

            DataTable dtpur = new DataTable();
            dtpur = ObjInvoiceEntry.GetPrintData(Invoice_EntryPropertyNew);

            FrmReportViewer FrmReportViewer = new FrmReportViewer();
            FrmReportViewer.DS.Tables.Add(dtpur);
            FrmReportViewer.GroupBy = "";
            FrmReportViewer.RepName = "";
            FrmReportViewer.RepPara = "";
            this.Cursor = Cursors.Default;
            FrmReportViewer.AllowSetFormula = true;

            FrmReportViewer.ShowForm("Purchase_Memo", 120, FrmReportViewer.ReportFolder.ACCOUNT);
            Invoice_EntryPropertyNew = null;
            dtpur = null;
            FrmReportViewer.DS.Tables.Clear();
            FrmReportViewer.DS.Clear();
            FrmReportViewer = null;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Global.Confirm("Are You Sure To Delete ?", "STORE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }
            int IntRes = 0;
            Invoice_EntryProperty Invoice_EntryPropertyNew = new Invoice_EntryProperty();
            Invoice_EntryPropertyNew.Purchase_Master_ID = Val.ToInt64(txtPurchaseMasterID.Text);
            IntRes = ObjInvoiceEntry.DeleteInvoiceEntryMaster(Invoice_EntryPropertyNew, Form_Type);

            if (IntRes != 0)
            {
                Global.Confirm("Data Deleted Successfully");
                //GetData();
                btnClear_Click(null, null);
            }
            else
            {
                Global.Confirm("Error in Data Delete");
                txtInvoiceNo.Focus();
            }
        }

        private void GrdPurchase_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                if (Global.Confirm("Are you sure delete selected row?", "STORE", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    Invoice_EntryProperty Invoice_EntryProperty = new Invoice_EntryProperty();
                    int IntRes = 0;

                    Int64 ItemPurchaseMasterID = Val.ToInt64(dgvPurchase.GetFocusedRowCellValue("ItemSaleReturnMasterID").ToString());
                    Invoice_EntryProperty.ItemPurchaseMasterID = Val.ToInt64(ItemPurchaseMasterID);
                    Invoice_EntryProperty.ItemPurchaseDetailID = Val.ToInt64(dgvPurchase.GetFocusedRowCellValue("ItemSaleReturnDtlID").ToString());

                    if (ItemPurchaseMasterID == 0)
                    {
                        dgvPurchase.DeleteRow(dgvPurchase.GetRowHandle(dgvPurchase.FocusedRowHandle));
                    }
                    else
                    {
                        IntRes = ObjInvoiceEntry.DeletePurchaseDetail(Invoice_EntryProperty, Form_Type);
                        dgvPurchase.DeleteRow(dgvPurchase.GetRowHandle(dgvPurchase.FocusedRowHandle));
                    }

                    if (IntRes == -1)
                    {
                        Global.Confirm("Error in Detail Deleted Data.");
                        Invoice_EntryProperty = null;
                    }
                    else
                    {
                        Global.Confirm("Detail Deleted successfully...");
                        Invoice_EntryProperty = null;
                    }
                }
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            GetData();
        }
    }
}