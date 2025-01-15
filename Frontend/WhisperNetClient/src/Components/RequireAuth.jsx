import { useLocation, Navigate, Outlet } from "react-router-dom";
import useAuth from "../Hooks/useAuth";

const RequireAuth = ({ allowedRoles }) => {
    const { auth } = useAuth();
    const location = useLocation();

    console.log("=== RequireAuth invoked ===");
    console.log("Allowed roles:", allowedRoles);
    console.log("Current auth object:", auth);

    if (auth?.roles?.some(role => allowedRoles?.includes(role))) {
        console.log("User has the required role(s). Rendering the protected component...");
        return <Outlet />;
    }

    // Якщо користувач залогінений, але не має необхідних ролей
    if (auth?.user) {
        console.log("User is logged in but lacks the required role(s). Redirecting to /unauthorized...");
        return <Navigate to="/unauthorized" state={{ from: location }} replace />;
    }

    // Якщо користувач взагалі не залогінений
    console.log("User is not logged in. Redirecting to /login...");
    return <Navigate to="/login" state={{ from: location }} replace />;
}

export default RequireAuth;
