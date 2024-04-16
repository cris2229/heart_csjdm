window.getSession = function () {
    const decoded = sessionStorage.getItem('decodetext');
    DotNet.invokeMethodAsync('Heart_CSJDM', 'getSession', decoded)
        .then(result => {
            console.log('Response from C#: ' + result);
        })
        .catch(error => {
            console.error('Error calling C# method:', error);
        });
}