import { PROFILE_URL, sendRequestWithToken } from "./commonService";

export async function getUser() {
    const user = await sendRequestWithToken(window.config.accountUrl, 'GET');
    return user; 
}

export async function updateUser(user) {
    user.photo = user.photo.toString()
    const newUser = await sendRequestWithToken(window.config.accountUrl, 'PATCH', user);
    window.location.href = PROFILE_URL;
    return newUser;
}