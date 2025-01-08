import Login from "./components/users/Login";
import SignUp from "./components/users/SignUp";
import UserProfile from "./components/users/UserProfile";
import UserPublicView from "./components/users/UserPublicView";
import { LOGIN_URL, PROFILE_URL, SIGNUP_URL } from "./services/commonService";

const AppRoutes = [
  {
    path: LOGIN_URL,
    element: <Login />
  },
  {
    path: PROFILE_URL,
    element: <UserProfile />
  },
  {
    path: SIGNUP_URL,
    element: <SignUp />
  },
  {
    path: '/all/:userId',
    element: <UserPublicView />
  }
];

export default AppRoutes;