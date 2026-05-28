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

export type NewAuctionPayload = {
  title: string;
  description: string;
  askingPrice: number;
  imageUrl: string;
  startDateTime: string;
  endDateTime: string;
};

export type EditAuctionPayload = {
  id: number;
  title: string;
  description: string;
  userId: number;
  askingPrice: number;
  imageUrl: string;
  startDateTime: string;
  endDateTime: string;
};
