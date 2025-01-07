import React from 'react';

const ImageComponent = ({ byteArray }) => {
  
  if (byteArray === null)
    return <image alt='Image'/>;

  const base64String = btoa(String.fromCharCode(...byteArray));
  const imageUrl = `data:image/jpeg;base64,${base64String}`;

  return <image src={imageUrl} alt='Image' />;
};

export default ImageComponent;