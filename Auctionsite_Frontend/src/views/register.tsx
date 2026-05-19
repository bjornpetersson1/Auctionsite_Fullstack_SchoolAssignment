import { useState } from "react";
import { useNavigate } from "react-router-dom";
import type { newUserPayload } from "../types/authTypes";
import { registerAPI } from "../api/authAPI";

export const Register = () => {
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
      setError("Passwords do not match");
      return;
    }
    if (
      newUser.email === "" ||
      newUser.name === "" ||
      newUser.password === ""
    ) {
      setError("Some fields are empty");
      return;
    }
    await registerAPI(newUser);
    navigate("/login");
  };
  return (
    <div className="form-view">
      <h3>Register new user</h3>
      <h4>Name</h4>
      <input
        type="text"
        placeholder="Name"
        onChange={(e) => setNewUser({ ...newUser, name: e.target.value })}
      ></input>
      <h4>Email</h4>
      <input
        type="text"
        placeholder="Email"
        onChange={(e) => setNewUser({ ...newUser, email: e.target.value })}
      ></input>
      <h4>Password</h4>
      <input
        type="password"
        placeholder="Password"
        onChange={(e) => setNewUser({ ...newUser, password: e.target.value })}
      ></input>
      <h4>Confirm password</h4>
      <input
        type="password"
        placeholder="Confirm password"
        onChange={(e) => setConfirmPassword(e.target.value)}
      ></input>
      {error && <p>{error}</p>}
      <button onClick={registerNewUser}>Register</button>
      <button onClick={() => navigate("/login")}>Cancel</button>
    </div>
  );
};
