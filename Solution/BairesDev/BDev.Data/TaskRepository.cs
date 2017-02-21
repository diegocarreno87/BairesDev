namespace BDev.Data
{
    using System;
    using System.Collections.Generic;
    using Common;
    using System.Data.SqlClient;
    using System.Data;
    using Util;
    public class TaskRepository : IRepository<Task>
    {
        /// <summary>
        /// Creates a new Task from the parameter
        /// </summary>
        /// <param name="task">Task object with all the properties that will be set on the database</param>
        public void Create(Task task)
        {
            try
            {
                SqlConnection sqlcon = Connection.Instance;

                if (sqlcon.State != ConnectionState.Open)
                {
                    sqlcon.Open();
                }

                string query = @" 
                    INSERT INTO [dbo].[BDevTasks]
                               ([Title],[Description],[AssignedTo],[Status])
                         VALUES
                               (@Title, @Description, @AssignedTo, @Status)";

                using (SqlCommand command = new SqlCommand(query, sqlcon))
                {
                    command.Parameters.AddWithValue("@Title", task.Title);
                    command.Parameters.AddWithValue("@Description", task.Description);
                    command.Parameters.AddWithValue("@AssignedTo", task.AssignedTo);
                    command.Parameters.AddWithValue("@Status", (int)task.Status);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Log.AddMessage(string.Format("-{0}: Error at TaskRepository.Create(): {1}", DateTime.Now.ToString("MM/dd/yyyy  hh:mm:ss"), ex.InnerException));
            }
        }

        /// <summary>
        /// Deletes the selected Task
        /// </summary>
        /// <param name="iD">ID of the task to be deleted</param>
        public void Delete(int iD)
        {
            try
            {
                SqlConnection sqlcon = Connection.Instance;

                if (sqlcon.State != ConnectionState.Open)
                {
                    sqlcon.Open();
                }

                string query = @" 
                    DELETE FROM [dbo].[BDevTasks] WHERE [ID] = @ID";

                using (SqlCommand command = new SqlCommand(query, sqlcon))
                {
                    command.Parameters.AddWithValue("@ID", iD);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Log.AddMessage(string.Format("-{0}: Error at TaskRepository.Delete(): {1}", DateTime.Now.ToString("MM/dd/yyyy  hh:mm:ss"), ex.InnerException));
            }
        }

        /// <summary>
        /// Delete all the Tasks by employee ID
        /// </summary>
        /// <param name="iD">Employee's ID</param>
        public void DeleteByEmployee(int iD)
        {
            try
            {
                SqlConnection sqlcon = Connection.Instance;

                if (sqlcon.State != ConnectionState.Open)
                {
                    sqlcon.Open();
                }

                string query = @" 
                    DELETE FROM [dbo].[BDevTasks] WHERE [AssignedTo] = @ID";

                using (SqlCommand command = new SqlCommand(query, sqlcon))
                {
                    command.Parameters.AddWithValue("@AssignedTo", iD);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Log.AddMessage(string.Format("-{0}: Error at TaskRepository.DeleteByEmployee(): {1}", DateTime.Now.ToString("MM/dd/yyyy  hh:mm:ss"), ex.InnerException));
            }
        }

        /// <summary>
        /// Updates the content for the selected task
        /// </summary>
        /// <param name="newT">Task object changes made from the UI and will be sent to the database</param>
        public void Edit(Task newT)
        {
            try
            {
                SqlConnection sqlcon = Connection.Instance;

                if (sqlcon.State != ConnectionState.Open)
                {
                    sqlcon.Open();
                }

                string query = @" 
                    UPDATE [dbo].[BDevTasks]
                       SET [Title] = @Title
                          ,[Description] = @Description
                          ,[Status] = @Status
                     WHERE [ID] = @ID";

                using (SqlCommand command = new SqlCommand(query, sqlcon))
                {
                    command.Parameters.AddWithValue("@Title", newT.Title);
                    command.Parameters.AddWithValue("@Description", newT.Description);
                    command.Parameters.AddWithValue("@Status", (int)newT.Status);
                    command.Parameters.AddWithValue("@ID", newT.ID);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Log.AddMessage(string.Format("-{0}: Error at TaskRepository.Edit(): {1}", DateTime.Now.ToString("MM/dd/yyyy  hh:mm:ss"), ex.InnerException));
            }
        }

        /// <summary>
        /// Gets all the tasks related to the selected employee
        /// </summary>
        /// <param name="id">ID of the employee</param>
        /// <returns>Collections of Tasks as results</returns>
        public ICollection<Task> GetAll(int id,int page, int records )
        {
            ICollection<Task> result = new List<Task>();

            try
            {
                SqlConnection sqlcon = Connection.Instance;

                if (sqlcon.State != ConnectionState.Open)
                {
                    sqlcon.Open();
                }

                using (SqlCommand command = new SqlCommand(@"   
                    SELECT  *
                    FROM    (   SELECT    ROW_NUMBER() OVER ( ORDER BY [ID] ) AS RowNum, *
                                FROM[dbo].[BDevTasks]
                                WHERE [AssignedTo] = @AssignedTo
                            ) AS RowConstrainedResult
                    WHERE   RowNum >= @From AND RowNum <= @To
                    ORDER BY RowNum

", sqlcon))
                {
                    command.Parameters.AddWithValue("@From", page * records - records + 1);
                    command.Parameters.AddWithValue("@To", page * records);
                    command.Parameters.AddWithValue("@AssignedTo", id);

                    SqlDataAdapter da = new SqlDataAdapter(command);

                    DataSet ds = new DataSet();

                    da.Fill(ds);

                    if (ds.Tables.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            result.Add(new Task()
                            {
                                ID = int.Parse(dr["ID"].ToString()),
                                Title = dr["Title"].ToString(),
                                Description = dr["Description"].ToString(),
                                AssignedTo = int.Parse(dr["AssignedTo"].ToString()),
                                Status = (TaskStatus)dr["Status"]
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.AddMessage(string.Format("-{0}: Error at TaskRepository.GetAll(): {1}", DateTime.Now.ToString("MM/dd/yyyy  hh:mm:ss"), ex.InnerException));
            }

            return result;
        }

        /// <summary>
        /// Gets an specific task from the list filtering by ID
        /// </summary>
        /// <param name="iD">ID of the task</param>
        /// <returns>Task object result</returns>
        public Task GetByID(int iD)
        {
            Task result = new Task();

            try
            {
                SqlConnection sqlcon = Connection.Instance;

                if (sqlcon.State != ConnectionState.Open)
                {
                    sqlcon.Open();
                }

                using (SqlCommand command = new SqlCommand(@"   
                    SELECT[ID],[Title],[Description],[AssignedTo],[Status]
                    FROM[dbo].[BDevTasks]
                    WHERE [ID] = @ID", sqlcon))
                {
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    command.Parameters.AddWithValue("@ID", iD);

                    DataSet ds = new DataSet();

                    da.Fill(ds);

                    if (ds.Tables.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];

                        result = new Task()
                        {
                            ID = int.Parse(dr["ID"].ToString()),
                            Title = dr["Title"].ToString(),
                            Description = dr["Description"].ToString(),
                            AssignedTo = int.Parse(dr["AssignedTo"].ToString()),
                            Status = (TaskStatus)dr["Status"]
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Log.AddMessage(string.Format("-{0}: Error at TaskRepository.GetByID(): {1}", DateTime.Now.ToString("MM/dd/yyyy  hh:mm:ss"), ex.InnerException));
            }

            return result;
        }
    }
}