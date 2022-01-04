export interface Product {
  id: number;
  name: string,
  price: number;
  category: string;
  date: string;
};

export type WritableProduct = Omit<Product, 'id' | 'date'>