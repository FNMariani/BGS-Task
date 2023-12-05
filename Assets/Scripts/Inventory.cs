using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PartType
{
    Body,
    Clothes,
    Hair,
    Hat
}

public class Inventory : MonoBehaviour
{
    [Header("Currency")]
    public int currency = 0;

    private List<GameItem> items = new List<GameItem>();

    public delegate void OnInventoryChanged();
    public OnInventoryChanged onInventoryChanged;

    void Start()
    {
        ClearInventory();
    }

    void Update()
    {
        
    }

    public bool AddItem(GameItem item)
    {
        {
            items.Add(item);

            onInventoryChanged?.Invoke();

            return true;
        }
    }
    public void RemoveItem(GameItem item)
    {
        items.Remove(item);

        onInventoryChanged?.Invoke();
    }

    public void ClearInventory()
    {
        items.Clear();

        onInventoryChanged?.Invoke();
    }

    public List<GameItem> GetInventory()
    {
        return items;
    }

    public bool BuyItem(GameItem item)
    {
        if (currency >= item.itemPrice)
        {
            currency -= item.itemPrice;
            AddItem(item);

            return true;
        }

        Debug.LogWarning("Not enough money to buy item: " + item.itemName);
        return false;
    }

    public bool SellItem(GameItem item)
    {
        if (items.Contains(item))
        {
            currency += item.itemPrice;
            RemoveItem(item);

            return true;
        }

        Debug.LogWarning("Item not found in inventory: " + item.itemName);
        return false;
    }
}
