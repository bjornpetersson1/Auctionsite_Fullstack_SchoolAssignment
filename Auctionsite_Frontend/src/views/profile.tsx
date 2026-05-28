import { useEffect, useState } from "react";
import { updatePassword } from "../api/authAPI";
import { useAuth } from "../context/auth-context";
import { type Auction } from "../types/auctionTypes";
import { getMyAuctions } from "../api/auctionAPI";
import { formatString, toUtcDate } from "../helpers/auction-helpers";
import { useNavigate } from "react-router-dom";

export const ProfilePage = () => {
  const [oldPassword, setOldPassword] = useState("");
  const [confirmNewPassword, setConfirmNewPassword] = useState("");
  const [newPassword, setNewPassword] = useState("");
  const [response, setResponse] = useState<string | null>(null);
  const [myAuctions, setMyAuctions] = useState<Auction[]>([]);
  const [error, setError] = useState<string | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const { fetchWithAuth } = useAuth();
  const { user } = useAuth();
  const navigate = useNavigate();

  useEffect(() => {
    const fetchAuctions = async () => {
      try {
        var data = await getMyAuctions(fetchWithAuth);
        setMyAuctions(data);
      } catch {
        setError("Kunde inte hämta några auktioner.");
      } finally {
        setLoading(false);
      }
    };
    fetchAuctions();
  }, [user.role]);

  const handleNewPassword = async () => {
    if (newPassword === "") {
      setResponse("Nya lösordet saknas");
    } else if (confirmNewPassword === newPassword) {
      try {
        await updatePassword(fetchWithAuth, oldPassword, newPassword);
        setResponse("Lösenord uppdaterat");
      } catch {
        setResponse("Tidigare lösenord är felaktigt");
      }
    } else {
      setResponse("Det nya lösenordet och bekräftelsen matchar inte");
    }
  };

  const handleAuctionClick = (auctionid: number) => {
    navigate(`/auctions/${auctionid}`);
  };
  return (
    <div className="form-view">
      <h2>{user.userName}</h2>
      <h3>Mina auktioner</h3>
      {loading && (
        <div className="spinner-wrapper">
          <div className="spinner" />
        </div>
      )}
      {!loading && (
        <table>
          <thead>
            <tr>
              <th>Titel</th>
              <th>Slutdatum</th>
              <th>Status</th>
            </tr>
          </thead>
          <tbody>
            {myAuctions.map((auction) => (
              <tr
                key={auction.id}
                onClick={() => handleAuctionClick(auction.id)}
                style={{ cursor: "pointer" }}
              >
                <td>{formatString(auction.title, 22)}</td>
                <td>
                  {toUtcDate(auction.endDateTime).toLocaleDateString("sv-SE")}
                </td>
                <td>
                  {auction.isOpen
                    ? "Öppen"
                    : toUtcDate(auction.startDateTime) > new Date()
                      ? "Kommande"
                      : "Avslutad"}
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
      <p>{error}</p>
      <h3>Uppdatera lösenord</h3>
      <h4>Tidigare lösenord</h4>
      <input
        type="password"
        placeholder="Tidigare lösenord"
        autoComplete="current-password"
        onChange={(e) => setOldPassword(e.target.value)}
      ></input>
      <input
        type="text"
        autoComplete="username"
        value={user.userName ?? ""}
        readOnly
        style={{
          display: "none",
        }}
      />
      <h4>Nytt lösenord</h4>
      <input
        type="password"
        placeholder="Nytt lösenord"
        autoComplete="new-password"
        onChange={(e) => setNewPassword(e.target.value)}
      ></input>
      <h4>Bekräfta nytt lösenord</h4>
      <input
        type="password"
        placeholder="Bekräfta nytt lösenord"
        autoComplete="new-password"
        onChange={(e) => setConfirmNewPassword(e.target.value)}
      ></input>
      <button onClick={handleNewPassword}>Uppdatera lösenord</button>
      <p>{response}</p>
    </div>
  );
};
