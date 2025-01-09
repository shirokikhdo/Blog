import Login from "./components/users/Login";
import SignUp from "./components/users/SignUp";
import UserProfile from "./components/users/UserProfile";
import UserPublicView from "./components/users/UserPublicView";
import SearchUser from "./components/users/SearchUser";
import NewsForUser from "./components/news/NewsForUser";
import { LOGIN_URL, PROFILE_URL, SIGNUP_URL, ALLUSERS_URL, ALLNEWS_URL} from "./services/commonService";
import { Home } from "./components/Home";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
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
    path: ALLUSERS_URL,
    element: <SearchUser />
  },
  {
    path: `${ALLUSERS_URL}/:userId`,
    element: <UserPublicView />
  },
  {
    path: ALLNEWS_URL,
    element: <NewsForUser />
  }
];

export default AppRoutes;