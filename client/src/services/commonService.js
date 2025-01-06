const BASE_URL = "login";

export const LOGIN_URL = '/login';

function sendRequest(url, successAction, errorAction){
    fetch(url)
        .then(response => {
            if(response.status === 401) {
                window.location.href = BASE_URL;
            } else {
                successAction();
            }
        })
        .catch(error => {
            errorAction();
        });
}

export async function getToken(login, password) {
    const url = window.config.accountUrl + "/token";
    const token = await sendAuthenticatedRequest(url, "POST", login, password);
    console.log(token);
}

async function sendAuthenticatedRequest(url, method, username, password, data) {
    var headers = new Headers();
    headers.set('Authorization', 'Basic ' + btoa(username + ':' + password));
    
    if (data) {
      headers.set('Content-Type', 'application/json');
    }

    var requestOptions = {
      method: method,
      headers: headers,
      body: data ? JSON.stringify(data) : undefined
    };
    var resultFetch = await fetch(url, requestOptions);
    
    if (resultFetch.ok) {
        const result = await resultFetch.json();
        return result;
    }
    else {
        throw new Error('Ошибка ' + resultFetch.status + ': ' + resultFetch.statusText);
      }
}