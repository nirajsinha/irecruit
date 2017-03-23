using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Omu.ValueInjecter;
using iRecruit.Data.Contexts;
using System.Data.SqlClient;

namespace iRecruit.Data.Helpers
{
    internal class StoredProcedureHelper : AppContext
    {


        /// <summary>
        /// Calls a stored procedure using the provided settings
        /// </summary>
        /// <param name="settings">All settings needed for the stored procedure call (e.g. name, parameters, etc.)</param>
        /// <returns>A list of data tables holding all of the results</returns>
        public List<DataTable> CallStoredProcedure(StoredProcedureSettings settings)
        {
            var results = new List<DataTable>();

            var cmd = this.Database.Connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = settings.ProcedureName;

            foreach (var p in settings.Parameters)
            {
                var parameter = cmd.CreateParameter();
                parameter.ParameterName = p.Name;
                parameter.Value = p.Value;
                parameter.DbType = p.DbType;
                parameter.Direction = p.Direction;
                cmd.Parameters.Add(parameter);
            }

            try
            {
                this.Database.Connection.Open();
                var reader = cmd.ExecuteReader();
                do
                {
                    var set = new DataTable();
                    set.Load(reader); // this automatically goes to the next result set
                    results.Add(set);
                } while (!reader.IsClosed);
            }
            catch (SqlException e)
            {
                // do something with the error
            }
            finally
            {
                cmd.Dispose();
                this.Database.Connection.Close();
            }

            return results;
        }

        /// <summary>
        /// Calls a stored procedure and returns multiple result sets
        /// </summary>
        /// <param name="settings">All settings needed for the stored procedure call (e.g. name, parameters, etc.)</param>
        /// <returns>A list of data tables holding all of the results</returns>
        public List<DataTable> MultiResultStoredProcedure(StoredProcedureSettings settings)
        {
            var results = new List<DataTable>();

            var cmd = this.Database.Connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = settings.ProcedureName;
            if (settings.Parameters != null)
            {
                foreach (var p in settings.Parameters)
                {
                    var parameter = cmd.CreateParameter();
                    parameter.ParameterName = p.Name;
                    parameter.Value = p.Value;
                    parameter.DbType = p.DbType;
                    parameter.Direction = p.Direction;
                    cmd.Parameters.Add(parameter);
                }
            }
            try
            {
                this.Database.Connection.Open();
                var reader = cmd.ExecuteReader();
                do
                {
                    var set = new DataTable();
                    set.Load(reader); // this automatically goes to the next result set
                    results.Add(set);
                } while (!reader.IsClosed);
            }
            catch (SqlException e)
            {
                // do something with the error
            }
            finally
            {
                for(int i = 0; i < cmd.Parameters.Count; i++)
                {
                    if(cmd.Parameters[i].Direction == ParameterDirection.Output)
                    {
                        settings.Parameters[i].Value = cmd.Parameters[i].Value;
                    }
                }
                cmd.Dispose();
                this.Database.Connection.Close();
            }

            return results;
        }

        /// <summary>
        /// Calls a stored procedure and inserts the record set into the database
        /// </summary>
        /// <param name="settings">All settings needed for the stored procedure call (e.g. name, parameters, etc.)</param>
        /// <returns>An integer value which will tell the client if the insertion was successful</returns>
        public int InsertStoredProcedure(StoredProcedureSettings settings)
        {
            int result = -1;
            var cmd = this.Database.Connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = settings.ProcedureName;
            if (settings.Parameters != null)
            {
                foreach (var p in settings.Parameters)
                {
                    var parameter = cmd.CreateParameter();
                    parameter.ParameterName = p.Name;
                    parameter.Value = p.Value;
                    parameter.DbType = p.DbType;
                    parameter.Direction = p.Direction;
                    result = cmd.Parameters.Add(parameter);
                }
            }
            try
            {
                this.Database.Connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                // do something with the error
            }
            finally
            {
                cmd.Dispose();
                this.Database.Connection.Close();
            }
            return result;
        }

        /// <summary>
        /// Serializes a DataTable to a List of T
        /// </summary>
        /// <typeparam name="T">Type to serialize to</typeparam>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public List<T> SerializeToResultObject<T>(DataTable dataTable) where T : new()
        {
            return InjectValuesFromDataReader<T>(dataTable.CreateDataReader());
        }

        /// <summary>
        /// Maps values from reader to properties of type T
        /// </summary>
        /// <typeparam name="T">Type to serialize to</typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static List<T> InjectValuesFromDataReader<T>(DataTableReader reader) where T : new()
        {
            var list = new List<T>();
            while (reader.Read())
            {
                var o = (T)Activator.CreateInstance(typeof(T));
                o.InjectFrom<ReaderInjection>(reader);
                list.Add(o);
            }
            return list;
        }


        /// <summary>
        /// Returns a single primitive type
        /// </summary>
        /// <typeparam name="T">Type to return</typeparam>
        /// <param name="dataTable">DataTable to get the value from</param>
        /// <returns></returns>
        public T SerializeToPrimitiveResult<T>(DataTable dataTable) where T : struct
        {
            if (dataTable == null || dataTable.Rows.Count == 0 || !(dataTable.Rows[0][0] is T))
            {
                throw new Exception("Return type is not valid");
            }

            return (T)dataTable.Rows[0][0];
        }
    }
}
