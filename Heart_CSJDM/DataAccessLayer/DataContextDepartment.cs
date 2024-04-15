using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Reflection.PortableExecutable;

namespace Payroll_Template.DataAccessLayer
{
    public class DataContextDepartment
    {
       
        

        public DataTable get_DepartmentList()
        {
            DataTable dataTable = new DataTable();
            try
            {
                var connectionString = ConfigurationManager.AppSettings["MySqlConnectionString"];
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();

                string sql = "CALL sp_getDepartmentList();";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                for (int i = 0; i < rdr.FieldCount; i++)
                {
                    dataTable.Columns.Add(rdr.GetName(i), rdr.GetFieldType(i));
                }

                // Populate the DataTable
                while (rdr.Read())
                {
                    DataRow row = dataTable.NewRow();
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        row[i] = rdr.GetValue(i);
                    }
                    dataTable.Rows.Add(row);
                }

                return dataTable;
                rdr.Close();

            }
            catch
            {
                
            }

            return dataTable;

        }
        public DataTable sp_getDeparmentInfo(int DeptID)
        {
            DataTable dataTable = new DataTable();
            try
            {
                var connectionString = ConfigurationManager.AppSettings["MySqlConnectionString"];
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();

                string sql = "CALL sp_getDeparmentInfo(" + DeptID + ");";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                for (int i = 0; i < rdr.FieldCount; i++)
                {
                    dataTable.Columns.Add(rdr.GetName(i), rdr.GetFieldType(i));
                }

                // Populate the DataTable
                while (rdr.Read())
                {
                    DataRow row = dataTable.NewRow();
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        row[i] = rdr.GetValue(i);
                    }
                    dataTable.Rows.Add(row);
                }

                return dataTable;
                rdr.Close();

            }
            catch
            {

                return dataTable;

            }
        }

        //public async Task Init()
        //{
        //    await _initDatabase();
        //    await _initTables();
        //}

        //private async Task _initDatabase()
        //{
        //    // create database if it doesn't exist
        //    var connectionString = ConfigurationManager.AppSettings["MySqlConnectionString"];
        //    using var connection = new MySqlConnection(connectionString);
        //    //var sql = $"CREATE DATABASE IF NOT EXISTS `{_dbSettings.Database}`;";
        //    await connection.ExecuteAsync(sql);
        //}

        //private async Task _initTables()
        //{
        //    // create tables if they don't exist
        //    using var connection = CreateConnection();
        //    await _initUsers();

        //    async Task _initUsers()
        //    {
        //        var sql = """
        //        CREATE TABLE IF NOT EXISTS Users (
        //            Id INT NOT NULL AUTO_INCREMENT,
        //            Title VARCHAR(255),
        //            FirstName VARCHAR(255),
        //            LastName VARCHAR(255),
        //            Email VARCHAR(255),
        //            Role INT,
        //            PasswordHash VARCHAR(255),
        //            PRIMARY KEY (Id)
        //        );
        //    """;
        //        await connection.ExecuteAsync(sql);
        //    }
        //}
    }
}
