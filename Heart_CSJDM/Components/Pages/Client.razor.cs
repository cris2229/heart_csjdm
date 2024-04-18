using Heart_CSJDM.DataAccessLayer;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using Heart_CSJDM.Model;
using Blazorise;
using Heart_CSJDM.Helper;
using Microsoft.JSInterop;

namespace Heart_CSJDM.Components.Pages
{
    public partial class Client 
    {

        
        
        public static string? qrcode { get; set; }
        public string SelectedItem { get; set; }
        public string IsCheckValue { get; set; }
        public string SelectedAssignedto { get; set; }
        public List<string> Items { get; set; }
        public List<string> ItemsAssignedto { get; set; }
        public List<Services> services { get; set; }
        public ClientInfo ClientInfo { get; set; }
        public List<Appointment> appointments { get; set; }
        public List<Appointment> ListOfAppointments { get; set; }
        private DataContext dataContext { get; set; }
        public List<TypeOfServices> _typeofservices { get; set; }
        public decoded decode { get; set; }
        protected override async Task OnInitializedAsync()
        {
            //var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            //var user = authState.User;

            //if (!user.Identity.IsAuthenticated)
            //{
            //    Navigation.NavigateTo("/Account/Login");
            //}
            
            dataContext = new DataContext();
            List<Appointment> Appoint = new List<Appointment>();
            appointments = Appoint;
            string q = qrcode;
            InitilizeClientDetails();

            _typeofservices = dataContext.sp_gettypeofservices();
            SelectedItem = Items.First();
            services = dataContext.sp_getServices();
            ItemsAssignedto = dataContext.sp_getAssignTo();

            InitAppointment(services);
            ListOfAppointments = dataContext.sp_getAppointment(ClientInfo.ClientID);
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var QRdecoded = await ProtectedSessionStore.GetAsync<string>("QRdecoded");
                await JSRuntime.InvokeVoidAsync("getSession");
                InitilizeClientDetails();
                ListOfAppointments = dataContext.sp_getAppointment(ClientInfo.ClientID);
                StateHasChanged();
            }
        }
        public void SelectItem(string item)
        {
            SelectedItem = item;
        }
        public void SelectItemAssignedTo(string item,string servicesname)
        {
            appointments.FirstOrDefault(item => item.Services == servicesname).AssignedTo = item;
            SelectedAssignedto = item;
            // Add your logic here for when an item is selected
        }
        public void IsCheckServices(string servicesname)
        {
            bool isCheck = appointments.FirstOrDefault(item => item.Services == servicesname).IsCheck;
            appointments.FirstOrDefault(item => item.Services == servicesname).IsCheck = !isCheck;
            //SelectedAssignedto = item;
            // Add your logic here for when an item is selected
        }
        public void InitilizeClientDetails()
        {
            ClientInfo = dataContext.sp_getclientInfo(qrcode);
        }
        public void InitAppointment(List<Services> services)
        {
            foreach (var serv in services)
            {  
                Appointment appoint = new Appointment
                {
                    Services = serv.ServicesName,
                    TypeOfServices = "1",
                    DateofAppointment = selectedDate.Value,

                };
                appointments.Add(appoint);
            }
        }
        public void Save_Appointment()
        {
            try {
                Helper.GenerateTransaction gettranID = new GenerateTransaction();
                List<Appointment> savingAppointments = new List<Appointment>();
                savingAppointments = appointments.Where(item => item.IsCheck).ToList();
                string transID = gettranID.getTrancsactionID();
                foreach (var serv in savingAppointments)
                {
                    serv.ClientID = ClientInfo.ClientID;
                    serv.TransactionID = transID;
                    serv.DateofAppointment = selectedDate.Value;
                    dataContext.sp_saveAppointments(serv);
                };
                ListOfAppointments = dataContext.sp_getAppointment(ClientInfo.ClientID);
                modalVisible = false;
            } catch (Exception ex)
            { 
            
            }
            
        }
        public void changeddate(DateTime dateTime)
        {
            DateofAppoint = dateTime;
        }
        private void DateChanged(ChangeEventArgs eventArgs)
        {
            var date = eventArgs.Value;
            selectedDate = Convert.ToDateTime(date);
            StateHasChanged();
        }
        [JSInvokable]
        public static void getSession(string value)
        { 
            qrcode = value;
        }
    }
}
