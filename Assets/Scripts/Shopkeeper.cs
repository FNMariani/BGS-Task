using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopkeeper : InteractableObject
{
    private Transform shopPanelContent;
    public GameObject shopItemPrefab;

    public List<GameItem> shopItems;

    public Inventory shopInventory;

    private ShopManager uiManager;

    private void Start()
    {
        //Set references
        shopPanelContent = GameObject.FindWithTag("ShopPanelContent_Shopkeeper").transform;
        if (shopPanelContent == null) { Debug.Log("ShopPanelContent_Shopkeeper null!"); }

        uiManager = GameObject.FindWithTag("ShopPanel").GetComponent<ShopManager>();
        if (uiManager == null) { Debug.Log("ShopPanel null!"); }

        foreach (GameItem item in shopItems)
        {
            //DEBUG
            //Debug.Log($"Item: {item.itemName}, Price: {item.itemPrice}");
        }

        //Initialize shop inventory
        foreach (GameItem item in shopItems)
        {
            shopInventory.AddItem(item);
        }

        //DEBUG
        //PopulateShopUI(shopItems);
    }
    public override void Interact()
    {
        OpenShopUI();
    }

    private void OpenShopUI()
    {
        uiManager.ShowShopPanel();

        PopulateShopUI(shopInventory.GetInventory());
    }

    public void PopulateShopUI(List<GameItem> shopItems)
    {
        ClearShopUI();

        foreach (GameItem item in shopItems)
        {
            GameObject shopItemObject = Instantiate(shopItemPrefab, shopPanelContent);
            ItemUI shopItemUI = shopItemObject.GetComponent<ItemUI>();

            if (shopItemUI != null)
            {
                shopItemUI.Initialize(uiManager, item, "Buy");
            }

            /*if (shopItemUI != null)
            {
                shopItemUI.UpdateUI(item);
            }*/
        }
    }

    private void ClearShopUI()
    {
        foreach (Transform child in shopPanelContent)
        {
            Destroy(child.gameObject);  
        }
    }

    public void InteractWithPlayer(Inventory playerInventory, GameItem item)
    {
        if (playerInventory.SellItem(item))
        {
            //Successful sale
            if (item.isEquipped)
            {
                GameObject player = GameObject.FindWithTag("Player");
                if (player != null)
                {
                    player.GetComponent<PlayerController>().Unequip(item);
                }
            }
            shopInventory.BuyItem(item);
        }
        else
        {
            //Sale failed
            Debug.LogWarning("Sale failed.");
        }
    }
}
