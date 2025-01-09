import { LOGIN_URL, PROFILE_URL, sendRequestWithToken, clearStore } from "./commonService";

export async function getUser() {
    const user = await sendRequestWithToken(window.config.accountUrl, 'GET');
    return user; 
}

export async function getPublicUser(userId) {
    const url = `${window.config.usersUrl}/${userId}`;
    const user = await sendRequestWithToken(url, 'GET');
    return user; 
}

export async function updateUser(user) {
    user.photo = user.photo.toString();
    const newUser = await sendRequestWithToken(window.config.accountUrl, 'PATCH', user);
    window.location.href = PROFILE_URL;
    return newUser;
}

export async function createUser(user) {
    user.photo = user.photo.toString();
    const newUser = await sendRequestWithToken(window.config.accountUrl, 'POST', user, false);
    window.location.href = PROFILE_URL;
    return newUser;
}

export function exitFromProfile() {
    const answer = window.confirm("Are your sure?")
    if(!answer)
        return;
    clearStore();
    window.location.href = LOGIN_URL;
}

export async function getUsersByName(username) {
    const url = `${window.config.usersUrl}/all/${username}`;
    const users = await sendRequestWithToken(url, 'GET');
    return users;
}