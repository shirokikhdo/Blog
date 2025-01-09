import { PROFILE_URL } from "../../services/commonService";
import { deleteNews, updateNews } from "../../services/newsService";
import ImageComponent from "../ImageComponent";
import ModalButton from "../ModalButton"
import NewsCreation from "./NewsCreation";

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
            <div className="img-box">
                <ImageComponent base64String={imageStr} />
            </div>
            <div>
                <p>{date}</p>
                {text}
            </div>
        </div>
    )
}

export default News;