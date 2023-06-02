using BLL.FunctionClasses.Report;
using BLL.PropertyClasses.Report;
using STORE.Class;
using System;
using System.Data;
using System.Windows.Forms;
namespace STORE.Report
{
    public partial class FrmTransaction_View : Form
    {
        BLL.FormEvents objBOFormEvents = new BLL.FormEvents();
        BLL.Validation Val = new BLL.Validation();
        string mStrReportGroup = string.Empty;
        string RptName = "";
        DataTable mDTDetail = new DataTable("New_Report_Detail");
        ReportParams ObjReportParams = new ReportParams();
        New_Report_DetailProperty ObjReportDetailProperty = new New_Report_DetailProperty();

        #region Counstructor

        public FrmTransaction_View()
        {
            InitializeComponent();
        }

        public void ShowForm(string pStrReportGroup)
        {
            mStrReportGroup = pStrReportGroup;
            Val.frmGenSet(this);
            AttachFormEvents();
            RptName = pStrReportGroup;
            //lblTitle.Text = mStrReportGroup + " Reports..";
            this.Text = mStrReportGroup + " Report";
            Global.LOOKUPPartyName(ChkCmbParty);
            ChkCmbParty.Focus();
            this.Show();
            SetControl();
            DTPTranFromDate.Text = Val.DBDate(System.DateTime.Now.ToShortDateString());
            DTPTranToDate.Text = Val.DBDate(System.DateTime.Now.ToShortDateString());
            DTPTranFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPTranFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPTranFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPTranFromDate.Properties.CharacterCasing = CharacterCasing.Upper;

            DTPTranToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPTranToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPTranToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPTranToDate.Properties.CharacterCasing = CharacterCasing.Upper;

            DTPTranFromDate.EditValue = DateTime.Now;
            DTPTranToDate.EditValue = DateTime.Now;
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
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            DTPTranFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPTranFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPTranFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPTranFromDate.Properties.CharacterCasing = CharacterCasing.Upper;

            DTPTranToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPTranToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPTranToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPTranToDate.Properties.CharacterCasing = CharacterCasing.Upper;

            DTPTranFromDate.EditValue = DateTime.Now;
            DTPTranToDate.EditValue = DateTime.Now;

            ChkCmbParty.SetEditValue(null);
            CmbCashBankType.SelectedIndex = 0;
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
            if (RptName.ToString() == "Transaction View")
            {
                ReportParams_Property.Group_By_Tag = ListGroupBy1.GetTagValue;
                ReportParams_Property.From_Date = Val.DBDate(DTPTranFromDate.Text);
                ReportParams_Property.To_Date = Val.DBDate(DTPTranToDate.Text);
                ReportParams_Property.Party_Code = Val.Trim(ChkCmbParty.Properties.GetCheckedItems());
                if (CmbCashBankType.EditValue.ToString() == "Select")
                {
                    ReportParams_Property.Cash_Type = Val.ToString("");
                }
                else
                {
                    ReportParams_Property.Cash_Type = Val.ToString(CmbCashBankType.EditValue);
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
            if (RptName.ToString() == "Transaction View")
            {
                DTab = ObjReportParams.Get_Transaction_View_Report(ReportParams_Property, "RP_TRANSACTION_VIEW");
            }
        }
        int mIntPivot = 0;
        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            PanelLoading.Visible = false;
            if (chkPivot.Checked == true)
            {
                mIntPivot = 1;
            }
            else
            {
                mIntPivot = 0;
            }
            //FrmGReportViewer FrmPReportViewer = new Report.FrmGReportViewer();
            ////if (chkPivot.Checked == true)
            ////{
            ////    FrmPReportViewer.IsPivot = true;
            ////}
            ////else
            ////{
            ////    FrmPReportViewer.IsPivot = false;
            ////}
            //FrmPReportViewer.mDTDetail = DTab;
            //FrmPReportViewer.Group_By_Tag = rptMultiSelect1.GetTagValue;
            //FrmPReportViewer.Group_By_Text = rptMultiSelect1.GetTextValue;

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
            if (mIntPivot == 0)
            {
                //FrmGReportViewer FrmPReportViewer = new Report.FrmGReportViewer();
                //FrmPReportViewer.mDTDetail = DTab;
                //FrmPReportViewer.Group_By_Tag = rptMultiSelect1.GetTagValue;
                //FrmPReportViewer.Group_By_Text = rptMultiSelect1.GetTextValue;

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
                FrmGReportViewerBand FrmGReportViewer = new Report.FrmGReportViewerBand();

                if (chkPivot.Checked == true)
                {
                    FrmGReportViewer.IsPivot = true;
                }
                else
                {
                    FrmGReportViewer.IsPivot = false;
                }

                FrmGReportViewer.Group_By_Tag = ListGroupBy1.GetTagValue;
                FrmGReportViewer.Group_By_Text = ListGroupBy1.GetTextValue;
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
                if (DTPTranFromDate.Text != "")
                {
                    FrmGReportViewer.FromIssue_Date = DTPTranFromDate.Text;
                }
                if (DTPTranFromDate.Text != "")
                {
                    FrmGReportViewer.ToIssue_Date = DTPTranToDate.Text;
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

                FrmPReportViewer.Group_By_Tag = ListGroupBy1.GetTagValue;
                FrmPReportViewer.Group_By_Text = ListGroupBy1.GetTextValue;
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
                //FrmGReportViewer FrmPReportViewer = new Report.FrmGReportViewer();
                //FrmPReportViewer.mDTDetail = DTab;
                //FrmPReportViewer.Group_By_Tag = ListGroupBy1.GetTagValue;
                //FrmPReportViewer.Group_By_Text = ListGroupBy1.GetTextValue;

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
            }
            BtnGenerateReport.Enabled = true;
            this.Enabled = true;
        }

        private void FrmReportList_Shown(object sender, EventArgs e)
        {
            mDTDetail.Columns.Add("COLUMN_NAME", typeof(string));
            mDTDetail.Columns.Add("FIELD_NAME", typeof(string));
            mDTDetail.Columns.Add("IS_GROUP", typeof(decimal));
            mDTDetail.Columns.Add("BANDS", typeof(string));

            if (RptName.ToString() == "Transaction View")
            {
                mDTDetail.Rows.Add("Debit", "DEBIT");
                mDTDetail.Rows.Add("Credit", "CREDIT");
                mDTDetail.Rows.Add("Party Name", "Party_Name");
                mDTDetail.Rows.Add("Case", "Cash_Type");
                mDTDetail.Rows.Add("Remark", "Remark");
            }
            for (int i = 0; i < mDTDetail.Rows.Count; i++)
            {
                mDTDetail.Rows[i]["IS_GROUP"] = 1;
                if (mDTDetail.Rows[i]["FIELD_NAME"].ToString() == "DEBIT")
                    mDTDetail.Rows[i]["BANDS"] = "DEBIT";
                if (mDTDetail.Rows[i]["FIELD_NAME"].ToString() == "CREDIT")
                    mDTDetail.Rows[i]["BANDS"] = "CREDIT";
            }
            if (ListGroupBy1.GetTextValue != "")
            {
                ObjReportDetailProperty.Default_Group_By = ListGroupBy1.GetTextValue;
            }
            ListGroupBy1.DTab = mDTDetail;
            ListGroupBy1.Default = "Party_Name,Cash_Type,Remark";// ObjReportDetailProperty.Default_Group_By;
            //rptMultiSelect1.DTab = mDTDetail;
        }

        private string GetFilterByValue()
        {
            string Str = "Filter By : ";

            if (DTPTranFromDate.Text != "")
            {
                Str += "From Issue Date : " + DTPTranFromDate.Text;
            }
            if (DTPTranToDate.Text != "")
            {
                Str += " & As On Date : " + DTPTranToDate.Text;
            }

            return Str;
        }

        private void LookupPartyName_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                FrmLedgerMaster frmCnt = new FrmLedgerMaster();
                frmCnt.ShowDialog();
                Global.LOOKUPPartyName(ChkCmbParty);
            }
        }
    }
}
