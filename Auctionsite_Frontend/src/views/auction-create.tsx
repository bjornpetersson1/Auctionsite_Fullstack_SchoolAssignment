import { useState } from "react";
import "./form.css";
import { registerAuction } from "../api/auctionAPI";
import type { Auction, NewAuctionPayload } from "../types/auctionTypes";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../context/auth-context";

export const AuctionCreate = () => {
  const { fetchWithAuth } = useAuth();
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [askingPrice, setAskingPrice] = useState(0);
  const [imageUrl, setImageUrl] = useState("");
  const [startDateTime, setStartDateTime] = useState("");
  const [endDateTime, setEndDateTime] = useState("");
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate();

  const submitAuction = async () => {
    if (
      !title ||
      !description ||
      !askingPrice ||
      !imageUrl ||
      !startDateTime ||
      !endDateTime
    ) {
      setError("Alla fält måste fyllas i");
      return;
    }
    try {
      const auction: NewAuctionPayload = {
        title,
        description,
        askingPrice,
        imageUrl,
        startDateTime: new Date(startDateTime).toISOString(),
        endDateTime: new Date(endDateTime).toISOString(),
      };
      const createdAuction: Auction = await registerAuction(
        fetchWithAuth,
        auction,
      );
      setError(null);
      navigate(`/auctions/${createdAuction.id}`);
    } catch (e) {
      setError(e instanceof Error ? e.message : "Något gick fel");
    }
  };

  return (
    <div className="form-view">
      <h3>Skapa ny auktion</h3>
      <input
        type="text"
        placeholder="Titel"
        onChange={(e) => setTitle(e.target.value)}
      ></input>
      <input
        type="text"
        placeholder="Beskrivning"
        onChange={(e) => setDescription(e.target.value)}
      ></input>
      <input
        type="number"
        placeholder="Minsta godtagbara pris"
        onChange={(e) => setAskingPrice(Number(e.target.value))}
      ></input>
      <h4>Startdatum och tid</h4>
      <input
        type="datetime-local"
        placeholder="Startdatum och tid"
        onChange={(e) => setStartDateTime(e.target.value)}
      ></input>
      <h4>Slutdatum och tid</h4>
      <input
        type="datetime-local"
        placeholder="Slutdatum och tid"
        onChange={(e) => setEndDateTime(e.target.value)}
      ></input>
      <input
        type="text"
        placeholder="Bild URL"
        onChange={(e) => setImageUrl(e.target.value)}
      ></input>
      <button onClick={() => submitAuction()}>Skapa auktion</button>
      {error && <p>{error}</p>}
    </div>
  );
};
