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
    public class DataContext
    {

        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // Specify the base path where appsettings.json is located
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // Specify the appsettings.json file
            .Build();

        public ClientInfo sp_getclientInfo(string qrcode)
        {

            ClientInfo clientinfo = new ClientInfo();
            try
            {
                
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();

                string sql = "CALL sp_getclientInfo('" + qrcode + "');";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ClientInfo client = new ClientInfo
                    {
                        ClientID = rdr.GetInt32("ClientID"),
                        LastName = rdr.GetString("LastName"),
                        FirstName = rdr.GetString("FirstName"),
                        MiddleName = rdr.GetString("MiddleName"),
                        //ID_No = rdr.GetString("ID_No"),
                        Contact_No = rdr.GetString("Contact_No"),
                        Birthdate = rdr.GetDateTime("Birthdate"),
                        Age = rdr.GetInt32("Age"),
                        Gender = rdr.GetString("Gender"),
                        Marital_Status = rdr.GetString("Marital_Status"),
                        Address = rdr.GetString("Address"),
                        QREncriptedData = rdr.GetString("QREncriptedData"),
                    };
                    clientinfo = client;
                }
                
                return clientinfo;
                rdr.Close();

            }
            catch
            {

            }

            return clientinfo;
        }
        public List<clients> sp_getclients()
        {
            List<clients> clientsList = new List<clients>();
            try
            {

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();

                MySqlCommand command = new MySqlCommand("sp_getClients", conn);
                command.CommandType = CommandType.StoredProcedure;
                MySqlDataReader rdr = command.ExecuteReader();
                

                while (rdr.Read())
                {
                    clients client = new clients
                    {
                        ClientID = rdr.GetInt32("ClientID"),
                        LastName = rdr.GetString("LastName"),
                        FirstName = rdr.GetString("FirstName"),
                        MiddleName = rdr.GetString("MiddleName"),
                        ExtensionName = rdr.GetString("ExtensionName"),
                        ID_No = rdr.GetString("ID_No"),
                        Contact_No = rdr.GetString("Contact_No"),
                        Birthdate = rdr.GetDateTime("Birthdate"),
                        Age = rdr.GetString("Age"),
                        Gender = rdr.GetString("Gender"),
                        Marital_Status = rdr.GetString("Marital_Status"),
                        Address_block = rdr.GetString("Address_block"),
                        Address_lot = rdr.GetString("Address_lot"),
                        Address_Subdivision = rdr.GetString("Address_Subdivision"),
                        Address_HouseNo = rdr.GetString("Address_HouseNo"),
                        Address_Building = rdr.GetString("Address_Building"),
                        Address_Barangay = rdr.GetString("Address_Barangay"),
                        Address_District = rdr.GetString("Address_District"),
                        Address_Municipality = rdr.GetString("Address_Municipality"),
                        Status = rdr.GetString("STATUS"),
                        QREncriptedData = rdr.GetString("QREncriptedData"),
                        ClientName = rdr.GetString("ClientName"),
                    };
                    clientsList.Add(client);
                }

                return clientsList;
                rdr.Close();

            }
            catch(Exception ex  )
            {

            }

            return clientsList;
        }
        public List<clients> sp_getfilteredClients(string value,int searchBy, int pCategoryID)
        {
            List<clients> clientsList = new List<clients>();
            try
            {

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();

                MySqlCommand command = new MySqlCommand("sp_getfilteredClients", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@pvalue", value);
                command.Parameters.AddWithValue("@searchBy", searchBy);
                command.Parameters.AddWithValue("@pCategoryID", pCategoryID);
                MySqlDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                    {
                    clients client = new clients
                    {
                        ClientID = rdr.GetInt32("ClientID"),
                        ClientName = rdr.GetString("ClientName"),
                    };
                    clientsList.Add(client);
                }

                return clientsList;
                rdr.Close();

            }
            catch (Exception ex)
            {

            }

            return clientsList;
        }
        public List<clients> sp_getfilteredClientsdetails(string value, int searchBy, int pCategoryID)
        {
            List<clients> clientsList = new List<clients>();
            try
            {

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();

                MySqlCommand command = new MySqlCommand("sp_getfilteredClientsdetails", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@pvalue", value);
                command.Parameters.AddWithValue("@searchBy", searchBy);
                command.Parameters.AddWithValue("@pCategoryID", pCategoryID);
                MySqlDataReader rdr = command.ExecuteReader();

                while (rdr.Read())
                {
                    clients client = new clients
                    {
                        ClientID = rdr.GetInt32("ClientID"),
                        LastName = rdr.GetString("LastName"),
                        FirstName = rdr.GetString("FirstName"),
                        MiddleName = rdr.GetString("MiddleName"),
                        ExtensionName = rdr.GetString("ExtensionName"),
                        ID_No = rdr.GetString("ID_No"),
                        Contact_No = rdr.GetString("Contact_No"),
                        Birthdate = rdr.GetDateTime("Birthdate"),
                        Age = rdr.GetString("Age"),
                        Gender = rdr.GetString("Gender"),
                        Marital_Status = rdr.GetString("Marital_Status"),
                        Address_block = rdr.GetString("Address_block"),
                        Address_lot = rdr.GetString("Address_lot"),
                        Address_Subdivision = rdr.GetString("Address_Subdivision"),
                        Address_HouseNo = rdr.GetString("Address_HouseNo"),
                        Address_Building = rdr.GetString("Address_Building"),
                        Address_Barangay = rdr.GetString("Address_Barangay"),
                        Address_District = rdr.GetString("Address_District"),
                        Address_Municipality = rdr.GetString("Address_Municipality"),
                        Status = rdr.GetString("STATUS"),
                        QREncriptedData = rdr.GetString("QREncriptedData"),
                    };
                    clientsList.Add(client);
                }

                return clientsList;
                rdr.Close();

            }
            catch (Exception ex)
            {

            }

            return clientsList;
        }
        public List<TypeOfServices> sp_gettypeofservices()
        {
            List<TypeOfServices> itemss = new List<TypeOfServices>();
            try
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();

                MySqlCommand command = new MySqlCommand("sp_gettypeofservices", conn);
                command.CommandType = CommandType.StoredProcedure;
                MySqlDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    TypeOfServices _typeofservices = new TypeOfServices
                    {
                        TypeOfServicesID = rdr.GetInt32("TypeOfServicesID"),
                        Name = rdr.GetString("Name"),
                    };
                    itemss.Add(_typeofservices);
                }
                return itemss;
                rdr.Close();
            }
            catch
            {

            }

            return itemss;
        }
        public List<Lookup> sp_getLookUpCriteria(string group)
        {
            List<Lookup> itemss = new List<Lookup>();
            try
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();

                MySqlCommand command = new MySqlCommand("sp_getLookUpCriteria", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@pGroupname", group);
                MySqlDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    Lookup _lookup = new Lookup
                    {
                        LookupCriteriaID = rdr.GetInt32("LookupCriteriaID"),
                        Code = rdr.GetString("Code"),
                        Description = rdr.GetString("Description"),
                    };
                    itemss.Add(_lookup);
                }
                return itemss;
                rdr.Close();
            }
            catch
            {

            }

            return itemss;
        }
        public List<Lookup> sp_getfilteredCategory(int searchBy)
        {
            List<Lookup> itemss = new List<Lookup>();
            try
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();

                MySqlCommand command = new MySqlCommand("sp_getfilteredCategory", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@searchBy", searchBy);
                MySqlDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    Lookup _lookup = new Lookup
                    {
                        LookupCriteriaID = rdr.GetInt32("LookupCriteriaID"),
                        Description = rdr.GetString("Description"),
                    };
                    itemss.Add(_lookup);
                }
                return itemss;
                rdr.Close();
            }
            catch
            {

            }

            return itemss;
        }
        public List<Sector> sp_getSector()
        {
            List<Sector> itemss = new List<Sector>();
            try
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();

                MySqlCommand command = new MySqlCommand("sp_getSector", conn);
                command.CommandType = CommandType.StoredProcedure;
                MySqlDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    Sector _sector = new Sector
                    {
                        SectorID = rdr.GetInt32("SectorID"),
                        Description = rdr.GetString("Description"),
                    };
                    itemss.Add(_sector);
                }
                return itemss;
                rdr.Close();
            }
            catch
            {

            }

            return itemss;
        }
        public List<Organization> sp_getOrganization()
        {
            List<Organization> itemss = new List<Organization>();
            try
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();

                MySqlCommand command = new MySqlCommand("sp_getOrganization", conn);
                command.CommandType = CommandType.StoredProcedure;
                MySqlDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    Organization _Org = new Organization
                    {
                        OrganizationID = rdr.GetInt32("OrganizationID"),
                        Description = rdr.GetString("Description"),
                    };
                    itemss.Add(_Org);
                }
                return itemss;
                rdr.Close();
            }
            catch
            {

            }

            return itemss;
        }
        public List<Barangay> sp_getBarangay()
        {
            List<Barangay> itemss = new List<Barangay>();
            try
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();

                MySqlCommand command = new MySqlCommand("sp_getBarangay", conn);
                command.CommandType = CommandType.StoredProcedure;
                MySqlDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    Barangay _barangay = new Barangay
                    {
                        BarangayID = rdr.GetInt32("BarangayID"),
                        Name = rdr.GetString("Name"),
                    };
                    itemss.Add(_barangay);
                }
                return itemss;
                rdr.Close();
            }
            catch
            {

            }

            return itemss;
        }

        public List<string> sp_getAssignTo()
        {
            List<string> itemss = new List<string>();
            try
            {

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();

                string sql = "CALL sp_getAssignTo();";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    itemss.Add(rdr.GetString(0));

                }
                return itemss;
                rdr.Close();

            }
            catch
            {

            }

            return itemss;
        }
        public List<Services> sp_getServices()
        {
            List<Services> services = new List<Services>();
            try
            {

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();

                string sql = "CALL sp_getServices();";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Services serv = new Services
                    {
                        ServicesID = rdr.GetInt32("ServicesID"),
                        ServicesName = rdr.GetString("ServicesName"),
                        // Populate other properties as needed
                    };
                    services.Add(serv);
                }
                return services;
                rdr.Close();

            }
            catch
            {

            }

            return services;
        }
        public void sp_saveAppointments(Appointment appointment)
        {
            List<Services> services = new List<Services>();
            try
            {

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();

                MySqlCommand command = new MySqlCommand("sp_saveAppointments", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@pDateofAppointment", appointment.DateofAppointment);
                command.Parameters.AddWithValue("@pTypeOfServices", appointment.TypeOfServices);
                command.Parameters.AddWithValue("@pServices", appointment.Services);
                command.Parameters.AddWithValue("@pAssignedTo", appointment.AssignedTo);
                command.Parameters.AddWithValue("@pStatus", appointment.Status);
                command.Parameters.AddWithValue("@pRemarks", appointment.Remarks);
                command.Parameters.AddWithValue("@pTransactionID", appointment.TransactionID);
                command.Parameters.AddWithValue("@pClientID", appointment.ClientID);
                command.ExecuteNonQuery();

            }
            catch
            {

            }
        }
        public void sp_saveTransactions(iTransactions _transactions)
        {
            List<Services> services = new List<Services>();
            try
            {

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();

                MySqlCommand command = new MySqlCommand("sp_saveTransactions", conn);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@pTransactionNo", _transactions.TransactionNo);
                command.Parameters.AddWithValue("@pUserID", _transactions.UserID);
                command.ExecuteNonQuery();

            }
            catch
            {

            }
        }
        public List<Appointment> sp_getAppointment(int ClientID)
        {
            List<Appointment> Appoint = new List<Appointment>();
            try
            {

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();

                MySqlCommand command = new MySqlCommand("sp_getAppointment", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@pClientID", ClientID);
                MySqlDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    Appointment appoints = new Appointment
                    {
                        //ClientID = rdr.GetInt32("ClientID"),
                        DateofAppointment = rdr.GetDateTime("DateofAppointment"),
                        Services = rdr.GetString("Services"),
                        AssignedTo = rdr.GetString("AssignedTo"),
                        Status = rdr.GetString("Status"),
                        Remarks = rdr.GetString("Remarks"),
                        TransactionID = rdr.GetString("TransactionID")
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
        public List<Appointment> sp_getAppointmentbyDate(DateTime? date)
        {
            List<Appointment> Appoint = new List<Appointment>();
            try
            {

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();

                MySqlCommand command = new MySqlCommand("sp_getAppointmentbyDate", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@pDate", date);
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
        public void sp_UpdateClientinfo(clients clients)
        {
            clients clientinfo = new clients();
            try
            {

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();

                MySqlCommand command = new MySqlCommand("sp_UpdateClientinfo", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@pClientID", clients.ClientID);
                command.Parameters.AddWithValue("@pLastName", clients.LastName);
                command.Parameters.AddWithValue("@pFirstName", clients.FirstName);
                command.Parameters.AddWithValue("@pMiddleName", clients.MiddleName);
                command.Parameters.AddWithValue("@pExtensionName", clients.ExtensionName);
                command.Parameters.AddWithValue("@pContact_No", clients.Contact_No);
                command.Parameters.AddWithValue("@pBirthdate", clients.Birthdate);
                command.Parameters.AddWithValue("@prAge", clients.Age);
                command.Parameters.AddWithValue("@pGender", clients.Gender);
                command.Parameters.AddWithValue("@pMarital_Status", clients.Marital_Status);
                command.Parameters.AddWithValue("@pAddress_block", clients.Address_block);
                command.Parameters.AddWithValue("@pAddress_lot", clients.Address_lot);
                command.Parameters.AddWithValue("@pAddress_Subdivision", clients.Address_Subdivision);
                command.Parameters.AddWithValue("@pAddress_HouseNo", clients.Address_HouseNo);
                command.Parameters.AddWithValue("@pAddress_Building", clients.Address_Building);
                command.Parameters.AddWithValue("@pAddress_Barangay", clients.Address_Barangay);
                command.Parameters.AddWithValue("@pAddress_District", clients.Address_District);
                command.Parameters.AddWithValue("@pAddress_Municipality", clients.Address_Municipality);
                command.Parameters.AddWithValue("@pQREncriptedData", clients.QREncriptedData);
                command.Parameters.AddWithValue("@pStatus", clients.Statusint);
                command.Parameters.AddWithValue("@pSectorID", clients.SectorID);
                command.Parameters.AddWithValue("@pOrganization", clients.OrganizationID);
                command.ExecuteNonQuery();

            }
            catch(Exception ex  )
            {

            }
        }
        public clients sp_getclientbyID(int ClientID)
        {
            clients clientinfo = new clients();
            try
            {

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();

                MySqlCommand command = new MySqlCommand("sp_getclientbyID", conn);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@pClientID", ClientID);
                MySqlDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    clients client = new clients
                    {
                        ClientID = rdr.GetInt32("ClientID"),
                        LastName = rdr.GetString("LastName"),
                        FirstName = rdr.GetString("FirstName"),
                        MiddleName = rdr.GetString("MiddleName"),
                        ExtensionName = rdr.GetString("ExtensionName"),
                        ID_No = rdr.GetString("ID_No"),
                        Contact_No = rdr.GetString("Contact_No"),
                        Birthdate = rdr.GetDateTime("Birthdate"),
                        Age = rdr.GetString("Age"),
                        Gender = rdr.GetString("Gender"),
                        Marital_Status = rdr.GetString("Marital_Status"),
                        Address_block = rdr.GetString("Address_block"),
                        Address_lot = rdr.GetString("Address_lot"),
                        Address_Subdivision = rdr.GetString("Address_Subdivision"),
                        Address_HouseNo = rdr.GetString("Address_HouseNo"),
                        Address_Building = rdr.GetString("Address_Building"),
                        Address_Barangay = rdr.GetString("Address_Barangay"),
                        Address_District = rdr.GetString("Address_District"),
                        Address_Municipality = rdr.GetString("Address_Municipality"),
                        Status = rdr.GetString("STATUS"),
                        QREncriptedData = rdr.GetString("QREncriptedData"),
                        SectorID = rdr.GetInt32("SectorID"),
                        OrganizationID = rdr.GetInt32("OrganizationID")
                    };
                    clientinfo = client;
                }

                return clientinfo;
                rdr.Close();

            }
            catch
            {

            }

            return clientinfo;
        }
    }
}
