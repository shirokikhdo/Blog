import Login from "./components/users/Login";
import UserProfile from "./components/users/UserProfile";
import { LOGIN_URL, PROFILE_URL } from "./services/commonService";

const AppRoutes = [
  {
    path: LOGIN_URL,
    element: <Login />
  },
  {
    path: PROFILE_URL,
    element: <UserProfile />
  }
];

export default AppRoutes;