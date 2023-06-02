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
    public partial class FrmSaleEntry : Form
    {
        BLL.FormEvents objBOFormEvents = new BLL.FormEvents();
        BLL.Validation Val = new BLL.Validation();
        ItemPurchase ObjItemPurchase = new ItemPurchase();
        ItemPurchaseMaster ObjPurchase = new ItemPurchaseMaster();
        Invoice_Entry ObjInvoiceEntry = new Invoice_Entry();
        string Form_Type = "";

        public FrmSaleEntry(string Type_)
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

            if (string.IsNullOrEmpty(LookupParty.Text.Trim()))
            {
                Global.Confirm("Party Name Is Required");
                LookupParty.Focus();
                return;
            }

            //Invoice_EntryProperty Invoice_EntryProperty = new Invoice_EntryProperty();
            //Invoice_EntryProperty.Invoice_No = Val.ToString(txtInvoiceNo.Text);
            //DataTable DTab = ObjInvoiceEntry.GetData_New(Invoice_EntryProperty);

            //if (DTab.Rows.Count > 0)
            //{
            //    Global.Message("Invoice Already Exists. Please Check...");
            //    return;
            //}
            //else
            //{
            PanelShow.Enabled = false;
            PanelSave.Enabled = true;
            GrdSalesDetEntry.Enabled = true;
            GetPurchaseDetail();
            DgvSalesDetEntry.BestFitColumns();
            DgvSalesDetEntry.Focus();
            //}
        }

        public Invoice_EntryProperty SaveItemPurchaseMaster()
        {
            Invoice_EntryProperty Invoice_EntryProperty = new Invoice_EntryProperty();

            Invoice_EntryProperty.Purchase_Master_ID = Val.ToInt64(lblMode.Tag);
            Invoice_EntryProperty.Financial_Year = GlobalDec.gEmployeeProperty.gFinancialYear;

            //string Invoice_No = Val.ToString(ObjInvoiceEntry.GEtMaximumID("Sales_Entry"));
            //txtInvoiceNo.Text = Invoice_No.ToString();

            Invoice_EntryProperty.Invoice_No = Val.ToString(txtInvoiceNo.Text);
            Invoice_EntryProperty.Invoice_Date = Val.DBDate(DTPInvoiceDate.Text);
            Invoice_EntryProperty.From_Party_Code = Val.ToInt64(LookupParty.EditValue);
            Invoice_EntryProperty.Remark = txtRemark.Text;

            Invoice_EntryProperty.Payment_Days = Val.ToString(txtPaymentDays.Text);
            Invoice_EntryProperty.Payment_Date = Val.DBDate(DTPPaymentDate.Text);

            //Invoice_EntryProperty.CGST = Val.Val(txtCGST.Text);
            //Invoice_EntryProperty.IGST = Val.Val(txtIGST.Text);
            //Invoice_EntryProperty.SGST = Val.Val(txtSGST.Text);
            Invoice_EntryProperty.SGST = Val.Val(txtSGSTPer.Text);
            Invoice_EntryProperty.SGST_Amt = Val.Val(txtSGSTAmt.Text);
            Invoice_EntryProperty.CGST = Val.Val(txtCGSTPer.Text);
            Invoice_EntryProperty.CGST_Amt = Val.Val(txtCGSTAmt.Text);
            Invoice_EntryProperty.IGST = Val.Val(txtIGSTPer.Text);
            Invoice_EntryProperty.IGST_Amt = Val.Val(txtIGSTAmt.Text);

            Invoice_EntryProperty.Discount_Per = Val.Val(txtDiscountPer.Text);
            Invoice_EntryProperty.Discount = Val.Val(txtDiscountAmt.Text);

            Invoice_EntryProperty.Gross_Amt = Val.Val(txtGrossAmtLocal.Text);
            Invoice_EntryProperty.Add_Amt = Val.Val(txtTotalAddAmount.Text);
            Invoice_EntryProperty.Less_Amt = Val.Val(txtTotalLessAmount.Text);
            Invoice_EntryProperty.Net_Amt = Val.Val(txtNetAmtLocal.Text);
            Invoice_EntryProperty.Is_Paid = Val.ToBoolean(CHKIsPaid.Checked);

            if (chkInvoiceNo.Checked == true)
            {
                Invoice_EntryProperty.Invoice_No_Checked = Val.ToBooleanToInt(chkInvoiceNo.Checked);
            }
            else
            {
                Invoice_EntryProperty.Invoice_No_Checked = Val.ToBooleanToInt(chkInvoiceNo.Checked);
            }

            return Invoice_EntryProperty;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValSave() == false)
            {
                return;
            }

            if (Global.Confirm("Are You Sure To Save ?", "SALES", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
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

            DataTable DTab = (System.Data.DataTable)GrdSalesDetEntry.DataSource;
            DTab.AcceptChanges();

            foreach (DataRow DRow in DTab.Rows)
            {
                if (Val.Val(DRow["ITEM_CODE"]) == 0)
                {
                    continue;
                }

                Invoice_EntryProperty Invoice_EntryProperty = new Invoice_EntryProperty();


                //Invoice_EntryProperty.SGST = Val.Val(DRow["SGST_Rate"]);
                //Invoice_EntryProperty.SGST_Amt = Val.Val(DRow["SGST_Amt"]);
                //Invoice_EntryProperty.CGST = Val.Val(DRow["CGST_Rate"]);
                //Invoice_EntryProperty.CGST_Amt = Val.Val(DRow["CGST_Amt"]);
                //Invoice_EntryProperty.IGST = Val.Val(DRow["IGST_Rate"]);
                //Invoice_EntryProperty.IGST_Amt = Val.Val(DRow["IGST_Amt"]);
                Invoice_EntryProperty.SGST = Val.Val(0);
                Invoice_EntryProperty.SGST_Amt = Val.Val(0);
                Invoice_EntryProperty.CGST = Val.Val(0);
                Invoice_EntryProperty.CGST_Amt = Val.Val(0);
                Invoice_EntryProperty.IGST = Val.Val(0);
                Invoice_EntryProperty.IGST_Amt = Val.Val(0);
                Int64 ItemSaleMasterID = Val.ToInt64(DRow["ItemSaleMasterID"]);
                if (ItemSaleMasterID == 0)
                {
                    Invoice_EntryProperty.ItemSaleMasterID = Val.ToInt64(Newmstid);
                    Invoice_EntryProperty.ItemSaleDetailID = Val.ToInt64(DRow["ItemSaleDetailID"]);
                }
                else
                {
                    Invoice_EntryProperty.ItemSaleMasterID = Val.ToInt64(DRow["ItemSaleMasterID"]);
                    Invoice_EntryProperty.ItemSaleDetailID = Val.ToInt64(DRow["ItemSaleDetailID"]);
                }
                Invoice_EntryProperty.HSN_ID = Val.ToInt64(DRow["HSN_ID"]);
                Invoice_EntryProperty.MTR = Val.ToDecimal(DRow["MTR"]);
                Invoice_EntryProperty.Line = Val.ToInt32(DRow["Line"]);
                Invoice_EntryProperty.Taka = Val.ToInt32(DRow["Taka"]);
                Invoice_EntryProperty.Cut = Val.ToDecimal(DRow["Cut"]);
                Invoice_EntryProperty.Item_Code = Val.ToInt64(DRow["Item_Code"]);
                Invoice_EntryProperty.Unit_Type = Val.ToString(DRow["Unit_Name"]);
                Invoice_EntryProperty.Quantity = Val.Val(DRow["Quantity"]);
                Invoice_EntryProperty.Rate_Dollar = Val.Val(DRow["Rate"]);
                Invoice_EntryProperty.Gross_Amt = Val.Val(DRow["Gross_Amt"]);
                //Invoice_EntryProperty.Discount_Per = Val.Val(DRow["Discount_Per"]);
                //Invoice_EntryProperty.Discount = Val.Val(DRow["Discount"]);
                Invoice_EntryProperty.Discount_Per = Val.Val(0);
                Invoice_EntryProperty.Discount = Val.Val(0);
                //Invoice_EntryProperty.Net_Amt = Val.Val(DRow["NetAmount"]);
                Invoice_EntryProperty.Net_Amt = Val.Val(txtNetAmtLocal.Text);
                Invoice_EntryProperty.Buta = Val.ToInt(DRow["Buta"]);
                Invoice_EntryProperty.Buta_Rate = Val.ToInt(DRow["Buta_Rate"]);

                AL.Add(Invoice_EntryProperty);
            }
            IntRes = ObjInvoiceEntry.SavePurchaseDetail(AL, Form_Type);

            if (IntRes != 0)
            {
                Global.Confirm("Save Data Successfully");

                if (Global.Confirm("Are You Sure To Print Sale Invoice ?", "SALES", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                {
                    GetData();
                    btnClear_Click(null, null);
                    return;
                }
                Invoice_EntryProperty Invoice_EntryProperty1 = new Invoice_EntryProperty();
                Invoice_EntryProperty1.Invoice_Date = Val.DBDate(DTPInvoiceDate.Text);
                Invoice_EntryProperty1.Invoice_No = Val.ToString(txtInvoiceNo.Text);
                Invoice_EntryProperty1.Trn_Id = Val.ToInt64(Newmstid);
                Invoice_EntryProperty1.Type = Val.ToString(Form_Type);

                DataTable dtpur = new DataTable();

                dtpur = ObjInvoiceEntry.GetPrintData(Invoice_EntryProperty1);

                string Remark = Val.ToString(dtpur.Rows[0]["REMARK"]);

                for (int j = 0; j < dtpur.Rows.Count; j++)
                {
                    dtpur.Rows[j]["REMARK"] = "";
                }


                for (int i = dtpur.Rows.Count; i < 14; i++)
                {
                    dtpur.Rows.Add();
                    if (i == 13)
                    {
                        dtpur.Rows[i]["COMPANY_NAME"] = dtpur.Rows[0]["COMPANY_NAME"];
                        dtpur.Rows[i]["BRANCH_NAME"] = dtpur.Rows[0]["BRANCH_NAME"];
                        dtpur.Rows[i]["LOCATION_NAME"] = dtpur.Rows[0]["LOCATION_NAME"];
                        dtpur.Rows[i]["FROM_PARTY"] = "ABCDEFGHRTYYGGG";
                        dtpur.Rows[i]["FROM_GSTIN"] = dtpur.Rows[0]["FROM_GSTIN"];
                        dtpur.Rows[i]["FROM_ADDRESS"] = dtpur.Rows[0]["FROM_ADDRESS"];
                        dtpur.Rows[i]["Party_Mobile"] = dtpur.Rows[0]["Party_Mobile"];
                        dtpur.Rows[i]["Party_PanNo"] = dtpur.Rows[0]["Party_PanNo"];
                        dtpur.Rows[i]["TO_PARTY"] = dtpur.Rows[0]["TO_PARTY"];
                        dtpur.Rows[i]["TO_GSTIN"] = dtpur.Rows[0]["TO_GSTIN"];
                        dtpur.Rows[i]["FROM_STATE"] = dtpur.Rows[0]["FROM_STATE"];
                        dtpur.Rows[i]["TO_STATE"] = dtpur.Rows[0]["TO_STATE"];
                        dtpur.Rows[i]["TO_ADDRESS"] = dtpur.Rows[0]["TO_ADDRESS"];
                        dtpur.Rows[i]["INVOICE_DATE"] = dtpur.Rows[0]["INVOICE_DATE"];
                        dtpur.Rows[i]["INVOICE_TYPE"] = dtpur.Rows[0]["INVOICE_TYPE"];
                        dtpur.Rows[i]["PARTY_INVOICE_NO"] = dtpur.Rows[0]["PARTY_INVOICE_NO"];
                        dtpur.Rows[i]["IS_REVERSE"] = dtpur.Rows[0]["IS_REVERSE"];
                        dtpur.Rows[i]["NET_AMT"] = dtpur.Rows[0]["NET_AMT"];
                        dtpur.Rows[i]["Discount_Per"] = dtpur.Rows[0]["Discount_Per"];
                        dtpur.Rows[i]["Discount_Amt"] = dtpur.Rows[0]["Discount_Amt"];
                        dtpur.Rows[i]["SGST_Per"] = dtpur.Rows[0]["SGST_Per"];
                        dtpur.Rows[i]["SGST_Amt"] = dtpur.Rows[0]["SGST_Amt"];
                        dtpur.Rows[i]["CGST_Per"] = dtpur.Rows[0]["CGST_Per"];
                        dtpur.Rows[i]["CGST_Amt"] = dtpur.Rows[0]["CGST_Amt"];
                        dtpur.Rows[i]["IGST_Per"] = dtpur.Rows[0]["IGST_Per"];
                        dtpur.Rows[i]["IGST_Amt"] = dtpur.Rows[0]["IGST_Amt"];
                        dtpur.Rows[i]["ShipTo_Ledger_Code"] = dtpur.Rows[0]["ShipTo_Ledger_Code"];
                        dtpur.Rows[i]["ShipTo_Ledger_Name"] = dtpur.Rows[0]["ShipTo_Ledger_Name"];
                        dtpur.Rows[i]["SHIP_GSTIN"] = dtpur.Rows[0]["SHIP_GSTIN"];
                        dtpur.Rows[i]["SHIP_ADDRESS"] = dtpur.Rows[0]["SHIP_ADDRESS"];
                        dtpur.Rows[i]["SHIP_Mobile"] = dtpur.Rows[0]["SHIP_Mobile"];
                        dtpur.Rows[i]["SHIP_PanNo"] = dtpur.Rows[0]["SHIP_PanNo"];
                        dtpur.Rows[i]["SHIP_STATE"] = dtpur.Rows[0]["SHIP_STATE"];
                        dtpur.Rows[i]["REMARK"] = Remark;
                    }
                    else
                    {
                        dtpur.Rows[i]["COMPANY_NAME"] = dtpur.Rows[0]["COMPANY_NAME"];
                        dtpur.Rows[i]["BRANCH_NAME"] = dtpur.Rows[0]["BRANCH_NAME"];
                        dtpur.Rows[i]["LOCATION_NAME"] = dtpur.Rows[0]["LOCATION_NAME"];
                        dtpur.Rows[i]["FROM_PARTY"] = dtpur.Rows[0]["FROM_PARTY"];
                        dtpur.Rows[i]["FROM_GSTIN"] = dtpur.Rows[0]["FROM_GSTIN"];
                        dtpur.Rows[i]["FROM_ADDRESS"] = dtpur.Rows[0]["FROM_ADDRESS"];
                        dtpur.Rows[i]["Party_Mobile"] = dtpur.Rows[0]["Party_Mobile"];
                        dtpur.Rows[i]["Party_PanNo"] = dtpur.Rows[0]["Party_PanNo"];
                        dtpur.Rows[i]["TO_PARTY"] = dtpur.Rows[0]["TO_PARTY"];
                        dtpur.Rows[i]["TO_GSTIN"] = dtpur.Rows[0]["TO_GSTIN"];
                        dtpur.Rows[i]["FROM_STATE"] = dtpur.Rows[0]["FROM_STATE"];
                        dtpur.Rows[i]["TO_STATE"] = dtpur.Rows[0]["TO_STATE"];
                        dtpur.Rows[i]["TO_ADDRESS"] = dtpur.Rows[0]["TO_ADDRESS"];
                        dtpur.Rows[i]["INVOICE_DATE"] = dtpur.Rows[0]["INVOICE_DATE"];
                        dtpur.Rows[i]["INVOICE_TYPE"] = dtpur.Rows[0]["INVOICE_TYPE"];
                        dtpur.Rows[i]["PARTY_INVOICE_NO"] = dtpur.Rows[0]["PARTY_INVOICE_NO"];
                        dtpur.Rows[i]["IS_REVERSE"] = dtpur.Rows[0]["IS_REVERSE"];
                        dtpur.Rows[i]["NET_AMT"] = dtpur.Rows[0]["NET_AMT"];
                        dtpur.Rows[i]["Discount_Per"] = dtpur.Rows[0]["Discount_Per"];
                        dtpur.Rows[i]["Discount_Amt"] = dtpur.Rows[0]["Discount_Amt"];
                        dtpur.Rows[i]["SGST_Per"] = dtpur.Rows[0]["SGST_Per"];
                        dtpur.Rows[i]["SGST_Amt"] = dtpur.Rows[0]["SGST_Amt"];
                        dtpur.Rows[i]["CGST_Per"] = dtpur.Rows[0]["CGST_Per"];
                        dtpur.Rows[i]["CGST_Amt"] = dtpur.Rows[0]["CGST_Amt"];
                        dtpur.Rows[i]["IGST_Per"] = dtpur.Rows[0]["IGST_Per"];
                        dtpur.Rows[i]["IGST_Amt"] = dtpur.Rows[0]["IGST_Amt"];
                        dtpur.Rows[i]["ShipTo_Ledger_Code"] = dtpur.Rows[0]["ShipTo_Ledger_Code"];
                        dtpur.Rows[i]["ShipTo_Ledger_Name"] = dtpur.Rows[0]["ShipTo_Ledger_Name"];
                        dtpur.Rows[i]["SHIP_GSTIN"] = dtpur.Rows[0]["SHIP_GSTIN"];
                        dtpur.Rows[i]["SHIP_ADDRESS"] = dtpur.Rows[0]["SHIP_ADDRESS"];
                        dtpur.Rows[i]["SHIP_Mobile"] = dtpur.Rows[0]["SHIP_Mobile"];
                        dtpur.Rows[i]["SHIP_PanNo"] = dtpur.Rows[0]["SHIP_PanNo"];
                        dtpur.Rows[i]["SHIP_STATE"] = dtpur.Rows[0]["SHIP_STATE"];
                        dtpur.Rows[i]["COMPANY_NAME"] = dtpur.Rows[0]["COMPANY_NAME"];
                        dtpur.Rows[i]["BRANCH_NAME"] = dtpur.Rows[0]["BRANCH_NAME"];
                        dtpur.Rows[i]["LOCATION_NAME"] = dtpur.Rows[0]["LOCATION_NAME"];
                        dtpur.Rows[i]["FROM_PARTY"] = dtpur.Rows[0]["FROM_PARTY"];
                        dtpur.Rows[i]["FROM_GSTIN"] = dtpur.Rows[0]["FROM_GSTIN"];
                        dtpur.Rows[i]["FROM_ADDRESS"] = dtpur.Rows[0]["FROM_ADDRESS"];
                        dtpur.Rows[i]["Party_Mobile"] = dtpur.Rows[0]["Party_Mobile"];
                        dtpur.Rows[i]["Party_PanNo"] = dtpur.Rows[0]["Party_PanNo"];
                        dtpur.Rows[i]["TO_PARTY"] = dtpur.Rows[0]["TO_PARTY"];
                        dtpur.Rows[i]["TO_GSTIN"] = dtpur.Rows[0]["TO_GSTIN"];
                        dtpur.Rows[i]["FROM_STATE"] = dtpur.Rows[0]["FROM_STATE"];
                        dtpur.Rows[i]["TO_STATE"] = dtpur.Rows[0]["TO_STATE"];
                        dtpur.Rows[i]["TO_ADDRESS"] = dtpur.Rows[0]["TO_ADDRESS"];
                        dtpur.Rows[i]["INVOICE_DATE"] = dtpur.Rows[0]["INVOICE_DATE"];
                        dtpur.Rows[i]["INVOICE_TYPE"] = dtpur.Rows[0]["INVOICE_TYPE"];
                        dtpur.Rows[i]["PARTY_INVOICE_NO"] = dtpur.Rows[0]["PARTY_INVOICE_NO"];
                        dtpur.Rows[i]["IS_REVERSE"] = dtpur.Rows[0]["IS_REVERSE"];
                        dtpur.Rows[i]["NET_AMT"] = dtpur.Rows[0]["NET_AMT"];
                        dtpur.Rows[i]["Discount_Per"] = dtpur.Rows[0]["Discount_Per"];
                        dtpur.Rows[i]["Discount_Amt"] = dtpur.Rows[0]["Discount_Amt"];
                        dtpur.Rows[i]["SGST_Per"] = dtpur.Rows[0]["SGST_Per"];
                        dtpur.Rows[i]["SGST_Amt"] = dtpur.Rows[0]["SGST_Amt"];
                        dtpur.Rows[i]["CGST_Per"] = dtpur.Rows[0]["CGST_Per"];
                        dtpur.Rows[i]["CGST_Amt"] = dtpur.Rows[0]["CGST_Amt"];
                        dtpur.Rows[i]["IGST_Per"] = dtpur.Rows[0]["IGST_Per"];
                        dtpur.Rows[i]["IGST_Amt"] = dtpur.Rows[0]["IGST_Amt"];
                        dtpur.Rows[i]["ShipTo_Ledger_Code"] = dtpur.Rows[0]["ShipTo_Ledger_Code"];
                        dtpur.Rows[i]["ShipTo_Ledger_Name"] = dtpur.Rows[0]["ShipTo_Ledger_Name"];
                        dtpur.Rows[i]["SHIP_GSTIN"] = dtpur.Rows[0]["SHIP_GSTIN"];
                        dtpur.Rows[i]["SHIP_ADDRESS"] = dtpur.Rows[0]["SHIP_ADDRESS"];
                        dtpur.Rows[i]["SHIP_Mobile"] = dtpur.Rows[0]["SHIP_Mobile"];
                        dtpur.Rows[i]["SHIP_PanNo"] = dtpur.Rows[0]["SHIP_PanNo"];
                        dtpur.Rows[i]["SHIP_STATE"] = dtpur.Rows[0]["SHIP_STATE"];
                        dtpur.Rows[i]["REMARK"] = "";
                    }
                }

                FrmReportViewer FrmReportViewer = new FrmReportViewer();
                FrmReportViewer.DS.Tables.Add(dtpur);
                FrmReportViewer.GroupBy = "";
                FrmReportViewer.RepName = "";
                FrmReportViewer.RepPara = "";
                this.Cursor = Cursors.Default;
                FrmReportViewer.AllowSetFormula = true;

                GetData();
                btnClear_Click(null, null);

                FrmReportViewer.ShowForm("Sale_Memo", 120, Report.FrmReportViewer.ReportFolder.ACCOUNT);
                Invoice_EntryPropertyNew = null;
                dtpur = null;
                FrmReportViewer.DS.Tables.Clear();
                FrmReportViewer.DS.Clear();
                FrmReportViewer = null;
            }
            else
            {
                Global.Confirm("Error in Data Save");
                txtInvoiceNo.Focus();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lblMode.Tag = 0;
            lblMode.Text = "Add Mode";

            DTPInvoiceDate.Text = "";
            LookupParty.EditValue = null;
            txtRemark.Text = "";
            txtPaymentDays.Text = "";

            string Invoice_No = Val.ToString(ObjInvoiceEntry.GEtMaximumID("Sales_Entry"));
            txtInvoiceNo.Text = Invoice_No.ToString();

            txtDiscountPer.Text = "0";
            txtDiscountAmt.Text = "0";
            txtSGSTPer.Text = "0";
            txtSGSTAmt.Text = "0";
            txtCGSTPer.Text = "0";
            txtCGSTAmt.Text = "0";
            txtIGSTPer.Text = "0";
            txtIGSTAmt.Text = "0";
            txtTotalAddAmount.Text = "0";
            txtTotalLessAmount.Text = "0";
            txtGrossAmtLocal.Text = "0";
            txtNetAmtLocal.Text = "0";

            DTPInvoiceDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPInvoiceDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPInvoiceDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPInvoiceDate.Properties.CharacterCasing = CharacterCasing.Upper;

            DTPInvoiceDate.EditValue = DateTime.Now;

            DTPPaymentDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPPaymentDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPPaymentDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPPaymentDate.Properties.CharacterCasing = CharacterCasing.Upper;

            DTPPaymentDate.EditValue = DateTime.Now;

            GrdSalesDetEntry.DataSource = null;

            CalculateGridAmount(DgvSalesDetEntry.FocusedRowHandle);

            PanelShow.Enabled = true;
            GrdSalesDetEntry.Enabled = false;
            PanelSave.Enabled = false;
            chkInvoiceNo.Checked = false;

            txtPassword.Text = "";
            CHKIsPaid.Checked = false;
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

            if (Val.ToString(LookupParty.Text.Trim()) == "")
            {
                Global.Confirm("From Party Is Required");
                LookupParty.Focus();
                return false;
            }

            //if (!ObjInvoiceEntry.ISExists(txtInvoiceNo.Text, Val.ToInt64(lblMode.Tag)).ToString().Trim().Equals(string.Empty))
            //{
            //    Global.Confirm("Invoice No Already Exist.");
            //    DTPInvoiceDate.Focus();
            //    return false;
            //}

            DataTable DTab = (DataTable)GrdSalesDetEntry.DataSource;
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
            GrdSalesDetEntry.DataSource = null;
            DataTable DTab = ObjInvoiceEntry.GetPurchaseDetail(Form_Type, Val.ToInt64(lblMode.Tag));
            GrdSalesDetEntry.DataSource = DTab;
            this.Cursor = Cursors.Default;
        }

        #endregion



        private void FrmItemPurchaseMaster_Shown(object sender, EventArgs e)
        {
            Global.LOOKUPItemRep(RepLookUpItem);
            Global.LOOKUPItemHSNRep(RepHSNCode);
            btnClear_Click(btnClear, null);
            Ledger_MasterProperty Party = new Ledger_MasterProperty();
            Party.Party_Type = "";
            Global.LOOKUPParty(LookupParty, Party);
            Party = null;
            this.Text = "Sales Invoice";

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
            //btnClear_Click(btnClear, null);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            PanelShow.Enabled = true;
            GrdSalesDetEntry.Enabled = false;
            PanelSave.Enabled = false;
            GrdSalesDetEntry.DataSource = null;
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
                    DataRow DRow = DgvSalesEntry.GetDataRow(e.RowHandle);
                    lblMode.Text = "Edit Mode";
                    lblMode.Tag = Val.ToString(DRow["ItemSaleMasterID"]);
                    txtInvoiceNo.Text = Val.ToString(DRow["Inovice_No"]);
                    DTPInvoiceDate.EditValue = Val.DBDate(DRow["Invoice_Date"].ToString());
                    LookupParty.EditValue = Val.ToInt64(DRow["Ledger_Code"]);
                    txtPaymentDays.Text = Val.ToString(DRow["Terms"]);
                    DTPPaymentDate.EditValue = Val.DBDate(DRow["Term_Date"].ToString());
                    txtRemark.Text = Val.ToString(DRow["Remark"]);
                    txtCGSTPer.Text = Val.ToString(DRow["CGST_Per"]);
                    txtCGSTAmt.Text = Val.ToString(DRow["CGST_Amt"]);
                    txtIGSTPer.Text = Val.ToString(DRow["IGST_Per"]);
                    txtIGSTAmt.Text = Val.ToString(DRow["IGST_Amt"]);
                    txtSGSTPer.Text = Val.ToString(DRow["SGST_Per"]);
                    txtSGSTAmt.Text = Val.ToString(DRow["SGST_Amt"]);
                    txtDiscountPer.Text = Val.ToString(DRow["Discount_Per"]);
                    txtDiscountAmt.Text = Val.ToString(DRow["Discount_Amt"]);
                    txtGrossAmtLocal.Text = Val.ToString(DRow["Gross_Amt"]);
                    txtNetAmtLocal.Text = Val.ToString(DRow["Net_Amt"]);
                    txtTotalAddAmount.Text = Val.ToString(DRow["Add_Amt"]);
                    txtTotalLessAmount.Text = Val.ToString(DRow["Less_Amt"]);
                    CHKIsPaid.Checked = Val.ToBoolean(DRow["Is_Paid"]);

                    GetPurchaseDetail();
                    PanelShow.Enabled = true;
                    GrdSalesDetEntry.Enabled = true;
                    PanelSave.Enabled = true;
                    CalculateGridAmount(DgvSalesDetEntry.FocusedRowHandle);
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
                //GrdSalesDetEntry.RefreshDataSource();
            }
        }

        public DataTable GetData()
        {
            Invoice_EntryProperty Invoice_EntryProperty = new Invoice_EntryProperty();
            Invoice_EntryProperty.From_Date = Val.DBDate(dtpSearchFromDate.Text);
            Invoice_EntryProperty.To_Date = Val.DBDate(dtpSearchToDate.Text);
            DataTable DTab = ObjInvoiceEntry.GetData(Invoice_EntryProperty, Form_Type);
            GrdSalesEntry.DataSource = DTab;
            GrdSalesEntry.RefreshDataSource();
            DgvSalesEntry.BestFitColumns();
            Invoice_EntryProperty = null;
            GetPurchaseDetail();
            return DTab;
        }

        private void RepLookUpItem_Validating(object sender, CancelEventArgs e)
        {
            LookUpEdit type = sender as LookUpEdit;

            DgvSalesDetEntry.SetRowCellValue(DgvSalesDetEntry.FocusedRowHandle, "Unit_Name", type.GetColumnValue("UNIT_NAME"));
            DgvSalesDetEntry.SetRowCellValue(DgvSalesDetEntry.FocusedRowHandle, "HSN_ID", type.GetColumnValue("HSN_ID"));
            //DgvSalesDetEntry.SetRowCellValue(DgvSalesDetEntry.FocusedRowHandle, "Rate", type.GetColumnValue("LAST_PURCHASE_RATE"));
            //DgvSalesDetEntry.SetRowCellValue(DgvSalesDetEntry.FocusedRowHandle, "SGST_Rate", type.GetColumnValue("SGST_RATE"));
            //DgvSalesDetEntry.SetRowCellValue(DgvSalesDetEntry.FocusedRowHandle, "CGST_Rate", type.GetColumnValue("CGST_RATE"));
            //DgvSalesDetEntry.SetRowCellValue(DgvSalesDetEntry.FocusedRowHandle, "IGST_Rate", type.GetColumnValue("IGST_RATE"));
        }

        //private void chkGST()
        //{
        //    if (txtPartyState.Text == GlobalDec.gEmployeeProperty.State_Code.ToString())
        //    {
        //        txtSGST.Visible = true;
        //        lblSGST.Visible = true;
        //        txtCGST.Visible = true;
        //        lblCGST.Visible = true;
        //        txtIGST.Visible = false;
        //        lblIGST.Visible = false;

        //        DgvSalesDetEntry.Columns["SGST_Rate"].Visible = true;
        //        DgvSalesDetEntry.Columns["SGST_Amt"].Visible = true;
        //        DgvSalesDetEntry.Columns["CGST_Rate"].Visible = true;
        //        DgvSalesDetEntry.Columns["CGST_Amt"].Visible = true;
        //        DgvSalesDetEntry.Columns["IGST_Rate"].Visible = false;
        //        DgvSalesDetEntry.Columns["IGST_Amt"].Visible = false;
        //    }
        //    else
        //    {
        //        txtSGST.Visible = false;
        //        lblSGST.Visible = false;
        //        txtCGST.Visible = false;
        //        lblCGST.Visible = false;
        //        txtIGST.Visible = true;
        //        lblIGST.Visible = true;
        //        DgvSalesDetEntry.Columns["SGST_Rate"].Visible = false;
        //        DgvSalesDetEntry.Columns["SGST_Amt"].Visible = false;
        //        DgvSalesDetEntry.Columns["CGST_Rate"].Visible = false;
        //        DgvSalesDetEntry.Columns["CGST_Amt"].Visible = false;
        //        DgvSalesDetEntry.Columns["IGST_Rate"].Visible = true;
        //        DgvSalesDetEntry.Columns["IGST_Amt"].Visible = true;
        //    }
        //}

        private void txtPaymentDays_EditValueChanged(object sender, EventArgs e)
        {
            //DgvSalesDetEntry.PostEditor();
            //if (DTPInvoiceDate.Text.Length <= 0 || txtPaymentDays.Text == "")
            //{
            //    txtPaymentDays.Text = "";
            //    DTPPaymentDate.EditValue = null;
            //}
            //else
            //{
            //    DateTime Date = Convert.ToDateTime(DTPInvoiceDate.EditValue).AddDays(Val.ToDouble(txtPaymentDays.Text));
            //    DTPPaymentDate.EditValue = Val.DBDate(Date.ToShortDateString());
            //}
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
                double MTR = 0;
                double Line = 0;
                double Taka = 0;
                double Cut = 0;

                if (Val.ToDouble(DgvSalesDetEntry.GetRowCellValue(rowindex, "MTR")) == 0)
                {
                    MTR = Val.ToDouble(1);
                }
                else
                {
                    MTR = Math.Round(Val.ToDouble(DgvSalesDetEntry.GetRowCellValue(rowindex, "MTR")), 2);
                }
                if (Val.ToDouble(DgvSalesDetEntry.GetRowCellValue(rowindex, "Line")) == 0)
                {
                    Line = Val.ToDouble(1);
                }
                else
                {
                    Line = Math.Round(Val.ToDouble(DgvSalesDetEntry.GetRowCellValue(rowindex, "Line")), 2);
                }
                if (Val.ToDouble(DgvSalesDetEntry.GetRowCellValue(rowindex, "Taka")) == 0)
                {
                    Taka = Val.ToDouble(1);
                }
                else
                {
                    Taka = Math.Round(Val.ToDouble(DgvSalesDetEntry.GetRowCellValue(rowindex, "Taka")), 2);
                }
                if (Val.ToDouble(DgvSalesDetEntry.GetRowCellValue(rowindex, "Cut")) == 0)
                {
                    Cut = Val.ToDouble(1);
                }
                else
                {
                    Cut = Math.Round(Val.ToDouble(DgvSalesDetEntry.GetRowCellValue(rowindex, "Cut")), 2);
                }

                //if (Val.ToDouble(DgvSalesDetEntry.GetRowCellValue(rowindex, "Cut")) != 0)
                //{
                DgvSalesDetEntry.SetRowCellValue(rowindex, "Quantity", Math.Round(MTR * Line * Taka / Cut, 2));
                //}
                //DgvSalesDetEntry.SetRowCellValue(rowindex, "Gross_Amt", Math.Round(Val.ToDouble(DgvSalesDetEntry.GetRowCellValue(rowindex, "Rate")) * Val.ToDouble(DgvSalesDetEntry.GetRowCellValue(rowindex, "Quantity")), 2));

                //DgvSalesDetEntry.SetRowCellValue(rowindex, "Gross_Amt", Math.Round(Val.ToDouble(DgvSalesDetEntry.GetRowCellValue(rowindex, "Rate")) * Val.ToDouble(DgvSalesDetEntry.GetRowCellValue(rowindex, "Quantity")), 2));
                DgvSalesDetEntry.SetRowCellValue(rowindex, "Gross_Amt", Math.Round((Val.ToDouble(DgvSalesDetEntry.GetRowCellValue(rowindex, "Rate")) * Val.ToDouble(DgvSalesDetEntry.GetRowCellValue(rowindex, "Quantity"))) + (Val.ToDouble(DgvSalesDetEntry.GetRowCellValue(rowindex, "Buta")) * Val.ToDouble(DgvSalesDetEntry.GetRowCellValue(rowindex, "Quantity")) * Val.ToDouble(DgvSalesDetEntry.GetRowCellValue(rowindex, "Buta_Rate"))), 2));

                //DgvSalesDetEntry.SetRowCellValue(rowindex, "Discount", Math.Round(Val.ToDouble(DgvSalesDetEntry.GetRowCellValue(rowindex, "Gross_Amt")) * Val.ToDouble(DgvSalesDetEntry.GetRowCellValue(rowindex, "Discount_Per")) / 100, 0));
                double GrossAmt = Val.ToDouble(DgvSalesDetEntry.Columns["Gross_Amt"].SummaryText);

                //DgvSalesDetEntry.SetRowCellValue(rowindex, "SGST_Amt", (Math.Round(Val.ToDouble(DgvSalesDetEntry.GetRowCellValue(rowindex, "SGST_Rate")) * (Val.ToDouble(DgvSalesDetEntry.GetRowCellValue(rowindex, "Gross_Amt")) - Val.ToDouble(DgvSalesDetEntry.GetRowCellValue(rowindex, "Discount"))), 2)) / 100);
                //DgvSalesDetEntry.SetRowCellValue(rowindex, "IGST_Amt", (Math.Round(Val.ToDouble(DgvSalesDetEntry.GetRowCellValue(rowindex, "IGST_Rate")) * (Val.ToDouble(DgvSalesDetEntry.GetRowCellValue(rowindex, "Gross_Amt")) - Val.ToDouble(DgvSalesDetEntry.GetRowCellValue(rowindex, "Discount"))), 2)) / 100);
                //DgvSalesDetEntry.SetRowCellValue(rowindex, "CGST_Amt", (Math.Round(Val.ToDouble(DgvSalesDetEntry.GetRowCellValue(rowindex, "CGST_Rate")) * (Val.ToDouble(DgvSalesDetEntry.GetRowCellValue(rowindex, "Gross_Amt")) - Val.ToDouble(DgvSalesDetEntry.GetRowCellValue(rowindex, "Discount"))), 2)) / 100);
                DgvSalesDetEntry.SetRowCellValue(rowindex, "NetAmount", Math.Round(Val.ToDouble(DgvSalesDetEntry.GetRowCellValue(rowindex, "Gross_Amt")) - Val.ToDouble(DgvSalesDetEntry.GetRowCellValue(rowindex, "Discount")), 2));
                double NetAmt = Val.ToDouble(DgvSalesDetEntry.Columns["NetAmount"].SummaryText);
                txtGrossAmtLocal.Text = GrossAmt.ToString();
                //txtNetAmtLocal.Text = NetAmt.ToString();

                decimal Dis_amt = Math.Round(Val.ToDecimal(GrossAmt) * Val.ToDecimal(txtDiscountPer.Text) / 100, 0);
                txtDiscountAmt.Text = Dis_amt.ToString();

                decimal Gross_Amt = Math.Round(Val.ToDecimal(GrossAmt) - Val.ToDecimal(Dis_amt), 0);

                decimal CGST_amt = Math.Round(Val.ToDecimal(Gross_Amt) * Val.ToDecimal(txtCGSTPer.Text) / 100, 0);
                txtCGSTAmt.Text = CGST_amt.ToString();

                decimal SGST_amt = Math.Round(Val.ToDecimal(Gross_Amt) * Val.ToDecimal(txtSGSTPer.Text) / 100, 0);
                txtSGSTAmt.Text = SGST_amt.ToString();

                decimal IGST_amt = Math.Round(Val.ToDecimal(Gross_Amt) * Val.ToDecimal(txtIGSTPer.Text) / 100, 0);
                txtIGSTAmt.Text = IGST_amt.ToString();

                decimal Net_Amount = Math.Round((Val.ToDecimal(GrossAmt) + Val.ToDecimal(txtCGSTAmt.Text) + Val.ToDecimal(txtSGSTAmt.Text) + Val.ToDecimal(txtIGSTAmt.Text) - Val.ToDecimal(txtDiscountAmt.Text)), 0);
                txtNetAmtLocal.Text = Net_Amount.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dgvPurchase_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            CalculateGridAmount(DgvSalesDetEntry.FocusedRowHandle);
            //GetSummary();
        }

        private void dgvPurchase_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            CalculateGridAmount(e.PrevFocusedRowHandle);
            //GetSummary();
        }

        private void dgvPurchase_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            CalculateGridAmount(DgvSalesDetEntry.FocusedRowHandle);
            //GetSummary();
        }

        //private void GetSummary()
        //{
        //    double IGST = Val.ToDouble(DgvSalesDetEntry.Columns["IGST_Amt"].SummaryText);
        //    double CGST = Val.ToDouble(DgvSalesDetEntry.Columns["CGST_Amt"].SummaryText);
        //    double SGST = Val.ToDouble(DgvSalesDetEntry.Columns["SGST_Amt"].SummaryText);
        //    double GrossAmt = Val.ToDouble(DgvSalesDetEntry.Columns["NetAmount"].SummaryText);
        //    txtIGST.Text = IGST.ToString();
        //    txtCGST.Text = CGST.ToString();
        //    txtSGST.Text = SGST.ToString();

        //    //if (txtPartyState.Text == GlobalDec.gEmployeeProperty.State_Code.ToString())
        //    //{
        //    //    //double GrsAmt = Math.Round(GrossAmt + CGST + SGST, 2);
        //    //    txtGrossAmtLocal.Text = GrossAmt.ToString();
        //    //}
        //    //else
        //    //{
        //    //double GrsAmt = Math.Round(GrossAmt + IGST, 2);
        //    //txtGrossAmtLocal.Text = GrossAmt.ToString();
        //    txtNetAmtLocal.Text = GrossAmt.ToString();

        //    //}
        //}

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
            if (Global.Confirm("Are You Sure To Print Sale Invoice ?", "SALES", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }
            Invoice_EntryProperty Invoice_EntryPropertyNew = new Invoice_EntryProperty();
            Invoice_EntryPropertyNew.Invoice_Date = Val.DBDate(DTPInvoiceDate.Text);
            Invoice_EntryPropertyNew.Invoice_No = Val.ToString(txtInvoiceNo.Text);
            Invoice_EntryPropertyNew.Trn_Id = Val.ToInt64(lblMode.Tag);
            Invoice_EntryPropertyNew.Type = Val.ToString(Form_Type);

            DataTable dtpur = new DataTable();
            dtpur = ObjInvoiceEntry.GetPrintData(Invoice_EntryPropertyNew); //ObjInvoice.GetPrintData(Property);

            string Remark = Val.ToString(dtpur.Rows[0]["REMARK"]);

            for (int j = 0; j < dtpur.Rows.Count; j++)
            {
                dtpur.Rows[j]["REMARK"] = "";
            }


            for (int i = dtpur.Rows.Count; i < 14; i++)
            {
                dtpur.Rows.Add();
                if (i == 13)
                {
                    dtpur.Rows[i]["COMPANY_NAME"] = dtpur.Rows[0]["COMPANY_NAME"];
                    dtpur.Rows[i]["BRANCH_NAME"] = dtpur.Rows[0]["BRANCH_NAME"];
                    dtpur.Rows[i]["LOCATION_NAME"] = dtpur.Rows[0]["LOCATION_NAME"];
                    dtpur.Rows[i]["FROM_PARTY"] = "ABCDEFGHRTYYGGG";
                    dtpur.Rows[i]["FROM_GSTIN"] = dtpur.Rows[0]["FROM_GSTIN"];
                    dtpur.Rows[i]["FROM_ADDRESS"] = dtpur.Rows[0]["FROM_ADDRESS"];
                    dtpur.Rows[i]["Party_Mobile"] = dtpur.Rows[0]["Party_Mobile"];
                    dtpur.Rows[i]["Party_PanNo"] = dtpur.Rows[0]["Party_PanNo"];
                    dtpur.Rows[i]["TO_PARTY"] = dtpur.Rows[0]["TO_PARTY"];
                    dtpur.Rows[i]["TO_GSTIN"] = dtpur.Rows[0]["TO_GSTIN"];
                    dtpur.Rows[i]["FROM_STATE"] = dtpur.Rows[0]["FROM_STATE"];
                    dtpur.Rows[i]["TO_STATE"] = dtpur.Rows[0]["TO_STATE"];
                    dtpur.Rows[i]["TO_ADDRESS"] = dtpur.Rows[0]["TO_ADDRESS"];
                    dtpur.Rows[i]["INVOICE_DATE"] = dtpur.Rows[0]["INVOICE_DATE"];
                    dtpur.Rows[i]["INVOICE_TYPE"] = dtpur.Rows[0]["INVOICE_TYPE"];
                    dtpur.Rows[i]["PARTY_INVOICE_NO"] = dtpur.Rows[0]["PARTY_INVOICE_NO"];
                    dtpur.Rows[i]["IS_REVERSE"] = dtpur.Rows[0]["IS_REVERSE"];
                    dtpur.Rows[i]["NET_AMT"] = dtpur.Rows[0]["NET_AMT"];
                    dtpur.Rows[i]["Discount_Per"] = dtpur.Rows[0]["Discount_Per"];
                    dtpur.Rows[i]["Discount_Amt"] = dtpur.Rows[0]["Discount_Amt"];
                    dtpur.Rows[i]["SGST_Per"] = dtpur.Rows[0]["SGST_Per"];
                    dtpur.Rows[i]["SGST_Amt"] = dtpur.Rows[0]["SGST_Amt"];
                    dtpur.Rows[i]["CGST_Per"] = dtpur.Rows[0]["CGST_Per"];
                    dtpur.Rows[i]["CGST_Amt"] = dtpur.Rows[0]["CGST_Amt"];
                    dtpur.Rows[i]["IGST_Per"] = dtpur.Rows[0]["IGST_Per"];
                    dtpur.Rows[i]["IGST_Amt"] = dtpur.Rows[0]["IGST_Amt"];
                    dtpur.Rows[i]["ShipTo_Ledger_Code"] = dtpur.Rows[0]["ShipTo_Ledger_Code"];
                    dtpur.Rows[i]["ShipTo_Ledger_Name"] = dtpur.Rows[0]["ShipTo_Ledger_Name"];
                    dtpur.Rows[i]["SHIP_GSTIN"] = dtpur.Rows[0]["SHIP_GSTIN"];
                    dtpur.Rows[i]["SHIP_ADDRESS"] = dtpur.Rows[0]["SHIP_ADDRESS"];
                    dtpur.Rows[i]["SHIP_Mobile"] = dtpur.Rows[0]["SHIP_Mobile"];
                    dtpur.Rows[i]["SHIP_PanNo"] = dtpur.Rows[0]["SHIP_PanNo"];
                    dtpur.Rows[i]["SHIP_STATE"] = dtpur.Rows[0]["SHIP_STATE"];
                    dtpur.Rows[i]["REMARK"] = Remark;
                }
                else
                {
                    dtpur.Rows[i]["COMPANY_NAME"] = dtpur.Rows[0]["COMPANY_NAME"];
                    dtpur.Rows[i]["BRANCH_NAME"] = dtpur.Rows[0]["BRANCH_NAME"];
                    dtpur.Rows[i]["LOCATION_NAME"] = dtpur.Rows[0]["LOCATION_NAME"];
                    dtpur.Rows[i]["FROM_PARTY"] = dtpur.Rows[0]["FROM_PARTY"];
                    dtpur.Rows[i]["FROM_GSTIN"] = dtpur.Rows[0]["FROM_GSTIN"];
                    dtpur.Rows[i]["FROM_ADDRESS"] = dtpur.Rows[0]["FROM_ADDRESS"];
                    dtpur.Rows[i]["Party_Mobile"] = dtpur.Rows[0]["Party_Mobile"];
                    dtpur.Rows[i]["Party_PanNo"] = dtpur.Rows[0]["Party_PanNo"];
                    dtpur.Rows[i]["TO_PARTY"] = dtpur.Rows[0]["TO_PARTY"];
                    dtpur.Rows[i]["TO_GSTIN"] = dtpur.Rows[0]["TO_GSTIN"];
                    dtpur.Rows[i]["FROM_STATE"] = dtpur.Rows[0]["FROM_STATE"];
                    dtpur.Rows[i]["TO_STATE"] = dtpur.Rows[0]["TO_STATE"];
                    dtpur.Rows[i]["TO_ADDRESS"] = dtpur.Rows[0]["TO_ADDRESS"];
                    dtpur.Rows[i]["INVOICE_DATE"] = dtpur.Rows[0]["INVOICE_DATE"];
                    dtpur.Rows[i]["INVOICE_TYPE"] = dtpur.Rows[0]["INVOICE_TYPE"];
                    dtpur.Rows[i]["PARTY_INVOICE_NO"] = dtpur.Rows[0]["PARTY_INVOICE_NO"];
                    dtpur.Rows[i]["IS_REVERSE"] = dtpur.Rows[0]["IS_REVERSE"];
                    dtpur.Rows[i]["NET_AMT"] = dtpur.Rows[0]["NET_AMT"];
                    dtpur.Rows[i]["Discount_Per"] = dtpur.Rows[0]["Discount_Per"];
                    dtpur.Rows[i]["Discount_Amt"] = dtpur.Rows[0]["Discount_Amt"];
                    dtpur.Rows[i]["SGST_Per"] = dtpur.Rows[0]["SGST_Per"];
                    dtpur.Rows[i]["SGST_Amt"] = dtpur.Rows[0]["SGST_Amt"];
                    dtpur.Rows[i]["CGST_Per"] = dtpur.Rows[0]["CGST_Per"];
                    dtpur.Rows[i]["CGST_Amt"] = dtpur.Rows[0]["CGST_Amt"];
                    dtpur.Rows[i]["IGST_Per"] = dtpur.Rows[0]["IGST_Per"];
                    dtpur.Rows[i]["IGST_Amt"] = dtpur.Rows[0]["IGST_Amt"];
                    dtpur.Rows[i]["ShipTo_Ledger_Code"] = dtpur.Rows[0]["ShipTo_Ledger_Code"];
                    dtpur.Rows[i]["ShipTo_Ledger_Name"] = dtpur.Rows[0]["ShipTo_Ledger_Name"];
                    dtpur.Rows[i]["SHIP_GSTIN"] = dtpur.Rows[0]["SHIP_GSTIN"];
                    dtpur.Rows[i]["SHIP_ADDRESS"] = dtpur.Rows[0]["SHIP_ADDRESS"];
                    dtpur.Rows[i]["SHIP_Mobile"] = dtpur.Rows[0]["SHIP_Mobile"];
                    dtpur.Rows[i]["SHIP_PanNo"] = dtpur.Rows[0]["SHIP_PanNo"];
                    dtpur.Rows[i]["SHIP_STATE"] = dtpur.Rows[0]["SHIP_STATE"];
                    dtpur.Rows[i]["COMPANY_NAME"] = dtpur.Rows[0]["COMPANY_NAME"];
                    dtpur.Rows[i]["BRANCH_NAME"] = dtpur.Rows[0]["BRANCH_NAME"];
                    dtpur.Rows[i]["LOCATION_NAME"] = dtpur.Rows[0]["LOCATION_NAME"];
                    dtpur.Rows[i]["FROM_PARTY"] = dtpur.Rows[0]["FROM_PARTY"];
                    dtpur.Rows[i]["FROM_GSTIN"] = dtpur.Rows[0]["FROM_GSTIN"];
                    dtpur.Rows[i]["FROM_ADDRESS"] = dtpur.Rows[0]["FROM_ADDRESS"];
                    dtpur.Rows[i]["Party_Mobile"] = dtpur.Rows[0]["Party_Mobile"];
                    dtpur.Rows[i]["Party_PanNo"] = dtpur.Rows[0]["Party_PanNo"];
                    dtpur.Rows[i]["TO_PARTY"] = dtpur.Rows[0]["TO_PARTY"];
                    dtpur.Rows[i]["TO_GSTIN"] = dtpur.Rows[0]["TO_GSTIN"];
                    dtpur.Rows[i]["FROM_STATE"] = dtpur.Rows[0]["FROM_STATE"];
                    dtpur.Rows[i]["TO_STATE"] = dtpur.Rows[0]["TO_STATE"];
                    dtpur.Rows[i]["TO_ADDRESS"] = dtpur.Rows[0]["TO_ADDRESS"];
                    dtpur.Rows[i]["INVOICE_DATE"] = dtpur.Rows[0]["INVOICE_DATE"];
                    dtpur.Rows[i]["INVOICE_TYPE"] = dtpur.Rows[0]["INVOICE_TYPE"];
                    dtpur.Rows[i]["PARTY_INVOICE_NO"] = dtpur.Rows[0]["PARTY_INVOICE_NO"];
                    dtpur.Rows[i]["IS_REVERSE"] = dtpur.Rows[0]["IS_REVERSE"];
                    dtpur.Rows[i]["NET_AMT"] = dtpur.Rows[0]["NET_AMT"];
                    dtpur.Rows[i]["Discount_Per"] = dtpur.Rows[0]["Discount_Per"];
                    dtpur.Rows[i]["Discount_Amt"] = dtpur.Rows[0]["Discount_Amt"];
                    dtpur.Rows[i]["SGST_Per"] = dtpur.Rows[0]["SGST_Per"];
                    dtpur.Rows[i]["SGST_Amt"] = dtpur.Rows[0]["SGST_Amt"];
                    dtpur.Rows[i]["CGST_Per"] = dtpur.Rows[0]["CGST_Per"];
                    dtpur.Rows[i]["CGST_Amt"] = dtpur.Rows[0]["CGST_Amt"];
                    dtpur.Rows[i]["IGST_Per"] = dtpur.Rows[0]["IGST_Per"];
                    dtpur.Rows[i]["IGST_Amt"] = dtpur.Rows[0]["IGST_Amt"];
                    dtpur.Rows[i]["ShipTo_Ledger_Code"] = dtpur.Rows[0]["ShipTo_Ledger_Code"];
                    dtpur.Rows[i]["ShipTo_Ledger_Name"] = dtpur.Rows[0]["ShipTo_Ledger_Name"];
                    dtpur.Rows[i]["SHIP_GSTIN"] = dtpur.Rows[0]["SHIP_GSTIN"];
                    dtpur.Rows[i]["SHIP_ADDRESS"] = dtpur.Rows[0]["SHIP_ADDRESS"];
                    dtpur.Rows[i]["SHIP_Mobile"] = dtpur.Rows[0]["SHIP_Mobile"];
                    dtpur.Rows[i]["SHIP_PanNo"] = dtpur.Rows[0]["SHIP_PanNo"];
                    dtpur.Rows[i]["SHIP_STATE"] = dtpur.Rows[0]["SHIP_STATE"];
                    dtpur.Rows[i]["REMARK"] = "";
                }
            }

            FrmReportViewer FrmReportViewer = new FrmReportViewer();
            FrmReportViewer.DS.Tables.Add(dtpur);
            FrmReportViewer.GroupBy = "";
            FrmReportViewer.RepName = "";
            FrmReportViewer.RepPara = "";
            this.Cursor = Cursors.Default;
            FrmReportViewer.AllowSetFormula = true;

            FrmReportViewer.ShowForm("Sale_Memo", 120, Report.FrmReportViewer.ReportFolder.ACCOUNT);
            Invoice_EntryPropertyNew = null;
            dtpur = null;
            FrmReportViewer.DS.Tables.Clear();
            FrmReportViewer.DS.Clear();
            FrmReportViewer = null;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Global.Confirm("Are You Sure To Delete ?", "SALES", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }
            int IntRes = 0;
            Invoice_EntryProperty Invoice_EntryPropertyNew = new Invoice_EntryProperty();
            Invoice_EntryPropertyNew.Purchase_Master_ID = Val.ToInt64(lblMode.Tag);
            IntRes = ObjInvoiceEntry.DeleteInvoiceEntryMaster(Invoice_EntryPropertyNew, Form_Type);

            if (IntRes != 0)
            {
                Global.Confirm("Data Deleted Successfully");
                GetData();
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
                    //dgvPurchase.DeleteRow(dgvPurchase.GetRowHandle(dgvPurchase.FocusedRowHandle));
                    Invoice_EntryProperty Invoice_EntryProperty = new Invoice_EntryProperty();
                    int IntRes = 0;

                    Int64 ItemPurchaseMasterID = Val.ToInt64(DgvSalesDetEntry.GetFocusedRowCellValue("ItemSaleMasterID").ToString());
                    Invoice_EntryProperty.ItemPurchaseMasterID = Val.ToInt64(ItemPurchaseMasterID);
                    Invoice_EntryProperty.ItemPurchaseDetailID = Val.ToInt64(DgvSalesDetEntry.GetFocusedRowCellValue("ItemSaleDetailID").ToString());

                    if (ItemPurchaseMasterID == 0)
                    {
                        DgvSalesDetEntry.DeleteRow(DgvSalesDetEntry.GetRowHandle(DgvSalesDetEntry.FocusedRowHandle));
                    }
                    else
                    {
                        IntRes = ObjInvoiceEntry.DeletePurchaseDetail(Invoice_EntryProperty, Form_Type);
                        DgvSalesDetEntry.DeleteRow(DgvSalesDetEntry.GetRowHandle(DgvSalesDetEntry.FocusedRowHandle));
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

        private void txtInvoiceNo_Validating(object sender, CancelEventArgs e)
        {
            //if (!isEditMode && !ObjInvoiceEntry.CheckValidInvoiceNo(Form_Type, txtInvoiceNo.Text))
            //{
            //    Global.Message("Invoice No : " + txtInvoiceNo.Text + " is already exist.");
            //    txtInvoiceNo.Focus();
            //}
        }

        private void chkToPartySame_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkToPartySame.Checked)
            //{
            //    LookupBillToParty.Text = LookupParty.Text;
            //    LookupBillToParty.EditValue = LookupParty.EditValue;
            //}
        }

        private void LookupBillToParty_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmLedgerMaster frmCnt = new FrmLedgerMaster();
                frmCnt.ShowDialog();
                Ledger_MasterProperty Party = new Ledger_MasterProperty();
                Party.Party_Type = "";
                Global.LOOKUPParty(LookupParty, Party);
                Party = null;
            }
        }

        private void LookupParty_EditValueChanged(object sender, EventArgs e)
        {
            //if (LookupParty.Text.Trim().Length > 0)
            //{
            //    txtPartyState.Text = LookupParty.GetColumnValue("Party_State_Code").ToString();
            //    string gst = LookupParty.GetColumnValue("GSTIN").ToString();
            //    if (gst.Length > 0) { CHKReverse.EditValue = 1; } else { CHKReverse.EditValue = 0; }
            //    DgvSalesDetEntry.PostEditor();
            //}
            //else
            //{
            //    txtPartyState.Text = ""; CHKReverse.EditValue = 0;
            //}
            //chkGST();
        }

        private void LookupParty_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmLedgerMaster frmCnt = new FrmLedgerMaster();
                frmCnt.ShowDialog();
                Ledger_MasterProperty Party = new Ledger_MasterProperty();
                Party.Party_Type = "";
                Global.LOOKUPParty(LookupParty, Party);
                Party = null;
            }
        }

        private void txtDiscountPer_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                decimal Dis_amt = Math.Round(Val.ToDecimal(ClmGrossAmt.SummaryItem.SummaryValue) * Val.ToDecimal(txtDiscountPer.Text) / 100, 0);
                txtDiscountAmt.Text = Dis_amt.ToString();
                decimal Net_Amount = Math.Round((Val.ToDecimal(ClmGrossAmt.SummaryItem.SummaryValue) + Val.ToDecimal(txtCGSTAmt.Text) + Val.ToDecimal(txtSGSTAmt.Text) + Val.ToDecimal(txtIGSTAmt.Text) - Val.ToDecimal(txtDiscountAmt.Text)), 0);
                txtNetAmtLocal.Text = Net_Amount.ToString();
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }

        private void txtCGSTPer_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                decimal Gross_Amt = Math.Round(Val.ToDecimal(ClmGrossAmt.SummaryItem.SummaryValue) - Val.ToDecimal(txtDiscountAmt.Text), 0);
                decimal CGST_amt = Math.Round(Val.ToDecimal(Gross_Amt) * Val.ToDecimal(txtCGSTPer.Text) / 100, 0);
                txtCGSTAmt.Text = CGST_amt.ToString();
                decimal Net_Amount = Math.Round((Val.ToDecimal(ClmGrossAmt.SummaryItem.SummaryValue) + Val.ToDecimal(txtCGSTAmt.Text) + Val.ToDecimal(txtSGSTAmt.Text) + Val.ToDecimal(txtIGSTAmt.Text) - Val.ToDecimal(txtDiscountAmt.Text)), 0);
                txtNetAmtLocal.Text = Net_Amount.ToString();
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }

        private void txtSGSTPer_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                decimal Gross_Amt = Math.Round(Val.ToDecimal(ClmGrossAmt.SummaryItem.SummaryValue) - Val.ToDecimal(txtDiscountAmt.Text), 0);
                decimal SGST_amt = Math.Round(Val.ToDecimal(Gross_Amt) * Val.ToDecimal(txtSGSTPer.Text) / 100, 0);
                txtSGSTAmt.Text = SGST_amt.ToString();
                decimal Net_Amount = Math.Round((Val.ToDecimal(ClmGrossAmt.SummaryItem.SummaryValue) + Val.ToDecimal(txtCGSTAmt.Text) + Val.ToDecimal(txtSGSTAmt.Text) + Val.ToDecimal(txtIGSTAmt.Text) - Val.ToDecimal(txtDiscountAmt.Text)), 0);
                txtNetAmtLocal.Text = Net_Amount.ToString();
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }

        private void txtIGSTPer_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                decimal Gross_Amt = Math.Round(Val.ToDecimal(ClmGrossAmt.SummaryItem.SummaryValue) - Val.ToDecimal(txtDiscountAmt.Text), 0);
                decimal IGST_amt = Math.Round(Val.ToDecimal(Gross_Amt) * Val.ToDecimal(txtIGSTPer.Text) / 100, 0);
                txtIGSTAmt.Text = IGST_amt.ToString();
                decimal Net_Amount = Math.Round((Val.ToDecimal(ClmGrossAmt.SummaryItem.SummaryValue) + Val.ToDecimal(txtCGSTAmt.Text) + Val.ToDecimal(txtSGSTAmt.Text) + Val.ToDecimal(txtIGSTAmt.Text) - Val.ToDecimal(txtDiscountAmt.Text)), 0);
                txtNetAmtLocal.Text = Net_Amount.ToString();
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return;
            }
        }

        private void txtPaymentDays_EditValueChanged_1(object sender, EventArgs e)
        {
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

        private void txtPassword_EditValueChanged(object sender, EventArgs e)
        {
            if (txtPassword.Text != "")
            {
                if (Val.ToString(txtPassword.Text) == "123")
                {
                    btnDelete.Visible = true;
                }
                else
                {
                    btnDelete.Visible = false;
                }
            }
        }

        private void chkInvoiceNo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkInvoiceNo.Checked == true)
            {
                txtInvoiceNo.Enabled = true;
            }
            else
            {
                txtInvoiceNo.Enabled = false;
            }
        }
    }
}