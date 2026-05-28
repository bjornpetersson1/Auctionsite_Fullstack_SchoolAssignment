import { useEffect, useState } from "react";
import "./form.css";
import {
  getAuctionById,
  getBidsByAuctionId,
  putEditAuction,
  registerAuction,
} from "../api/auctionAPI";
import type {
  Auction,
  EditAuctionPayload,
  NewAuctionPayload,
} from "../types/auctionTypes";
import { useParams, useNavigate } from "react-router-dom";
import { useAuth } from "../context/auth-context";
import { toUtcDate } from "../helpers/auction-helpers";

export const AuctionCreateAndEdit = () => {
  const { fetchWithAuth } = useAuth();
  const { user } = useAuth();
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [askingPrice, setAskingPrice] = useState(0);
  const [imageUrl, setImageUrl] = useState("");
  const [startDateTime, setStartDateTime] = useState("");
  const [endDateTime, setEndDateTime] = useState("");
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate();
  const { id } = useParams();
  const isEditing = !!id;
  const [auction, setAuction] = useState<Auction | null>(null);
  const [hasBids, setHasBids] = useState(false);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const data: Auction = await getAuctionById(fetchWithAuth, Number(id));
        const bidsData = await getBidsByAuctionId(fetchWithAuth, data.id);
        setHasBids((bidsData?.bids?.length ?? 0) > 0);
        setAuction(data);
        setTitle(data.title);
        setDescription(data.description);
        setAskingPrice(data.askingPrice);
        setStartDateTime(
          toUtcDate(data.startDateTime).toISOString().substring(0, 16),
        );
        setEndDateTime(
          toUtcDate(data.endDateTime).toISOString().substring(0, 16),
        );
        setImageUrl(data.imageUrl);
      } catch {
        setError("Kunde inte hämta auktionsinfo");
      }
    };
    if (isEditing) fetchData();
  }, [isEditing]);
  // useEffect(() => {
  //   const fetchAuction = async () => {
  //     try {
  //       const data: Auction = await getAuctionById(fetchWithAuth, Number(id));
  //       setAuction(data);
  //       setTitle(data.title);
  //       setDescription(data.description);
  //       setAskingPrice(data.askingPrice);
  //       setStartDateTime(
  //         toUtcDate(data.startDateTime).toISOString().substring(0, 16),
  //       );
  //       setEndDateTime(
  //         toUtcDate(data.endDateTime).toISOString().substring(0, 16),
  //       );
  //       setImageUrl(data.imageUrl);
  //     } catch (error) {
  //       setError("Kunde inte hämta auktionsinfo");
  //     }
  //   };
  //   if (isEditing) fetchAuction();
  // }, [isEditing]);
  // useEffect(() => {
  //   const fetchBids = async () => {
  //     try {
  //       const bidsData = await getBidsByAuctionId(
  //         fetchWithAuth,
  //         Number(auction?.id),
  //       );
  //       setHasBids((bidsData?.bids?.length ?? 0) > 0);
  //     } catch (error) {
  //       setError("Kunde inte hämta budinfo");
  //     }
  //   };
  //   if (isEditing) fetchBids();
  // }, [auction]);

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

  const editAuction = async () => {
    try {
      const edit: EditAuctionPayload = {
        id: Number(id),
        title,
        description,
        userId: Number(user.userId),
        askingPrice,
        imageUrl,
        startDateTime: new Date(startDateTime).toISOString(),
        endDateTime: new Date(endDateTime).toISOString(),
      };
      var data = await putEditAuction(fetchWithAuth, edit);
      if (data !== null) navigate(`/auctions/${data.id}`);
    } catch (e) {
      setError(e instanceof Error ? e.message : "Något gick fel");
    }
  };

  return (
    <div className="form-view">
      <h3>{isEditing ? "Redigera auktion" : "Skapa ny auktion"}</h3>
      <h4>Titel</h4>
      <input
        type="text"
        placeholder="Titel"
        defaultValue={auction?.title ?? ""}
        onChange={(e) => setTitle(e.target.value)}
      ></input>
      <h4>Beskrivning</h4>
      <input
        type="text"
        placeholder="Beskrivning"
        defaultValue={auction?.description ?? ""}
        onChange={(e) => setDescription(e.target.value)}
      ></input>
      <h4>Minsta godtagbara pris</h4>
      <input
        type="number"
        placeholder="Minsta godtagbara pris"
        defaultValue={auction?.askingPrice ?? ""}
        disabled={hasBids}
        onChange={(e) => setAskingPrice(Number(e.target.value))}
      ></input>
      {hasBids && <p>Kan inte ändra lägstapris efter att bud lagts</p>}
      <h4>Startdatum och tid</h4>
      <input
        type="datetime-local"
        placeholder="Startdatum och tid"
        defaultValue={auction?.startDateTime ?? ""}
        onChange={(e) => setStartDateTime(e.target.value)}
      ></input>
      <h4>Slutdatum och tid</h4>
      <input
        type="datetime-local"
        placeholder="Slutdatum och tid"
        defaultValue={auction?.endDateTime ?? ""}
        onChange={(e) => setEndDateTime(e.target.value)}
      ></input>
      <h4>Bild URL</h4>
      <input
        type="text"
        placeholder="Bild URL"
        defaultValue={auction?.imageUrl ?? ""}
        onChange={(e) => setImageUrl(e.target.value)}
      ></input>
      <button onClick={isEditing ? () => editAuction() : () => submitAuction()}>
        {isEditing ? "Uppdatera auktion" : "Skapa auktion"}
      </button>
      {error && <p>{error}</p>}
    </div>
  );
};
