import ImageComponent from "../ImageComponent";

const NewsView = ({date, text, imageStr}) => {
    return (
        <div>
            <div className="img-box">
                <ImageComponent base64String={imageStr}/>
            </div>
            <div>
                <p>{date}</p>
                <p>{text}</p>
            </div>
        </div>);
}

export default NewsView;