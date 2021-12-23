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

export const enum CartEvent {
  ADD,
  DELETE
};

export interface CartPayloadAdd {
  event: CartEvent.ADD;
  product: OrderProduct;
}

export interface CartPayloadDelete {
  event: CartEvent.DELETE;
  productId: number;
}

export type CartPayload = CartPayloadAdd | CartPayloadDelete;