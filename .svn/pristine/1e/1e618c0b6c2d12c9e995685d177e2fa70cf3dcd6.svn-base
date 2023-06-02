using System.Data;
using DLL;
using BLL.PropertyClasses.Master;
using System;
namespace BLL.FunctionClasses.Master
{
    public class CapitalMaster
    {
        InterfaceLayer Ope = new InterfaceLayer();

        #region Other Function

        public int Save(Capital_MasterProperty pClsProperty)
        {
            Request Request = new Request();

            Request.AddParams("@Capital_code", pClsProperty.Capital_Code, DbType.Int64, ParameterDirection.Input);
            Request.AddParams("@Capital_Name", pClsProperty.Capital_Name, DbType.String, ParameterDirection.Input);
            Request.AddParams("@Remark", pClsProperty.Remark, DbType.String, ParameterDirection.Input);

            Request.CommandText = "Capital_Master_Save";
            Request.CommandType = CommandType.StoredProcedure;
            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }

        public DataTable GetData_Search()
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.CommandText = "Capital_Master_Search_GetData";
            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request, "");
            return DTab;
        }

        public string ISExists(string CapitalName, Int64 CapitalCode)
        {
            Validation Val = new Validation();
            return Val.ToString(Ope.FindText(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, "Capital_Master", "Capital_Name", "AND Capital_Name = '" + CapitalName + "' AND NOT Capital_Code =" + CapitalCode));
        }

        public int CapitalEntry_Save(Capital_MasterProperty pClsProperty)
        {
            Request Request = new Request();

            Request.AddParams("@ID", pClsProperty.Capital_Entry_Code, DbType.Int64, ParameterDirection.Input);
            Request.AddParams("@Capital_code", pClsProperty.Capital_Code, DbType.Int64, ParameterDirection.Input);
            Request.AddParams("@Party_code", pClsProperty.Party_Code, DbType.Int64, ParameterDirection.Input);
            Request.AddParams("@Amount", pClsProperty.Amount, DbType.Decimal, ParameterDirection.Input);
            Request.AddParams("@Remark", pClsProperty.Remark, DbType.String, ParameterDirection.Input);
            Request.AddParams("@Entry_Date", pClsProperty.Entry_Date, DbType.Date, ParameterDirection.Input);
            Request.AddParams("@Insert_Date", pClsProperty.Insert_Date, DbType.Date, ParameterDirection.Input);

            Request.CommandText = "Capital_Entry_Master_Save";
            Request.CommandType = CommandType.StoredProcedure;
            return Ope.ExecuteNonQuery(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, Request);
        }

        public DataTable Capital_GetData_Search()
        {
            DataTable DTab = new DataTable();
            Request Request = new Request();

            Request.CommandText = "Capital_Entry_Search_GetData";
            Request.CommandType = CommandType.StoredProcedure;
            Ope.GetDataTable(BLL.DBConnections.ConnectionString, BLL.DBConnections.ProviderName, DTab, Request, "");
            return DTab;
        }

        #endregion
    }
}
