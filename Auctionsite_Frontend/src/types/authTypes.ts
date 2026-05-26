export type newUserPayload = {
  name: string;
  email: string;
  password: string;
};

export type loginPayload = {
  email: string;
  password: string;
};

export type LoginPayloadContext = {
  token: string | null;
  userId: number | null;
  userName: string | null;
  role: string | null;
  isAuthenticated: boolean;
};

export type getUserPayload = {
  id: number;
  name: string;
  email: string;
  role: string;
  isActive: boolean;
};
