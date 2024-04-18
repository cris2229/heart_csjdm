using Heart_CSJDM.DataAccessLayer;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using Heart_CSJDM.Model;
using Blazorise;
using Heart_CSJDM.Helper;

namespace Heart_CSJDM.Components.Pages
{
    public partial class Transactions
    {
        public List<iTransactions> ListOfTransactions { get; set; }
        public List<Appointment> _appointments { get; set; }
        private DateTime? selectedDate { get; set; } = DateTime.Today;
        private DataContext dataContext { get; set; }
        private DataContextTransactions _dataContexttransaction { get; set; }
        public List<string> ItemsAssignedto { get; set; }
        public List<string> ItemsStatus { get; set; }
        public List<Services> services { get; set; }
        public List<TypeOfServices> _typeofservices { get; set; }
        public string SelectedItem { get; set; }
        public List<Lookup> _lookups { get; set; }
        public List<Lookup> _filterlookups { get; set; }
        public TempVariable _temp { get; set; }
        private bool modalVisible;
        protected override async Task OnInitializedAsync()
        {
            dataContext = new DataContext();
            _dataContexttransaction = new DataContextTransactions();
            _appointments = new List<Appointment>();
            ListOfTransactions = new List<iTransactions>();
            ItemsAssignedto = new List<string>();
            services = new List<Services>();
            _typeofservices = dataContext.sp_gettypeofservices();
            _lookups = new List<Lookup>();
            ItemsStatus = new List<string> { "Active", "In-Acitve" };
            _temp = new TempVariable();
            _filterlookups = new List<Lookup>();

        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {

                ListOfTransactions = _dataContexttransaction.sp_getTransactionsbyDate(selectedDate);
                ItemsAssignedto = dataContext.sp_getAssignTo();
                services = dataContext.sp_getServices();
                _typeofservices = dataContext.sp_gettypeofservices();
                _lookups = dataContext.sp_getLookUpCriteria("AppointmentFilter");
                SelectedItem = _typeofservices.First().Name;
                _temp.lookupID = _lookups.FirstOrDefault().LookupCriteriaID;
                StateHasChanged();
            }
        }
        private void DateChanged(ChangeEventArgs eventArgs)
        {
            var date = eventArgs.Value;
            selectedDate = Convert.ToDateTime(date);
            ListOfTransactions = _dataContexttransaction.sp_getTransactionsbyDate(selectedDate);
            StateHasChanged();
        }
       

        List<clients> GetFilteredSuggestions(string input)
        {
            // Implement your own logic to filter suggestions based on input
            return dataContext.sp_getfilteredClients(input, _temp.lookupID, _temp.lookupCategoryID);
        }
        private Task ShowModal(string _transactionNo)
        {
            modalVisible = true;
            _appointments = _dataContexttransaction.sp_getAppointmentbyTransactions(_transactionNo);
            return Task.CompletedTask;
        }
        private Task HideModal()
        {
            modalVisible = false;

            return Task.CompletedTask;
        }
        
       
    }
}
