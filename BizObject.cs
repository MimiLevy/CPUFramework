using CPUFrameWork;
using System.Data.SqlClient;
using System.Data;

namespace CPUFramework
{
    public class BizObject
    {
        string _tablename = ""; string _getsproc = ""; string _updatesproc = ""; string _deletesproc = "";
        string _primarykeyname = ""; string _primarykeyparamname = "";
        DataTable _datatable = new();
        public BizObject(string tablename)
        {
            _tablename = tablename;
            _getsproc = tablename + "Get";
            _updatesproc = tablename + "Update";
            _deletesproc = tablename + "Delete";
            _primarykeyname = tablename + "Id";
            _primarykeyparamname = "@" + _primarykeyname;
        }
        public DataTable Load(int primarykeyvalue)
        {
            DataTable dt = new();
            SqlCommand cmd = SQLUtility.GetSqlCommand(_getsproc);
            SQLUtility.SetParamValue(cmd, _primarykeyparamname, primarykeyvalue);
            dt = SQLUtility.GetDataTable(cmd);
            _datatable = dt;
            return dt;
        }
        public  void Delete(DataTable datatable)
        {
            int id = (int)datatable.Rows[0][_primarykeyname];
            SqlCommand cmd = SQLUtility.GetSqlCommand(_deletesproc);
            SQLUtility.SetParamValue(cmd,_primarykeyparamname, id);
            SQLUtility.ExecuteSQL(cmd);
        }
        public void Save(DataTable dataTable)
        {
            if (dataTable.Rows.Count == 0)
            {
                throw new Exception($"Cannot call {_tablename} Save method becuase there are no rows in the table.");
            }
            DataRow r = dataTable.Rows[0];
            SQLUtility.SaveDataRow(r, _updatesproc);
        }
    }
}
