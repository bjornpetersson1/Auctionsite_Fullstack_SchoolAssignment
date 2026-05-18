export const formatMsToDate = (ms: number) => {
  if (ms <= 0) return "Avslutad";
  const totalSeconds = Math.floor(ms / 1000);
  const days = Math.floor(totalSeconds / 86400);
  const hours = Math.floor((totalSeconds % 86400) / 3600);
  const minutes = Math.floor((totalSeconds % 3600) / 60);
  const seconds = totalSeconds % 60;
  return `${days} d ${hours}:${minutes}:${seconds}`;
};

export const formatString = (text: string, length: number) => {
  if (text.length > length + 3) return `${text.slice(0, length)}...`;
  else return text;
};

export const formatDateTime = (dateTime: string) => {
  const d = new Date(dateTime);
  const date = d.toLocaleDateString("sv-SE");
  const time = d
    .toLocaleTimeString("sv-SE", { hour: "2-digit", minute: "2-digit" })
    .replace(":", ".");
  return `${date} kl ${time}`;
};
