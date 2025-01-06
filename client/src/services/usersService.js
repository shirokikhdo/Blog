import {sendRequestWithToken} from "./commonService"

export async function getUser() {
    const user = await sendRequestWithToken(window.config.accountUrl, 'GET');
    return user; 
}