export type Auction = {
  id: number;
  isActive: boolean;
  isOpen: boolean;
  title: string;
  description: string;
  askingPrice: number;
  imageUrl: string;
  startDateTime: string;
  endDateTime: string;
  createdAt: string;
  editedAt: string;
  userId: number;
};

export type Bid = {
  id: number;
  auctionId: number;
  amount: number;
  placedAt: string;
  bidderName: string;
  userId: number;
};
