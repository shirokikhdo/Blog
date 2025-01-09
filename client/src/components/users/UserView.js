import ImageComponent from '../ImageComponent';
import NewsByUser from '../news/NewsByUser';
import { createNews } from '../../services/newsService';
import { PROFILE_URL } from '../../services/commonService';
import ModalButton from './../ModalButton';
import NewsCreation from '../news/NewsCreation';

const UserView = ( { user } ) => {

    const addNewNews = async (news) => {
        await createNews(news);
        window.location.href = PROFILE_URL;
      }

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
            <ModalButton
                modalContent = {<NewsCreation
                                    id = {0}
                                    oldText={''}
                                    oldImage={''}
                                    setAction={addNewNews} />}
                title = "New post"
                btnName="New"/>
            <NewsByUser userId = {user.id}/>
        </div>
    );
}

export default UserView;