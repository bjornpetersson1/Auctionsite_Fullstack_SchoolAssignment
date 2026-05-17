import { useEffect, useState } from "react";
import type { Auction, Bid } from "../types/auctionTypes";
import { useNavigate } from "react-router-dom";

export const AuctionCard = ({
  auction,
  allBids,
}: {
  auction: Auction;
  allBids: Bid[];
}) => {
  const [highestBid, setHighestBid] = useState<number>(0);
  const [highestBidder, setHighestBidder] = useState<string>("");
  const [timeRemaining, setTimeRemaining] = useState<number>(0);
  const navigate = useNavigate();

  useEffect(() => {
    const interval = setInterval(() => {
      setTimeRemaining(new Date(auction.endDateTime).getTime() - Date.now());
    }, 1000);

    return () => clearInterval(interval);
  }, []);

  useEffect(() => {
    if (allBids.length === 0) {
      setHighestBid(auction.askingPrice);
      setHighestBidder("önskat pris");
    } else {
      var bids = allBids
        .filter((b) => b.auctionId === auction.id)
        .sort((a, b) => b.amount - a.amount);
      setHighestBid(bids[0]?.amount ?? auction.askingPrice);
      setHighestBidder(bids[0]?.bidderName ?? "Ingen budgivare");
    }
  }, [allBids]);

  const formatMsToDate = (ms: number) => {
    if (ms <= 0) return "Avslutad";
    const totalSeconds = Math.floor(ms / 1000);
    const days = Math.floor(totalSeconds / 86400);
    const hours = Math.floor((totalSeconds % 86400) / 3600);
    const minutes = Math.floor((totalSeconds % 3600) / 60);
    const seconds = totalSeconds % 60;
    return `${days} d ${hours}:${minutes}:${seconds}`;
  };
  const handleClick = (id: number) => {
    navigate(`/auctions/${id}`);
  };

  return (
    <>
      <div className="list-card" onClick={() => handleClick(auction.id)}>
        <img src={auction.imageUrl} alt={auction.title} />
        <h3>{auction.title.slice(0, 20)}</h3>
        <p>{auction.description.slice(0, 50)}...</p>
        <p>
          {highestBid} kr, {highestBidder}
        </p>
        <p>{formatMsToDate(timeRemaining)}</p>
      </div>
    </>
  );
};
