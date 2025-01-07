import React, { useState } from 'react';
import ImageComponent from '../ImageComponent';
import ImageUploader from '../ImageUploader';

const UserProfileCreation = ({ user, setAction }) => {
  const [username, setUsername] = useState(user.name);
  const [email, setEmail] = useState(user.email);
  const [password, setPassword] = useState();
  const [description, setDescription] = useState(user.description);
  const [photo, setPhoto] = useState(user.photo);

  const endCreate = () => {
    if(password.length === 0)
      return;
    const newUser = {
      name: username,
      email: email,
      password: password,
      description: description,
      photo: photo,
    };
    setAction(newUser);
  }

  return (
    <div style={{display: 'flex', flexDirection: 'column'}}>
        <h2>User Profile</h2>
        <p>Name</p>
        <input 
          type="text"
          onChange={e => setUsername(e.target.value)}
          defaultValue={username}/>
        <p>Email</p>
        <input 
          type="text"
          onChange={e => setEmail(e.target.value)}
          defaultValue={email}/>
        <p>Password</p>
        <input 
          type="text"
          onChange={e => setPassword(e.target.value)}
          defaultValue={password}/>
        <p>Description</p>
        <textarea
          onChange={e => setDescription(e.target.value)}
          defaultValue={description}/>
        <ImageUploader byteImageAction={(bytes) => setPhoto(bytes)}/>
        
        <ImageComponent byteArray={user.photo} />
        <button onClick={endCreate}>Ok</button>
    </div>
  );
};

export default UserProfileCreation;