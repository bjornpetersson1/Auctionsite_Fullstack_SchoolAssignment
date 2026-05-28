import { useNavigate, useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import {
  deleteLatestBid,
  getAuctionById,
  getBidsByAuctionId,
  placeBid,
} from "../api/auctionAPI";
import { useAuth } from "../context/auth-context";
import type { Auction, Bid } from "../types/auctionTypes";
import {
  formatDateTime,
  formatMsToDate,
  toUtcDate,
} from "../helpers/auction-helpers";
import "./auction-details.css";

export const AuctionDetails = () => {
  const { fetchWithAuth } = useAuth();
  const { id } = useParams();
  const { user } = useAuth();
  const [auction, setAuction] = useState<Auction | undefined>(undefined);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  const [bids, setBids] = useState<Bid[]>([]);
  const [bid, setBid] = useState(0);
  const [timeRemaining, setTimeRemaining] = useState<number>(0);
  const navigate = useNavigate();

  useEffect(() => {
    if (auction) {
      const interval = setInterval(() => {
        setTimeRemaining(toUtcDate(auction.endDateTime).getTime() - Date.now());
      }, 1000);

      return () => clearInterval(interval);
    }
  }, [auction]);

  useEffect(() => {
    const fetchAuction = async () => {
      try {
        var data = await getAuctionById(fetchWithAuth, Number(id));
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
      var data = await getBidsByAuctionId(fetchWithAuth, Number(id));
      setBids(data.bids);
    } catch {
      setError("Gick inte att hämta bud");
    }
  };

  const handlePlacedBid = async () => {
    try {
      await placeBid(fetchWithAuth, Number(id), bid);
      await fetchBids();
      setError(null);
    } catch (e) {
      setError(e instanceof Error ? e.message : "Något gick fel");
    }
  };
  const handleDeleteBid = async () => {
    try {
      await deleteLatestBid(fetchWithAuth, Number(id));
      await fetchBids();
      setError(null);
    } catch (e) {
      setError(e instanceof Error ? e.message : "Något gick fel");
    }
  };

  if (loading)
    return (
      <div className="spinner-wrapper">
        <div className="spinner" />
      </div>
    );
  if (auction !== undefined) {
    return (
      <div className="auction-details">
        <img src={auction.imageUrl} alt={auction.title} />
        <div className="boolButtons">
          <h3>{auction.title}</h3>
          {user.userId == auction.userId && (
            <button onClick={() => navigate(`/create-auction/${auction.id}`)}>
              Redigera auktion
            </button>
          )}
        </div>
        <p>Säljar-id: {auction.userId}</p>
        <p>{auction.description}</p>
        <p>Avslutas om: {formatMsToDate(timeRemaining)}</p>
        {!auction.isOpen && toUtcDate(auction.startDateTime) > new Date() && (
          <p>Auktionen har inte startat än</p>
        )}
        {user.isAuthenticated &&
          Number(user.userId) !== auction.userId &&
          auction.isActive &&
          auction.isOpen && (
            <div className="bid-form">
              <input
                type="number"
                placeholder="lägg ditt bud"
                onChange={(e) => setBid(Number(e.target.value))}
              ></input>
              <button onClick={() => handlePlacedBid()}>Lägg bud</button>
              {bids.length > 0 && Number(user.userId) === bids[0].userId && (
                <button onClick={handleDeleteBid}>Ångra senaste bud</button>
              )}
              <p className="bid-error">{error}</p>
            </div>
          )}
        {auction.isOpen && (
          <ul className="bids-list">
            {bids.map((bid) => (
              <li key={bid.id} className="bid-item">
                <p>{bid.amount} kr</p>
                <p>{bid.bidderName}</p>
                <p>{formatDateTime(bid.placedAt)}</p>
              </li>
            ))}
          </ul>
        )}
        {!auction.isOpen && toUtcDate(auction.startDateTime) <= new Date() && (
          <div>
            {bids.length > 0 ? (
              <ul className="bids-list">
                <li className="bid-item">
                  <p>{bids[0].amount} kr</p>
                  <p>{bids[0].bidderName}</p>

                  <p>{formatDateTime(bids[0].placedAt)}</p>
                </li>
              </ul>
            ) : (
              <p>Inga bud lades på denna auktion</p>
            )}
          </div>
        )}
      </div>
    );
  } else {
    return <p>{error}</p>;
  }
};
