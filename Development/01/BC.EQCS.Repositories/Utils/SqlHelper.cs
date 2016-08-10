using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BC.EQCS.Repositories.Utils
{
    internal static class SqlHelper
    {
        /// <summary>
        /// Create an data table of string values and contain it in an sql parameter for use by a query
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static SqlParameter CreateCodesSqlParameter(IEnumerable<string> values) 
        {
            const string valueColumn = "Value";

            var codesTable = new DataTable();

            codesTable.Columns.Add(valueColumn, typeof(string));
            foreach (var code in values)
            {
                var row = codesTable.NewRow();
                row[valueColumn] = code;
                codesTable.Rows.Add(row);
            }
            var param = new SqlParameter("@codes", codesTable) { TypeName = "dbo.Codes" };
            return param;
        }
    }
}
