import ImageComponent from "../ImageComponent";

const News = ( { text, imageStr, date } ) => {
    return(
        <div style={{
            backgroundColor: '#0365d630', 
            margin: '10px', 
            padding: '10px', 
            borderRadius: '15px'}}>
            <div>
                <p>{date}</p>
                {text}
            </div>
            <ImageComponent base64String={imageStr} />
        </div>
    )
}

export default News;