const BASE_URL = "login";
const TOKEN_NAME = 'Token';
const ISONLINE_NAME = 'ONLINE';

export const LOGIN_URL = '/login';
export const PROFILE_URL = '/profile';
export const SIGNUP_URL = '/signup';
export const ALLUSERS_URL = '/all';
export const ALLNEWS_URL = '/allnews';

export async function getToken(login, password) {
    const url = window.config.accountUrl + "/token";
    const token = await sendAuthenticatedRequest(url, "POST", login, password);
    localStorage.setItem(TOKEN_NAME, token.accessToken);
    localStorage.setItem(ISONLINE_NAME, '1');
    window.location.href = PROFILE_URL;
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

export async function sendRequestWithToken(url, method, data, withToken = true) {
    var headers = new Headers();

    if (withToken){
      const token = localStorage.getItem(TOKEN_NAME);
      headers.set('Authorization', `Bearer ${token}`);
    }

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
      try {
        const result = await resultFetch.json();
        return result;
      }
      catch {
        return;
      }
    }
    else {
        errorRequest(resultFetch.status);
      }
}

function errorRequest(status) {
    if (status === 401){
      window.location.href = BASE_URL;
      clearStore();
    }
  }

export function clearStore() {
  localStorage.clear();
}

export function isUserOnline() {
  return localStorage.getItem(ISONLINE_NAME) === '1';
}