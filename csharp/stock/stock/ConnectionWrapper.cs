using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Transactions;

namespace stock
{
    public static class ConnectionWrapper
    {

        public static DbProviderFactory factory = new MySqlClientFactory();

        public static Dictionary<Transaction, DbConnection> s_connection = new Dictionary<Transaction, DbConnection>(10);

        public static DbConnection GetConnection(string connKey)
        {

            var tran = Transaction.Current;

            DbConnection conn = null;
            if (Transaction.Current == null)
            {
                conn = factory.CreateConnection();
                conn.ConnectionString = GetConnectionString(connKey);
                conn.Open();
                return conn;
            }

            if(Transaction.Current != null
                && s_connection.ContainsKey(tran))
            {
                return s_connection[tran];
            }

            conn = factory.CreateConnection();
            conn.Open();
            conn.BeginTransaction();
            tran.TransactionCompleted += TransactionCompletedEventHandler;        

            lock(s_connection)
            {
                s_connection.Add(tran, conn);
            }
            return conn;
        }

        private static void TransactionCompletedEventHandler(object sender, TransactionEventArgs e)
        {
            var tran = e.Transaction;
            if(tran == null)
            {
                return;
            }
            DbConnection conn;
            s_connection.TryGetValue(tran, out conn);
            if (conn != null)
            {
                lock (s_connection)
                {
                    s_connection.Remove(tran);
                }

                try
                {
                    conn.Dispose();
                }
                catch 
                {
                }
            }
        }

        private static string GetConnectionString(string connKey)
        {
            var connStr = ConfigurationManager.ConnectionStrings[connKey].ConnectionString;
            return connStr;
        }
    }
}
