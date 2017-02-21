namespace BDev.Data
{
    using BDev.Common;
    using BDev.Util;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class UserRepository : IRepository<User>
    {
        /// <summary>
        /// Validate the username and password entered by the user
        /// </summary>
        /// <param name="user">User object with the login info</param>
        /// <returns></returns>
        public bool ValidateUserPassword(User user)
        {
            bool result = false;

            try
            {
                SqlConnection sqlcon = Connection.Instance;

                if (sqlcon.State != ConnectionState.Open)
                {
                    sqlcon.Open();
                }

                using (SqlCommand command = new SqlCommand(@"   
                    SELECT [Username], [Password]
                    FROM [dbo].[BDevUsers]
                    WHERE [Username] = @Username AND [Password] = @Password", sqlcon))
                {
                    command.Parameters.AddWithValue("@Username", user.username);
                    command.Parameters.AddWithValue("@Password", user.password);
                    
                    SqlDataAdapter da = new SqlDataAdapter(command);

                    DataSet ds = new DataSet();

                    da.Fill(ds);

                    if (ds.Tables.Count > 0)
                    {
                        if(ds.Tables[0].Rows.Count > 0)
                        {
                            result = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.AddMessage(string.Format("-{0}: Error at EmployeeRepository.GetByID(): {1}", DateTime.Now.ToString("MM/dd/yyyy  hh:mm:ss"), ex.InnerException));
            }

            return result;
        }

        /// <summary>
        /// TODO: Create a new login user
        /// </summary>
        /// <param name="e"></param>
        public void Create(User e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// TODO: Delete an existing login user
        /// </summary>
        /// <param name="iD"></param>
        public void Delete(int iD)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// TODO: Update an existing login user
        /// </summary>
        /// <param name="newE"></param>
        public void Edit(User newE)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// TODO: Get the info for an existing login user
        /// </summary>
        /// <param name="iD"></param>
        /// <returns></returns>
        public User GetByID(int iD)
        {
            throw new NotImplementedException();
        }
    }
}
