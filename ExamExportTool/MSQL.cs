using System.Data;
using System.Data.SqlClient;


namespace ExamExportTool
{
    public static class MSQL
    {

       // private const string CONNECTIONSTRING = "Server=devdb01;Database=Common;Trusted_Connection=True;";
        
        private static SqlConnection conn;

        public static void ConnectToSQL(string connectionString)
        {

            if (conn == null)
            {
                conn = new SqlConnection(connectionString);

                conn.Open();
            }
            else
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
            }
        }

        public static void CloseConnection()
        {
            if (conn != null)
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Close();
                    conn = null;
                }
            }
        }

        public static DataTable GetDataTable(string SQL)
        {
            DataTable dt = new DataTable();

            using (SqlDataAdapter da = new SqlDataAdapter(SQL, conn))
            {

                da.Fill(dt);

            }

            return dt;
        }

        public static DataTable ExecuteSQLScript(string File, string TableName)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand(System.IO.File.ReadAllText(File).Replace("<Table>", TableName), conn))
            {

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {

                    da.Fill(dt);

                }

            }

            return dt;
        }

        public static string GetDebugPrint(string Text)
        {
            return "PRINT('" + Text + "')";
        }
    }
}
