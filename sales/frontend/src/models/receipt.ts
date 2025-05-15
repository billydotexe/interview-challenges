export type ReceiptInfo = {
  items: {
    id: number;
    name: string;
    price: number;
    quantity: number;
    imported : boolean;
  }[];
  tax: number;
  total: number;
};