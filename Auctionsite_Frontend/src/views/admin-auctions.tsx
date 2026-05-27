import { useEffect, useState } from "react";
import {
  deactivateAuction,
  getAuctionList,
  reactivateAuction,
} from "../api/auctionAPI";
import { useAuth } from "../context/auth-context";
import type { Auction } from "../types/auctionTypes";
import { getNameById } from "../api/authAPI";
import { AdminNavbar } from "../components/admin-header";

export const AdminAuctionsList = () => {
  const { fetchWithAuth } = useAuth();
  const { user } = useAuth();
  const [auctions, setAuctions] = useState<Auction[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  const [userNames, setUserNames] = useState<Record<number, string>>({});

  useEffect(() => {
    const fetchAuctions = async () => {
      const includeAll = user.role === "admin";
      try {
        var data = await getAuctionList(fetchWithAuth, includeAll);
        var sortedData = data.auctions.sort(
          (a: Auction, b: Auction) => a.id - b.id,
        );
        setAuctions(sortedData);
      } catch {
        setError("Kunde inte hämta några auktioner.");
      } finally {
        setLoading(false);
      }
    };
    fetchAuctions();
  }, [user.role]);

  useEffect(() => {
    if (auctions.length === 0) return;
    const fetchNames = async () => {
      const uniqueIds = [...new Set(auctions.map((a) => a.userId))];
      const names = await Promise.all(
        uniqueIds.map((id) => getNameById(fetchWithAuth, id)),
      );
      const map: Record<number, string> = {};
      uniqueIds.forEach((id, i) => (map[id] = names[i].name));
      setUserNames(map);
    };
    fetchNames();
  }, [auctions]);

  const toggleIsActive = async (id: number, isActive: boolean) => {
    if (isActive) {
      await deactivateAuction(fetchWithAuth, id);
    } else {
      await reactivateAuction(fetchWithAuth, id);
    }
    setAuctions((prev) =>
      prev.map((a) => (a.id === id ? { ...a, isActive: !a.isActive } : a)),
    );
  };

  if (loading) return <div className="spinner" />;
  return (
    <div>
      <AdminNavbar />
      <p>{error}</p>
      <table>
        <thead>
          <tr>
            <th>Id</th>
            <th>Titel</th>
            <th>Ägare</th>
            <th>Slutdatum</th>
            <th>Öppen/stängd</th>
            <th>Status</th>
          </tr>
        </thead>
        <tbody>
          {auctions.map((auction) => (
            <tr key={auction.id}>
              <td>{auction.id}</td>
              <td>{auction.title}</td>
              <td>{userNames[auction.userId] ?? auction.userId}</td>
              <td>{auction.endDateTime.substring(0, 10)}</td>
              <td>{auction.isOpen ? "Öppen" : "Stängd"}</td>
              <td>{auction.isActive ? "Aktiv" : "Inaktiv"}</td>
              <td>
                <button
                  onClick={() => toggleIsActive(auction.id, auction.isActive)}
                >
                  Aktivera/Inaktivera
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};
