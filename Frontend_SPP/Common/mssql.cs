using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
//using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Linq;
//using System.Text.Encodings.Web;

namespace Frontend_SPP.Common
{
    public class mssql
    {
        //HtmlEncoder _htmlEncoder;
        //JavaScriptEncoder _javaScriptEncoder;
        //UrlEncoder _urlEncoder;

        //public mssql(HtmlEncoder htmlEncoder,
        //                  JavaScriptEncoder javascriptEncoder,
        //                  UrlEncoder urlEncoder)
        //{
        //    _htmlEncoder = htmlEncoder;
        //    _javaScriptEncoder = javascriptEncoder;
        //    _urlEncoder = urlEncoder;
        //}

        static string _strConn = ConfigurationManager.AppSetting["ConnectionStrings:DefaultConnection"];

        //sql command
        public static Boolean SqlCommandFunction(SqlCommand cmd)
        {
            SqlConnection con = new SqlConnection(_strConn);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        //Execute query (insert, update, delete) menggunakan stored procedure
        public static int ExecuteNonQuery(string strStoredProcedureName, List<SqlParameter> ListSqlParams)
        {
            SqlConnection sqlConn = null;
            SqlCommand sqlCmd = null;
            SqlParameter sqlParam;
            int iRowsAffected = 0;

            try
            {
                sqlConn = new SqlConnection();
                sqlConn.ConnectionString = _strConn;
                if (sqlConn.State == ConnectionState.Closed)
                {
                    sqlConn.Open();
                }
                sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlConn;
                sqlCmd.CommandText = strStoredProcedureName;
                sqlCmd.CommandTimeout = 600;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                //param
                if (ListSqlParams != null)
                {
                    for (int iCount = 0; iCount < ListSqlParams.Count; iCount++)
                    {
                        sqlParam = new SqlParameter();
                        sqlParam = ListSqlParams[iCount];
                        sqlCmd.Parameters.Add(sqlParam);
                    }
                }

                iRowsAffected = sqlCmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                iRowsAffected = 0;
            }
            finally
            {
                if (sqlConn.State == ConnectionState.Open)
                {
                    sqlCmd.Dispose();
                    sqlCmd = null;
                    sqlConn.Close();
                    sqlConn.Dispose();
                    sqlConn = null;
                }
            }

            return (iRowsAffected);
        }

        //Mengambil data dari table menggunakan stored procedure
        public static DataTable GetDataTable(string strStoredProcedureName, List<SqlParameter> ListSqlParams)
        {
            SqlConnection sqlConn = null;
            SqlDataAdapter sqlDa = null;
            SqlCommand sqlCmd;
            SqlParameter sqlParam;
            DataTable dt = null;

            try
            {
                sqlConn = new SqlConnection();
                sqlConn.ConnectionString = _strConn;
                if (sqlConn.State == ConnectionState.Closed)
                {
                    sqlConn.Open();
                }
                sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlConn;
                sqlCmd.CommandTimeout = 600;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = strStoredProcedureName;
                //param
                if (ListSqlParams != null)
                {
                    for (int iCount = 0; iCount < ListSqlParams.Count; iCount++)
                    {
                        sqlParam = new SqlParameter();
                        sqlParam = ListSqlParams[iCount];
                        sqlCmd.Parameters.Add(sqlParam);
                    }
                }

                sqlDa = new SqlDataAdapter(sqlCmd);
                dt = new DataTable();
                sqlDa.Fill(dt);
            }
            catch (Exception)
            {
                //throw Ex;
            }
            finally
            {
                if (sqlConn.State == ConnectionState.Open)
                {
                    sqlDa.Dispose();
                    sqlDa = null;
                    sqlConn.Close();
                    sqlConn.Dispose();
                    sqlConn = null;
                }
            }

            return (dt);
        }

        //Execute query (insert, update, delete) menggunakan sql query
        public static int ExecuteNonQuery(string strSqlQuery)
        {
            SqlConnection sqlConn = null;
            SqlCommand sqlCmd = null;
            int iRowsAffected = 0;

            try
            {
                sqlConn = new SqlConnection();
                sqlConn.ConnectionString = _strConn;
                if (sqlConn.State == ConnectionState.Closed)
                {
                    sqlConn.Open();
                }
                sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlConn;
                sqlCmd.CommandText = strSqlQuery;
                iRowsAffected = sqlCmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                iRowsAffected = 0;
            }
            finally
            {
                if (sqlConn.State == ConnectionState.Open)
                {
                    sqlCmd.Dispose();
                    sqlCmd = null;
                    sqlConn.Close();
                    sqlConn.Dispose();
                    sqlConn = null;
                }
            }

            return (iRowsAffected);
        }

        //Mengambil data dari table menggunakan sql query
        public static DataTable GetDataTable(string strSqlQuery)
        {
            SqlConnection sqlConn = null;
            SqlDataAdapter sqlDa = null;
            SqlCommand sqlCmd;
            DataTable dt = null;

            try
            {
                sqlConn = new SqlConnection();
                sqlConn.ConnectionString = _strConn;
                if (sqlConn.State == ConnectionState.Closed)
                {
                    sqlConn.Open();
                }
                sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlConn;
                sqlCmd.CommandText = strSqlQuery;
                sqlCmd.CommandTimeout = 600;
                sqlDa = new SqlDataAdapter(sqlCmd);
                dt = new DataTable();
                sqlDa.Fill(dt);
            }
            catch (Exception)
            {
                //throw Ex;
            }
            finally
            {
                if (sqlConn.State == ConnectionState.Open)
                {
                    sqlDa.Dispose();
                    sqlDa = null;
                    sqlConn.Close();
                    sqlConn.Dispose();
                    sqlConn = null;
                }
            }

            return (dt);
        }

        public static DataRow GetDataRow(string strSqlQuery)
        {
            SqlConnection sqlConn = null;
            SqlDataAdapter sqlDa = null;
            SqlCommand sqlCmd;
            DataTable dt = null;

            try
            {
                sqlConn = new SqlConnection();
                sqlConn.ConnectionString = _strConn;
                if (sqlConn.State == ConnectionState.Closed)
                {
                    sqlConn.Open();
                }
                sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlConn;
                sqlCmd.CommandText = strSqlQuery;
                sqlDa = new SqlDataAdapter(sqlCmd);
                dt = new DataTable();
                sqlDa.Fill(dt);
            }
            catch (Exception)
            {
                dt = null;
            }
            finally
            {
                if (sqlConn.State == ConnectionState.Open)
                {
                    sqlDa.Dispose();
                    sqlDa = null;
                    sqlConn.Close();
                    sqlConn.Dispose();
                    sqlConn = null;
                }
            }

            if ((dt.Rows.Count == 0) || (dt == null))
            {
                return null;
            }
            else
            {
                return dt.Rows[0];
            }
        }

        //Execute query (insert, update, delete) menggunakan sql query - SCALAR
        public static int ExecuteScalar(string strSqlQuery)
        {
            SqlConnection sqlConn = null;
            SqlCommand sqlCmd = null;
            int iRowsAffected = 0;

            try
            {
                sqlConn = new SqlConnection();
                sqlConn.ConnectionString = _strConn;
                if (sqlConn.State == ConnectionState.Closed)
                {
                    sqlConn.Open();
                }
                sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlConn;
                sqlCmd.CommandText = strSqlQuery;
                iRowsAffected = (int)sqlCmd.ExecuteScalar();
            }
            catch (Exception)
            {
                iRowsAffected = 0;
            }
            finally
            {
                if (sqlConn.State == ConnectionState.Open)
                {
                    sqlCmd.Dispose();
                    sqlCmd = null;
                    sqlConn.Close();
                    sqlConn.Dispose();
                    sqlConn = null;
                }
            }

            return (iRowsAffected);
        }

        public static Boolean CheckIfExist(string Table, string Field, string Value)
        {
            bool isExist = false;
            try
            {
                DataTable dt = GetDataTable("SELECT " + Field + " FROM " + Table + " WHERE " + Field + " = '" + Value + "'");
                isExist = dt.Rows.Count > 0;
            }
            catch (Exception)
            {
                isExist = false;
            }
            return isExist;
        }

        //Execute query (insert, update, delete) menggunakan sql query - SCALAR
        public static Guid ExecuteScalarGUID(string strSqlQuery)
        {
            SqlConnection sqlConn = null;
            SqlCommand sqlCmd = null;
            Guid iRowsAffected;

            try
            {
                sqlConn = new SqlConnection();
                sqlConn.ConnectionString = _strConn;
                if (sqlConn.State == ConnectionState.Closed)
                {
                    sqlConn.Open();
                }
                sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlConn;
                sqlCmd.CommandText = strSqlQuery;
                iRowsAffected = (Guid)sqlCmd.ExecuteScalar();
            }
            catch (Exception)
            {
                iRowsAffected = Guid.NewGuid();
            }
            finally
            {
                if (sqlConn.State == ConnectionState.Open)
                {
                    sqlCmd.Dispose();
                    sqlCmd = null;
                    sqlConn.Close();
                    sqlConn.Dispose();
                    sqlConn = null;
                }
            }

            return (iRowsAffected);
        }

        //Execute query (insert, update, delete) menggunakan sql query - SCALAR
        public static int ExecuteScalarInt(string strSqlQuery)
        {
            SqlConnection sqlConn = null;
            SqlCommand sqlCmd = null;
            int iRowsAffected;

            try
            {
                sqlConn = new SqlConnection();
                sqlConn.ConnectionString = _strConn;
                if (sqlConn.State == ConnectionState.Closed)
                {
                    sqlConn.Open();
                }
                sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlConn;
                sqlCmd.CommandText = strSqlQuery;
                iRowsAffected = (int)sqlCmd.ExecuteScalar();
            }
            catch (Exception)
            {
                iRowsAffected = 0;
            }
            finally
            {
                if (sqlConn.State == ConnectionState.Open)
                {
                    sqlCmd.Dispose();
                    sqlCmd = null;
                    sqlConn.Close();
                    sqlConn.Dispose();
                    sqlConn = null;
                }
            }

            return (iRowsAffected);
        }

        public static DataSet GetDataSet(string strStoredProcedureName, List<SqlParameter> ListSqlParams)
        {
            SqlConnection sqlConn = null;
            SqlDataAdapter sqlDa = null;
            SqlCommand sqlCmd;
            SqlParameter sqlParam;
            DataSet ds = null;

            try
            {
                sqlConn = new SqlConnection();
                sqlConn.ConnectionString = _strConn;
                if (sqlConn.State == ConnectionState.Closed)
                {
                    sqlConn.Open();
                }
                sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlConn;
                sqlCmd.CommandTimeout = 600;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = strStoredProcedureName;
                //param
                if (ListSqlParams != null)
                {
                    for (int iCount = 0; iCount < ListSqlParams.Count; iCount++)
                    {
                        sqlParam = new SqlParameter();
                        sqlParam = ListSqlParams[iCount];
                        sqlCmd.Parameters.Add(sqlParam);
                    }
                }

                sqlDa = new SqlDataAdapter(sqlCmd);
                ds = new DataSet();
                sqlDa.Fill(ds);
            }
            catch (Exception)
            {
                //throw Ex;
            }
            finally
            {
                if (sqlConn.State == ConnectionState.Open)
                {
                    sqlDa.Dispose();
                    sqlDa = null;
                    sqlConn.Close();
                    sqlConn.Dispose();
                    sqlConn = null;
                }
            }

            return (ds);
        }

        public static DataSet GetDataSet(string strStoredProcedureName)
        {
            SqlConnection sqlConn = null;
            SqlDataAdapter sqlDa = null;
            SqlCommand sqlCmd;
            DataSet ds = null;

            try
            {
                sqlConn = new SqlConnection();
                sqlConn.ConnectionString = _strConn;
                if (sqlConn.State == ConnectionState.Closed)
                {
                    sqlConn.Open();
                }
                sqlCmd = new SqlCommand();
                sqlCmd.Connection = sqlConn;
                sqlCmd.CommandTimeout = 600;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = strStoredProcedureName;

                sqlDa = new SqlDataAdapter(sqlCmd);
                ds = new DataSet();
                sqlDa.Fill(ds);
            }
            catch (Exception)
            {
                //throw Ex;
            }
            finally
            {
                if (sqlConn.State == ConnectionState.Open)
                {
                    sqlDa.Dispose();
                    sqlDa = null;
                    sqlConn.Close();
                    sqlConn.Dispose();
                    sqlConn = null;
                }
            }

            return (ds);
        }

        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        public static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    object value = dr[column.ColumnName];
                    if (value == DBNull.Value)
                        value = null;
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, value, null);
                    else
                        continue;                    

                    //if (pro.Name == column.ColumnName)
                    //    pro.SetValue(obj, dr[column.ColumnName], null);
                    //else
                    //    continue;
                }
            }
            return obj;
        }

        public static int InsertImagesKP(Byte[] bytes, string SpName, string ParamName, string ID)
        {
            int iRowsAffected = 0;
            //create a connection to the database        
            using (SqlConnection connection = new SqlConnection(_strConn))
            {

                using (SqlCommand command = new SqlCommand(SpName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    //add the parameters
                    command.Parameters.Add(ParamName, SqlDbType.VarBinary).Value = bytes;
                    command.Parameters.Add("ID", SqlDbType.NVarChar).Value = ID;

                    try
                    {
                        //open the connection if closed
                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                        }

                        //execute the stored procedure
                        iRowsAffected = command.ExecuteNonQuery();

                    }
                    catch (Exception)
                    {
                        //handle error per Image

                    }
                }
            }

            return (iRowsAffected);
        }

        public static int InsertFile(Byte[] bytes, string SpName, string ParamName, string ID, string Filename, string Extension)
        {
            int iRowsAffected = 0;
            //create a connection to the database        
            using (SqlConnection connection = new SqlConnection(_strConn))
            {

                using (SqlCommand command = new SqlCommand(SpName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    //add the parameters
                    command.Parameters.Add(ParamName, SqlDbType.VarBinary).Value = bytes;
                    command.Parameters.Add("ID", SqlDbType.NVarChar).Value = ID;
                    command.Parameters.Add("Filename", SqlDbType.NVarChar).Value = Filename;
                    command.Parameters.Add("Extension", SqlDbType.NVarChar).Value = Extension;

                    try
                    {
                        //open the connection if closed
                        if (connection.State == ConnectionState.Closed)
                        {
                            connection.Open();
                        }

                        //execute the stored procedure
                        iRowsAffected = command.ExecuteNonQuery();

                    }
                    catch (Exception)
                    {
                        //handle error per Image

                    }
                }
            }

            return (iRowsAffected);
        }

        public static byte[] ReadToEnd(System.IO.Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }

        public static List<T> ConvertToList<T>(DataTable dt)
        {
            var columnNames = dt.Columns.Cast<DataColumn>()
                    .Select(c => c.ColumnName)
                    .ToList();
            var properties = typeof(T).GetProperties();
            return dt.AsEnumerable().Select(row =>
            {
                var objT = Activator.CreateInstance<T>();
                foreach (var pro in properties)
                {
                    if (columnNames.Contains(pro.Name))
                    {
                        PropertyInfo pI = objT.GetType().GetProperty(pro.Name);
                        pro.SetValue(objT, row[pro.Name] == DBNull.Value ? null : Convert.ChangeType(row[pro.Name], pI.PropertyType));
                    }
                }
                return objT;
            }).ToList();
        }

        public static List<T> BindList<T>(DataTable dt)
        {
            // Example 1:
            // Get private fields + non properties
            var fields = typeof(T).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            // Example 2: Your case
            // Get all public fields
            //var fields = typeof(T).GetFields();

            List<T> lst = new List<T>();

            foreach (DataRow dr in dt.Rows)
            {
                // Create the object of T
                var ob = Activator.CreateInstance<T>();

                foreach (var fieldInfo in fields)
                {
                    foreach (DataColumn dc in dt.Columns)
                    {
                        // Matching the columns with fields
                        if (fieldInfo.Name == dc.ColumnName)
                        {
                            // Get the value from the datatable cell
                            object value = dr[dc.ColumnName];

                            // Set the value into the object
                            fieldInfo.SetValue(ob, value);
                            break;
                        }
                    }
                }

                lst.Add(ob);
            }

            return lst;
        }

        public static List<T> BindListAdvanced<T>(DataTable dt)
        {
            // Example 1:
            // Get private fields + non properties
            //var fields = typeof(T).GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            // Example 2: Your case
            // Get all public fields
            var fields = typeof(T).GetFields();

            List<T> lst = new List<T>();

            foreach (DataRow dr in dt.Rows)
            {
                // Create the object of T
                var ob = Activator.CreateInstance<T>();

                foreach (var fieldInfo in fields)
                {
                    foreach (DataColumn dc in dt.Columns)
                    {
                        // Matching the columns with fields
                        string fnTemplate = "<{0}>k__BackingField";
                        string tFN = string.Format(fnTemplate, dc.ColumnName).ToLower();
                        if (fieldInfo.Name.ToLower() == tFN)
                        //if (fieldInfo.Name == dc.ColumnName)
                        {
                            Type type = fieldInfo.FieldType;

                            // Get the value from the datatable cell
                            object value = GetValue(dr[dc.ColumnName], type);

                            // Set the value into the object
                            fieldInfo.SetValue(ob, value);
                            break;
                        }
                    }
                }

                lst.Add(ob);
            }

            return lst;
        }

        static object GetValue(object ob, Type targetType)
        {
            if (targetType == null)
            {
                return null;
            }
            else if (targetType == typeof(String))
            {
                return ob + "";
            }
            else if (targetType == typeof(int))
            {
                int i = 0;
                int.TryParse(ob + "", out i);
                return i;
            }
            else if (targetType == typeof(short))
            {
                short i = 0;
                short.TryParse(ob + "", out i);
                return i;
            }
            else if (targetType == typeof(long))
            {
                long i = 0;
                long.TryParse(ob + "", out i);
                return i;
            }
            else if (targetType == typeof(ushort))
            {
                ushort i = 0;
                ushort.TryParse(ob + "", out i);
                return i;
            }
            else if (targetType == typeof(uint))
            {
                uint i = 0;
                uint.TryParse(ob + "", out i);
                return i;
            }
            else if (targetType == typeof(ulong))
            {
                ulong i = 0;
                ulong.TryParse(ob + "", out i);
                return i;
            }
            else if (targetType == typeof(double))
            {
                double i = 0;
                double.TryParse(ob + "", out i);
                return i;
            }
            //else if (targetType == typeof(DateTime))
            //{
            //    // do the parsing here...
            //}
            //else if (targetType == typeof(bool))
            //{
            //    // do the parsing here...
            //}
            //else if (targetType == typeof(decimal))
            //{
            //    // do the parsing here...
            //}
            //else if (targetType == typeof(float))
            //{
            //    // do the parsing here...
            //}
            //else if (targetType == typeof(byte))
            //{
            //    // do the parsing here...
            //}
            //else if (targetType == typeof(sbyte))
            //{
            //    // do the parsing here...
            //}


            return ob;
        }
    }
}
