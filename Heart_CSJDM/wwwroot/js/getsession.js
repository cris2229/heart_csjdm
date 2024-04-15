window.getSession = function () {
    const decoded = sessionStorage.getItem('decodetext');
    console.log(decoded);
}