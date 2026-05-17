import { apiFetch } from "./client";

export const getAuctionList = async (includeAll: boolean = false) => {
  return await apiFetch(`/api/auctions?includeAll=${includeAll}`, {
    method: "GET",
  });
};

export const getAuctionById = async (id: number) => {
  return await apiFetch(`/api/auctions/${id}`, {
    method: "GET",
  });
};

export const getAllBids = async () => {
  return await apiFetch(`/api/auctions/bids`, {
    method: "GET",
  });
};
