export interface Product {
  id: number;
  name: string,
  price: number;
  category: string;
  date: string;
};

export type AddProduct = Omit<Product, 'id' | 'date'>