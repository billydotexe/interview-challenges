"use client";

import React, { useEffect, useState } from "react";
import Select from "react-select";
import { cartConfig } from "@/app/config";
import Receipt from "@/components/receipt";
import { ReceiptInfo } from "@/models/receipt";
import { CartItem, Item } from "@/models/item";

export const Cart: React.FC = () => {
  const [items, setItems] = useState<Item[]>([]);
  const [cart, setCart] = useState<CartItem[]>([]);
  const [receipt, setReceipt] = useState<ReceiptInfo>();
  const [showReceipt, setShowReceipt] = useState(false);

  useEffect(() => {
    setItems(cartConfig.items);
  }, []);

  const ItemSelectOptions = items.map((item) => ({
    value: item.id,
    label: item.name,
  }));

  const addItem = () => {
    let filter = cart.filter((x) => x.itemId === null);
    if (filter.length > 0) {
      return;
    }
    setCart((prev) => [
      ...prev,
      {
        id: crypto.randomUUID(), //generating a random uuid to identify products in the cart
        name: null,
        type: null,
        itemId: null,
        quantity: 1,
        isImported: false,
        price: null,
      },
    ]);
  };

  const updateCartItem = (id: string, updates: Partial<CartItem>) => {
    setCart((prev) => {
      return prev.map((item) =>
        item.id === id ? { ...item, ...updates } : item
      );
    });
  };

  const removeItem = (id: string) => {
    setCart((prev) => prev.filter((item) => item.id !== id));
  };

  const submitCart = async () => {
    const res = await fetch(cartConfig.apiUrl, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(cart),
    });

    const json = await res.json();

    console.log(json as ReceiptInfo);

    setReceipt(json as ReceiptInfo);
    setShowReceipt(true);
  };

  return (
    <div>
      <button onClick={addItem}>Add Item</button>

      {cart.map((cartItem) => (
        <div key={cartItem.itemId}>
          <div>
            <Select
              classNamePrefix="my-select"
              options={ItemSelectOptions}
              placeholder="Select item"
              value={ItemSelectOptions.find(
                (opt) => opt.value === cartItem.itemId
              )}
              onChange={(selected) => {
                updateCartItem(cartItem.id, {
                  itemId: selected ? selected.value : null,
                  name: selected ? selected.label : null,
                  type: selected
                    ? items.filter((x) => x.id === selected.value)[0]?.type
                    : null,
                  price: selected
                    ? items.filter((x) => x.id === selected.value)[0]?.price
                    : null,
                });
              }}
            />
          </div>

          <input
            type="number"
            min={1}
            value={cartItem.quantity}
            onChange={(e) => {
              updateCartItem(cartItem.id, {
                quantity: parseInt(e.target.value, 10),
              });
            }}
          />
          <br />
          <label>
            <input
              type="checkbox"
              checked={cartItem.isImported}
              onChange={(e) => {
                updateCartItem(cartItem.id, {
                  isImported: e.target.checked,
                });
              }}
            />
            Imported
          </label>
          <br />
          <label>price: {cartItem.price}</label>
          <br />
          <button
            onClick={() => {
              removeItem(cartItem.id);
            }}
          >
            Remove
          </button>
        </div>
      ))}
      {cart.length > 0 && cart.filter((x) => x.name != null).length > 0 && (
        <button onClick={submitCart}>Submit Cart</button>
      )}
      {showReceipt && (
        <Receipt
          Items={receipt!.Items}
          Tax={receipt!.Tax}
          Total={receipt!.Total}
        />
      )}
    </div>
  );
};
