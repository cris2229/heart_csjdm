window.initializePage = function () {
    console.log('Page loaded');
    function onScanSuccess(decodedText, decodedResult) {
        // Handle on success condition with the decoded text or result.
        console.log(`Scan result: ${decodedText}`, decodedResult);
        // ...
       /* var decodedText1 = document.getElementById("txtdecode");*/
        //decodedText1.value = decodedText;
        /*DotNet.invokeMethodAsync('Heart_CSJDM', 'refresh');*/
        //var btnredirect = document.getElementById("btnredirect");
        //btnredirect.hidden = false;
        sessionStorage.setItem('decodetext', decodedText);
        location.href = "/Client/" ;
        //DotNet.invokeMethodAsync('Heart_CSJDM', 'Heart_CSJDM.Components.Pages.ScanQR.MyMethod')
        //    .then(data => {
        //        console.log('Razor method executed successfully');
        //        // You can handle the result here if needed
        //    })
        //    .catch(error => {
        //        console.error('Error executing Razor method:', error);
        //    });
        html5QrcodeScanner.clear();
        // ^ this will stop the scanner (video feed) and clear the scan area.
    }
    var html5QrcodeScanner = new Html5QrcodeScanner(
        "reader", { fps: 10, qrbox: 250 });
    html5QrcodeScanner.render(onScanSuccess);

    window.getValue = (element) => {
        // Access the input element using the reference and return its value
        return element.value;
    };
};