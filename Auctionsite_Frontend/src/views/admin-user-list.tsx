import { useEffect, useState } from "react";
import { useAuth } from "../context/auth-context";
import { deactivateUser, getAllUsers, reactivateUser } from "../api/authAPI";
import { AdminNavbar } from "../components/admin-header";
import type { getUserPayload } from "../types/authTypes";

export const AdminUsersList = () => {
  const { fetchWithAuth } = useAuth();
  const { user } = useAuth();
  const [users, setUsers] = useState<getUserPayload[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchAuctions = async () => {
      try {
        var data = await getAllUsers(fetchWithAuth);
        setUsers(data);
      } catch {
        setError("Kunde inte hämta några användare.");
      } finally {
        setLoading(false);
      }
    };
    fetchAuctions();
  }, [user.role]);

  const toggleIsActive = async (id: number, isActive: boolean) => {
    if (isActive) {
      await deactivateUser(fetchWithAuth, id);
    } else {
      await reactivateUser(fetchWithAuth, id);
    }
    setUsers((prev) =>
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
            <th>Namn</th>
            <th>Email</th>
            <th>Roll</th>
            <th>Status</th>
          </tr>
        </thead>
        <tbody>
          {users.map((userLoad) => (
            <tr key={userLoad.id}>
              <td>{userLoad.id}</td>
              <td>{userLoad.name}</td>
              <td>{userLoad.email}</td>
              <td>{userLoad.role} </td>
              <td>{userLoad.isActive ? "Aktiv" : "Inaktiv"}</td>
              <td>
                <button
                  onClick={() => toggleIsActive(userLoad.id, userLoad.isActive)}
                  disabled={Number(user.userId) === userLoad.id}
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
