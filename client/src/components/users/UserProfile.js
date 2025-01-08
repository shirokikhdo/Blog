import React, { useEffect, useState } from 'react';
import { exitFromProfile, getUser, updateUser } from '../../services/usersService';
import ImageComponent from '../ImageComponent';
import ModalButton from '../ModalButton';
import UserProfileCreation from './UserProfileCreation';

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
        <p>Name: {user.name}</p>
        <p>Email: {user.email}</p>
        <p>Description: {user.description}</p>
        <ImageComponent base64String={user.photo} />
        <ModalButton
          modalContent = {<UserProfileCreation
                            user={user}
                            setAction={updateUserView} />}
          title = "Редактирование профиля"
          btnName="Open Modal"/>
        <button 
          className='btn btn-secondary'
          onClick={() => exitFromProfile()}>
            Exit
        </button>
    </div>
  );
};

export default UserProfile;