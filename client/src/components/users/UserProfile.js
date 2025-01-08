import React, { useEffect, useState } from 'react';
import { exitFromProfile, getUser, updateUser } from '../../services/usersService';
import ImageComponent from '../ImageComponent';
import ModalButton from '../ModalButton';
import UserProfileCreation from './UserProfileCreation';
import NewsByUser from '../news/NewsByUser';

const UserProfile = () => {
  const [user, setUser] = useState({
    name: '',
    email: '',
    password: '',
    description: '',
    photo: '',
  });

  useEffect(() => {
    const fetchUser = async () => {
      const data = await getUser();
      setUser(data);
    };

    fetchUser();
  }, []);

  const updateUserView = (newUser) => {
    setUser(newUser);
    updateUser(newUser);
  }

  return (
    <div>
      <h2>User Profile</h2>
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
          <div style={{
            display: 'flex',
            justifyContent: 'space-around'}}>
            <ModalButton
              modalContent = {<UserProfileCreation
                            user={user}
                            setAction={updateUserView} />}
              title = "Редактирование профиля"
              btnName="Edit"/>
            <button 
              className='btn btn-secondary'
              onClick={() => exitFromProfile()}>
                Exit
            </button>
          </div>
        </div>
      </div>
      <NewsByUser userId = {user.id}/>
    </div>
  );
};

export default UserProfile;