import { useState } from "react";
import { type Auction } from "../types/auctionTypes";
import { useAuth } from "../context/auth-context";

export const AuctionSearchBar = () => {
  const [query, setQuery] = useState<string>("");
  const [auctions, setAuctions] = useState<Auction[]>([]);
  const { fetchWithAuth } = useAuth();

  const handleSearch = () => {
    
  };

  return (
    <div>
      <input
        type="text"
        placeholder="sök efter auktioner"
        onChange={(e) => setQuery(e.target.value)}
      ></input>
      <button onClick={handleSearch}>Sök</button>
    </div>
  );
};
