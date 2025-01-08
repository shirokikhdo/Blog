import { useEffect, useState } from "react";
import { getNewsByUser } from "../../services/newsService";
import News from "./News";

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
                return <News
                        key = {key}
                        text = {el.text}
                        imageStr = {el.imageStr}
                        date = {el.postDate} />
            })}
        </div>
    )
}

export default NewsByUser;