import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import UserView from "./UserView";
import { getPublicUser } from "./../../services/usersService";
import { subscribeToUser } from "./../../services/usersService";

const UserPublicView = () => {
    const [user, setUser] = useState({
        id: 0,
        name: '',
        email: '',
        password: '',
        description: '',
        photo: '',
      });

      const params = useParams();
      const userId = params.userId;

      const subscribeClick = () => {
        subscribeToUser(userId);
      }
    
      useEffect(() => {
        const fetchUser = async () => {
          const data = await getPublicUser(userId);
          setUser(data);
        };
    
        fetchUser();
      }, []);

    return <UserView user={user} />
}

export default UserPublicView;