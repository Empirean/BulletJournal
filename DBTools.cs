using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace BulletJournal
{

    class DBTools
    {
        string connectionString;
        
        public DBTools(string s)
        {
            connectionString = s;
        }

        public void GenericNonQueryAction(string command, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandText = command;
                    comm.CommandType = CommandType.Text;
                    comm.Parameters.AddRange(parameters);

                    try
                    {
                        conn.Open();
                        comm.ExecuteNonQuery();
                    }
                    catch(SqlException)
                    {
                        throw;
                    }
                    finally
                    {
                        conn.Close();
                    }

                }
            }
        }

        public int GenericScalarAction(string command, params SqlParameter[] parameters)
        {
            int modified = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandText = command;
                    comm.CommandType = CommandType.Text;
                    comm.Parameters.AddRange(parameters);

                    try
                    {
                        conn.Open();
                        modified = (int) comm.ExecuteScalar();
                    }
                    catch (SqlException)
                    {
                        throw;
                    }
                    finally
                    {
                        conn.Close();
                    }

                }
            }

            return modified;
        }
        
        public DataTable GenericQueryAction(string command, params SqlParameter[] parameters)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandText = command;
                    comm.CommandType = CommandType.Text;
                    comm.Parameters.AddRange(parameters);

                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(comm))
                    {
                        try
                        {
                            conn.Open();
                            sqlDataAdapter.Fill(dt);
                        }
                        catch (SqlException)
                        {
                            throw;
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
            }

            return dt;
        }
    }
}
