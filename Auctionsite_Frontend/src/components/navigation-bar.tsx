import { Link } from "react-router-dom";
import "./navigation-bar.css";
import { useAuth } from "../context/auth-context";

export const Navbar = () => {
  const { user, logout } = useAuth();
  return (
    <div className="navbar">
      {user.role === "admin" && <Link to="/admin">Admin</Link>}
      <Link to="/">Auctions</Link>
      {user.isAuthenticated && (
        <Link to="/create-auction">Create new auctions</Link>
      )}
      {!user.isAuthenticated && <Link to="/login">Login</Link>}
      {!user.isAuthenticated && <Link to="/register">Register new user</Link>}
      {user.isAuthenticated && <button onClick={logout}>Logout</button>}
      {user.isAuthenticated && <h3>{user.userName}</h3>}
    </div>
  );
};
