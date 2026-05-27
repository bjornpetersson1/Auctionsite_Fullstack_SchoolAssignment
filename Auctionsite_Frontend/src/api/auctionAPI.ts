import type { NewAuctionPayload } from "../types/auctionTypes";
import { type ApiFetch } from "./client";

export const getAuctionList = async (
  fetchWithAuth: ApiFetch,
  includeAll: boolean = false,
) => {
  return await fetchWithAuth(`/api/auctions?includeAll=${includeAll}`, {
    method: "GET",
  });
};

export const getAuctionById = async (fetchWithAuth: ApiFetch, id: number) => {
  return await fetchWithAuth(`/api/auctions/${id}`, {
    method: "GET",
  });
};

export const getAllBids = async (fetchWithAuth: ApiFetch) => {
  return await fetchWithAuth(`/api/auctions/bids`, {
    method: "GET",
  });
};

export const getBidsByAuctionId = async (
  fetchWithAuth: ApiFetch,
  id: number,
) => {
  return await fetchWithAuth(`/api/auctions/${id}/bids`, {
    method: "GET",
  });
};

export const placeBid = async (
  fetchWithAuth: ApiFetch,
  auctionId: number,
  amount: number,
) => {
  return await fetchWithAuth(
    `/api/auctions/${auctionId}/bids?amount=${amount}`,
    {
      method: "POST",
    },
  );
};

export const registerAuction = async (
  fetchWithAuth: ApiFetch,
  auction: NewAuctionPayload,
) => {
  return await fetchWithAuth("/api/auctions", {
    method: "POST",
    body: JSON.stringify(auction),
  });
};

export const deactivateAuction = async (
  fetchWithAuth: ApiFetch,
  id: number,
) => {
  return await fetchWithAuth(`/api/admin/auctions/${id}/deactivate`, {
    method: "PATCH",
  });
};

export const reactivateAuction = async (
  fetchWithAuth: ApiFetch,
  id: number,
) => {
  return await fetchWithAuth(`/api/admin/auctions/${id}/reactivate`, {
    method: "PATCH",
  });
};
