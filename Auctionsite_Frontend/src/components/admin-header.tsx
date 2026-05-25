import { Link } from "react-router-dom";
import "./admin-header.css";
import { useAuth } from "../context/auth-context";

export const AdminNavbar = () => {
  const { user } = useAuth();
  return (
    <div className="admin-navbar">
      {user.role === "admin" && <Link to="/admin/auctions">Auctions</Link>}
      {user.role === "admin" && <Link to="/admin/users">Users</Link>}
    </div>
  );
};
