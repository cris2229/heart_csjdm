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
        public List<clients> clients { get; set; }
        public List<clients> suggestedclients { get; set; }
        private DateTime? selectedDate { get; set; } = DateTime.Today;
        private DataContext dataContext { get; set; }
        protected override async Task OnInitializedAsync()
        {
            dataContext = new DataContext();
            ListOfAppointments = dataContext.sp_getAppointmentbyDate(selectedDate);
            clients = dataContext.sp_getclients();
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
            return clients.Where(s => s.ClientName.Contains(input)).ToList();
        }
    }
}
