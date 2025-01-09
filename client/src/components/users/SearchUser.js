import { useState } from "react";
import { getUsersByName } from "../../services/usersService";
import UserShortView from "./UserShortView";
import { isUserOnline, LOGIN_URL } from "../../services/commonService";

const UserSearch = () => {
    const [users, setUsers] = useState([]);

    if(!isUserOnline())
        window.location.href = LOGIN_URL;

    const getUsers = async (username) => {
        if(username === '')
            return;

        const allUsers = await getUsersByName(username);
        setUsers(allUsers);
    }

    return(
        <div>
            <input 
                type='text'
                onChange={e => getUsers(e.target.value)}/>
            {users.map((x, key) => 
                <UserShortView 
                    key={key} 
                    user={x} />)}
        </div>
    );
}

export default UserSearch;