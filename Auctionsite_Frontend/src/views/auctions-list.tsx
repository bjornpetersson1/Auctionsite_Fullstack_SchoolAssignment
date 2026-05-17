import { useEffect, useState } from "react";
import { useAuth } from "../context/auth-context";
import { getAllBids, getAuctionList } from "../api/auctionAPI";
import type { Auction, Bid } from "../types/auctionTypes";
import { AuctionCard } from "../components/auction-card";
import "./auction-list.css";

export const AuctionsList = () => {
  const { user } = useAuth();
  const [auctions, setAuctions] = useState<Auction[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  const [bids, setBids] = useState<Bid[]>([]);

  useEffect(() => {
    const fetchBids = async () => {
      var data = await getAllBids();
      console.log(data);
      setBids(data.bids);
    };
    const fetchAuctions = async () => {
      const includeAll = user.role === "admin";
      try {
        var data = await getAuctionList(includeAll);
        setAuctions(data.auctions);
      } catch {
        setError("Kunde inte hämta några auktioner.");
      } finally {
        setLoading(false);
      }
    };
    fetchBids();
    fetchAuctions();
  }, [user.role]);

  if (loading) return <div className="spinner" />;
  if (error) return <p>{error}</p>;
  if (auctions.length === 0) return <p>Inga auktioner tillgängliga just nu</p>;
  return (
    <>
      <p>Auctions list</p>
      <div className="auctions-list">
        <ul className="auctions-grid">
          {auctions.map((auction) => (
            <li key={auction.id}>
              <AuctionCard auction={auction} allBids={bids} />
            </li>
          ))}
        </ul>
      </div>
    </>
  );
};
