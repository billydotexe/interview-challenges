import { ReceiptInfo } from "@/models/receipt";
import React from "react";

const Receipt: React.FC<ReceiptInfo> = ({ Items, Tax, Total }) => {
  return (
    <div>
      <h2>Receipt</h2>
      <ul>
        {Items.map((item, index) => {
          return (
            <li key={index}>
              {item.Quantity} x {item.IsImported ? "imported " : ""}{item?.Name ?? "Unknown"}: (${item?.Price.toFixed(2) ?? "0.00"})
            </li>
          );
        })}
      </ul>
      <p>
        <strong>Total:</strong> {Total}
      </p>
      <p>
        <strong>Tax:</strong> {Tax}
      </p>
    </div>
  );
};

export default Receipt;
