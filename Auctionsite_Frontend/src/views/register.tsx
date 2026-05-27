import { useState } from "react";
import { useNavigate } from "react-router-dom";
import type { newUserPayload } from "../types/authTypes";
import { registerAPI } from "../api/authAPI";
import { useAuth } from "../context/auth-context";

export const Register = () => {
  const { fetchWithAuth } = useAuth();
  const [newUser, setNewUser] = useState<newUserPayload>({
    name: "",
    email: "",
    password: "",
  });
  const [confirmPassword, setConfirmPassword] = useState("");
  const [error, setError] = useState("");
  const navigate = useNavigate();

  const registerNewUser = async () => {
    if (!newUser) return;
    if (newUser.password !== confirmPassword) {
      setError("Lösenorden matchar inte");
      return;
    }
    if (
      newUser.email === "" ||
      newUser.name === "" ||
      newUser.password === ""
    ) {
      setError("Något fält är tomt");
      return;
    }
    await registerAPI(fetchWithAuth, newUser);
    navigate("/login");
  };
  return (
    <div className="form-view">
      <h3>Registrera ny användare</h3>
      <h4>Namn</h4>
      <input
        type="text"
        placeholder="Namn"
        onChange={(e) => setNewUser({ ...newUser, name: e.target.value })}
      ></input>
      <h4>Email</h4>
      <input
        type="text"
        placeholder="Email"
        onChange={(e) => setNewUser({ ...newUser, email: e.target.value })}
      ></input>
      <h4>Lösenord</h4>
      <input
        type="password"
        placeholder="Lösenord"
        onChange={(e) => setNewUser({ ...newUser, password: e.target.value })}
      ></input>
      <h4>Bekräfta lösenord</h4>
      <input
        type="password"
        placeholder="Bekräfta lösenord"
        onChange={(e) => setConfirmPassword(e.target.value)}
      ></input>
      {error && <p>{error}</p>}
      <div className="boolButtons">
        <button onClick={registerNewUser}>Registrera</button>
        <button onClick={() => navigate("/login")}>Avbryt</button>
      </div>
    </div>
  );
};
