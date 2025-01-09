import { useState, useEffect } from "react";
import { getNewsByUser } from "../../services/newsService";
import News from "./News";

const NewsProfileView = ({userId}) => {
    const [news, setNews] = useState([]);

    const getAllNews = async () => {
        if (userId === 0) return;
        const allNews = await getNewsByUser(userId);
        setNews(allNews);
    }

    useEffect ( ()=> {
        getAllNews();
    },[userId]);

    return (
        <div>
            {news.map((el, key) => {
                return <News 
                            key={key} 
                            id = {el.id}
                            text = {el.text} 
                            imageStr={el.image} 
                            date={el.postDate}
                            updateAction={getAllNews}
                />
            })}
        </div>
    )
}

export default NewsProfileView;