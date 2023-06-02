using BLL.PropertyClasses.Transaction;
using DLL;
using System;
using System.Collections;
using System.Data;

namespace BLL.FunctionClasses.Transaction
{
    public class Invoice_Entry
    {
        InterfaceLayer Ope = new InterfaceLayer();
        Validation Val = new Validation();

        #region Other Function

        public string ISExists(string Invoice_No, Int64 LblMode)
        {
            Validation Val = new Validation();
            return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "Item_Sales_Master", "Inovice_No", "AND Inovice_No = '" + Invoice_No + "' AND NOT ItemSaleMasterID =" + LblMode));
        }

        public string ISExists_New(string Invoice_No, Int64 LblMode)
        {
            Validation Val = new Validation();
            return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "Item_Material_Sales_Master", "Inovice_No", "AND Inovice_No = '" + Invoice_No + "' AND NOT ItemSaleMasterID =" + LblMode));
        }

        public int FindNewID(string Form_Type)
        {
            int IntRes = 0;
            if (Form_Type == "P")
            {
                IntRes = Ope.FindNewID(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "Item_Purchase_Master", "isnull(MAX(ItemPurchaseMasterID),0)", "");
            }
            else if (Form_Type == "PR")
            {
                IntRes = Ope.FindNewID(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "Item_Purchase_ReturnMaster", "isnull(MAX(ItemPurchaseRtnMasterID),0)", "");
            }
            else if (Form_Type == "S")
            {
                IntRes = Ope.FindNewID(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "Item_Sales_Master", "isnull(MAX(ItemSaleMasterID),0)", "");
            }
            else if (Form_Type == "SR")
            {
                IntRes = Ope.FindNewID(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "Item_Sales_Return_Master", "isnull(MAX(ItemSaleReturnMasterID),0)", "");
            }
            return IntRes;
        }

        public string FindNewInvoiceNo(string Form_Type)
        {
            string IntRes = string.Empty;
            if (Form_Type == "PR")
            {
                IntRes = Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "Item_Purchase_ReturnMaster", "isnull('PR' + CAST(SUBSTRING(max(Inovice_No), 3, 3) +1 AS varchar(50)),'PR1')", "AND Company_Code = '" + GlobalDec.gEmployeeProperty.Company_Code + "' AND Location_Code = '" + GlobalDec.gEmployeeProperty.Location_Code + "' AND Branch_Code = '" + GlobalDec.gEmployeeProperty.Branch_Code + "' AND FinancialYear = '" + GlobalDec.gEmployeeProperty.gFinancialYear + "' ");
            }
            else if (Form_Type == "S")
            {
                IntRes = Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "Item_Sales_Master", "isnull('S' + CAST(SUBSTRING(max(Inovice_No), 2, 1) +1 AS varchar(50)),'S1')", "AND Company_Code = '" + GlobalDec.gEmployeeProperty.Company_Code + "' AND Location_Code = '" + GlobalDec.gEmployeeProperty.Location_Code + "' AND Branch_Code = '" + GlobalDec.gEmployeeProperty.Branch_Code + "' AND FinancialYear = '" + GlobalDec.gEmployeeProperty.gFinancialYear + "' ");
            }
            return IntRes;
        }

        public int Save(Item_Purchase pClsProperty)
        {
            Request Request = new Request();
            Request.AddParams("@ItemPurchaseID", pClsProperty.ItemPurchaseID, DbType.Decimal, ParameterDirection.Input);
            Request.AddParams("@ItemPurchaseMasterID", pClsProperty.ItemPurchaseMasterID, DbType.Decimal, ParameterDirection.Input);
            Request.AddParams("@ItemName", pClsProperty.ItemName, DbType.Int32, ParameterDirection.Input);
            Request.AddParams("@Unit", pClsProperty.Unit, DbType.Date, ParameterDirection.Input);
            Request.AddParams("@Quantity", pClsProperty.Quantity, DbType.String, ParameterDirection.Input);
            Request.AddParams("@Price", pClsProperty.Price, DbType.String, ParameterDirection.Input);
            Request.AddParams("@Amount", pClsProperty.Amount, DbType.String, ParameterDirection.Input);
            Request.AddParams("@Vat", pClsProperty.Vat, DbType.String, ParameterDirection.Input);
            Request.AddParams("@AddVat", pClsProperty.AddVat, DbType.String, ParameterDirection.Input);
            Request.AddParams("@Discount", pClsProperty.Discount, DbType.String, ParameterDirection.Input);
            Request.AddParams("@AddAmount", pClsProperty.AddAmount, DbType.String, ParameterDirection.Input);
            Request.AddParams("@LessAmount", pClsProperty.LessAmount, DbType.String, ParameterDirection.Input);
            Request.AddParams("@NetAmount", pClsProperty.NetAmount, DbType.Decimal, ParameterDirection.Input);

            Request.CommandText = "Item_Purchase_Save";
            Request.CommandType = CommandType.StoredProcedure;
            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }

        public Invoice_EntryProperty SaveInvoiceEntryMaster(Invoice_EntryProperty pClsProperty, string Form_Type)
        {
            Request Request = new Request();

            Request.AddParams("@ItemPurchaseMasterID", pClsProperty.Purchase_Master_ID, DbType.Int64, ParameterDirection.Input);
            Request.AddParams("@Company_Code", GlobalDec.gEmployeeProperty.Company_Code, DbType.Int64, ParameterDirection.Input);
            Request.AddParams("@Branch_Code", GlobalDec.gEmployeeProperty.Branch_Code, DbType.Int64, ParameterDirection.Input);
            Request.AddParams("@Location_Code", GlobalDec.gEmployeeProperty.Location_Code, DbType.Int64, ParameterDirection.Input);
            Request.AddParams("@Ledger_Code", pClsProperty.From_Party_Code, DbType.Int64, ParameterDirection.Input);
            Request.AddParams("@Invoice_Date", pClsProperty.Invoice_Date, DbType.Date, ParameterDirection.Input);
            Request.AddParams("@Inovice_No", pClsProperty.Invoice_No, DbType.String, ParameterDirection.Input);
            Request.AddParams("@FinancialYear", pClsProperty.Financial_Year, DbType.String, ParameterDirection.Input);
            Request.AddParams("@Payment_Type", pClsProperty.Payment_Mode, DbType.String, ParameterDirection.Input);
            Request.AddParams("@Terms", pClsProperty.Payment_Days, DbType.String, ParameterDirection.Input);
            Request.AddParams("@Term_Date", pClsProperty.Payment_Date, DbType.Date, ParameterDirection.Input);
            Request.AddParams("@Trans_Date", pClsProperty.Invoice_Date, DbType.Date, ParameterDirection.Input);

            Request.AddParams("@SGST_Rate", pClsProperty.SGST, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@SGST_Amt", pClsProperty.SGST_Amt, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@CGST_Rate", pClsProperty.CGST, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@CGST_Amt", pClsProperty.CGST_Amt, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@IGST_Rate", pClsProperty.IGST, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@IGST_Amt", pClsProperty.IGST_Amt, DbType.Double, ParameterDirection.Input);

            Request.AddParams("@Discount_Per", pClsProperty.Discount_Per, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@Discount", pClsProperty.Discount, DbType.Double, ParameterDirection.Input);

            Request.AddParams("@Remark", pClsProperty.Remark, DbType.String, ParameterDirection.Input);
            Request.AddParams("@Is_Reverse", pClsProperty.IS_Reverse, DbType.Int64, ParameterDirection.Input);
            Request.AddParams("@Gross_Amt", pClsProperty.Gross_Amt, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@Add_Amt", pClsProperty.Add_Amt, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@Less_Amt", pClsProperty.Less_Amt, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@Net_Amt", pClsProperty.Net_Amt, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@Total_Amt", pClsProperty.Total_Amt, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@Challan_No", pClsProperty.Challan_No, DbType.String, ParameterDirection.Input);


            if (Form_Type == "P")
            {
                Request.CommandText = "Purchase_Master_Save";
                Request.CommandType = CommandType.StoredProcedure;

                DataTable DTAB = new DataTable();
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTAB, Request);

                if (DTAB != null)
                {
                    if (DTAB.Rows.Count > 0)
                    {
                        pClsProperty.Purchase_Master_ID = Convert.ToInt64(DTAB.Rows[0][0]);
                    }
                }
                else
                {
                    pClsProperty.Purchase_Master_ID = 0;
                }
            }
            else if (Form_Type == "PR")
            {
                Request.CommandText = "Purchase_ReturnMaster_Save";
                Request.CommandType = CommandType.StoredProcedure;

                DataTable DTAB = new DataTable();
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTAB, Request);

                if (DTAB != null)
                {
                    if (DTAB.Rows.Count > 0)
                    {
                        pClsProperty.Purchase_Master_ID = Convert.ToInt64(DTAB.Rows[0][0]);
                    }
                }
                else
                {
                    pClsProperty.Purchase_Master_ID = 0;
                }
            }
            else if (Form_Type == "S")
            {
                Request.AddParams("@ShipTo_Ledger_Code", pClsProperty.BillTo_Party_Code, DbType.String, ParameterDirection.Input);
                Request.CommandText = "Sales_Master_Save";
                Request.CommandType = CommandType.StoredProcedure;

                DataTable DTAB = new DataTable();
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTAB, Request);

                if (DTAB != null)
                {
                    if (DTAB.Rows.Count > 0)
                    {
                        pClsProperty.Purchase_Master_ID = Convert.ToInt64(DTAB.Rows[0][0]);
                    }
                }
                else
                {
                    pClsProperty.Purchase_Master_ID = 0;
                }
            }
            else if (Form_Type == "SR")
            {
                Request.CommandText = "Sales_ReturnMaster_Save";
                Request.CommandType = CommandType.StoredProcedure;

                DataTable DTAB = new DataTable();
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTAB, Request);

                if (DTAB != null)
                {
                    if (DTAB.Rows.Count > 0)
                    {
                        pClsProperty.Purchase_Master_ID = Convert.ToInt64(DTAB.Rows[0][0]);
                    }
                }
                else
                {
                    pClsProperty.Purchase_Master_ID = 0;
                }
            }
            else if (Form_Type == "SE")
            {
                Request.AddParams("@Is_Paid", pClsProperty.Is_Paid, DbType.Boolean, ParameterDirection.Input);
                Request.AddParams("@ShipTo_Ledger_Code", pClsProperty.BillTo_Party_Code, DbType.String, ParameterDirection.Input);
                Request.AddParams("@Invoice_No_Checked", pClsProperty.Invoice_No_Checked, DbType.Boolean, ParameterDirection.Input);
                Request.CommandText = "Sales_Entry_Save";
                Request.CommandType = CommandType.StoredProcedure;

                DataTable DTAB = new DataTable();
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTAB, Request);

                if (DTAB != null)
                {
                    if (DTAB.Rows.Count > 0)
                    {
                        pClsProperty.Purchase_Master_ID = Convert.ToInt64(DTAB.Rows[0][0]);
                    }
                }
                else
                {
                    pClsProperty.Purchase_Master_ID = 0;
                }
            }
            else if (Form_Type == "MSE")
            {
                Request.AddParams("@Is_Paid", pClsProperty.Is_Paid, DbType.Boolean, ParameterDirection.Input);
                Request.AddParams("@ShipTo_Ledger_Code", pClsProperty.BillTo_Party_Code, DbType.String, ParameterDirection.Input);
                Request.CommandText = "Material_Sales_Entry_Save";
                Request.CommandType = CommandType.StoredProcedure;

                DataTable DTAB = new DataTable();
                Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTAB, Request);

                if (DTAB != null)
                {
                    if (DTAB.Rows.Count > 0)
                    {
                        pClsProperty.Purchase_Master_ID = Convert.ToInt64(DTAB.Rows[0][0]);
                    }
                }
                else
                {
                    pClsProperty.Purchase_Master_ID = 0;
                }
            }

            return pClsProperty;
        }

        public Request SaveItemPurchaseDetail(Invoice_EntryProperty pClsProperty)
        {
            Request Request = new Request();
            Request.AddParams("@ItemPurchaseDetailID", pClsProperty.ItemPurchaseDetailID, DbType.Int64, ParameterDirection.Input);
            Request.AddParams("@ItemPurchaseMasterID", pClsProperty.ItemPurchaseMasterID, DbType.Int64, ParameterDirection.Input);
            Request.AddParams("@Item_Code", pClsProperty.Item_Code, DbType.Int64, ParameterDirection.Input);
            Request.AddParams("@HSN_ID", pClsProperty.HSN_ID, DbType.Int64, ParameterDirection.Input);
            Request.AddParams("@Unit_Name", pClsProperty.Unit_Type, DbType.String, ParameterDirection.Input);
            Request.AddParams("@Quantity", pClsProperty.Quantity, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@Rate", pClsProperty.Rate_Dollar, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@Gross_Amt", pClsProperty.Gross_Amt, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@Discount", pClsProperty.Discount_Per, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@SGST_Rate", pClsProperty.SGST, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@SGST_Amt", pClsProperty.SGST_Amt, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@CGST_Rate", pClsProperty.CGST, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@CGST_Amt", pClsProperty.CGST_Amt, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@IGST_Rate", pClsProperty.IGST, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@IGST_Amt", pClsProperty.IGST_Amt, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@NetAmount", pClsProperty.Net_Amt, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@Remarks", pClsProperty.Remark, DbType.String, ParameterDirection.Input);

            return Request;
        }

        public Request SaveItemPurchaseReturnDetail(Invoice_EntryProperty pClsProperty)
        {
            Request Request = new Request();
            Request.AddParams("@ItemPurchaseReturnDtlID", pClsProperty.ItemPurchaseReturnDtlID, DbType.Int64, ParameterDirection.Input);
            Request.AddParams("@ItemPurchaseRtnMasterID", pClsProperty.ItemPurchaseRtnMasterID, DbType.Int64, ParameterDirection.Input);
            Request.AddParams("@Item_Code", pClsProperty.Item_Code, DbType.Int64, ParameterDirection.Input);
            Request.AddParams("@HSN_ID", pClsProperty.HSN_ID, DbType.Int64, ParameterDirection.Input);
            Request.AddParams("@Unit_Name", pClsProperty.Unit_Type, DbType.String, ParameterDirection.Input);
            Request.AddParams("@Quantity", pClsProperty.Quantity, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@Rate", pClsProperty.Rate_Dollar, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@Gross_Amt", pClsProperty.Gross_Amt, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@Discount", pClsProperty.Discount_Per, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@SGST_Rate", pClsProperty.SGST, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@SGST_Amt", pClsProperty.SGST_Amt, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@CGST_Rate", pClsProperty.CGST, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@CGST_Amt", pClsProperty.CGST_Amt, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@IGST_Rate", pClsProperty.IGST, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@IGST_Amt", pClsProperty.IGST_Amt, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@NetAmount", pClsProperty.Net_Amt, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@Remarks", pClsProperty.Remark, DbType.String, ParameterDirection.Input);

            return Request;
        }

        public Request SaveItemSalesDetail(Invoice_EntryProperty pClsProperty)
        {
            Request Request = new Request();
            Request.AddParams("@ItemSaleDetailID", pClsProperty.ItemSaleDetailID, DbType.Int64, ParameterDirection.Input);
            Request.AddParams("@ItemSaleMasterID", pClsProperty.ItemSaleMasterID, DbType.Int64, ParameterDirection.Input);
            Request.AddParams("@Item_Code", pClsProperty.Item_Code, DbType.Int64, ParameterDirection.Input);
            Request.AddParams("@HSN_ID", pClsProperty.HSN_ID, DbType.Int64, ParameterDirection.Input);
            Request.AddParams("@Unit_Name", pClsProperty.Unit_Type, DbType.String, ParameterDirection.Input);
            Request.AddParams("@Quantity", pClsProperty.Quantity, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@Rate", pClsProperty.Rate_Dollar, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@Gross_Amt", pClsProperty.Gross_Amt, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@Discount", pClsProperty.Discount_Per, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@SGST_Rate", pClsProperty.SGST, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@SGST_Amt", pClsProperty.SGST_Amt, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@CGST_Rate", pClsProperty.CGST, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@CGST_Amt", pClsProperty.CGST_Amt, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@IGST_Rate", pClsProperty.IGST, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@IGST_Amt", pClsProperty.IGST_Amt, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@NetAmount", pClsProperty.Net_Amt, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@Remarks", pClsProperty.Remark, DbType.String, ParameterDirection.Input);

            return Request;
        }

        public Request SaveItemSalesDetailEmb(Invoice_EntryProperty pClsProperty)
        {
            Request Request = new Request();
            Request.AddParams("@ItemSaleDetailID", pClsProperty.ItemSaleDetailID, DbType.Int64, ParameterDirection.Input);
            Request.AddParams("@ItemSaleMasterID", pClsProperty.ItemSaleMasterID, DbType.Int64, ParameterDirection.Input);
            Request.AddParams("@Item_Code", pClsProperty.Item_Code, DbType.Int64, ParameterDirection.Input);
            Request.AddParams("@HSN_ID", pClsProperty.HSN_ID, DbType.Int64, ParameterDirection.Input);
            Request.AddParams("@Unit_Name", pClsProperty.Unit_Type, DbType.String, ParameterDirection.Input);
            Request.AddParams("@MTR", pClsProperty.MTR, DbType.Decimal, ParameterDirection.Input);
            Request.AddParams("@Line", pClsProperty.Line, DbType.Int32, ParameterDirection.Input);
            Request.AddParams("@Taka", pClsProperty.Taka, DbType.Int32, ParameterDirection.Input);
            Request.AddParams("@Cut", pClsProperty.Cut, DbType.Decimal, ParameterDirection.Input);
            Request.AddParams("@Quantity", pClsProperty.Quantity, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@Rate", pClsProperty.Rate_Dollar, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@Gross_Amt", pClsProperty.Gross_Amt, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@Discount", pClsProperty.Discount, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@Discount_Per", pClsProperty.Discount_Per, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@SGST_Rate", pClsProperty.SGST, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@SGST_Amt", pClsProperty.SGST_Amt, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@CGST_Rate", pClsProperty.CGST, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@CGST_Amt", pClsProperty.CGST_Amt, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@IGST_Rate", pClsProperty.IGST, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@IGST_Amt", pClsProperty.IGST_Amt, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@NetAmount", pClsProperty.Net_Amt, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@Buta", pClsProperty.Buta, DbType.Int32, ParameterDirection.Input);
            Request.AddParams("@Buta_Rate", pClsProperty.Buta_Rate, DbType.Int32, ParameterDirection.Input);

            return Request;
        }

        public Request SaveItemMaterialSalesDetailEmb(Invoice_EntryProperty pClsProperty)
        {
            Request Request = new Request();
            Request.AddParams("@ItemSaleDetailID", pClsProperty.ItemSaleDetailID, DbType.Int64, ParameterDirection.Input);
            Request.AddParams("@ItemSaleMasterID", pClsProperty.ItemSaleMasterID, DbType.Int64, ParameterDirection.Input);
            Request.AddParams("@Item_Code", pClsProperty.Item_Code, DbType.Int64, ParameterDirection.Input);
            Request.AddParams("@HSN_ID", pClsProperty.HSN_ID, DbType.Int64, ParameterDirection.Input);
            Request.AddParams("@Unit_Name", pClsProperty.Unit_Type, DbType.String, ParameterDirection.Input);
            Request.AddParams("@MTR", pClsProperty.MTR, DbType.Decimal, ParameterDirection.Input);
            Request.AddParams("@Weight", pClsProperty.Weight, DbType.Decimal, ParameterDirection.Input);
            Request.AddParams("@Quantity", pClsProperty.Quantity, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@Rate", pClsProperty.Rate, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@Gross_Amt", pClsProperty.Gross_Amt, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@Discount", pClsProperty.Discount, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@Discount_Per", pClsProperty.Discount_Per, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@SGST_Rate", pClsProperty.SGST, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@SGST_Amt", pClsProperty.SGST_Amt, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@CGST_Rate", pClsProperty.CGST, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@CGST_Amt", pClsProperty.CGST_Amt, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@IGST_Rate", pClsProperty.IGST, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@IGST_Amt", pClsProperty.IGST_Amt, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@NetAmount", pClsProperty.Net_Amt, DbType.Double, ParameterDirection.Input);

            return Request;
        }


        public Request SaveItemSalesReturnDetail(Invoice_EntryProperty pClsProperty)
        {
            Request Request = new Request();
            Request.AddParams("@ItemSaleReturnDtlID", pClsProperty.ItemSaleReturnDtlID, DbType.Int64, ParameterDirection.Input);
            Request.AddParams("@ItemSaleReturnMasterID", pClsProperty.ItemSaleReturnMasterID, DbType.Int64, ParameterDirection.Input);
            Request.AddParams("@Item_Code", pClsProperty.Item_Code, DbType.Int64, ParameterDirection.Input);
            Request.AddParams("@HSN_ID", pClsProperty.HSN_ID, DbType.Int64, ParameterDirection.Input);
            Request.AddParams("@Unit_Name", pClsProperty.Unit_Type, DbType.String, ParameterDirection.Input);
            Request.AddParams("@Quantity", pClsProperty.Quantity, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@Rate", pClsProperty.Rate_Dollar, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@Gross_Amt", pClsProperty.Gross_Amt, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@Discount", pClsProperty.Discount_Per, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@SGST_Rate", pClsProperty.SGST, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@SGST_Amt", pClsProperty.SGST_Amt, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@CGST_Rate", pClsProperty.CGST, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@CGST_Amt", pClsProperty.CGST_Amt, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@IGST_Rate", pClsProperty.IGST, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@IGST_Amt", pClsProperty.IGST_Amt, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@NetAmount", pClsProperty.Net_Amt, DbType.Double, ParameterDirection.Input);
            Request.AddParams("@Remarks", pClsProperty.Remark, DbType.String, ParameterDirection.Input);

            return Request;
        }


        public int SavePurchaseDetail(ArrayList AL, string Form_Type)
        {
            int IntRes = 0;
            Request Request = new Request();
            if (Form_Type == "P")
            {
                foreach (Invoice_EntryProperty Obj in AL)
                {
                    Request = SaveItemPurchaseDetail(Obj);
                    Request.CommandText = "Purchase_Detail_SAVE";
                    Request.CommandType = CommandType.StoredProcedure;
                    IntRes += Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
                }
            }
            else if (Form_Type == "PR")
            {
                foreach (Invoice_EntryProperty Obj in AL)
                {
                    Request = SaveItemPurchaseReturnDetail(Obj);
                    Request.CommandText = "Purchase_ReturnDetail_SAVE";
                    Request.CommandType = CommandType.StoredProcedure;
                    IntRes += Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
                }
            }
            else if (Form_Type == "S")
            {
                foreach (Invoice_EntryProperty Obj in AL)
                {
                    Request = SaveItemSalesDetail(Obj);
                    Request.CommandText = "Sales_Detail_SAVE";
                    Request.CommandType = CommandType.StoredProcedure;
                    IntRes += Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
                }
            }
            else if (Form_Type == "SR")
            {
                foreach (Invoice_EntryProperty Obj in AL)
                {
                    Request = SaveItemSalesReturnDetail(Obj);
                    Request.CommandText = "Sales_ReturnDetail_SAVE";
                    Request.CommandType = CommandType.StoredProcedure;
                    IntRes += Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
                }
            }
            else if (Form_Type == "SE")
            {
                foreach (Invoice_EntryProperty Obj in AL)
                {
                    Request = SaveItemSalesDetailEmb(Obj);
                    Request.CommandText = "Sales_Entry_Detail_SAVE";
                    Request.CommandType = CommandType.StoredProcedure;
                    IntRes += Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
                }
            }
            else if (Form_Type == "MSE")
            {
                foreach (Invoice_EntryProperty Obj in AL)
                {
                    Request = SaveItemMaterialSalesDetailEmb(Obj);
                    Request.CommandText = "Material_Sales_Entry_Detail_SAVE";
                    Request.CommandType = CommandType.StoredProcedure;
                    IntRes += Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
                }
            }
            return IntRes;
        }

        public DataTable GetPrintData(Invoice_EntryProperty Property)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.AddParams("@INVOICE_DATE", Property.Invoice_Date, DbType.Date, ParameterDirection.Input);
            Request.AddParams("@INVOICE_NO", Property.Invoice_No, DbType.String, ParameterDirection.Input);
            Request.AddParams("@TRN_ID", Property.Trn_Id, DbType.Int64, ParameterDirection.Input);
            Request.AddParams("@TRN_TYPE", Property.Type, DbType.String, ParameterDirection.Input);

            Request.CommandText = "Acc_purchase_entry_print";
            Request.CommandType = CommandType.StoredProcedure;

            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request, "");

            return DTab;
        }

        public DataTable GetData(Invoice_EntryProperty Property, string Form_Type)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.AddParams("@From_Type", Form_Type, DbType.String, ParameterDirection.Input);
            Request.AddParams("@From_Date", Property.From_Date, DbType.String, ParameterDirection.Input);
            Request.AddParams("@To_Date", Property.To_Date, DbType.String, ParameterDirection.Input);

            Request.CommandText = "Item_Purchase_Master_GetData";
            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request, "");
            return DTab;
        }

        public DataTable GetData_New(Invoice_EntryProperty Property)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.AddParams("@Invoice_No", Property.Invoice_No, DbType.String, ParameterDirection.Input);

            Request.CommandText = "Item_Sale_Master_GetData";
            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request, "");
            return DTab;
        }
        public DataTable GetData_Material(Invoice_EntryProperty Property)
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.AddParams("@Invoice_No", Property.Invoice_No, DbType.String, ParameterDirection.Input);

            Request.CommandText = "Item_Material_Sale_Master_GetData";
            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request, "");
            return DTab;
        }

        public DataTable GetPurchaseDetail(string Form_Type, Int64 pIntItemCode = 0)
        {
            Request Request = new Request();
            DataTable DTab = new DataTable();
            Request.AddParams("@From_Type", Form_Type, DbType.String, ParameterDirection.Input);
            Request.AddParams("@Master_ID", pIntItemCode, DbType.Int64, ParameterDirection.Input);

            Request.CommandText = "Item_Purchase_Detail_GetData";

            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request, "");
            return DTab;
        }

        public int DeleteInvoiceEntryMaster(Invoice_EntryProperty pClsProperty, string Form_Type)
        {
            Request Request = new Request();
            Request.AddParams("@ItemPurchaseMasterID", pClsProperty.Purchase_Master_ID, DbType.Int64, ParameterDirection.Input);
            Request.AddParams("@From_Type", Form_Type, DbType.String, ParameterDirection.Input);
            Request.CommandText = "Item_Delete_GetData";
            Request.CommandType = CommandType.StoredProcedure;
            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }

        public int DeletePurchaseDetail(Invoice_EntryProperty pClsProperty, string Form_Type)
        {
            int IntRes = 0;
            Request Request = new Request();

            Request.AddParams("@ItemPurchaseMasterID", pClsProperty.ItemPurchaseMasterID, DbType.Int64, ParameterDirection.Input);
            Request.AddParams("@ItemPurchaseDetailID", pClsProperty.ItemPurchaseDetailID, DbType.Int64, ParameterDirection.Input);
            Request.AddParams("@From_Type", Form_Type, DbType.String, ParameterDirection.Input);

            Request.CommandText = "Purchase_ReturnDetail_Delete";
            Request.CommandType = CommandType.StoredProcedure;
            IntRes += Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);

            return IntRes;
        }

        public bool CheckValidInvoiceNo(string Form_Type, string num)
        {
            string IntRes = "";
            if (Form_Type == "P")
            {
                IntRes = Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "Item_Purchase_Master", "ItemPurchaseMasterID", "AND ItemPurchaseMasterID = '" + num + "'");
            }
            else if (Form_Type == "PR")
            {
                IntRes = Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "Item_Purchase_ReturnMaster", "ItemPurchaseRtnMasterID", "AND ItemPurchaseRtnMasterID = '" + num + "'");
            }
            else if (Form_Type == "S")
            {
                IntRes = Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "Item_Sales_Master", "Inovice_No", "AND Inovice_No = '" + num + "' AND Company_Code = '" + GlobalDec.gEmployeeProperty.Company_Code + "' AND Location_Code = '" + GlobalDec.gEmployeeProperty.Location_Code + "' AND Branch_Code = '" + GlobalDec.gEmployeeProperty.Branch_Code + "' AND FinancialYear = '" + GlobalDec.gEmployeeProperty.gFinancialYear + "' ");
            }
            else if (Form_Type == "SR")
            {
                IntRes = Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "Item_Sales_Return_Master", "ItemSaleReturnMasterID", "AND ItemSaleReturnMasterID = '" + num + "'");
            }
            if (IntRes == "")
                return true;
            else
                return false;
        }

        public string GEtMaximumID(string StrIDType)
        {
            DataTable DtPreView = new DataTable();
            string RetMaxID = string.Empty;

            Request Request = new Request();
            Request.CommandType = CommandType.StoredProcedure;
            Request.CommandText = "SL_MAXIMUM_ID_GETDATA";
            Request.AddParams("@ID_NAME", StrIDType, DbType.String);
            Request.AddParams("@COMPANY_CODE", GlobalDec.gEmployeeProperty.Company_Code, DbType.Int64);
            Request.AddParams("@OUT_SRNO", "", DbType.String, ParameterDirection.Output);
            //ArrayList AL = new ArrayList();

            //AL = Ope.ExecuteNonQueryWithRetunValues(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);

            //if (AL == null || AL.Count == 0)
            //{
            //    return null;
            //}

            //RetMaxID = Val.ToString(AL[0]);

            DataTable DTAB = new DataTable();
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTAB, Request);
            if (DTAB != null)
            {
                if (DTAB.Rows.Count > 0)
                {
                    RetMaxID = Convert.ToString(DTAB.Rows[0][0]);
                }
            }

            return RetMaxID;
        }

        public string GEtMaximumID_New(string StrIDType)
        {
            DataTable DtPreView = new DataTable();
            string RetMaxID = string.Empty;

            Request Request = new Request();
            Request.CommandType = CommandType.StoredProcedure;
            Request.CommandText = "SL_MAXIMUM_ID_GETDATA";
            Request.AddParams("@ID_NAME", StrIDType, DbType.String);
            Request.AddParams("@COMPANY_CODE", 0, DbType.Int64);
            Request.AddParams("@OUT_SRNO", "", DbType.String, ParameterDirection.Output);
            //ArrayList AL = new ArrayList();

            //AL = Ope.ExecuteNonQueryWithRetunValues(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);

            //if (AL == null || AL.Count == 0)
            //{
            //    return null;
            //}

            //RetMaxID = Val.ToString(AL[0]);

            DataTable DTAB = new DataTable();
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTAB, Request);
            if (DTAB != null)
            {
                if (DTAB.Rows.Count > 0)
                {
                    RetMaxID = Convert.ToString(DTAB.Rows[0][0]);
                }
            }

            return RetMaxID;
        }

        public int Save_Cheque_Print_data(string Pay_To, string Amount, string Cheque_date, string Amount_In_Word)
        {
            int IntRes = 0;
            Request Request = new Request();

            Request.AddParams("@Pay_To", Pay_To, DbType.String);
            Request.AddParams("@Amount", Amount, DbType.String);
            Request.AddParams("@Cheque_Date", Cheque_date, DbType.Date);
            Request.AddParams("@Amount_In_Word", Amount_In_Word, DbType.String);

            Request.CommandText = "Cheque_Print_Data_Save";
            Request.CommandType = CommandType.StoredProcedure;
            IntRes = Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);

            return IntRes;
        }

        #endregion
    }
}
