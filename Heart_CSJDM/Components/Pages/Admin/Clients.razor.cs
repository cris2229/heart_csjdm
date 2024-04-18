using Heart_CSJDM.DataAccessLayer;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using Heart_CSJDM.Model;
using Blazorise;
using Heart_CSJDM.Helper;
using QRCoder;
using MySqlX.XDevAPI;
using Microsoft.JSInterop;
using System.Drawing.Imaging;
using System.Drawing;
using ZstdSharp.Unsafe;

namespace Heart_CSJDM.Components.Pages.Admin
{
    public partial class Clients 
    {
        public List<clients> clients { get; set; }
        private DataContext dataContext { get; set; }
        public clients ClientInfo { get; set; }
        public  List<Sector> _sector { get; set; }
        public List<Organization> _organization { get; set; }
        public List<Barangay> _barangay { get; set; }
        public TempVariable _temp { get; set; }

        private bool showModal = false;
        string inputValue = "";
        private bool modalVisible;
        public List<Lookup> _lookups { get; set; }
        public List<Lookup> _filterlookups { get; set; }
        protected override async Task OnInitializedAsync()
        {
            //var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            //var user = authState.User;

            //if (!user.Identity.IsAuthenticated)
            //{
            //    Navigation.NavigateTo("/Account/Login");
            //}

            dataContext = new DataContext();
            ClientInfo = new Model.clients();
            clients = new List<clients>();
            _sector = new List<Sector>();
            _organization = new List<Organization>();
            _barangay = new List<Barangay>();
            _temp = new TempVariable();
            _lookups = new List<Lookup>();
            _filterlookups = new List<Lookup>();
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                clients = dataContext.sp_getclients();
                _sector = dataContext.sp_getSector();
                _organization = dataContext.sp_getOrganization();
                _barangay = dataContext.sp_getBarangay();
                _lookups = dataContext.sp_getLookUpCriteria("AppointmentFilter");
                _temp.lookupID = _lookups.FirstOrDefault().LookupCriteriaID;
                StateHasChanged();
            }
        }
        private Task ShowModal(int clientid)
        {
            modalVisible = true;
            InitilizeClientDetails(clientid);
            //GenerateQRCodes();
            return Task.CompletedTask;
        }

        private Task HideModal()
        {
            modalVisible = false;

            return Task.CompletedTask;
        }
        public void InitilizeClientDetails(int clientid)
        {
            ClientInfo = dataContext.sp_getclientbyID(clientid);
            _temp.sector = _sector.FirstOrDefault(item => item.SectorID == ClientInfo.SectorID).Description;
            _temp.organization = _organization.FirstOrDefault(item => item.OrganizationID == ClientInfo.OrganizationID).Description;
            _temp.BarangayID = _barangay.FirstOrDefault(item => item.Name == ClientInfo.Address_Barangay).BarangayID;
        }
        private async Task SubmitForm()
        {
            ClientInfo.Address_Barangay = _barangay.FirstOrDefault(item => item.BarangayID == _temp.BarangayID).Name;
            ClientInfo.Address_District = _barangay.FirstOrDefault(item => item.BarangayID == _temp.BarangayID).District;
            dataContext.sp_UpdateClientinfo(ClientInfo);
            modalVisible = false;
            clients = dataContext.sp_getclients();
        }
        private async Task GenerateQRCodes()
        {
            if (!string.IsNullOrEmpty(ClientInfo.QREncriptedData))
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(ClientInfo.QREncriptedData, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(20);

                // Convert the Bitmap to Base64 string
                using (MemoryStream stream = new MemoryStream())
                {
                    qrCodeImage.Save(stream, ImageFormat.Png);
                    var bytes = stream.ToArray();
                    var base64 = Convert.ToBase64String(bytes);
                    var imgSrc = $"data:image/png;base64,{base64}";

                    // Update the HTML element with the generated QR code image
                    //await JSRuntime.InvokeVoidAsync("updateQRCode", imgSrc);
                }
            }
        }
        void OnInput(ChangeEventArgs e)
        {
            inputValue = e.Value.ToString();
            clients = GetFilteredSuggestions(inputValue);
        }

        List<clients> GetFilteredSuggestions(string input)
        {
            // Implement your own logic to filter suggestions based on input
            return dataContext.sp_getfilteredClientsdetails(input, _temp.lookupID, _temp.lookupCategoryID);
        }
        private void filterChange(ChangeEventArgs e)
        {
            _temp.lookupID = Convert.ToInt32(e.Value);
            _filterlookups = dataContext.sp_getfilteredCategory(_temp.lookupID);
        }
        private void filterChangev2(ChangeEventArgs e)
        {
            _temp.lookupCategoryID = Convert.ToInt32(e.Value);
            clients = GetFilteredSuggestions(inputValue);
        }
    }
}
