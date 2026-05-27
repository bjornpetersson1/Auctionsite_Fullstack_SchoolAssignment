import type { loginPayload, newUserPayload } from "../types/authTypes";
import { type ApiFetch } from "./client";

export const registerAPI = async (
  fetchWithAuth: ApiFetch,
  user: newUserPayload,
) => {
  return await fetchWithAuth("/api/Auth/register", {
    method: "POST",
    body: JSON.stringify(user),
  });
};

export const loginUserAPI = async (
  fetchWithAuth: ApiFetch,
  user: loginPayload,
) => {
  const response = await fetchWithAuth("/api/Auth/login", {
    method: "POST",
    body: JSON.stringify(user),
  });
  return response;
};

export const getNameById = async (fetchWithAuth: ApiFetch, id: number) => {
  const response = await fetchWithAuth(`/api/Auth/${id}/user-name`, {
    method: "GET",
  });
  return response;
};

export const getAllUsers = async (fetchWithAuth: ApiFetch) => {
  const response = await fetchWithAuth(`/api/Auth/users`, {
    method: "GET",
  });
  return response;
};

export const deactivateUser = async (fetchWithAuth: ApiFetch, id: number) => {
  return await fetchWithAuth(`/api/Admin/users/${id}/deactivate`, {
    method: "PATCH",
  });
};

export const reactivateUser = async (fetchWithAuth: ApiFetch, id: number) => {
  return await fetchWithAuth(`/api/Admin/users/${id}/reactivate`, {
    method: "PATCH",
  });
};

export const refreshToken = async (fetchWithAuth: ApiFetch) => {
  return await fetchWithAuth("/api/Auth/refresh", {
    method: "POST",
  });
};
