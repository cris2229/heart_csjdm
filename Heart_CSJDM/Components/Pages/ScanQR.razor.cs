using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using Mono.TextTemplating;

namespace Heart_CSJDM.Components.Pages
{
    public partial class ScanQR
    {
        private ElementReference inputElement;
        
        protected override async Task OnInitializedAsync()
        {
            //var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            //var user = authState.User;

            //if (!user.Identity.IsAuthenticated)
            //{
            //    Navigation.NavigateTo("/Account/Login");
            //}
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("initializePage");
            }
        }

        private  async Task test()
        {
            string inputValue = await JSRuntime.InvokeAsync<string>("getValue", inputElement);
            ProtectedSessionStore.SetAsync("QRdecoded", inputValue);
            Navigation.NavigateTo("/Client/");
            
        }
        [JSInvokable]
        public string MyMethod()
        {
            // Method logic
            return "Result";
        }
        public void reloadData(ChangeEventArgs e)
        {
            // Reload data or trigger actions based on the changed input value
            inputValue = e.Value.ToString();
            Console.WriteLine("Input value changed: " + inputValue);
            // Perform data reloading or other actions here
        }
    }
}
