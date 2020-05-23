using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace SAMS.Repository
{
    public class BaseDao
    {
        private SqlConnection _connection;

        public BaseDao()
        {
            Init();
        }

        private void Init()
        {
            
        }

        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["SAMSEntities"].ConnectionString;
        }
        private SqlConnection CreateConnection()
        {
            _connection = new SqlConnection(GetConnectionString());
            _connection.Open();
            return _connection;
        }

        //private SqlCommand CreateCommand();

        public void ExecuteNonQuery(string commandText, List<SqlParam> collection = null)
        {
            using (SqlConnection conn = this.CreateConnection())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = commandText;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    if (collection != null && collection.Count > 0)
                    {
                        cmd.AssignParameters(collection);
                    }

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public async Task<bool> ExecuteNonQueryAsync(string commandText, List<SqlParam> collection = null, CommandType commandType = CommandType.Text)
        {
            using (SqlConnection conn = this.CreateConnection())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = commandText;
                    cmd.CommandType = commandType;
                    cmd.Connection = conn;
                    if (collection != null && collection.Count > 0)
                    {
                        cmd.AssignParameters(collection);
                    }

                    return (await cmd.ExecuteNonQueryAsync()) > 0;
                }
            }
        }

        public DataSet ExecuteDataSet(string commandText, List<SqlParam> collection = null)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = this.CreateConnection())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = commandText;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;
                    if (collection != null && collection.Count > 0)
                    {
                        cmd.AssignParameters(collection);
                    }

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }
                    return ds;
                }
            }
        }

        public DataTable ExecuteDataTable(string sql, List<SqlParam> collection = null)
        {
            using (var ds = ExecuteDataSet(sql, collection))
            {
                if (ds == null || ds.Tables.Count == 0)
                    return null;
                if (ds.Tables[0].Rows.Count == 0)
                    return null;
                return ds.Tables[0];

            }
        }

        public async Task<DataTable> ExecuteDataTableAsync(string commandText, List<SqlParam> collection = null, CommandType commandType = CommandType.Text)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = this.CreateConnection())
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = commandText;
                    cmd.CommandType = commandType;
                    cmd.Connection = conn;
                    if (collection != null && collection.Count > 0)
                    {
                        cmd.AssignParameters(collection);
                    }

                    SqlDataReader rdr = await cmd.ExecuteReaderAsync();

                    if (rdr.HasRows)
                    {
                        dt.Load(rdr);
                    }

                    return dt;
                }
            }
        }
        /// <summary>
        /// Gets Data from DataBase in the form of generic list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandText"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        public async Task<List<T>> FetchListAsync<T>(string commandText, List<SqlParam> collection = null, CommandType commandType = CommandType.Text) where T : class, new()
        {
            return (await this.ExecuteDataTableAsync(commandText, collection, commandType)).ToList<T>();
        }
        /// <summary>
        /// Gets single generic data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandText"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        public async Task<T> FetchItemAsync<T>(string commandText, List<SqlParam> collection = null, CommandType commandType = CommandType.Text) where T : class, new()
        {
            return (await this.FetchListAsync<T>(commandText, collection, commandType)).FirstOrDefault();
        }
       
        public DataRow ExecuteDataRow(string sql)
        {
            using (var ds = ExecuteDataSet(sql))
            {
                if (ds == null || ds.Tables.Count == 0)
                    return null;
                if (ds.Tables[0].Rows.Count == 0)
                    return null;
                return ds.Tables[0].Rows[0];
            }
        }

        public async Task<DataRow> ExecuteDataRowAsync(string sql)
        {
            using (var dt = await ExecuteDataTableAsync(sql))
            {
                if (dt.Rows.Count == 0)
                    return null;
                return dt.Rows[0];
            }
        }

       
    }

    public class SqlParam
    {
        public SqlParam()
        {

        }

        public SqlParam(string propertyName, SqlDbType dbType, object value)
        {
            this.propertyName = propertyName;
            this.dbType = dbType;
            this.value = value;
        }
        public SqlParam(string propertyName, SqlDbType dbType, object value, int size) : this(propertyName, dbType, value)
        {
            this.size = size;
        }
        public string propertyName { get; set; }
        public SqlDbType dbType { get; set; }
        public int size { get; set; }
        public object value { get; set; }
    }
}
