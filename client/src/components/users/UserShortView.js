import ImageComponent from "./../ImageComponent";

const UserShortView = ({user}) => {
    const userClick = (userId) => {
        window.location.href = `/all/${userId}`;
    }
    
    return(
        <div
            className="user-short" 
            onClick={() => userClick(user.id)}>
            <div className="user-short-img">
                <ImageComponent base64String={user.photo} />
            </div>
            <div className="user-short-data">
                <p>{user.name}</p>
                <p>{user.description}</p>
            </div>            
        </div>
    );
}

export default UserShortView;