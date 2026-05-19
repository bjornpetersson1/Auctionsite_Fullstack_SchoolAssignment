import type { NewAuctionPayload } from "../types/auctionTypes";
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

export const getBidsByAuctionId = async (id: number) => {
  return await apiFetch(`/api/auctions/${id}/bids`, {
    method: "GET",
  });
};

export const placeBid = async (auctionId: number, amount: number) => {
  return await apiFetch(`/api/auctions/${auctionId}/bids?amount=${amount}`, {
    method: "POST",
  });
};

export const registerAuction = async (auction: NewAuctionPayload) => {
  return await apiFetch("/api/auctions", {
    method: "POST",
    body: JSON.stringify(auction),
  });
};
