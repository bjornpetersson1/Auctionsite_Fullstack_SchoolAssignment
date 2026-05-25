import type { loginPayload, newUserPayload } from "../types/authTypes";
import { apiFetch } from "./client";

export const registerAPI = async (user: newUserPayload) => {
  return await apiFetch("/api/Auth/register", {
    method: "POST",
    body: JSON.stringify(user),
  });
};

export const loginUserAPI = async (user: loginPayload) => {
  const response = await apiFetch("/api/Auth/login", {
    method: "POST",
    body: JSON.stringify(user),
  });
  return response;
};

export const getNameById = async (id: number) => {
  const response = await apiFetch(`/api/Auth/${id}/user-name`, {
    method: "GET",
  });
  return response;
};
