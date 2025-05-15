import { ReceiptInfo } from "@/models/receipt";
import React from "react";

const Receipt: React.FC<ReceiptInfo> = ({ items, tax, total }) => {
  return (
    <div>
      <h2>Receipt</h2>
      <ul>
        {items.map((item, index) => {
          return (
            <li key={index}>
              {item.quantity} x {item.imported ? "imported " : ""}{item?.name ?? "Unknown"}: (${item?.price.toFixed(2) ?? "0.00"})
            </li>
          );
        })}
      </ul>
      <p>
        <strong>Total:</strong> {total}
      </p>
      <p>
        <strong>Tax:</strong> {tax}
      </p>
    </div>
  );
};

export default Receipt;
