using Heart_CSJDM.DataAccessLayer;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using Heart_CSJDM.Model;
using Blazorise;
using Heart_CSJDM.Helper;
using Microsoft.JSInterop;
using QRCoder;

namespace Heart_CSJDM.Components.Pages
{
    public partial class Appointments
    {
        public List<Appointment> ListOfAppointments { get; set; }
        public List<Appointment> forAppointments { get; set; }
        public List<clients> clients { get; set; }
        public List<clients> suggestedclients { get; set; }
        private DateTime? selectedDate { get; set; } = DateTime.Today;
        private DataContext dataContext { get; set; }
        private DataContextTransactions _dataContexttransaction { get; set; }
        public List<string> ItemsAssignedto { get; set; }
        public List<string> ItemsStatus { get; set; }
        public List<string> Itemsservices { get; set; }
        public string SelectedAssignedto { get; set; }
        public string SelectedServices { get; set; }
        public List<Services> services { get; set; }
        public List<TypeOfServices> _typeofservices { get; set; }
        public string SelectedItem { get; set; }
        public List<string> Items { get; set; }
        public List<Lookup> _lookups { get; set; }
        public List<Lookup> _filterlookups { get; set; }
        public iTransactions _trans { get; set; }
        public TempVariable _temp { get; set; }
        private bool modalVisible;
        protected override async Task OnInitializedAsync()
        {
            dataContext = new DataContext();
            _dataContexttransaction = new DataContextTransactions();
            forAppointments = new List<Appointment>();
            ListOfAppointments = new List<Appointment>();
            ItemsAssignedto = new List<string>();
            services = new List<Services>();
            _typeofservices = dataContext.sp_gettypeofservices();
            _lookups = new List<Lookup>();
            ItemsStatus = new List<string> { "Active", "In-Acitve" };
            _temp = new TempVariable();
            _filterlookups = new List<Lookup>();
            _trans = new iTransactions();

        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                ListOfAppointments = dataContext.sp_getAppointmentbyDate(selectedDate);
                ItemsAssignedto = dataContext.sp_getAssignTo();
                services = dataContext.sp_getServices();
                _typeofservices = dataContext.sp_gettypeofservices();
                _lookups = dataContext.sp_getLookUpCriteria("AppointmentFilter");
                SelectedItem = _typeofservices.First().Name;
                _temp.lookupID = _lookups.FirstOrDefault().LookupCriteriaID;
            }
        }
        public void SelectItemAssignedTo(string item, int clientid,int AppointID)
        {
            forAppointments.FirstOrDefault(item => item.ClientID == clientid && item.AppointmentID == AppointID).AssignedTo = item;
            SelectedAssignedto = item;
        }
        public void SelectItemStatus(string item, int clientid, int AppointID)
        {
            forAppointments.FirstOrDefault(item => item.ClientID == clientid && item.AppointmentID == AppointID).Status = item;

        }
        public void SelectItemServices(Services item, int clientid, int AppointID)
        {
            forAppointments.FirstOrDefault(item => item.ClientID == clientid && item.AppointmentID == AppointID).Services = item.ServicesName;
        }
        private void DateChanged(ChangeEventArgs eventArgs)
        {
            var date = eventArgs.Value;
            selectedDate = Convert.ToDateTime(date);
            ListOfAppointments = dataContext.sp_getAppointmentbyDate(selectedDate);
            StateHasChanged();
        }
        void OnInput(ChangeEventArgs e)
        {
            inputValue = e.Value.ToString();
            // Filter suggestions based on input value
            suggestedclients = GetFilteredSuggestions(inputValue);
             showSuggestions = true;
            
        }

        List<clients> GetFilteredSuggestions(string input)
        {
            // Implement your own logic to filter suggestions based on input
            return dataContext.sp_getfilteredClients(input,_temp.lookupID,_temp.lookupCategoryID);
        }
        private Task ShowModal()
        {
            modalVisible = true;
            return Task.CompletedTask;
        }
        private Task HideModal()
        {
            modalVisible = false;

            return Task.CompletedTask;
        }
        private async Task SubmitForm()
        {
            
        }
        public void Save_Appointment()
        {
            try
            {
                Helper.GenerateTransaction gettrannumber = new GenerateTransaction();
                string gettransaction = gettrannumber.getTrancsactionID();
                foreach (var serv in forAppointments)
                {
                    serv.TypeOfServices = SelectedItem;
                    serv.TransactionID = gettransaction;
                    serv.DateofAppointment = selectedDate.Value;
                    dataContext.sp_saveAppointments(serv);
                };
                _trans.TransactionNo = gettransaction;
                _trans.UserID = 1;
                _dataContexttransaction.sp_saveTransactions(_trans);
                ListOfAppointments = dataContext.sp_getAppointmentbyDate(selectedDate);
                forAppointments.Clear();
                modalVisible = false;
            }
            catch (Exception ex)
            {

            }

        }
        public void AddAppointment(int clientID, string clientname)
        {
            try
            {
                Appointment appoint = new Appointment
                {
                   AppointmentID = forAppointments.Count,
                   ClientID = clientID,
                   ClientName = clientname,
                   TypeOfServices = SelectedItem,
                };
                forAppointments.Add(appoint);
                showSuggestions = false;
            }
            catch (Exception ex)
            {

            }

        }
        public void SelectItem(string item)
        {
            SelectedItem = item;
        }
        private void filterChange(ChangeEventArgs e)
        {
            _temp.lookupID = Convert.ToInt32(e.Value);
            _filterlookups = dataContext.sp_getfilteredCategory(_temp.lookupID);
        }
        private void filterChangev2(ChangeEventArgs e)
        {
            _temp.lookupCategoryID = Convert.ToInt32(e.Value);
            suggestedclients = GetFilteredSuggestions(inputValue);
            showSuggestions = true;
        }
    }
}
