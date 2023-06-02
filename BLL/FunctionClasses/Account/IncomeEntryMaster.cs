using System.Data;
using DLL;
using BLL.PropertyClasses.Master;
using System;
namespace BLL.FunctionClasses.Master
{
    public class IncomeEntryMaster
    {
        InterfaceLayer Ope = new InterfaceLayer();

        #region Other Function
        
        public int IncomeEntry_Save(IncomeEntry_MasterProperty pClsProperty)
        {
            Request Request = new Request();

            Request.AddParams("@ID", pClsProperty.Income_ID, DbType.Int64, ParameterDirection.Input);
            Request.AddParams("@Party_code", pClsProperty.Party_Code, DbType.Int64, ParameterDirection.Input);
            Request.AddParams("@Cash_Type", pClsProperty.Cash_Type, DbType.String, ParameterDirection.Input);
            Request.AddParams("@Amount", pClsProperty.Amount, DbType.Decimal, ParameterDirection.Input);
            Request.AddParams("@Remark", pClsProperty.Remark, DbType.String, ParameterDirection.Input);
            Request.AddParams("@Entry_Date", pClsProperty.Entry_Date, DbType.Date, ParameterDirection.Input);
            Request.AddParams("@Insert_Date", pClsProperty.Insert_Date, DbType.Date, ParameterDirection.Input);

            Request.CommandText = "Income_Entry_Master_Save";
            Request.CommandType = CommandType.StoredProcedure;
            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }

        public DataTable Income_Entry_GetData_Search()
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.CommandText = "Income_Entry_Search_GetData";
            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request, "");
            return DTab;
        }

        #endregion
    }
}
