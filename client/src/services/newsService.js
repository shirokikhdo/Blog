import {sendRequestWithToken} from "./commonService";

export async function getNewsByUser(userId) {
    const url = `${window.config.newsUrl}/${userId}`;
    const allNews = await sendRequestWithToken(url, 'GET');
    return allNews;
}

export async function getNews() {
    const allNews = await sendRequestWithToken(window.config.newsUrl, 'GET');
    return allNews;
}

export async function createNews(news) {
    news.image = news.image.toString();
    const newNews = await sendRequestWithToken(window.config.newsUrl, 'POST', news);
    return newNews;
}

export async function updateNews(news) {
    news.image = news.image.toString();
    const updateNews = await sendRequestWithToken(window.config.newsUrl, 'PATCH', news);
    return updateNews;
}

export async function deleteNews(newsId) {
    const url = `${window.config.newsUrl}/${newsId}`;
    await sendRequestWithToken(url, 'DELETE');
}