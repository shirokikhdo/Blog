import { LOGIN_URL } from "../../services/commonService";
import { createUser } from "../../services/usersService";
import UserProfileCreation from "./UserProfileCreation";

const SignUp = () => {

    const userDefault = {
        id: 0,
        name: '',
        email: '',
        password: '',
        description: '',
        photo: ''
    }

    const signUpAction = (newUser) => {
        createUser(newUser);
    }

    const openLoginPage = () => {
        window.location.href = LOGIN_URL;
    }

    return(
        <div>
            <UserProfileCreation 
                user={userDefault} 
                setAction={signUpAction} />
            <button
                className="btn btn-link"
                onClick={openLoginPage}>
                    Sign In
            </button>
        </div>
    )
}

export default SignUp;