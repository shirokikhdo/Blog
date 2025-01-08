import ImageComponent from '../ImageComponent';
import NewsByUser from '../news/NewsByUser';

const UserView = ( { user } ) => {
    return(
        <div>
            <h2>{user.name}</h2>
            <div 
                style={{
                display: 'flex', 
                flexDirection: 'row', 
                justifyContent: 'center'}}>
                <div className='image-box' style={{width: '50%'}}>
                    <ImageComponent base64String={user.photo} />
                </div>
                <div className='user-data' style={{margin: '0 10%'}}>
                    <p>Name: {user.name}</p>
                    <p>Email: {user.email}</p>
                    <p>Description: {user.description}</p>
                </div>
            </div>
            <NewsByUser userId = {user.id}/>
        </div>
    );
}

export default UserView;