import type {
  EditAuctionPayload,
  NewAuctionPayload,
} from "../types/auctionTypes";
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

export const getAuctionsListFromQuery = async (
  fetchWithAuth: ApiFetch,
  query: string,
  includeClosed: boolean = false,
) => {
  const result = await fetchWithAuth(
    `/api/auctions/search-auctions?query=${encodeURIComponent(query)}&includeClosed=${includeClosed}`,
  );
  return result?.auctions ?? [];
};

export const getMyAuctions = async (fetchWithAuth: ApiFetch) => {
  const result = await fetchWithAuth(`/api/auctions/my-auctions`, {
    method: "GET",
  });

  return result?.auctions ?? [];
};

export const putEditAuction = async (
  fetchWithAuth: ApiFetch,
  auction: EditAuctionPayload,
) => {
  const result = await fetchWithAuth(`/api/auctions`, {
    method: "PUT",
    body: JSON.stringify(auction),
  });
  return result;
};

export const deleteLatestBid = async (
  fetchWithAuth: ApiFetch,
  auctionId: number,
) => {
  return await fetchWithAuth(`/api/auctions/${auctionId}/bids`, {
    method: "DELETE",
  });
};

export const deleteAuction = async (
  fetchWithAuth: ApiFetch,
  auctionId: number,
  createrId: number,
) => {
  return await fetchWithAuth(`/api/auctions`, {
    method: "DELETE",
    body: JSON.stringify({ auctionID: auctionId, createrId }),
  });
};
