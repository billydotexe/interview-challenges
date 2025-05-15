export type ReceiptInfo = {
  Items: {
    Id: number;
    Name: string;
    Price: number;
    Quantity: number;
    IsImported : boolean;
  }[];
  Tax: number;
  Total: number;
};