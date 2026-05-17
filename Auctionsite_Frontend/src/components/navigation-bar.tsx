import { Link } from "react-router-dom";
import "./navigation-bar.css";
import { useAuth } from "../context/auth-context";

export const Navbar = () => {
  const { user } = useAuth();
  return (
    <div className="navbar">
      <Link to="/">Auctions</Link>
      <Link to="/login">Login</Link>
      <Link to="/register">Register new user</Link>
      {user.isAuthenticated && (
        <Link to="/create-auction">Create new auctions</Link>
      )}
      {user.role === "admin" && <Link to="/admin">Admin</Link>}
      <h3>{user.userName}</h3>
    </div>
  );
};
