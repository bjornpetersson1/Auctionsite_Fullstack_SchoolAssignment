import { createContext, useContext, useEffect, useState } from "react";
import type { LoginPayloadContext } from "../types/authTypes";

type AuthContextType = {
  user: LoginPayloadContext;
  login: (user: LoginPayloadContext) => void;
  logout: () => void;
  fetchWithAuth: (path: string, options?: RequestInit) => Promise<any>;
};

const AuthContext = createContext<AuthContextType | null>(null);

interface fetchOptions extends RequestInit {
  _retry?: boolean;
}

const BASE_URL = "https://localhost:5000";

export const AuthProvider = ({ children }: { children: React.ReactNode }) => {
  const [user, setUser] = useState<LoginPayloadContext>({
    token: null,
    userId: null,
    userName: null,
    role: null,
    isAuthenticated: false,
  });

  useEffect(() => {
    fetchWithAuth("/api/Auth/me")
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
  const fetchWithAuth = async (path: string, options: fetchOptions = {}) => {
    const response = await fetch(`${BASE_URL}${path}`, {
      ...options,
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
        ...options.headers,
      },
    });

    if (response.status === 401 && !options._retry) {
      const refreshUrl = `${BASE_URL}/api/Auth/refresh`;
      const refresh = await fetch(refreshUrl, {
        method: "POST",
        credentials: "include",
      });
      if (refresh.ok) {
        fetchWithAuth(refreshUrl, { ...options, _retry: true });
      } else {
        logout();
        return;
      }
    }

    if (!response.ok) {
      if (response.status === 403) {
        throw new Error("Du har inte behörighet att utföra denna åtgärd");
      }
      const body = await response.json().catch(() => null);
      throw new Error(body?.message ?? `HTTP ${response.status}`);
    }

    const text = await response.text();
    return text ? JSON.parse(text) : null;
  };
  return (
    <AuthContext.Provider value={{ user, login, logout, fetchWithAuth }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => {
  const ctx = useContext(AuthContext);
  if (!ctx) throw new Error("useAuth måste användas inom AuthProvider");
  return ctx;
};
