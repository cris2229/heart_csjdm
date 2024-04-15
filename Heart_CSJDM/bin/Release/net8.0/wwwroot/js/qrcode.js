window.updateQRCode = function (imgSrc) {
    var qrCodeDiv = document.getElementById('qrcode');
    qrCodeDiv.innerHTML = `<img src="${imgSrc}" alt="QR Code" />`;
}