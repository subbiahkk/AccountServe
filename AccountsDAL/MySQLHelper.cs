using AccountsDAL.Common.cs;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsDAL
{
    /// <summary>
    /// An ADO.NET helper class
    /// </summary>
   public class AdoHelper : IDisposable
    {
        // Internal members
        public static string ConnString;
        protected string _connString = null;
        protected MySqlConnection _conn = null;
        protected MySqlTransaction _trans = null;
        protected bool _disposed = false;

        /// <summary>
        /// Sets or returns the connection string use by all instances of this class.
        /// </summary>
        public static string ConnectionString { get; set; }

        /// <summary>
        /// Returns the current MySqlTransaction object or null if no transaction
        /// is in effect.
        /// </summary>
        public MySqlTransaction Transaction { get { return _trans; } }

        /// <summary>
        /// Constructor using global connection string.
        /// </summary>
        public AdoHelper()
        {
            _connString = Constants.ConnString;
           // _connString = "Server=localhost;port=3306;Database=accounts;Uid=root;Pwd=Password1"; 
            Connect();
        }

        /// <summary>
        /// Constructure using connection string override
        /// </summary>
        /// <param name="connString">Connection string for this instance</param>
        public AdoHelper(string connString)
        {
            _connString = Constants.ConnString;
           // _connString = "Server=localhost;port=3306;Database=accounts;Uid=root;Pwd=Password1"; //connString;
            Connect();
        }

        // Creates a MySqlConnection using the current connection string
        protected void Connect()
        {
            _conn = new MySqlConnection(_connString);
            _conn.Open();
        }

        /// <summary>
        /// Constructs a MySqlCommand with the given parameters. This method is normally called
        /// from the other methods and not called directly. But here it is if you need access
        /// to it.
        /// </summary>
        /// <param name="qry">MySql query or stored procedure name</param>
        /// <param name="type">Type of MySql command</param>
        /// <param name="args">Query arguments. Arguments should be in pairs where one is the
        /// name of the parameter and the second is the value. The very last argument can
        /// optionally be a MySqlParameter object for specifying a custom argument type</param>
        /// <returns></returns>
        public MySqlCommand CreateCommand(string qry, CommandType type, params object[] args)
        {
            MySqlCommand cmd = new MySqlCommand(qry, _conn);

            // Associate with current transaction, if any
            if (_trans != null)
                cmd.Transaction = _trans;

            // Set command type
            cmd.CommandType = type;

            // Construct MySql parameters
            if (args != null)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i] is string && i < (args.Length - 1))
                    {
                        MySqlParameter parm = new MySqlParameter();
                        parm.ParameterName = (string)args[i];
                        parm.Value = args[++i];
                        cmd.Parameters.Add(parm);
                    }
                    else if (args[i] is MySqlParameter)
                    {
                        cmd.Parameters.Add((MySqlParameter)args[i]);
                    }
                    else throw new ArgumentException("Invalid number or type of arguments supplied");
                }
            }
            return cmd;
        }

        #region Exec Members

        /// <summary>
        /// Executes a query that returns no results
        /// </summary>
        /// <param name="qry">Query text</param>
        /// <param name="args">Any number of parameter name/value pairs and/or MySqlParameter arguments</param>
        /// <returns>The number of rows affected</returns>
        public int ExecNonQuery(string qry, params object[] args)
        {
            using (MySqlCommand cmd = CreateCommand(qry, CommandType.Text, args))
            {
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Executes a stored procedure that returns no results
        /// </summary>
        /// <param name="proc">Name of stored proceduret</param>
        /// <param name="args">Any number of parameter name/value pairs and/or MySqlParameter arguments</param>
        /// <returns>The number of rows affected</returns>
        public int ExecNonQueryProc(string proc, params object[] args)
        {
            using (MySqlCommand cmd = CreateCommand(proc, CommandType.StoredProcedure, args))
            {
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Executes a query that returns a single value
        /// </summary>
        /// <param name="qry">Query text</param>
        /// <param name="args">Any number of parameter name/value pairs and/or MySqlParameter arguments</param>
        /// <returns>Value of first column and first row of the results</returns>
        public object ExecScalar(string qry, params object[] args)
        {
            using (MySqlCommand cmd = CreateCommand(qry, CommandType.Text, args))
            {
                return cmd.ExecuteScalar();
            }
        }

        /// <summary>
        /// Executes a query that returns a single value
        /// </summary>
        /// <param name="proc">Name of stored proceduret</param>
        /// <param name="args">Any number of parameter name/value pairs and/or MySqlParameter arguments</param>
        /// <returns>Value of first column and first row of the results</returns>
        public object ExecScalarProc(string qry, params object[] args)
        {
            using (MySqlCommand cmd = CreateCommand(qry, CommandType.StoredProcedure, args))
            {
                return cmd.ExecuteScalar();
            }
        }

        /// <summary>
        /// Executes a query and returns the results as a MySqlDataReader
        /// </summary>
        /// <param name="qry">Query text</param>
        /// <param name="args">Any number of parameter name/value pairs and/or MySqlParameter arguments</param>
        /// <returns>Results as a MySqlDataReader</returns>
        public MySqlDataReader ExecDataReader(string qry, params object[] args)
        {
            //using(MySqlConnection conn = new MySqlConnection(_connString))
            using (MySqlCommand cmd = CreateCommand(qry, CommandType.Text, args))
            {
                return cmd.ExecuteReader();
            }
        }

        /// <summary>
        /// Executes a stored procedure and returns the results as a MySqlDataReader
        /// </summary>
        /// <param name="proc">Name of stored proceduret</param>
        /// <param name="args">Any number of parameter name/value pairs and/or MySqlParameter arguments</param>
        /// <returns>Results as a MySqlDataReader</returns>
        public MySqlDataReader ExecDataReaderProc(string qry, params object[] args)
        {
            using (MySqlCommand cmd = CreateCommand(qry, CommandType.StoredProcedure, args))
            {
                return cmd.ExecuteReader();
            }
        }

        /// <summary>
        /// Executes a query and returns the results as a DataSet
        /// </summary>
        /// <param name="qry">Query text</param>
        /// <param name="args">Any number of parameter name/value pairs and/or MySqlParameter arguments</param>
        /// <returns>Results as a DataSet</returns>
        public DataSet ExecDataSet(string qry, params object[] args)
        {
            using (MySqlCommand cmd = CreateCommand(qry, CommandType.Text, args))
            {
                MySqlDataAdapter adapt = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapt.Fill(ds);
                return ds;
            }
        }

        /// <summary>
        /// Executes a stored procedure and returns the results as a Data Set
        /// </summary>
        /// <param name="proc">Name of stored proceduret</param>
        /// <param name="args">Any number of parameter name/value pairs and/or MySqlParameter arguments</param>
        /// <returns>Results as a DataSet</returns>
        public DataSet ExecDataSetProc(string qry, params object[] args)
        {
            using (MySqlCommand cmd = CreateCommand(qry, CommandType.StoredProcedure, args))
            {
                MySqlDataAdapter adapt = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapt.Fill(ds);
                return ds;
            }
        }

        #endregion

        #region Transaction Members

        /// <summary>
        /// Begins a transaction
        /// </summary>
        /// <returns>The new MySqlTransaction object</returns>
        public MySqlTransaction BeginTransaction()
        {
            Rollback();
            _trans = _conn.BeginTransaction();
            return Transaction;
        }

        /// <summary>
        /// Commits any transaction in effect.
        /// </summary>
        public void Commit()
        {
            if (_trans != null)
            {
                _trans.Commit();
                _trans = null;
            }
        }

        /// <summary>
        /// Rolls back any transaction in effect.
        /// </summary>
        public void Rollback()
        {
            if (_trans != null)
            {
                _trans.Rollback();
                _trans = null;
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                // Need to dispose managed resources if being called manually
                if (disposing)
                {
                    if (_conn != null)
                    {
                        Rollback();
                        _conn.Dispose();
                        _conn = null;
                    }
                }
                _disposed = true;
            }
        }

        #endregion
    }
}
