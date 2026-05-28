import { useState } from "react";
import { updatePassword } from "../api/authAPI";
import { useAuth } from "../context/auth-context";

export const ProfilePage = () => {
  const [oldPassword, setOldPassword] = useState("");
  const [confirmNewPassword, setConfirmNewPassword] = useState("");
  const [newPassword, setNewPassword] = useState("");
  const [response, setResponse] = useState<string | null>(null);
  const { fetchWithAuth } = useAuth();
  const { user } = useAuth();

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
  return (
    <div className="form-view">
      <h2>{user.userName}</h2>
      <h3>Uppdatera lösenord</h3>
      <h4>Tidigare lösenord</h4>
      <input
        type="password"
        placeholder="Tidigare lösenord"
        onChange={(e) => setOldPassword(e.target.value)}
      ></input>
      <h4>Nytt lösenord</h4>
      <input
        type="password"
        placeholder="Nytt lösenord"
        onChange={(e) => setNewPassword(e.target.value)}
      ></input>
      <h4>Bekräfta nytt lösenord</h4>
      <input
        type="password"
        placeholder="Bekräfta nytt lösenord"
        onChange={(e) => setConfirmNewPassword(e.target.value)}
      ></input>
      <button onClick={handleNewPassword}>Byt lösenord</button>
      <p>{response}</p>
    </div>
  );
};
