export interface Order {
  id: number;
  userId: number;
  totalPrice: number;
  nrProducts: number;
  issuedAt: string;
}