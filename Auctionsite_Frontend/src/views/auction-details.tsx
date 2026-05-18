import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import {
  getAuctionById,
  getBidsByAuctionId,
  placeBid,
} from "../api/auctionAPI";
import { useAuth } from "../context/auth-context";
import type { Auction, Bid } from "../types/auctionTypes";
import { formatDateTime, formatMsToDate } from "../helpers/auction-helpers";
import "./auction-details.css";

export const AuctionDetails = () => {
  const { id } = useParams();
  const { user } = useAuth();
  const [auction, setAuction] = useState<Auction | undefined>(undefined);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  const [bids, setBids] = useState<Bid[]>([]);
  const [bid, setBid] = useState(0);
  const [timeRemaining, setTimeRemaining] = useState<number>(0);

  useEffect(() => {
    if (auction) {
      const interval = setInterval(() => {
        setTimeRemaining(new Date(auction.endDateTime).getTime() - Date.now());
      }, 1000);

      return () => clearInterval(interval);
    }
  }, [auction]);

  useEffect(() => {
    const fetchAuction = async () => {
      try {
        var data = await getAuctionById(Number(id));
        setAuction(data);
      } catch {
        setError("Gick inte att hitta auktionen");
      } finally {
        setLoading(false);
      }
    };
    fetchBids();
    fetchAuction();
  }, [id]);

  const fetchBids = async () => {
    try {
      var data = await getBidsByAuctionId(Number(id));
      setBids(data.bids);
    } catch {
      setError("Gick inte att hämta bud");
    }
  };

  const handlePlacedBid = async () => {
    try {
      await placeBid(Number(id), bid);
      await fetchBids();
      setError(null);
    } catch {
      setError("felaktigt bud");
    }
  };

  if (loading) return <div className="spinner" />;
  if (auction !== undefined) {
    return (
      <div className="auction-details">
        <img src={auction.imageUrl} alt={auction.title} />
        <h3>{auction.title}</h3>
        <p>Säljar-id: {auction.userId}</p>
        <p>{auction.description}</p>
        <p>Avslutas om: {formatMsToDate(timeRemaining)}</p>
        {user.isAuthenticated && user.userId !== auction.userId && (
          <div className="bid-form">
            <input
              type="number"
              placeholder="lägg ditt bud"
              onChange={(e) => setBid(Number(e.target.value))}
            ></input>
            <button onClick={() => handlePlacedBid()}>Lägg bud</button>
            <p className="bid-error">{error}</p>
          </div>
        )}
        <ul className="bids-list">
          {bids.map((bid) => (
            <li key={bid.id} className="bid-item">
              <p>{bid.amount} kr</p>
              <p>{bid.bidderName}</p>
              <p>{formatDateTime(bid.placedAt)}</p>
            </li>
          ))}
        </ul>
      </div>
    );
  } else {
    return <p>{error}</p>;
  }
};
