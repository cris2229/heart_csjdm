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

namespace Heart_CSJDM.Components.Pages.Admin
{
    public partial class Clients 
    {
        public List<clients> clients { get; set; }
        private DataContext dataContext { get; set; }
        public clients ClientInfo { get; set; }
        private bool showModal = false;

        private bool modalVisible;
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
            clients = dataContext.sp_getclients();
        }
        private Task ShowModal(int clientid)
        {
            modalVisible = true;
            InitilizeClientDetails(clientid);
            GenerateQRCodes();
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
        }
        private async Task SubmitForm()
        {
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
                    await JSRuntime.InvokeVoidAsync("updateQRCode", imgSrc);
                }
            }
        }

    }
}
