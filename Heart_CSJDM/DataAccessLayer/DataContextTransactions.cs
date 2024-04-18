
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Extensions.Options;
using  MySql.Data.MySqlClient;
using System.Configuration;
using System.Reflection.PortableExecutable;
using Microsoft.Extensions.Configuration;
using Blazorise;
using Microsoft.CodeAnalysis.Rename;
using Heart_CSJDM.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Windows.Input;
using Heart_CSJDM.Components.Pages;

namespace Heart_CSJDM.DataAccessLayer
{
    public class DataContextTransactions
    {

        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // Specify the base path where appsettings.json is located
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // Specify the appsettings.json file
            .Build();

        public List<iTransactions> sp_getTransactionsbyDate(DateTime? date)
        {
            List<iTransactions> Trans = new List<iTransactions>();
            try
            {

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();

                MySqlCommand command = new MySqlCommand("sp_getTransactionsbyDate", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@pDate",  date);
                MySqlDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    iTransactions _trans = new iTransactions
                    {
                        //ClientID = rdr.GetInt32("ClientID"),
                        TransactionID = rdr.GetInt32("TransactionID"),
                        TransactionNo = rdr.GetString("TransactionNo"),
                        TransactionDate = rdr.GetDateTime("TransactionDate"),
                        UserID = rdr.GetInt32("UserID")
                    };
                    Trans.Add(_trans);
                }
                return Trans;
                rdr.Close();
            }
            catch (Exception ex)
            {

            }
            return Trans;
        }
        public void sp_saveTransactions(iTransactions _tran)
        {
            List<Services> services = new List<Services>();
            try
            {

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();

                MySqlCommand command = new MySqlCommand("sp_saveTransactions", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@pTransactionNo", _tran.TransactionNo);
                command.Parameters.AddWithValue("@pUserID", _tran.UserID);
                command.ExecuteNonQuery();

            }
            catch
            {

            }
        }
        public List<Appointment> sp_getAppointmentbyTransactions(string pTransactionno)
        {
            List<Appointment> Appoint = new List<Appointment>();
            try
            {

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();

                MySqlCommand command = new MySqlCommand("sp_getAppointmentbyTransactions", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@pTransactionno", pTransactionno);
                MySqlDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    Appointment appoints = new Appointment
                    {
                        AppointmentID = rdr.GetInt32("AppointmentID"),
                        DateofAppointment = rdr.GetDateTime("DateofAppointment"),
                        Services = rdr.GetString("Services"),
                        AssignedTo = rdr.GetString("AssignedTo"),
                        Status = rdr.GetString("Status"),
                        Remarks = rdr.GetString("Remarks"),
                        TransactionID = rdr.GetString("TransactionID"),
                        ClientName = rdr.GetString("ClientName")
                    };
                    Appoint.Add(appoints);
                }
                return Appoint;
                rdr.Close();
            }
            catch (Exception ex)
            {

            }
            return Appoint;
        }
    }
}
