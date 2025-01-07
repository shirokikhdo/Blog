import React, { useEffect, useState } from 'react';
import { getUser } from '../../services/usersService';
import ImageComponent from '../ImageComponent';
import ModalButton from '../ModalButton';
import UserProfileCreation from './UserProfileCreation';

const UserProfile = () => {
  const [user, setUser] = useState({
    name: '',
    email: '',
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

  const updateUser = (newUser) => {
    setUser(newUser);
    updateUser(newUser);
  }

  return (
    <div>
        <h2>User Profile</h2>
        <p>Name: {user.name}</p>
        <p>Email: {user.email}</p>
        <p>Password: {user.password}</p>
        <p>Description: {user.description}</p>
        <ImageComponent byteArray={user.photo} />
        <ModalButton
          modalContent = {<UserProfileCreation
                            user={user}
                            setAction={updateUser}
                            btnName="фывфыв" />}
          title = "Редактирование профиля"/>
    </div>
  );
};

export default UserProfile;