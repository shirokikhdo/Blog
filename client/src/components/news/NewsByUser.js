import { useEffect, useState } from "react";
import { getNewsByUser } from "../../services/newsService";
import News from "./News";

const NewsByUser = ( { userId } ) => {
    const [news, setNews] = useState([]);
    const [updateUser, setUpdateUser] = useState(0);
    
    const getAllNews = async () => {
        if(!userId || userId === 0)
            return;
        const allNews = await getNewsByUser(userId);
        setNews(allNews);
    }

    useEffect( () => {
        getAllNews();
    }, [userId, updateUser]);

    return(
        <div>
            {news.map((el, key) => {
                return <News
                        key = {key}
                        id = {el.id}
                        text = {el.text}
                        imageStr = {el.image}
                        date = {el.postDate}
                        updateAction={setUpdateUser} />
            })}
        </div>
    )
}

export default NewsByUser;