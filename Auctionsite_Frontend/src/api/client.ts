export type ApiFetch = (path: string, options?: RequestInit) => Promise<any>;

// const BASE_URL = "https://localhost:5000";

// interface ApiFetchOptions extends RequestInit {
//   _retry?: boolean;
// }

// export async function apiFetch(path: string, options: ApiFetchOptions = {}) {
//   const response = await fetch(`${BASE_URL}${path}`, {
//     ...options,
//     credentials: "include",
//     headers: {
//       "Content-Type": "application/json",
//       ...options.headers,
//     },
//   });

//   if (response.status === 401 && !options._retry) {
//     const path = `${BASE_URL}/api/Auth/refresh`;
//     const refresh = await fetch(path, {
//       method: "POST",
//       credentials: "include",
//     });
//     if (refresh.ok) {
//       apiFetch(path, { ...options, _retry: true });
//     } else {
//       throw new Error("SESSION_EXPIRED");
//     }
//   }

//   if (!response.ok) {
//     const body = await response.json().catch(() => null);
//     throw new Error(body?.message ?? `HTTP ${response.status}`);
//   }

//   return response.json();
// }
