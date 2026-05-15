const BASE_URL = "https://localhost:5000";

export async function apiFetch(path: string, options: RequestInit = {}) {
  const token = localStorage.getItem("token");
  const headers: HeadersInit = {
    "Content-Type": "application/json",
    ...options.headers,
  };

  if (token) {
    (headers as Record<string, string>)["Authorization"] = `Bearer ${token}`;
  }

  const response = await fetch(`${BASE_URL}${path}`, { ...options, headers });

  if (!response.ok) {
    throw new Error(`HTTP ${response.status}`);
  }

  return response.json();
}
