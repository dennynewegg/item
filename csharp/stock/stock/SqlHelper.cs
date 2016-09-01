﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace stock
{
    public static class SqlHelper
    {
        private static string LocalDb = "LocalDb";

        private static DbConnection GetConnection(string connKey)
        {
            return ConnectionWrapper.GetConnection(connKey);
        }

        private static DbConnection GetConnection()
        {
            return GetConnection(LocalDb);
        }

        public static void ExecuteNonQuery(string sql, params object[] param)
        {
            Execute(GetSqlCmd(sql, param), cmd =>cmd.ExecuteNonQuery());
        }

        private static List<T> GetList<T>(string sql, params object[] param)
        {
            return ToList<T>(GetDataTable(sql,param)) ;
        }

        public static DataTable GetDataTable(string sql, params object[] param)
        {
            var ds = GetDateSet(sql, param);
            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return new DataTable();
        }

        public static DataSet GetDateSet(string sql, params object[] param)
        {
            var ds = new DataSet();
            Execute(GetSqlCmd(sql, param), cmd =>
            {
                var adapter = ConnectionWrapper.factory.CreateDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(ds);
            });
            return ds;
        }

        private static T GetEntity<T>(string sql,params object[] param)
        {
            return GetList<T>(sql, param).FirstOrDefault();
        }


        private static void Execute(DbCommand cmd, Action<DbCommand> action)
        {
            var conn = GetConnection();
            bool isOpenConn = false;
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    isOpenConn = true;
                    conn.Open();
                }
                cmd.Connection = conn;
                action(cmd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                try
                {
                    if (isOpenConn
                        && conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
                catch
                {
                }

            }
        }


        private static DbCommand GetSqlCmd(string sql, params object[] param)
        {
            var cmd = ConnectionWrapper.factory.CreateCommand(); //conn.CreateCommand();
            sql = sql.ToUpper();
            cmd.Parameters.AddRange(CreateParametersForList(ref sql,string.Empty,param as IEnumerable));
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            return cmd;
        }


        private static DbParameter[] CreateParametersForList(ref string sql, string suffix, IEnumerable source)
        {
            if (source == null)
            {
                return new DbParameter[0];
            }
           
            var enumerable = source as object[] ?? source.Cast<object>().ToArray();

            if (enumerable.First() is DbParameter)
            {
                return enumerable.Cast<DbParameter>().ToArray();
            }
            if (enumerable.First().GetType().IsValueType)
            {
                var sqlVariables = ParseSqlVariable(sql);
                if (sqlVariables.Count == 1
                    && enumerable.Length > 1)
                {
                    return CreateParametersForList(ref sql, suffix, enumerable.Select(item => new object[] {item}));
                }
                return CreateParametersForValueList(ref sql,suffix, enumerable.ToArray());
            }

            var paramList = new List<DbParameter>(enumerable.Count()*10);
            var sqlBuilder = new StringBuilder(sql.Length * (enumerable.Count() + 10));
            for (var i = 0; i < enumerable.Count(); i++)
            {
                var paramSql = sql;
                var item = enumerable[i];
                if (item is IEnumerable)
                {
                    paramList.AddRange(CreateParametersForList(ref paramSql, string.Format("{0}_{1}", suffix, i),
                        item as IEnumerable));
                }
                else if(item.GetType().IsClass)
                {
                    paramList.AddRange(CreateParametersForEntity(ref paramSql,item,string.Format("_{0}",i)));
                }
                sqlBuilder.AppendLine(paramSql);
            }
            if (sqlBuilder.Length > 0)
            {
                sql = sqlBuilder.ToString();
            }
            return paramList.ToArray();
        }

        private static DbParameter[] CreateParametersForEntity(ref string sql, object entity, string suffix = null)
        {
            if (suffix == null)
            {
                suffix = string.Empty;
            }
            var sqlVariables = ParseSqlVariable(sql);
            if (sqlVariables.Count <= 0) return null;

            var paramList = new List<DbParameter>(sqlVariables.Count);

            foreach (string sqlVar in sqlVariables)
            {
                var value = GetObjectValue(entity, sqlVar.Substring(1));
                var newSqlVar = sqlVar + suffix;
                sql = sql.Replace(sqlVar, newSqlVar);
                paramList.Add(CreateParameter(newSqlVar, value));
            }
            return paramList.ToArray();
        }
        
        private static DbParameter[] CreateParametersForValueList(ref string sql,string suffix , params object[] param)
        {
            if (param == null)
            {
                return null;
            }

            if (suffix == null)
            {
                suffix = string.Empty;
            }

            var sqlVariables = ParseSqlVariable(sql);
            if (sqlVariables.Count <= 0) return null;
            var paramList = new List<DbParameter>(sqlVariables.Count);
            for (var i = 0; i < sqlVariables.Count; i++)
            {
                object value = null;
                if (param.Length > i)
                {
                    value = param[i];
                }
                var sqlVar = sqlVariables[i];
                var newsqlVar = sqlVar + suffix;
                sql = sql.Replace(sqlVar, newsqlVar);
                paramList.Add(CreateParameter(newsqlVar, value));
            }

            return paramList.ToArray();
        }

        public static DbParameter CreateParameter(string paramName, object value)
        {
            if (value is DbParameter)
            {
                return value as DbParameter;
            }
            DbParameter parameter = ConnectionWrapper.factory.CreateParameter();
            parameter.Direction = ParameterDirection.Input;
            parameter.ParameterName = paramName;
            parameter.IsNullable = true;
            parameter.Value = value;
            return parameter;
        }

        private static object GetObjectValue(object obj, string propertyName)
        {
            if (obj == null)
            {
                return null;
            }
            var type = obj.GetType();
            return (from property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    where String.Compare(property.Name, propertyName, StringComparison.OrdinalIgnoreCase) == 0 
                    && property.CanRead
                    select property.GetValue(obj, null)).FirstOrDefault();
        }

        private static List<string> ParseSqlVariable(string sql)
        {
            if (string.IsNullOrWhiteSpace(sql))
            {
                return null;
            }
            return SQLAnalyser.GetInputParamList(sql);
        }

        public static List<T> ToList<T>(DataTable tb)
        {
            if (tb == null
                || tb.Rows.Count == 0)
            {
                return new List<T>(0);
            }
            var map = new List<KeyValuePair<DataColumn, PropertyInfo>>();
            var type = typeof (T);

            foreach (var propertyInfo in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (propertyInfo.CanWrite)
                {
                    foreach (DataColumn column in tb.Columns)
                    {
                        if (string.Compare(column.ColumnName, propertyInfo.Name, StringComparison.OrdinalIgnoreCase) ==
                            0)
                        {
                            map.Add(new KeyValuePair<DataColumn, PropertyInfo>(column, propertyInfo));
                        }
                        break;
                    }
                }
            }
            if (map.Count <= 0) return new List<T>(0);
            var list = new List<T>(tb.Rows.Count);
            foreach (DataRow dr in tb.Rows)
            {
                var item = Activator.CreateInstance<T>();
                foreach (var kv in map)
                {
                    kv.Value.SetValue(item,dr[kv.Key],null);
                }
                list.Add(item);
            }
            return list;
        }
    }
}
