import {sendRequestWithToken} from "./commonService";

export async function getNewsByUser(userId) {
    const url = `${window.config.newsUrl}/${userId}`;
    const allNews = await sendRequestWithToken(url, 'GET');
    return allNews;
}