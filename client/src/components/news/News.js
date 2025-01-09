import { deleteNews, updateNews } from "../../services/newsService";
import ModalButton from "../ModalButton"
import NewsCreation from "./NewsCreation";
import NewsView from "./NewsView";

const News = ( { id, text, imageStr, date, updateAction } ) => {

    const updateNewsView = async (news) => {
        await updateNews(news);
        updateAction();
    }

    const deleteNewsView = async () => {
        await deleteNews(id);
        updateAction();
    }
    
    return(
        <div className="news-item">
            <div className="news-actions">
                <ModalButton
                    modalContent = {<NewsCreation
                                        id = {id}
                                        oldText={text}
                                        oldImage={imageStr}
                                        setAction={updateNewsView} />}
                    title = "Edit post"
                    btnName="Edit"/>
                <button 
                    className='btn btn-danger'
                    onClick={() => deleteNewsView()}>
                        Delete
                </button>
            </div>
            <NewsView
                date={date}
                text={text}
                imageStr={imageStr} />
        </div>
    )
}

export default News;