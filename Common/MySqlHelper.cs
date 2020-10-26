using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// ado.net连接sqlser的帮助类
    /// </summary>
    public class SqlHelper
    {
        //连接字符串
        private static string connstr = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;

        /// <summary>
        /// 创建连接
        /// </summary>
        /// <returns></returns>
        public static SqlConnection CreateConnection()
        {
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            return conn;
        }
        
        /// <summary>
        /// 执行增删改操作
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="conn"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql, SqlConnection conn, params SqlParameter[] parameters)
        {
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// 执行增删改操作
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                return ExecuteNonQuery(sql, conn, parameters);
            }
        }

        /// <summary>
        /// 单条查询（返回第一条数据）
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="conn"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql, SqlConnection conn, params SqlParameter[] parameters)
        {
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteScalar();
            }
        }

        /// <summary>
        /// 单条查询（返回第一条数据）
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                return ExecuteScalar(sql, conn, parameters);
            }
        }

        /// <summary>
        /// 查询（返回DataTable）
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="conn"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DataTable ExecuteQuery(string sql, SqlConnection conn, params SqlParameter[] parameters)
        {
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.Parameters.AddRange(parameters);
                DataTable dt = new DataTable();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }
                return dt;
            }
        }

        /// <summary>
        /// 查询（返回DataTable）
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DataTable ExecuteQuery(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                return ExecuteQuery(sql, conn, parameters);
            }
        }

        /// <summary>
        /// 查询（返回DataTable）
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="conn"></param>
        /// <returns></returns>
        public static DataTable ExecuteQuery(string sql, SqlConnection conn)
        {
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                DataTable dt = new DataTable();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }
                return dt;
            }
        }
    }
}
