import { Link, useNavigate } from "react-router-dom";
import "./navigation-bar.css";
import { useAuth } from "../context/auth-context";
import { AuctionSearchBar } from "./search-bar";

export const Navbar = () => {
  const { user, logout } = useAuth();
  const navigate = useNavigate();
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
      {user.isAuthenticated && (
        <h3 onClick={() => navigate("/profile")} style={{ cursor: "pointer" }}>
          {user.userName}
        </h3>
      )}

      <AuctionSearchBar />
    </div>
  );
};
