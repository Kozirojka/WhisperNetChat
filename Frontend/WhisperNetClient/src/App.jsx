import Register from "./Components/Register/Register";
import Login from "./Components/login/Login";
import { Routes, Route } from "react-router-dom";
import Layout from "./Components/Layout";
import Home from "./Components/Home/Home";
import Admin from "./Components/Admin/Admin";
import Missing from "./Components/Missing";
import RequireAuth from "./Components/RequireAuth";
import Unauthorized from "./Components/Unauthorized";

const ROLES = {
  User: "User",
  Admin: "Admin",
};
function App() {
  return (
    <>
      <Routes>
        <Route path="/" element={<Layout />} />

        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/unauthorized" element={<Unauthorized />} />

        <Route
          element={<RequireAuth allowedRoles={[ROLES.User, ROLES.Admin]} />}
        >
          {/* Робимо батьківський Layout */}
          <Route path="/" element={<Layout />}>
            <Route index element={<Home />} /> {/* на "/" */}
            
            <Route
              path="admin"
              element={
                <RequireAuth allowedRoles={[ROLES.Admin]}>
                  <Admin />
                </RequireAuth>
              }
            />
          </Route>
        </Route>
        <Route path="*" element={<Missing />} />
      </Routes>
    </>
  );
}

export default App;
