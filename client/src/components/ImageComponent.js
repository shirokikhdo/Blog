import React from 'react';

const ImageComponent = ({ base64String }) => {
  
  if (base64String === null 
      || base64String === ""
      || base64String === undefined) 
    return <div></div>;

  const imageUrl = `data:image/jpeg;base64,${base64String}`;

  return <img style={{width: '100%'}} src={imageUrl} alt="Image" />;
};

export default ImageComponent;