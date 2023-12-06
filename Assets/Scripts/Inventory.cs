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

[System.Serializable]
public class ItemInstance
{
    public GameItem itemType;

    public ItemInstance(GameItem itemData)
    {
        itemType = itemData;
    }
}

public class Inventory : MonoBehaviour
{
    [Header("Currency")]
    public int currency = 0;

    public GameItem newItemType;
    public List<ItemInstance> items = new ();

    void Start()
    {
        ClearInventory();
    }

    void Update()
    {

    }

    public bool AddItem(ItemInstance item)
    {
        items.Add(item);
        item.itemType.isEquipped = false;

        return true;
    }
    public void RemoveItem(ItemInstance item)
    {
       items.Remove(item);
    }

    public void ClearInventory()
    {
        items.Clear();
    }

    public List<ItemInstance> GetInventory()
    {
        return items;
    }

    public bool BuyItem(ItemInstance item)
    {
        if (currency >= item.itemType.itemPrice)
        {
            currency -= item.itemType.itemPrice;
            AddItem(item);

            return true;
        }

        Debug.LogWarning("Not enough money to buy item: " + item.itemType.itemName);
        return false;
    }

    public bool SellItem(ItemInstance item)
    {
        if (items.Contains(item))
        {
            currency += item.itemType.itemPrice;
            RemoveItem(item);

            return true;
        }

        Debug.LogWarning("Item not found in inventory: " + item.itemType.itemName);
        return false;
    }
}
