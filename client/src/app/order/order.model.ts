import { Product } from "../product/product.model";

export interface Order {
  id: number;
  userId: number;
  totalPrice: number;
  nrProducts: number;
  issuedAt: string;
}

export interface OrderProduct extends Product {
  quantity: number;
};
export interface CurrentOrder {
  id: number;
  products: OrderProduct[];
  totalPrice: number;
}