using BLL.FunctionClasses.Report;
using BLL.PropertyClasses.Report;
using STORE.Class;
using System;
using System.Data;
using System.Windows.Forms;
namespace STORE.Report
{
    public partial class FrmItemPurchaseReport : Form
    {
        BLL.FormEvents objBOFormEvents = new BLL.FormEvents();
        BLL.Validation Val = new BLL.Validation();
        string mStrReportGroup = string.Empty;
        string RptName = "";
        ReportParams ObjReportParams = new ReportParams();
        DataTable mDTDetail = new DataTable("New_Report_Detail");
        New_Report_DetailProperty ObjReportDetailProperty = new New_Report_DetailProperty();
        int mIntPivot = 0;

        #region Counstructor

        public FrmItemPurchaseReport()
        {
            InitializeComponent();
        }
        public void ShowForm(string pStrReportGroup)
        {
            mStrReportGroup = pStrReportGroup;
            Val.frmGenSet(this);
            AttachFormEvents();
            RptName = pStrReportGroup;
            this.Text = mStrReportGroup + " Report";
            this.Show();
            SetControl();

            ChkCmbParty.Focus();
            DTPInvoiceFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPInvoiceFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPInvoiceFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPInvoiceFromDate.Properties.CharacterCasing = CharacterCasing.Upper;

            DTPInvoiceToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPInvoiceToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPInvoiceToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPInvoiceToDate.Properties.CharacterCasing = CharacterCasing.Upper;

            DTPInvoiceFromDate.EditValue = DateTime.Now;
            DTPInvoiceToDate.EditValue = DateTime.Now;
            SelRdo.SelectedIndex = 0;
        }

        private void AttachFormEvents()
        {
            objBOFormEvents.CurForm = this;
            objBOFormEvents.FormKeyDown = true;
            objBOFormEvents.FormKeyPress = true;
            objBOFormEvents.FormResize = true;
            objBOFormEvents.FormClosing = true;
            objBOFormEvents.ObjToDispose.Add(Val);
            objBOFormEvents.ObjToDispose.Add(objBOFormEvents);
        }

        #endregion

        private void SetControl()
        {
            Global.LOOKUPLedgerComboBox(ChkCmbParty);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            DTPInvoiceFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPInvoiceFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPInvoiceFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPInvoiceFromDate.Properties.CharacterCasing = CharacterCasing.Upper;

            DTPInvoiceToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPInvoiceToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPInvoiceToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPInvoiceToDate.Properties.CharacterCasing = CharacterCasing.Upper;

            DTPInvoiceFromDate.EditValue = DateTime.Now;
            DTPInvoiceToDate.EditValue = DateTime.Now;
            SelRdo.SelectedIndex = 0;
            ChkCmbParty.SetEditValue(null);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        ReportParams_Property ReportParams_Property = new BLL.PropertyClasses.Report.ReportParams_Property();
        DataTable DTab;

        private void BtnGenerateReport_Click(object sender, EventArgs e)
        {
            PanelLoading.Visible = true;
            if (mStrReportGroup == "Item Purchase")
            {
                ReportParams_Property.Ledger_Code = Val.Trim(ChkCmbParty.Properties.GetCheckedItems());
                ReportParams_Property.Invoice_No = Val.ToString(txtInvoiceNo.Text);
                ReportParams_Property.Stock_From_Issue_Date = Val.DBDate(DTPInvoiceFromDate.Text);
                ReportParams_Property.Stock_To_Issue_Date = Val.DBDate(DTPInvoiceToDate.Text);
                if (Val.ToString(this.SelRdo.EditValue) == "S")
                {
                    ReportParams_Property.Transaction_Type = Val.ToString("PS");
                }
                else if (Val.ToString(this.SelRdo.EditValue) == "D")
                {
                    ReportParams_Property.Transaction_Type = Val.ToString("PD");
                }
            }
            else if (mStrReportGroup == "Item Purchase Return")
            {
                ReportParams_Property.Ledger_Code = Val.Trim(ChkCmbParty.Properties.GetCheckedItems());
                ReportParams_Property.Invoice_No = Val.ToString(txtInvoiceNo.Text);
                ReportParams_Property.Stock_From_Issue_Date = Val.DBDate(DTPInvoiceFromDate.Text);
                ReportParams_Property.Stock_To_Issue_Date = Val.DBDate(DTPInvoiceToDate.Text);
                if (Val.ToString(this.SelRdo.EditValue) == "S")
                {
                    ReportParams_Property.Transaction_Type = Val.ToString("PRS");
                }
                else if (Val.ToString(this.SelRdo.EditValue) == "D")
                {
                    ReportParams_Property.Transaction_Type = Val.ToString("PRD");
                }
            }
            else if (mStrReportGroup == "Item Sale")
            {
                ReportParams_Property.Ledger_Code = Val.Trim(ChkCmbParty.Properties.GetCheckedItems());
                ReportParams_Property.Invoice_No = Val.ToString(txtInvoiceNo.Text);
                ReportParams_Property.Stock_From_Issue_Date = Val.DBDate(DTPInvoiceFromDate.Text);
                ReportParams_Property.Stock_To_Issue_Date = Val.DBDate(DTPInvoiceToDate.Text);
                if (Val.ToString(this.SelRdo.EditValue) == "S")
                {
                    ReportParams_Property.Transaction_Type = Val.ToString("SS");
                }
                else if (Val.ToString(this.SelRdo.EditValue) == "D")
                {
                    ReportParams_Property.Transaction_Type = Val.ToString("SD");
                }
            }
            else if (mStrReportGroup == "Item Sale Return")
            {
                ReportParams_Property.Ledger_Code = Val.Trim(ChkCmbParty.Properties.GetCheckedItems());
                ReportParams_Property.Invoice_No = Val.ToString(txtInvoiceNo.Text);
                ReportParams_Property.Stock_From_Issue_Date = Val.DBDate(DTPInvoiceFromDate.Text);
                ReportParams_Property.Stock_To_Issue_Date = Val.DBDate(DTPInvoiceToDate.Text);
                if (Val.ToString(this.SelRdo.EditValue) == "S")
                {
                    ReportParams_Property.Transaction_Type = Val.ToString("SRS");
                }
                else if (Val.ToString(this.SelRdo.EditValue) == "D")
                {
                    ReportParams_Property.Transaction_Type = Val.ToString("SRD");
                }
            }
            else if (mStrReportGroup == "Item Sale Entry")
            {
                ReportParams_Property.Ledger_Code = Val.Trim(ChkCmbParty.Properties.GetCheckedItems());
                ReportParams_Property.Invoice_No = Val.ToString(txtInvoiceNo.Text);
                ReportParams_Property.Stock_From_Issue_Date = Val.DBDate(DTPInvoiceFromDate.Text);
                ReportParams_Property.Stock_To_Issue_Date = Val.DBDate(DTPInvoiceToDate.Text);
                if (Val.ToString(this.SelRdo.EditValue) == "S")
                {
                    ReportParams_Property.Transaction_Type = Val.ToString("SES");
                }
                else if (Val.ToString(this.SelRdo.EditValue) == "D")
                {
                    ReportParams_Property.Transaction_Type = Val.ToString("SED");
                }
            }
            else if (mStrReportGroup == "Item Material Sale Entry")
            {
                ReportParams_Property.Ledger_Code = Val.Trim(ChkCmbParty.Properties.GetCheckedItems());
                ReportParams_Property.Invoice_No = Val.ToString(txtInvoiceNo.Text);
                ReportParams_Property.Stock_From_Issue_Date = Val.DBDate(DTPInvoiceFromDate.Text);
                ReportParams_Property.Stock_To_Issue_Date = Val.DBDate(DTPInvoiceToDate.Text);
                if (Val.ToString(this.SelRdo.EditValue) == "S")
                {
                    ReportParams_Property.Transaction_Type = Val.ToString("MSES");
                }
                else if (Val.ToString(this.SelRdo.EditValue) == "D")
                {
                    ReportParams_Property.Transaction_Type = Val.ToString("MSED");
                }
            }

            if (this.backgroundWorker1.IsBusy)
            {
            }
            else
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            DTab = ObjReportParams.Get_Item_Purchase_Report(ReportParams_Property, "RP_ITEM_PURCHASE_DETAIL");
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            //FrmGReportViewer FrmPReportViewer = new Report.FrmGReportViewer();
            //FrmPReportViewer.mDTDetail = DTab;

            //FrmPReportViewer.Report_Type = "Summary";

            //FrmPReportViewer.ReportHeaderName = RptName.ToString();
            //FrmPReportViewer.DTab = DTab;
            //FrmPReportViewer.FilterBy = GetFilterByValue();

            //if (FrmPReportViewer.DTab == null || FrmPReportViewer.DTab.Rows.Count == 0)
            //{
            //    this.Cursor = Cursors.Default;
            //    FrmPReportViewer.Dispose();
            //    FrmPReportViewer = null;
            //    Global.Confirm("Data Not Found");
            //    return;
            //}
            //FrmPReportViewer.MdiParent = Global.gMainFormRef;
            //FrmPReportViewer.ShowForm();

            PanelLoading.Visible = false;
            if (chkPivot.Checked == true)
            {
                mIntPivot = 1;
            }
            else
            {
                mIntPivot = 0;
            }
            if (mIntPivot == 0)
            {
                FrmGReportViewerBand FrmGReportViewer = new Report.FrmGReportViewerBand();

                if (chkPivot.Checked == true)
                {
                    FrmGReportViewer.IsPivot = true;
                }
                else
                {
                    FrmGReportViewer.IsPivot = false;
                }

                FrmGReportViewer.Group_By_Tag = ListGroupBy.GetTagValue;
                FrmGReportViewer.Group_By_Text = ListGroupBy.GetTextValue;
                FrmGReportViewer.ObjReportDetailProperty = ObjReportDetailProperty;
                FrmGReportViewer.mDTDetail = mDTDetail;
                FrmGReportViewer.Report_Type = "Summary";
                FrmGReportViewer.Report_Code = ObjReportDetailProperty.Report_code;
                FrmGReportViewer.ReportHeaderName = ObjReportDetailProperty.Report_Header_Name;
                FrmGReportViewer.FilterBy = GetFilterByValue();
                // FrmGReportViewer.DTab = DTab;
                FrmGReportViewer.Remark = ObjReportDetailProperty.Remark; // Add : Narendra : 27-09-2014
                FrmGReportViewer.ReportParams_Property = ReportParams_Property; // Add : Narendra : 27-09-2014
                FrmGReportViewer.Procedure_Name = ObjReportDetailProperty.Procedure_Name;// Add : Narendra : 27-09-2014
                FrmGReportViewer.FilterByFormName = this.Name;

                FrmGReportViewer.DTab = DTab;
                if (DTPInvoiceFromDate.Text != "")
                {
                    FrmGReportViewer.FromIssue_Date = DTPInvoiceFromDate.Text;
                }
                if (DTPInvoiceToDate.Text != "")
                {
                    FrmGReportViewer.ToIssue_Date = DTPInvoiceToDate.Text;
                }

                if (FrmGReportViewer.DTab == null || FrmGReportViewer.DTab.Rows.Count == 0)
                {
                    this.Cursor = Cursors.Default;
                    FrmGReportViewer.Dispose();
                    FrmGReportViewer = null;
                    MessageBox.Show("Data Not Found");
                    BtnGenerateReport.Enabled = true;
                    btnExit.Enabled = true;
                    this.Enabled = true;
                    return;
                }
                this.Cursor = Cursors.Default;
                FrmGReportViewer.ShowForm();
            }
            else
            {
                FrmGReportViewer FrmPReportViewer = new Report.FrmGReportViewer();
                FrmPReportViewer.mDTDetail = DTab;

                FrmPReportViewer.Report_Type = "Summary";

                FrmPReportViewer.ReportHeaderName = RptName.ToString();
                FrmPReportViewer.DTab = DTab;
                FrmPReportViewer.FilterBy = GetFilterByValue();

                FrmPReportViewer.Group_By_Tag = ListGroupBy.GetTagValue;
                FrmPReportViewer.Group_By_Text = ListGroupBy.GetTextValue;
                FrmPReportViewer.Report_Type = "Summary";

                if (FrmPReportViewer.DTab == null || FrmPReportViewer.DTab.Rows.Count == 0)
                {
                    this.Cursor = Cursors.Default;
                    FrmPReportViewer.Dispose();
                    FrmPReportViewer = null;
                    Global.Confirm("Data Not Found");
                    return;
                }
                FrmPReportViewer.MdiParent = Global.gMainFormRef;
                FrmPReportViewer.ShowForm();
            }
            BtnGenerateReport.Enabled = true;
            this.Enabled = true;
        }

        private void FrmReportList_Shown(object sender, EventArgs e)
        {
            //DataTable mDTDetail = new DataTable();
            //mDTDetail.Columns.Add("COLUMN_NAME", typeof(string));
            //mDTDetail.Columns.Add("FIELD_NAME", typeof(string));
            //if (RptName.ToString() == "Stock Asset")
            //{
            //    mDTDetail.Rows.Add("COMPANY", "COMPANY_NAME");
            //    mDTDetail.Rows.Add("BRANCH", "BRANCH_NAME");
            //    mDTDetail.Rows.Add("LOCATION", "LOCATION_NAME");
            //    mDTDetail.Rows.Add("ITEM", "ITEM_NAME");
            //    mDTDetail.Rows.Add("ITEM GROUP NAME", "ITEM_GROUP_NAME");
            //    labelControl23.Text = "Stock Date";
            //}
            //rptMultiSelect1.DTab = mDTDetail;

            //mDTDetail.Columns.Add("COLUMN_NAME", typeof(string));
            //mDTDetail.Columns.Add("FIELD_NAME", typeof(string));
            //mDTDetail.Columns.Add("IS_GROUP", typeof(decimal));
            //mDTDetail.Columns.Add("BANDS", typeof(string));

            //if (RptName.ToString() == "Transaction View")
            //{
            //    mDTDetail.Rows.Add("Debit", "DEBIT");
            //    mDTDetail.Rows.Add("Credit", "CREDIT");
            //    mDTDetail.Rows.Add("Party Name", "Party_Name");
            //    mDTDetail.Rows.Add("Case", "Cash_Type");
            //    mDTDetail.Rows.Add("Remark", "Remark");
            //}
            //for (int i = 0; i < mDTDetail.Rows.Count; i++)
            //{
            //    mDTDetail.Rows[i]["IS_GROUP"] = 1;
            //    if (mDTDetail.Rows[i]["FIELD_NAME"].ToString() == "DEBIT")
            //        mDTDetail.Rows[i]["BANDS"] = "DEBIT";
            //    if (mDTDetail.Rows[i]["FIELD_NAME"].ToString() == "CREDIT")
            //        mDTDetail.Rows[i]["BANDS"] = "CREDIT";
            //}
            //if (ListGroupBy.GetTextValue != "")
            //{
            //    ObjReportDetailProperty.Default_Group_By = ListGroupBy.GetTextValue;
            //}
            //ListGroupBy.DTab = mDTDetail;
            //ListGroupBy.Default = "Party_Name,Cash_Type,Remark";
        }

        private string GetFilterByValue()
        {
            string Str = "Filter By : ";
            if (DTPInvoiceFromDate.Text != "")
            {
                Str += "From Issue Date : " + DTPInvoiceFromDate.Text;
            }
            if (DTPInvoiceToDate.Text != "")
            {
                Str += " & As On Date : " + DTPInvoiceToDate.Text;
            }
            return Str;
        }
    }
}
