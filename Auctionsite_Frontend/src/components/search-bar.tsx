import { useEffect, useRef, useState } from "react";
import { type Auction } from "../types/auctionTypes";
import { useAuth } from "../context/auth-context";
import { getAuctionsListFromQuery } from "../api/auctionAPI";
import { useNavigate } from "react-router-dom";
import { formatString, toUtcDate } from "../helpers/auction-helpers";

export const AuctionSearchBar = () => {
  const [query, setQuery] = useState<string>("");
  const [includeClosed, setIncludeClosed] = useState<boolean>(false);
  const [isSearchDone, setIsSearchDone] = useState<boolean>(false);
  const [auctions, setAuctions] = useState<Auction[]>([]);
  const { fetchWithAuth } = useAuth();
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate();
  const containerRef = useRef<HTMLDivElement>(null);
  const { user } = useAuth();

  useEffect(() => {
    const handleClickOutside = (e: MouseEvent) => {
      if (
        containerRef.current &&
        !containerRef.current.contains(e.target as Node)
      ) {
        setIsSearchDone(false);
      }
    };
    document.addEventListener("mousedown", handleClickOutside);
    return () => document.removeEventListener("mousedown", handleClickOutside);
  }, []);

  const handleSearch = async () => {
    try {
      const resultList: Auction[] = await getAuctionsListFromQuery(
        fetchWithAuth,
        query,
        includeClosed,
      );
      setAuctions(resultList);
    } catch (error) {
      setError(error instanceof Error ? error.message : "Något gick fel");
    } finally {
      setIsSearchDone(true);
    }
  };

  const handleResultClick = (id: number) => {
    navigate(`/auctions/${id}`);
    setIsSearchDone(false);
  };

  return (
    <div className="search-container" ref={containerRef}>
      <input
        type="text"
        autoComplete="username"
        value={user.userName ?? ""}
        readOnly
        style={{
          display: "none",
        }}
      />
      <input
        type="text"
        autoComplete="new-password"
        placeholder="sök efter auktioner"
        onChange={(e) => setQuery(e.target.value)}
      ></input>
      <button onClick={handleSearch}>Sök</button>
      <label className="search-checkbox-label">
        <input
          type="checkbox"
          checked={includeClosed}
          onChange={(e) => setIncludeClosed(e.target.checked)}
        />
        Inkludera avslutade auktioner
      </label>
      <p>{error}</p>
      {isSearchDone && auctions.length > 0 && (
        <div className="search-dropdown">
          <table>
            <thead>
              <tr>
                <th>Titel</th>
                <th>Slutdatum</th>
                <th>Öppen/Avslutad</th>
              </tr>
            </thead>
            <tbody>
              {auctions.map((auction) => (
                <tr
                  key={auction.id}
                  onClick={() => handleResultClick(auction.id)}
                  style={{ cursor: "pointer" }}
                >
                  <td>{formatString(auction.title, 22)}</td>
                  <td>
                    {toUtcDate(auction.endDateTime).toLocaleDateString("sv-SE")}
                  </td>
                  <td>{auction.isOpen ? "Öppen" : "Avslutad"}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
      {isSearchDone && auctions.length == 0 && (
        <div className="search-dropdown">
          <p>Inga auktioner hittade</p>
        </div>
      )}
    </div>
  );
};
