import { createContext, useContext, useEffect, useState } from "react";
import type { LoginPayloadContext } from "../types/authTypes";
import { apiFetch } from "../api/client";

type AuthContextType = {
  user: LoginPayloadContext;
  login: (user: LoginPayloadContext) => void;
  logout: () => void;
};

const AuthContext = createContext<AuthContextType | null>(null);

export const AuthProvider = ({ children }: { children: React.ReactNode }) => {
  const [user, setUser] = useState<LoginPayloadContext>({
    token: null,
    userId: null,
    userName: null,
    role: null,
    isAuthenticated: false,
  });

  useEffect(() => {
    apiFetch("/api/Auth/me")
      .then((data) => {
        setUser({
          token: null,
          userId: data.userId,
          userName: data.name,
          role: data.role,
          isAuthenticated: true,
        });
      })
      .catch(() => {});
  }, []);

  const login = (payload: LoginPayloadContext) => {
    setUser({
      token: null,
      userId: payload.userId,
      userName: payload.userName,
      role: payload.role,
      isAuthenticated: true,
    });
  };

  const logout = () => {
    setUser({
      token: null,
      userId: null,
      userName: null,
      role: null,
      isAuthenticated: false,
    });
  };
  return (
    <AuthContext.Provider value={{ user, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => {
  const ctx = useContext(AuthContext);
  if (!ctx) throw new Error("useAuth måste användas inom AuthProvider");
  return ctx;
};
