// Triggers a browser download from bytes fetched server-side via an authenticated HttpClient
// call. Needed because a plain NavigationManager.NavigateTo redirect straight to the WebApi
// can't carry the JWT bearer token the API requires - only our own HttpClient pipeline
// (JwtForwardingHandler) attaches it.
window.fileDownload = {
    saveAs: function (fileName, contentType, base64Data) {
        const byteChars = atob(base64Data);
        const byteNumbers = new Array(byteChars.length);
        for (let i = 0; i < byteChars.length; i++) {
            byteNumbers[i] = byteChars.charCodeAt(i);
        }
        const byteArray = new Uint8Array(byteNumbers);
        const blob = new Blob([byteArray], { type: contentType });

        const url = URL.createObjectURL(blob);
        const link = document.createElement("a");
        link.href = url;
        link.download = fileName;
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
        URL.revokeObjectURL(url);
    }
};
