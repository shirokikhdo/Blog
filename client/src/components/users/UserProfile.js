import React, { useEffect, useState } from 'react';
import { getUser } from '../../services/usersService';
import ImageComponent from '../ImageComponent';

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

  return (
    <div>
        <h2>User Profile</h2>
        <p>Name: {user.name}</p>
        <p>Email: {user.email}</p>
        <p>Description: {user.description}</p>
        <ImageComponent byteArray={user.photo} />
    </div>
  );
};

export default UserProfile;