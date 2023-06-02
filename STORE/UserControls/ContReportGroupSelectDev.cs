using System;
using System.Data;
using System.Linq;
//using DevExpress.DXCore.Controls.XtraEditors.Controls;

namespace STORE.UserControls
{
    public partial class ContReportGroupSelectDev : DevExpress.XtraEditors.XtraUserControl
    {
        public ContReportGroupSelectDev()
        {
            InitializeComponent();
        }

        private DataTable _DTab = new DataTable();

        public DataTable DTab
        {
            get { return _DTab; }
            set
            {
                _DTab = value;

                if (_DTab == null)
                {
                    return;
                }

                ListTo.Items.Clear();
                ListFrom.Items.Clear();

                if (DTab == null)
                {
                    return;
                }
                else
                {
                    //-----------Hiren-------------Start

                    //DTab = DTab.AsEnumerable().Where(m => m.Field<decimal?>("IS_GROUP").ToString() == "1").CopyToDataTable();

                    //-----------Hiren-------------End
                }

                //-----------Hiren-------------Start
                if (DTab.AsEnumerable().Where(m => m.Field<decimal?>("IS_GROUP") == 1).Count() > 0)
                {
                    ListFrom.DisplayMember = "COLUMN_NAME";
                    ListFrom.ValueMember = "FIELD_NAME";
                    ListFrom.DataSource = DTab.AsEnumerable().Where(m => m.Field<decimal?>("IS_GROUP").ToString() == "1").CopyToDataTable();

                    ListTo.DisplayMember = "COLUMN_NAME";
                    ListTo.ValueMember = "FIELD_NAME";
                    ListTo.DataSource = DTab.Clone();
                }
                else
                {
                    ListFrom.DisplayMember = "COLUMN_NAME";
                    ListFrom.ValueMember = "FIELD_NAME";
                    ListFrom.DataSource = null;

                    ListTo.DisplayMember = "COLUMN_NAME";
                    ListTo.ValueMember = "FIELD_NAME";
                    ListTo.DataSource = null;
                }
                //-----------Hiren-------------End
            }
        }

        public void RemoveRows(string StrItem) // Add By Khushbu 17/11/2014
        {
            DataTable dt_From = (DataTable)ListFrom.DataSource;

            int row_count = ListFrom.SelectedItems.Count - 1;

            for (int i = row_count; i >= 0; i--)
            {
                if (dt_From.Rows[i]["FIELD_NAME"].ToString().Contains(StrItem))
                {
                    DataRow dr_New = dt_From.NewRow();
                    dr_New["FIELD_NAME"] = StrItem;
                    dt_From.Rows.Add(dr_New);
                }
            }
        }

        public string GetTextValue
        {
            get
            {
                DataTable dt_ListTo = ((DataTable)ListTo.DataSource);
                string StrValue = "";
                if (dt_ListTo != null)
                {
                    foreach (DataRow Item in dt_ListTo.Rows)
                    {
                        StrValue = StrValue + Item["COLUMN_NAME"].ToString() + ",";
                    }
                }
                if (StrValue != "")
                {
                    StrValue = StrValue.Substring(0, StrValue.Length - 1);
                }
                return StrValue;
            }
        }

        public string GetTagValue
        {
            get
            {
                DataTable dt_ListTo = ((DataTable)ListTo.DataSource);
                string StrValue = "";
                if (dt_ListTo != null)
                {
                    foreach (DataRow Item in dt_ListTo.Rows)
                    {
                        StrValue = StrValue + Item["FIELD_NAME"].ToString() + ",";
                    }
                }
                if (StrValue != "")
                {
                    StrValue = StrValue.Substring(0, StrValue.Length - 1);
                }
                return StrValue;
            }
        }


        private string _Default;

        public string Default
        {
            get { return _Default; }
            set
            {
                _Default = value;
                if (Default == null || Default == "")
                {
                    return;
                }
                DataTable dt_ListFrom = (DataTable)ListFrom.DataSource;
                string[] StrSplit = Default.Split(',');

                int cnt = 0;

                if (dt_ListFrom != null)
                {
                    if (dt_ListFrom.Rows.Count > 0)
                    {
                        for (int IntI = 0; IntI < StrSplit.Length; IntI++)
                        {
                            cnt = 0;

                            foreach (DataRow iTem in dt_ListFrom.Rows)
                            {
                                if (iTem["FIELD_NAME"].ToString().ToUpper() == StrSplit[IntI].ToUpper() || iTem["COLUMN_NAME"].ToString().ToUpper() == StrSplit[IntI].ToUpper())
                                {
                                    ListFrom.SelectedIndex = cnt;
                                    MoveRight_Click(null, null);
                                    break;
                                }
                                cnt++;
                            }
                        }
                    }
                }

            }
        }

        private void MoveRight_Click(object sender, EventArgs e)
        {

            DataTable dt_From = (DataTable)ListFrom.DataSource;

            int row_count = ListFrom.SelectedItems.Count - 1;

            for (int i = row_count; i >= 0; i--)
            {
                bool ISExists = false;

                foreach (DataRowView iTem2 in ListTo.Items)
                {
                    if (((DataRowView)ListFrom.SelectedItems[i]).Row["FIELD_NAME"] == iTem2.Row["FIELD_NAME"])
                    {
                        ISExists = true;
                        break;
                    }
                }
                if (ISExists == false)
                {
                    ((DataTable)ListTo.DataSource).Rows.Add(((DataRowView)ListFrom.SelectedItems[i]).Row.ItemArray);
                    (dt_From).Rows.Remove(((DataRowView)ListFrom.SelectedItems[i]).Row);
                }
            }

            ListFrom.DataSource = dt_From;
            ListFrom.Refresh();
        }

        private void MoveLeft_Click(object sender, EventArgs e)
        {

            DataTable dt_To = (DataTable)ListTo.DataSource;

            int row_count = ListTo.SelectedItems.Count - 1;

            for (int i = row_count; i >= 0; i--)
            {
                bool ISExists = false;

                foreach (DataRowView iTem2 in ListFrom.Items)
                {
                    if (((DataRowView)ListTo.SelectedItems[i]).Row["FIELD_NAME"] == iTem2.Row["FIELD_NAME"])
                    {
                        ISExists = true;
                        break;
                    }
                }
                if (ISExists == false)
                {
                    ((DataTable)ListFrom.DataSource).Rows.Add(((DataRowView)ListTo.SelectedItems[i]).Row.ItemArray);
                    (dt_To).Rows.Remove(((DataRowView)ListTo.SelectedItems[i]).Row);
                }
            }

            ListTo.DataSource = dt_To;
            ListTo.Refresh();
        }

        private void MoveUp_Click(object sender, EventArgs e)
        {
            DataTable dt_To = (DataTable)ListTo.DataSource;

            if (ListTo.SelectedItems.Count == 0)
            {
                return;
            }
            var currentIndex = ListTo.SelectedIndex;
            DataRow dr_Sel = dt_To.NewRow();

            dr_Sel.ItemArray = dt_To.Rows[currentIndex].ItemArray;

            if (currentIndex > 0)
            {
                dt_To.Rows.InsertAt(dr_Sel, currentIndex - 1);
                dt_To.Rows.RemoveAt(currentIndex + 1);
                ListTo.SelectedIndex = currentIndex - 1;
            }
            else if (currentIndex == 0)
            {
                dt_To.Rows.InsertAt(dr_Sel, dt_To.Rows.Count + 1);
                dt_To.Rows.RemoveAt(currentIndex);
                ListTo.SelectedIndex = dt_To.Rows.Count;
            }
        }

        private void MoveDown_Click(object sender, EventArgs e)
        {
            DataTable dt_To = (DataTable)ListTo.DataSource;

            if (ListTo.SelectedItems.Count == 0)
            {
                return;
            }
            var currentIndex = ListTo.SelectedIndex;
            DataRow dr_Sel = dt_To.NewRow();

            dr_Sel.ItemArray = dt_To.Rows[currentIndex].ItemArray;

            if (currentIndex < dt_To.Rows.Count - 1)
            {
                dt_To.Rows.InsertAt(dr_Sel, currentIndex + 2);
                dt_To.Rows.RemoveAt(currentIndex);
                ListTo.SelectedIndex = currentIndex + 1;
            }
            else if (currentIndex == dt_To.Rows.Count - 1)
            {
                dt_To.Rows.InsertAt(dr_Sel, 0);
                dt_To.Rows.RemoveAt(currentIndex + 1);
                ListTo.SelectedIndex = 0;
            }
        }

        private void ListFrom_DoubleClick(object sender, EventArgs e)
        {
            MoveRight_Click(sender, null);
        }

        private void ListTo_DoubleClick(object sender, EventArgs e)
        {
            MoveLeft_Click(sender, null);
        }
    }
}
