using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


public class Sql
{
    private readonly string connectionString;

    public Sql(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public DataTable ExecuteQuery(string query, Dictionary<string, object> parameters)
    {
        DataTable dataTable = new DataTable();

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
        }
        catch (SqlException sqlEx)
        {
            throw new Exception("SQL error occurred while executing the query: " + sqlEx.Message);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while executing the query: " + ex.Message);
        }

        return dataTable;
    }

    public int ExecuteNonQuery(string query, Dictionary<string, object> parameters)
    {
        int affectedRows = 0;

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }

                    affectedRows = command.ExecuteNonQuery();
                }
            }
        }
        catch (SqlException sqlEx)
        {
            throw new Exception("SQL error occurred while executing the non-query: " + sqlEx.Message);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while executing the non-query: " + ex.Message);
        }

        return affectedRows;
    }

    public int Delete(string query, Dictionary<string, object> parameters)
    {
        try
        {
            return ExecuteNonQuery(query, parameters);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while deleting: " + ex.Message);
        }
    }

    public int Insert(string query, Dictionary<string, object> parameters)
    {
        try
        {
            return ExecuteNonQuery(query, parameters);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while inserting: " + ex.Message);
        }
    }

    public void ReplaceRecords(string deleteQuery, Dictionary<string, object> deleteParameters, string insertQuery, List<Dictionary<string, object>> insertParametersList)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlTransaction transaction = null;

            try
            {
                connection.Open();
                transaction = connection.BeginTransaction();

                // Delete operation
                try
                {
                    using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection, transaction))
                    {
                        if (deleteParameters != null)
                        {
                            foreach (var param in deleteParameters)
                            {
                                deleteCommand.Parameters.AddWithValue(param.Key, param.Value);
                            }
                        }

                        deleteCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while deleting records: " + ex.Message);
                }

                // Insert operations
                try
                {
                    foreach (var insertParameters in insertParametersList)
                    {
                        using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection, transaction))
                        {
                            foreach (var param in insertParameters)
                            {
                                insertCommand.Parameters.AddWithValue(param.Key, param.Value);
                            }

                            insertCommand.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while inserting records: " + ex.Message);
                }

                // Commit transaction
                transaction.Commit();
            }
            catch (SqlException sqlEx)
            {
                transaction?.Rollback();
                throw new Exception("SQL error occurred during the replace operation: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                throw new Exception("An error occurred during the replace operation: " + ex.Message);
            }
        }
    }
}
