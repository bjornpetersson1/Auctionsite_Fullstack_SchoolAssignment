import { useState } from "react";
import "./form.css";
import { registerAuction } from "../api/auctionAPI";
import type { NewAuctionPayload } from "../types/auctionTypes";
import { useNavigate } from "react-router-dom";

export const AuctionCreate = () => {
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
      await registerAuction(auction);
      setError(null);
      navigate("/");
    } catch {
      setError("Något gick fel");
    }
  };

  return (
    <div className="form-view">
      <h3>Create new auction</h3>
      <input
        type="text"
        placeholder="Title"
        onChange={(e) => setTitle(e.target.value)}
      ></input>
      <input
        type="text"
        placeholder="Description"
        onChange={(e) => setDescription(e.target.value)}
      ></input>
      <input
        type="number"
        placeholder="Asking price"
        onChange={(e) => setAskingPrice(Number(e.target.value))}
      ></input>
      <h4>Start date and time</h4>
      <input
        type="datetime-local"
        placeholder="start date and time"
        onChange={(e) => setStartDateTime(e.target.value)}
      ></input>
      <h4>End date and time</h4>
      <input
        type="datetime-local"
        placeholder="end date and time"
        onChange={(e) => setEndDateTime(e.target.value)}
      ></input>
      <input
        type="text"
        placeholder="Picture URL"
        onChange={(e) => setImageUrl(e.target.value)}
      ></input>
      <button onClick={() => submitAuction()}>Skapa auktion</button>
      {error && <p>{error}</p>}
    </div>
  );
};
