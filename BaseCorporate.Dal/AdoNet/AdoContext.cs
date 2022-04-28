using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using BaseCorporate.Utility.Helper;
using Microsoft.Data.SqlClient;

namespace BaseCorporate.Dal.AdoNet
{
    public static class AdoContext
    {
        public static DataTable GetDataTable(SqlCommand command)
        {
            command.Connection = new SqlConnection(AppParameter.AppSettings.ConnectionString);
            command.Connection.Open();
            var dataReader = command.ExecuteReader();
            var dataTable = new DataTable();
            try { dataTable.Load(dataReader); }
            catch {/**/}
            //catch (SqlException ex) { throw new Exception(ex.Message + " [" + query + "]"); }
            finally { command.Connection.Close(); command.Dispose(); GC.SuppressFinalize(command); dataReader.Dispose(); GC.SuppressFinalize(dataReader); }
            return dataTable;
        }
    }
}
