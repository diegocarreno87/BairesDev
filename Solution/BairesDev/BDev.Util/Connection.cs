namespace BDev.Util
{
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Web;

    public class Connection
    {
        private static SqlConnection instance;

        private Connection() { }

        public static SqlConnection Instance
        {
            get
            {
                var con = HttpContext.Current.Items["DbSession"] as SqlConnection;
                if (con == null)
                {
                    con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                    HttpContext.Current.Items.Add("DbSession", con);
                }
                return con;
            }
        }
    }
}
