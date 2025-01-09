import ImageComponent from "../ImageComponent";

const News = ( { text, imageStr, date } ) => {
    return(
        <div className="news-item">
            <div>
                <p>{date}</p>
                {text}
            </div>
            <ImageComponent base64String={imageStr} />
        </div>
    )
}

export default News;