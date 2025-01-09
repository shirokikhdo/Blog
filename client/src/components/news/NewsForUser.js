import { useState, useEffect } from "react";
import { getNews } from "../../services/newsService";
import NewsView from "./NewsView";

export const NewsForUser = () => {
    const [news, setNews] = useState([]);

    const getAllNews = async () => {
        const allNews = await getNews();
        setNews(allNews);
    }

    useEffect ( ()=> {
        getAllNews();
    },[]);

    return (
        <div>
            {news.map((el, key) => {
                return <NewsView 
                            key={key} 
                            date={el.postDate} 
                            text={el.text} 
                            imageStr={el.image}/>
            })}
        </div>
    )
}

export default NewsForUser;