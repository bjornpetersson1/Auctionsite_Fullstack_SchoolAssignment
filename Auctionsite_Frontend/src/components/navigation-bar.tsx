import { Link } from "react-router-dom";
import "./navigation-bar.css";

export const Navbar = () => {
  return (
    <div className="navbar">
      <Link to="/">Auctions</Link>
      <Link to="/auctions/:id">Auction details</Link>
      <Link to="/login">Login</Link>
      <Link to="/register">Register new user</Link>
      <Link to="/create-auction">Create new auctions</Link>
      <Link to="/admin">Admin</Link>
    </div>
  );
};
