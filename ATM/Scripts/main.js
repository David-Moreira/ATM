function AjaxRequest(method, url, logError, json, callBackSucess, callBackFailed) {
    let xhttp = new XMLHttpRequest();
    xhttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            callBackSucess(JSON.parse(xhttp.responseText));
        }
        else if (this.readyState == 4 && this.status != 200) {
            if (logError)
                console.log("Failed: ", xhttp.responseText);
            if (callBackFailed != null) {
                callBackFailed();
            }
        }
    };

    xhttp.open(method, url, true);
    xhttp.setRequestHeader("Content-Type", "application/json");
    xhttp.send(json);
}