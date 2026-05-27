import { Link } from "react-router-dom";
import "./navigation-bar.css";
import { useAuth } from "../context/auth-context";

export const Navbar = () => {
  const { user, logout } = useAuth();
  return (
    <div className="navbar">
      {user.role === "admin" && <Link to="/admin">Admin</Link>}
      <Link to="/">Auktioner</Link>
      {user.isAuthenticated && (
        <Link to="/create-auction">Skapa ny auktion</Link>
      )}
      {!user.isAuthenticated && <Link to="/login">Logga in</Link>}
      {!user.isAuthenticated && (
        <Link to="/register">Registrera ny användare</Link>
      )}
      {user.isAuthenticated && <button onClick={logout}>Logga ut</button>}
      {user.isAuthenticated && <h3>{user.userName}</h3>}
    </div>
  );
};
