import { useState } from "react";
import type { loginPayload } from "../types/authTypes";
import { useNavigate } from "react-router-dom";
import { loginUserAPI } from "../api/authAPI";
import { useAuth } from "../context/auth-context";

export const Login = () => {
  const [loginLoad, setLoginLoad] = useState<loginPayload>({
    email: "",
    password: "",
  });
  const [error, setError] = useState("");
  const navigate = useNavigate();
  const { login } = useAuth();

  const loginUser = async () => {
    try {
      const response = await loginUserAPI(loginLoad);
      console.log(response);
      login({
        token: null,
        userId: null,
        userName: response.name,
        role: null,
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
        placeholder="Password"
        onChange={(e) =>
          setLoginLoad({ ...loginLoad, password: e.target.value })
        }
      ></input>
      {error && <p>{error}</p>}
      <button onClick={loginUser}>Login</button>
    </div>
  );
};
