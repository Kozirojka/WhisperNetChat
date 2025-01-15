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
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/unauthorized" element={<Unauthorized />} />

        <Route path="/" element={<Layout />}>
          
          <Route element={<RequireAuth allowedRoles={[ROLES.User]} />}>
            <Route index element={<Home />} /> 
          </Route>

          <Route element={<RequireAuth allowedRoles={[ROLES.Admin]} />}>
            <Route path="/admin" element={<Admin />} />
          </Route>

        </Route>

        <Route path="*" element={<Missing />} />
      </Routes>
    </>
  );
}

export default App;
