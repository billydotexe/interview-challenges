export type Item = {
  id: number;
  type: string;
  name: string;
  price: number;
};

export type CartItem = {
  id: string;
  name: string | null;
  type: string | null;
  itemId: number | null;
  price: number | null;
  quantity: number;
  isImported: boolean;
};