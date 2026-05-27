import { useState } from "react";
import type { loginPayload } from "../types/authTypes";
import { useNavigate } from "react-router-dom";
import { loginUserAPI } from "../api/authAPI";
import { useAuth } from "../context/auth-context";

export const Login = () => {
  const { fetchWithAuth } = useAuth();
  const [loginLoad, setLoginLoad] = useState<loginPayload>({
    email: "",
    password: "",
  });
  const [error, setError] = useState("");
  const navigate = useNavigate();
  const { login } = useAuth();

  const loginUser = async () => {
    try {
      const response = await loginUserAPI(fetchWithAuth, loginLoad);
      console.log(response);
      login({
        token: null,
        userId: response.id,
        userName: response.name,
        role: response.role,
        isAuthenticated: true,
      });
      navigate("/");
    } catch (e) {
      setError(e instanceof Error ? e.message : "Login failed");
    }
  };

  return (
    <div className="form-view">
      <h3>Login</h3>
      <h4>Email</h4>
      <input
        type="text"
        placeholder="Email"
        onChange={(e) => setLoginLoad({ ...loginLoad, email: e.target.value })}
      ></input>
      <h4>Password</h4>
      <input
        type="password"
        placeholder="Lösenord"
        onChange={(e) =>
          setLoginLoad({ ...loginLoad, password: e.target.value })
        }
      ></input>
      {error && <p>{error}</p>}
      <button onClick={loginUser}>Logga in</button>
    </div>
  );
};
