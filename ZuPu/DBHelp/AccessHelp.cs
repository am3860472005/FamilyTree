﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZuPu.DBHelp
{
    #region access 操作类
    public class AccessHelp
    {
        private string OleDbConnectionString;    //数据库连接

        /// <summary>
        /// 构造函数
        /// 初始化连接数据库参数
        /// </summary>
        public AccessHelp()
        {
            //Microsoft Access2003的连接语句
            //OleDbConnectionString = "Provider = Microsoft.ACE.OLEDB.4.0;Data Source=.\\Data\\DataBaseName.mdb;Jet OLEDB:Database Password=123456";
            //Microsoft Access2007及以上的连接语句
            OleDbConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0;Data Source=.\\DB\\ZuPu.mdb;Jet OLEDB:Database Password=123456";
        }

        /// <summary>
        /// 构造函数
        /// 初始化连接数据库参数
        /// </summary>
        /// <param name="ConSqlServer">连接对象</param>
        public AccessHelp(string ConSqlServer)
        {
            OleDbConnectionString = ConSqlServer;
        }

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        /// <param name="cnn">连接</param>
        public void Open(OleDbConnection cnn)
        {
            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        /// <param name="cnn">连接</param>
        public void Close(OleDbConnection cnn)
        {
            if (cnn != null)
            {
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
                cnn.Dispose();
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <returns>是否存在</returns>
        public bool ChaXun(string strSql)
        {
            OleDbConnection cnn = new OleDbConnection(OleDbConnectionString);
            OleDbCommand cmd = new OleDbCommand();
            try
            {
                Open(cnn);
                cmd = new OleDbCommand(strSql, cnn);
                return cmd.ExecuteReader().Read();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                cmd.Dispose();
                Close(cnn);
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <returns>第一行第一列结果</returns>
        public string ChaXun2(string strSql)
        {
            OleDbConnection cnn = new OleDbConnection(OleDbConnectionString);
            OleDbCommand cmd = new OleDbCommand();
            try
            {
                Open(cnn);
                cmd = new OleDbCommand(strSql, cnn);
                return cmd.ExecuteScalar().ToString().Trim();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                cmd.Dispose();
                Close(cnn);
            }
        }

        /// <summary>
        /// 查询（OleDbDataReader）
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <returns>查询结果</returns>
        public OleDbDataReader GetDR(string strSql)
        {
            OleDbConnection cnn = new OleDbConnection(OleDbConnectionString);
            OleDbCommand cmd = new OleDbCommand();
            try
            {
                Open(cnn);
                cmd = new OleDbCommand(strSql, cnn);
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                cmd.Dispose();
            }
        }

        /// <summary>
        /// 查询（DataSet）
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <returns>查询结果</returns>
        public DataSet GetDS(string strSql)
        {
            OleDbConnection cnn = new OleDbConnection(OleDbConnectionString);
            OleDbDataAdapter sda = new OleDbDataAdapter();
            try
            {
                Open(cnn);
                sda = new OleDbDataAdapter(strSql, cnn);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                return ds;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                sda.Dispose();
                Close(cnn);
            }
        }

        /// <summary>
        /// 查询（DataSet）
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <param name="tableName">指定DataSet["tableName"]表</param>
        /// <returns>查询结果</returns>
        public DataSet GetDS(string strSql, string tableName)
        {
            OleDbConnection cnn = new OleDbConnection(OleDbConnectionString);
            OleDbDataAdapter sda = new OleDbDataAdapter();
            try
            {
                Open(cnn);
                sda = new OleDbDataAdapter(strSql, cnn);
                DataSet ds = new DataSet();
                sda.Fill(ds, tableName);
                return ds;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                sda.Dispose();
                Close(cnn);
            }
        }

        /// <summary>
        /// 查询（DataTable）
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <returns>查询结果</returns>
        public DataTable GetDT(string strSql)
        {
            return GetDS(strSql).Tables[0];
        }

        /// <summary>
        /// 查询（DataView）
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <returns>查询结果</returns>
        public DataView GetDV(string strSql)
        {
            return GetDS(strSql).Tables[0].DefaultView;
        }

        /// <summary>
        /// 增删改，无图片
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <returns>影响的行数</returns>
        public int RunSql(string strSql)
        {
            OleDbConnection cnn = new OleDbConnection(OleDbConnectionString);
            OleDbCommand cmd = new OleDbCommand();
            try
            {
                Open(cnn);
                cmd = new OleDbCommand(strSql, cnn);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                cmd.Dispose();
                Close(cnn);
            }
        }

        /// <summary>
        /// 增改，有图片
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <param name="picbyte">图片的二进制数据</param>
        /// <returns>影响的行数</returns>
        public int RunSql(string strSql, byte[] picbyte)
        {
            OleDbConnection cnn = new OleDbConnection(OleDbConnectionString);
            OleDbCommand cmd = new OleDbCommand();
            try
            {
                Open(cnn);
                cmd = new OleDbCommand(strSql, cnn);
                cmd.Parameters.AddWithValue("@Image", SqlDbType.Image);
                cmd.Parameters["@Image"].Value = picbyte;
                return cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                cmd.Dispose();
                Close(cnn);
            }
        }
    }
    #endregion
}
