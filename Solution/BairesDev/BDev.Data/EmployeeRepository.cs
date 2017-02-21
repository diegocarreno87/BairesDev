namespace BDev.Data
{
    using System;
    using BDev.Common;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Data;
    using Util;

    public class EmployeeRepository : IRepository<Employee>
    {
        /// <summary>
        /// Creates a new Employee from the parameters
        /// </summary>
        /// <param name="e">Employee object with all the required properties</param>
        public void Create(Employee e)
        {
            try
            {
                SqlConnection sqlcon = Connection.Instance;

                if (sqlcon.State != System.Data.ConnectionState.Open)
                {
                    sqlcon.Open();
                }
                   
                string query = @" 
                    INSERT INTO [dbo].[BDevEmployees]
                               ([Name],[Type],[Image])
                         VALUES
                               (@Name,@Type,@Image)";

                using (SqlCommand command = new SqlCommand(query, sqlcon))
                {
                    command.Parameters.AddWithValue("@Name", e.Name);
                    command.Parameters.AddWithValue("@Type", (int)e.Type);
                    command.Parameters.AddWithValue("@Image", e.Image);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Log.AddMessage(string.Format("-{0}: Error at EmployeeRepository.Create(): {1}", DateTime.Now.ToString("MM/dd/yyyy  hh:mm:ss"), ex.InnerException));
            }
        }

        /// <summary>
        /// Deletes the selected Employee from the parameters.
        /// </summary>
        /// <param name="iD">ID of the employee to be deleted</param>
        public void Delete(int iD)
        {
            try
            {
                SqlConnection sqlcon = Connection.Instance;

                if (sqlcon.State != ConnectionState.Open)
                {
                    sqlcon.Open();
                }
                
                string query = @" DELETE FROM [dbo].[BDevEmployees] WHERE [ID] = @iD";

                using (SqlCommand command = new SqlCommand(query, sqlcon))
                {
                    command.Parameters.AddWithValue("@iD", iD);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Log.AddMessage(string.Format("-{0}: Error at EmployeeRepository.Delete(): {1}", DateTime.Now.ToString("MM/dd/yyyy  hh:mm:ss"), ex.InnerException));
            }
        }

        /// <summary>
        /// Updates the info about the employee from the parameters
        /// </summary>
        /// <param name="newE">Employee object with the info to be updated on the database</param>
        public void Edit(Employee newE)
        {
            try
            {
                SqlConnection sqlcon = Connection.Instance;

                if (sqlcon.State != ConnectionState.Open)
                {
                    sqlcon.Open();
                }

                string query = @" 
                    UPDATE [dbo].[BDevEmployees]
                       SET [Name] = @Name,[Type] = @Type,[Image] = @Image
                     WHERE [ID] = @ID";

                using (SqlCommand command = new SqlCommand(query, sqlcon))
                {
                    command.Parameters.AddWithValue("@Name", newE.Name);
                    command.Parameters.AddWithValue("@Type", (int)newE.Type);
                    command.Parameters.AddWithValue("@Image", newE.Image);
                    command.Parameters.AddWithValue("@ID", newE.ID);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Log.AddMessage(string.Format("-{0}: Error at EmployeeRepository.Edit(): {1}", DateTime.Now.ToString("MM/dd/yyyy  hh:mm:ss"), ex.InnerException));
            }
        }

        /// <summary>
        /// Gets all the employees currently saved on the database
        /// </summary>
        /// <returns>Collection of employee objects</returns>
        public ICollection<Employee> GetAll(string filterName, int page, int records)
        {
            ICollection<Employee> result = new List<Employee>();

            try
            {
                SqlConnection sqlcon = Connection.Instance;

                if (sqlcon.State != ConnectionState.Open)
                {
                    sqlcon.Open();
                }

                using (SqlCommand command = new SqlCommand(string.Format(@"   
                    SELECT  *
                    FROM    (   SELECT    ROW_NUMBER() OVER ( ORDER BY [ID] ) AS RowNum, *
                                FROM [dbo].[BDevEmployees]
					            WHERE Name LIKE '%{0}%'
                            ) AS RowConstrainedResult
                    WHERE   RowNum >= @From AND RowNum <= @To
                    ORDER BY RowNum", filterName), sqlcon))
                {
                    command.Parameters.AddWithValue("@From", page * records - records + 1);
                    command.Parameters.AddWithValue("@To", page * records);

                    SqlDataAdapter da = new SqlDataAdapter(command);
                    
                    DataSet ds = new DataSet();

                    da.Fill(ds);

                    if (ds.Tables.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            result.Add(new Employee()
                            {
                                ID = int.Parse(dr["ID"].ToString()),
                                Name = dr["Name"].ToString(),
                                Image = (byte[])dr["Image"],
                                Type = (EmployeeType)dr["Type"]
                            });

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.AddMessage(string.Format("-{0}: Error at EmployeeRepository.GetAll(): {1}", DateTime.Now.ToString("MM/dd/yyyy  hh:mm:ss"), ex.InnerException));
            }

            return result;
        }

        /// <summary>
        /// Gets the Employee object from the database filtering by the parameters
        /// </summary>
        /// <param name="iD">ID of the employee</param>
        /// <returns></returns>
        public Employee GetByID(int iD)
        {
            Employee result = null;
            
            try
            {
                SqlConnection sqlcon = Connection.Instance;

                if (sqlcon.State != ConnectionState.Open)
                {
                    sqlcon.Open();
                }

                using (SqlCommand command = new SqlCommand(@"   
                    SELECT [ID],[Name],[Type],[Image]
                      FROM [dbo].[BDevEmployees]
                    WHERE [ID] = @ID", sqlcon))
                {
                    command.Parameters.AddWithValue("@ID", iD);

                    SqlDataAdapter da = new SqlDataAdapter(command);

                    DataSet ds = new DataSet();

                    da.Fill(ds);

                    if (ds.Tables.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];

                        result = new Employee()
                        {
                            ID = int.Parse(dr["ID"].ToString()),
                            Name = dr["Name"].ToString(),
                            Image = (byte[])dr["Image"],
                            Type = (EmployeeType)dr["Type"]
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Log.AddMessage(string.Format("-{0}: Error at EmployeeRepository.GetByID(): {1}", DateTime.Now.ToString("MM/dd/yyyy  hh:mm:ss"), ex.InnerException));
            }

            return result;
        }

    }
}
