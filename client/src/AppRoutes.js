import Login from "./components/users/Login";
import { LOGIN_URL } from "./services/commonService";

const AppRoutes = [
  {
    path: LOGIN_URL,
    element: <Login />
  }
];

export default AppRoutes;