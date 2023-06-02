using BLL.FunctionClasses.Report;
using BLL.PropertyClasses.Report;
using STORE.Class;
using System;
using System.Data;
using System.Windows.Forms;
namespace STORE.Report
{
    public partial class FrmReportList : Form
    {
        BLL.FormEvents objBOFormEvents = new BLL.FormEvents();
        BLL.Validation Val = new BLL.Validation();
        string mStrReportGroup = string.Empty;
        string RptName = "";
        ReportParams ObjReportParams = new ReportParams();
        New_Report_DetailProperty ObjReportDetailProperty = new New_Report_DetailProperty();

        #region Counstructor

        public FrmReportList()
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
            this.Show();
            SetControl();
            ChkCmbCompany.Focus();
            //DTPStockFromDate.Text = Val.DBDate(System.DateTime.Now.ToShortDateString());
            //DTPStockToDate.Text = Val.DBDate(System.DateTime.Now.ToShortDateString());
            //DTPAcceptFromDate.Text = Val.DBDate(System.DateTime.Now.ToShortDateString());
            //DTPAcceptToDate.Text = Val.DBDate(System.DateTime.Now.ToShortDateString());

            DTPStockFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPStockFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPStockFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPStockFromDate.Properties.CharacterCasing = CharacterCasing.Upper;

            DTPAcceptToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
            DTPAcceptToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
            DTPAcceptToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
            DTPAcceptToDate.Properties.CharacterCasing = CharacterCasing.Upper;

            DTPStockFromDate.EditValue = DateTime.Now;
            DTPAcceptToDate.EditValue = DateTime.Now;
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
            Global.LOOKUPCompanyComboBox(ChkCmbCompany);
            ChkCmbCompany.SetEditValue(BLL.GlobalDec.gEmployeeProperty.Company_Code);

            Global.LOOKUPBranchComboBox(ChkCmbBranch);
            ChkCmbBranch.SetEditValue(BLL.GlobalDec.gEmployeeProperty.Branch_Code);

            Global.LOOKUPLocationComboBox(ChkCmbLocation);
            ChkCmbLocation.SetEditValue(BLL.GlobalDec.gEmployeeProperty.Location_Code);

            Global.LOOKUPCompanyComboBox(ChkCmbFromComp);
            Global.LOOKUPCompanyComboBox(ChkCmbToComp);
            Global.LOOKUPBranchComboBox(ChkCmbFromBranch);
            Global.LOOKUPBranchComboBox(ChkCmbToBranch);
            Global.LOOKUPLocationComboBox(ChkCmbFromLocation);
            Global.LOOKUPLocationComboBox(ChkCmbToLocation);

            Global.LOOKUPItemComboBox(ChkCmbItem);
            Global.LOOKUPItemCategoryComboBox(ChkCmbCategory);
            Global.LOOKUPItemGroupComboBox(ChkCmbGroup);

            //Global.LOOKUPDepartment(ChkCmbFromDept);
            //Global.LOOKUPDepartment(ChkCmbToDept);

            //Global.LOOKUPProcess(ChkCmbFromProcess);
            //Global.LOOKUPProcess(ChkCmbToProcess);

            //Global.LOOKUPEmployee(ChkCmbEmployee);
            //Global.LOOKUPRough(ChkCmbRoughName);
            //Global.LOOKUPRoughTypeCode(ChkCmbRoughType);

            //Global.LOOKUPClarity(ChkCmbMfgClarity, "Mfg");
            //Global.LOOKUPClarity(ChkCmbClvClarity, "Clv");
            //Global.LOOKUPShape(ChkCmbShape);
            //Global.LOOKUPSieve(ChkCmbSieve);
            //Global.LOOKUPColor(ChkCmbColor);
            //Global.LOOKUPProcess(ChkCmbProcess);
            //Global.LOOKUPParty(ChkCmbFromParty);
            //Global.LOOKUPParty(ChkCmbToParty);
            //Global.LOOKUPCity(ChkCmbCity);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (RptName.ToString() == "Stock Asset")
            {
                //DTPStockFromDate.Text = Val.DBDate(System.DateTime.Now.ToShortDateString());
                //DTPStockToDate.Text = Val.DBDate(System.DateTime.Now.ToShortDateString());
                DTPStockFromDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                DTPStockFromDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                DTPStockFromDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                DTPStockFromDate.Properties.CharacterCasing = CharacterCasing.Upper;

                DTPAcceptToDate.Properties.Mask.Culture = new System.Globalization.CultureInfo("en-US");
                DTPAcceptToDate.Properties.Mask.EditMask = "dd/MMM/yyyy";
                DTPAcceptToDate.Properties.Mask.UseMaskAsDisplayFormat = true;
                DTPAcceptToDate.Properties.CharacterCasing = CharacterCasing.Upper;

                DTPStockFromDate.EditValue = "";
                DTPAcceptToDate.EditValue = "";
                DTPAcceptFromDate.Text = "";
                DTPAcceptToDate.Text = "";
            }
            //else if (RptName.ToString() == "Party Transfer")
            //{
            //    DTPAcceptFromDate.Text = Val.DBDate(System.DateTime.Now.ToShortDateString());
            //    DTPAcceptToDate.Text = Val.DBDate(System.DateTime.Now.ToShortDateString());
            //    DTPStockFromDate.Text = "";
            //    DTPStockToDate.Text = "";
            //}
            //else if (RptName.ToString() == "Party OutStanding")
            //{
            //    DTPStockToDate.Text = Val.DBDate(System.DateTime.Now.ToShortDateString());
            //    DTPStockFromDate.Text = "";
            //    DTPAcceptFromDate.Text = "";
            //    DTPAcceptToDate.Text = "";
            //}
            //else if (RptName.ToString() == "Rough Transfer")
            //{
            //    DTPStockToDate.Text = Val.DBDate(System.DateTime.Now.ToShortDateString());
            //    DTPStockFromDate.Text = "";
            //    DTPAcceptFromDate.Text = "";
            //    DTPAcceptToDate.Text = "";
            //}

            ChkCmbCompany.SetEditValue(null);
            ChkCmbCompany.SetEditValue(BLL.GlobalDec.gEmployeeProperty.Company_Code);
            ChkCmbBranch.SetEditValue(null);
            ChkCmbBranch.SetEditValue(BLL.GlobalDec.gEmployeeProperty.Branch_Code);
            ChkCmbLocation.SetEditValue(null);
            ChkCmbLocation.SetEditValue(BLL.GlobalDec.gEmployeeProperty.Location_Code);
            //ChkCmbDeptName.SetEditValue(null);
            ChkCmbFromComp.SetEditValue(null);
            ChkCmbToComp.SetEditValue(null);
            ChkCmbFromBranch.SetEditValue(null);
            ChkCmbToBranch.SetEditValue(null);
            ChkCmbFromLocation.SetEditValue(null);
            ChkCmbToLocation.SetEditValue(null);
            //ChkCmbFromDept.SetEditValue(null);
            //ChkCmbToDept.SetEditValue(null);
            ChkCmbItem.SetEditValue(null);
            ChkCmbGroup.SetEditValue(null);
            ChkCmbCategory.SetEditValue(null);
            //ChkCmbToProcess.SetEditValue(null);
            //ChkCmbEmployee.SetEditValue(null);
            //ChkCmbRoughName.SetEditValue(null);
            //ChkCmbRoughType.SetEditValue(null);
            //ChkCmbRoughName.DeselectAll();
            //ChkCmbMfgClarity.SetEditValue(null);
            //ChkCmbShape.SetEditValue(null);
            //ChkCmbSieve.SetEditValue(null);
            //ChkCmbColor.SetEditValue(null);
            //ChkCmbClvClarity.SetEditValue(null); 
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        ReportParams_Property ReportParams_Property = new BLL.PropertyClasses.Report.ReportParams_Property();
        DataTable DTab;

        private void BtnGenerateReport_Click(object sender, EventArgs e)
        {
            if (RptName.ToString() == "Stock Asset")
            {
                ReportParams_Property.Group_By_Tag = rptMultiSelect1.GetTagValue;
                ReportParams_Property.Company_Code = Val.Trim(ChkCmbCompany.Properties.GetCheckedItems());
                ReportParams_Property.From_Company_Code = Val.Trim(ChkCmbFromComp.Properties.GetCheckedItems());
                ReportParams_Property.To_Company_Code = Val.Trim(ChkCmbToComp.Properties.GetCheckedItems());
                ReportParams_Property.Branch_Code = Val.Trim(ChkCmbBranch.Properties.GetCheckedItems());
                ReportParams_Property.From_Branch_Code = Val.Trim(ChkCmbFromBranch.Properties.GetCheckedItems());
                ReportParams_Property.To_Branch_Code = Val.Trim(ChkCmbToBranch.Properties.GetCheckedItems());
                ReportParams_Property.Location_Code = Val.Trim(ChkCmbLocation.Properties.GetCheckedItems());
                ReportParams_Property.From_Location_Code = Val.Trim(ChkCmbFromLocation.Properties.GetCheckedItems());
                ReportParams_Property.To_Location_Code = Val.Trim(ChkCmbToLocation.Properties.GetCheckedItems());
                //ReportParams_Property.Rough_Name = Global.GetCommaStr(Val.Trim(ChkCmbRoughName.Properties.GetCheckedItems()));
                //ReportParams_Property.Rough_Type_Code = Val.Trim(ChkCmbRoughType.Properties.GetCheckedItems());
                //ReportParams_Property.Main_Employee_Code = Val.Trim(ChkCmbEmployee.Properties.GetCheckedItems());
                //ReportParams_Property.Shape_Code = Val.Trim(ChkCmbShape.Properties.GetCheckedItems());
                //ReportParams_Property.Sieve_Code = Val.Trim(ChkCmbSieve.Properties.GetCheckedItems());
                //ReportParams_Property.Color_Code = Val.Trim(ChkCmbColor.Properties.GetCheckedItems());
                //ReportParams_Property.MFG_Clarity_Code = Val.Trim(ChkCmbMfgClarity.Properties.GetCheckedItems());
                //ReportParams_Property.CLV_Clarity_Code = Val.Trim(ChkCmbClvClarity.Properties.GetCheckedItems());

                //ReportParams_Property.Department_Code = Val.Trim(ChkCmbDeptName.Properties.GetCheckedItems());
                //ReportParams_Property.From_Department_Code = Val.Trim(ChkCmbFromDept.Properties.GetCheckedItems());
                //ReportParams_Property.To_Department_Code = Val.Trim(ChkCmbToDept.Properties.GetCheckedItems());
                ReportParams_Property.Item_Code = Val.Trim(ChkCmbItem.Properties.GetCheckedItems());
                ReportParams_Property.Group_Code = Val.Trim(ChkCmbGroup.Properties.GetCheckedItems());
                ReportParams_Property.Category_Code = Val.Trim(ChkCmbCategory.Properties.GetCheckedItems());
                //ReportParams_Property.To_Process_Code = Val.Trim(ChkCmbToProcess.Properties.GetCheckedItems());
                ReportParams_Property.Stock_From_Issue_Date = Val.DBDate(DTPStockFromDate.Text);
                ReportParams_Property.Stock_To_Issue_Date = Val.DBDate(DTPStockToDate.Text);
            }
            //else if (RptName.ToString() == "Party Transfer")
            //{
            //    if (Val.Trim(ChkCmbProcess.Properties.GetCheckedItems()) == "")
            //    {
            //        Global.Confirm("Please Select At Least One Process To Show Party Transfer Report");
            //        ChkCmbProcess.Focus();
            //        return;
            //    }

            //   // ReportParams_Property.Group_By_Tag = rptMultiSelect1.GetTagValue;
            //    ReportParams_Property.Company_Code = Val.Trim(ChkCmbCompany.Properties.GetCheckedItems());
            //    ReportParams_Property.Process_Code = Val.Trim(ChkCmbProcess.Properties.GetCheckedItems());
            //    ReportParams_Property.From_Party_Code = Val.Trim(ChkCmbFromParty.Properties.GetCheckedItems());
            //    ReportParams_Property.To_Party_Code = Val.Trim(ChkCmbToParty.Properties.GetCheckedItems());                
            //    ReportParams_Property.From_Location_Code = Val.Trim(ChkCmbFromLocation.Properties.GetCheckedItems());
            //    ReportParams_Property.To_Location_Code = Val.Trim(ChkCmbToLocation.Properties.GetCheckedItems());
            //    ReportParams_Property.Rough_Name = Global.GetCommaStr(Val.Trim(ChkCmbRoughName.Properties.GetCheckedItems()));
            //    ReportParams_Property.Rough_Type_Code = Val.Trim(ChkCmbRoughType.Properties.GetCheckedItems());
            //    ReportParams_Property.Shape_Code = Val.Trim(ChkCmbShape.Properties.GetCheckedItems());
            //    ReportParams_Property.Sieve_Code = Val.Trim(ChkCmbSieve.Properties.GetCheckedItems());
            //    ReportParams_Property.Color_Code = Val.Trim(ChkCmbColor.Properties.GetCheckedItems());
            //    ReportParams_Property.MFG_Clarity_Code = Val.Trim(ChkCmbMfgClarity.Properties.GetCheckedItems());
            //    ReportParams_Property.CLV_Clarity_Code = Val.Trim(ChkCmbClvClarity.Properties.GetCheckedItems());
            //    ReportParams_Property.Department_Code = Val.Trim(ChkCmbDeptName.Properties.GetCheckedItems());
            //    ReportParams_Property.From_Issue_Date = Val.DBDate(DTPTranFromDate.Text);
            //    ReportParams_Property.To_Issue_Date = Val.DBDate(DTPTranToDate.Text);
            //    ReportParams_Property.From_Receive_Date = Val.DBDate(DTPAcceptFromDate.Text);
            //    ReportParams_Property.To_Receive_Date = Val.DBDate(DTPAcceptToDate.Text);
            //}
            //else if (RptName.ToString() == "Party OutStanding")
            //{
            //    if (Val.Trim(ChkCmbProcess.Properties.GetCheckedItems()) == "")
            //    {
            //        Global.Confirm("Please Select At Least One Process To Party OutStanding Report");
            //        ChkCmbProcess.Focus();
            //        return;
            //    }

            //   // ReportParams_Property.Group_By_Tag = rptMultiSelect1.GetTagValue;
            //    ReportParams_Property.Company_Code = Val.Trim(ChkCmbCompany.Properties.GetCheckedItems());
            //    ReportParams_Property.Branch_Code = Val.Trim(ChkCmbBranch.Properties.GetCheckedItems());
            //    ReportParams_Property.Process_Code = Val.Trim(ChkCmbProcess.Properties.GetCheckedItems());
            //    ReportParams_Property.From_Party_Code = Val.Trim(ChkCmbFromParty.Properties.GetCheckedItems());
            //    ReportParams_Property.To_Party_Code = Val.Trim(ChkCmbToParty.Properties.GetCheckedItems());
            //    ReportParams_Property.From_Location_Code = Val.Trim(ChkCmbFromLocation.Properties.GetCheckedItems());
            //    ReportParams_Property.To_Location_Code = Val.Trim(ChkCmbToLocation.Properties.GetCheckedItems());
            //    ReportParams_Property.Rough_Name = Global.GetCommaStr(Val.Trim(ChkCmbRoughName.Properties.GetCheckedItems()));
            //    ReportParams_Property.Rough_Type_Code = Val.Trim(ChkCmbRoughType.Properties.GetCheckedItems());
            //    ReportParams_Property.Shape_Code = Val.Trim(ChkCmbShape.Properties.GetCheckedItems());
            //    ReportParams_Property.Sieve_Code = Val.Trim(ChkCmbSieve.Properties.GetCheckedItems());
            //    ReportParams_Property.Color_Code = Val.Trim(ChkCmbColor.Properties.GetCheckedItems());
            //    ReportParams_Property.MFG_Clarity_Code = Val.Trim(ChkCmbMfgClarity.Properties.GetCheckedItems());
            //    ReportParams_Property.CLV_Clarity_Code = Val.Trim(ChkCmbClvClarity.Properties.GetCheckedItems());
            //    ReportParams_Property.Department_Code = Val.Trim(ChkCmbDeptName.Properties.GetCheckedItems());

            //    ReportParams_Property.From_Issue_Date = Val.DBDate(DTPTranFromDate.Text);
            //    ReportParams_Property.To_Issue_Date = Val.DBDate(DTPTranToDate.Text);
            //    ReportParams_Property.From_Receive_Date = Val.DBDate(DTPAcceptFromDate.Text);
            //    ReportParams_Property.To_Receive_Date = Val.DBDate(DTPAcceptToDate.Text);
            //}
            //else if (RptName.ToString() == "Rough Transfer")
            //{
            //    if (Val.Trim(ChkCmbProcess.Properties.GetCheckedItems()) == "")
            //    {
            //        Global.Confirm("Please Select At Least One Process To Party OutStanding Report");
            //        ChkCmbProcess.Focus();
            //        return;
            //    }

            //    //ReportParams_Property.Group_By_Tag = rptMultiSelect1.GetTagValue;
            //    ReportParams_Property.Company_Code = Val.Trim(ChkCmbCompany.Properties.GetCheckedItems());
            //    ReportParams_Property.Branch_Code = Val.Trim(ChkCmbBranch.Properties.GetCheckedItems());
            //    ReportParams_Property.Process_Code = Val.Trim(ChkCmbProcess.Properties.GetCheckedItems());
            //    ReportParams_Property.From_Party_Code = Val.Trim(ChkCmbFromParty.Properties.GetCheckedItems());
            //    ReportParams_Property.To_Party_Code = Val.Trim(ChkCmbToParty.Properties.GetCheckedItems());
            //    ReportParams_Property.From_Location_Code = Val.Trim(ChkCmbFromLocation.Properties.GetCheckedItems());
            //    ReportParams_Property.To_Location_Code = Val.Trim(ChkCmbToLocation.Properties.GetCheckedItems());
            //    ReportParams_Property.Rough_Name = Global.GetCommaStr(Val.Trim(ChkCmbRoughName.Properties.GetCheckedItems()));
            //    ReportParams_Property.Rough_Type_Code = Val.Trim(ChkCmbRoughType.Properties.GetCheckedItems());
            //    ReportParams_Property.Shape_Code = Val.Trim(ChkCmbShape.Properties.GetCheckedItems());
            //    ReportParams_Property.Sieve_Code = Val.Trim(ChkCmbSieve.Properties.GetCheckedItems());
            //    ReportParams_Property.Color_Code = Val.Trim(ChkCmbColor.Properties.GetCheckedItems());
            //    ReportParams_Property.MFG_Clarity_Code = Val.Trim(ChkCmbMfgClarity.Properties.GetCheckedItems());
            //    ReportParams_Property.CLV_Clarity_Code = Val.Trim(ChkCmbClvClarity.Properties.GetCheckedItems());
            //    ReportParams_Property.Department_Code = Val.Trim(ChkCmbDeptName.Properties.GetCheckedItems());

            //    ReportParams_Property.From_Issue_Date = Val.DBDate(DTPTranFromDate.Text);
            //    ReportParams_Property.To_Issue_Date = Val.DBDate(DTPTranToDate.Text);
            //    ReportParams_Property.From_Receive_Date = Val.DBDate(DTPAcceptFromDate.Text);
            //    ReportParams_Property.To_Receive_Date = Val.DBDate(DTPAcceptToDate.Text);
            //}

            if (this.backgroundWorker1.IsBusy)
            {

            }
            else
            {
                backgroundWorker1.RunWorkerAsync();
            }

            //if (RptName.ToString() == "Department Transfer")
            //{
            //    DTab = ObjReportParams.Get_MIX_Prepolishing_Stock_Report(ReportParams_Property, "RP_MIX_CLV_DEPT_STOCKSUM");
            //}
            //else if (RptName.ToString() == "Party Transfer")
            //{
            //    DTab = ObjReportParams.Get_MIX_Party_Stock_Report(ReportParams_Property, "RP_CLV_OUTSIDE_LABOURINS");
            //}
            //else if (RptName.ToString() == "Party OutStanding")
            //{
            //    DTab = ObjReportParams.Get_MIX_Prepolishing_OutSide_Stock_Report(ReportParams_Property, "RP_OUTSIDE_OUTSTANDSUM");
            //}
            //else if (RptName.ToString() == "Rough Transfer")
            //{
            //    DTab = ObjReportParams.Get_MIX_Prepolishing_OutSide_Stock_Report(ReportParams_Property, "RP_OUTSIDE_TOTALROUTRN");
            //}

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
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (RptName.ToString() == "Stock Asset")
            {
                DTab = ObjReportParams.Get_Item_Stock_Report(ReportParams_Property, "RP_ITEM_STOCK_DETAIL");
            }
            //else if (RptName.ToString() == "Party Transfer")
            //{
            //    DTab = ObjReportParams.Get_MIX_Party_Stock_Report(ReportParams_Property, "RP_CLV_OUTSIDE_LABOURINS");
            //}
            //else if (RptName.ToString() == "Party OutStanding")
            //{
            //    DTab = ObjReportParams.Get_MIX_Prepolishing_OutSide_Stock_Report(ReportParams_Property, "RP_OUTSIDE_OUTSTANDSUM");
            //}
            //else if (RptName.ToString() == "Rough Transfer")
            //{
            //    DTab = ObjReportParams.Get_MIX_Prepolishing_OutSide_Stock_Report(ReportParams_Property, "RP_OUTSIDE_TOTALROUTRN");
            //}
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            FrmGReportViewer FrmPReportViewer = new Report.FrmGReportViewer();
            FrmPReportViewer.mDTDetail = DTab;
            //FrmPReportViewer.Group_By_Tag = rptMultiSelect1.GetTagValue;
            //FrmPReportViewer.Group_By_Text = rptMultiSelect1.GetTextValue;

            FrmPReportViewer.Report_Type = "Summary";

            FrmPReportViewer.ReportHeaderName = RptName.ToString();
            FrmPReportViewer.DTab = DTab;
            FrmPReportViewer.FilterBy = GetFilterByValue();

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

        private void FrmReportList_Shown(object sender, EventArgs e)
        {
            DataTable mDTDetail = new DataTable();
            mDTDetail.Columns.Add("COLUMN_NAME", typeof(string));
            mDTDetail.Columns.Add("FIELD_NAME", typeof(string));
            if (RptName.ToString() == "Stock Asset")
            {
                mDTDetail.Rows.Add("COMPANY", "COMPANY_NAME");
                mDTDetail.Rows.Add("BRANCH", "BRANCH_NAME");
                mDTDetail.Rows.Add("LOCATION", "LOCATION_NAME");
                mDTDetail.Rows.Add("ITEM", "ITEM_NAME");
                mDTDetail.Rows.Add("ITEM GROUP NAME", "ITEM_GROUP_NAME");
                //mDTDetail.Rows.Add("ROUGH", "ROUGH_NAME");
                //mDTDetail.Rows.Add("BARCODE", "BARCODE");
                //mDTDetail.Rows.Add("ROUGH TYPE", "ROUGH_TYPE_NAME");
                //mDTDetail.Rows.Add("DEPARTMENT", "DEPARTMENT_NAME");
                //mDTDetail.Rows.Add("OTHER SOURCE", "OTHER_SOURCE_NAME");
                //mDTDetail.Rows.Add("PROCESS", "PROCESS_NAME");
                //mDTDetail.Rows.Add("SIEVE", "SIEVE_NAME");
                //mDTDetail.Rows.Add("STOCK DATE", "ENTRY_DATE");
                labelControl23.Text = "Stock Date";
                labelControl24.Visible = false;
                DTPAcceptFromDate.Visible = false;
                DTPAcceptToDate.Visible = false;
                DTPAcceptFromDate.Text = "";
                DTPAcceptToDate.Text = "";
            }
            //else if (RptName.ToString() == "Party Transfer")
            //{
            //    mDTDetail.Rows.Add("PARTY NAME", "PARTY_NAME");
            //    mDTDetail.Rows.Add("RECEIPT PARTY", "TO_PARTY_NAME");
            //    mDTDetail.Rows.Add("PROCESS", "PROCESS_NAME");
            //    mDTDetail.Rows.Add("ROUGH", "ROUGH_NAME");
            //    mDTDetail.Rows.Add("BARCODE", "BARCODE");
            //    mDTDetail.Rows.Add("ROUGH TYPE", "ROUGH_TYPE_NAME");
            //    mDTDetail.Rows.Add("DEPARTMENT", "DEPARTMENT_NAME");
            //    mDTDetail.Rows.Add("SHAPE", "SHAPE_NAME");
            //    mDTDetail.Rows.Add("COLOR", "COLOR_NAME");
            //    mDTDetail.Rows.Add("SIEVE", "SIEVE_NAME");
            //    mDTDetail.Rows.Add("MFG CLARITY", "MFG_CLARITY_NAME");
            //    mDTDetail.Rows.Add("CLV CLARITY", "CLV_CLARITY_NAME");
            //    mDTDetail.Rows.Add("ISSUE DATE", "ISSUE_DATE");
            //    mDTDetail.Rows.Add("RECEIPT DATE", "RECEIPT_DATE");
            //    labelControl23.Text = "Issue Date";
            //    labelControl24.Text = "Receive Date";
            //    labelControl24.Visible = true;
            //    DTPAcceptFromDate.Visible = true;
            //    DTPAcceptToDate.Visible = true;
            //    DTPStockFromDate.Text = "";
            //    DTPStockToDate.Text = "";
            //}
            //else if (RptName.ToString() == "Party OutStanding")
            //{
            //    mDTDetail.Rows.Add("BILL OF ENTRY", "BILL_OF_ENTRY_NO");
            //    mDTDetail.Rows.Add("PARTY_NAME", "PARTY_NAME");
            //    mDTDetail.Rows.Add("ISSUE PARTY", "TO_PARTY_NAME");
            //    mDTDetail.Rows.Add("PROCESS", "PROCESS_NAME");
            //    mDTDetail.Rows.Add("ROUGH", "ROUGH_NAME");
            //    mDTDetail.Rows.Add("ROUGH TYPE", "ROUGH_TYPE_NAME");
            //    mDTDetail.Rows.Add("BARCODE", "BARCODE");
            //    mDTDetail.Rows.Add("SHAPE", "SHAPE_NAME");
            //    mDTDetail.Rows.Add("COLOR", "COLOR_NAME");
            //    mDTDetail.Rows.Add("SIEVE", "SIEVE_NAME");
            //    mDTDetail.Rows.Add("MFG CLARITY", "MFG_CLARITY_NAME");
            //    mDTDetail.Rows.Add("CLV CLARITY", "CLV_CLARITY_NAME");
            //    mDTDetail.Rows.Add("ISS DATE", "ISSUE_DATE");
            //    labelControl23.Text = "Issue Date";
            //    labelControl24.Visible = false;
            //    DTPAcceptFromDate.Visible = false;
            //    DTPAcceptToDate.Visible = false;
            //    DTPStockFromDate.Text = "";
            //    DTPAcceptFromDate.Text = "";
            //    DTPAcceptToDate.Text = "";
            //}
            //else if (RptName.ToString() == "Rough Transfer")
            //{
            //    mDTDetail.Rows.Add("BILL OF ENTRY", "BILL_OF_ENTRY_NO");
            //    mDTDetail.Rows.Add("PARTY_NAME", "PARTY_NAME");
            //    mDTDetail.Rows.Add("ISSUE PARTY", "TO_PARTY_NAME");
            //    mDTDetail.Rows.Add("PROCESS", "PROCESS_NAME");
            //    mDTDetail.Rows.Add("ROUGH", "ROUGH_NAME");
            //    mDTDetail.Rows.Add("ROUGH TYPE", "ROUGH_TYPE_NAME");
            //    mDTDetail.Rows.Add("BARCODE", "BARCODE");
            //    mDTDetail.Rows.Add("SHAPE", "SHAPE_NAME");
            //    mDTDetail.Rows.Add("COLOR", "COLOR_NAME");
            //    mDTDetail.Rows.Add("SIEVE", "SIEVE_NAME");
            //    mDTDetail.Rows.Add("MFG CLARITY", "MFG_CLARITY_NAME");
            //    mDTDetail.Rows.Add("CLV CLARITY", "CLV_CLARITY_NAME");
            //    mDTDetail.Rows.Add("ISS DATE", "ISSUE_DATE");
            //    labelControl23.Text = "Issue Date";
            //    labelControl24.Visible = false;
            //    DTPAcceptFromDate.Visible = false;
            //    DTPAcceptToDate.Visible = false;
            //    DTPStockFromDate.Text = "";
            //    DTPAcceptFromDate.Text = "";
            //    DTPAcceptToDate.Text = "";
            //}
            rptMultiSelect1.DTab = mDTDetail;
        }

        private string GetFilterByValue()
        {
            string Str = "Filter By : ";

            // if (Val.Trim(ChkCmbProcess.Properties.GetCheckedItems()) != "0") Str += "Process : " + ChkCmbProcess.Text + " , ";
            if (DTPStockFromDate.Text != "")
            {
                Str += "From Issue Date : " + DTPStockFromDate.Text;
            }
            if (DTPStockToDate.Text != "")
            {
                Str += " & As On Date : " + DTPStockToDate.Text;
            }
            //if (DTPAcceptFromDate.Text != "")
            //{
            //    Str += "From Receive Date : " + DTPAcceptFromDate.Text;
            //}
            //if (DTPAcceptToDate.Text != "")
            //{
            //    Str += " & As On Date : " + DTPAcceptToDate.Text;
            //}
            return Str;
        }
    }
}
