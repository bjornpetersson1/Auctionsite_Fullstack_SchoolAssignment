import { Route, Routes } from "react-router-dom";
import "./App.css";
import { AuctionsList } from "./views/auctions-list";
import { Navbar } from "./components/navigation-bar";
import { AuctionDetails } from "./views/auction-details";
import { Login } from "./views/login";
import { AuctionCreate } from "./views/auction-create";
import { Register } from "./views/register";
import { AdminPage } from "./views/admin";
import { AuthProvider } from "./context/auth-context";
import { AdminRoute, ProtectedRoute } from "./components/protected-routes";
import { AdminAuctionsList } from "./views/admin-auctions";
import { AdminUsersList } from "./views/admin-user-list";
import { ProfilePage } from "./views/profile";

function App() {
  return (
    <>
      <AuthProvider>
        <Navbar />
        <Routes>
          <Route path="/" element={<AuctionsList />} />
          <Route path="/auctions/:id" element={<AuctionDetails />} />
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Register />} />
          <Route element={<ProtectedRoute />}>
            <Route path="/create-auction" element={<AuctionCreate />} />
          </Route>
          <Route element={<ProtectedRoute />}>
            <Route path="/profile" element={<ProfilePage />} />
          </Route>
          <Route element={<AdminRoute />}>
            <Route path="/admin" element={<AdminPage />} />
          </Route>
          <Route element={<AdminRoute />}>
            <Route path="/admin/auctions" element={<AdminAuctionsList />} />
          </Route>
          <Route element={<AdminRoute />}>
            <Route path="/admin/users" element={<AdminUsersList />} />
          </Route>
        </Routes>
      </AuthProvider>
    </>
  );
}

export default App;
