import { useEffect, useState } from "react";
import { getNewsByUser } from "../../services/newsService";
import NewsView from './NewsView';

const NewsByUser = ( { userId } ) => {
    const [news, setNews] = useState([]);
    
    const getAllNews = async () => {
        if(!userId || userId === 0)
            return;
        const allNews = await getNewsByUser(userId);
        setNews(allNews);
    }

    useEffect( () => {
        getAllNews();
    }, [userId]);

    return(
        <div>
            {news.map((el, key) => {
                return <NewsView
                        key = {key}
                        text = {el.text}
                        imageStr = {el.image}
                        date = {el.postDate} />
            })}
        </div>
    )
}

export default NewsByUser;