window.updateQRCode = function (imgSrc) {
    var qrCodeDiv = document.getElementById('qrcode');
    qrCodeDiv.innerHTML = `<img style="width:100px;height:100px" src="${imgSrc}" alt="QR Code" />`;
}