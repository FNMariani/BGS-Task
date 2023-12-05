using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopkeeper : InteractableObject
{
    GameObject shopUI;
    public Transform panelContent;
    public GameObject shopItemPrefab;

    public List<GameItem> shopItems;

    public Inventory shopInventory;

    public ShopManager uiManager;

    private void Start()
    {
        //shopUI = GameObject.FindWithTag("shopUI");

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
            GameObject shopItemObject = Instantiate(shopItemPrefab, panelContent);
            ShopItemUI shopItemUI = shopItemObject.GetComponent<ShopItemUI>();

            if (shopItemUI != null)
            {
                shopItemUI.Initialize(uiManager, item);
            }

            /*if (shopItemUI != null)
            {
                shopItemUI.UpdateUI(item);
            }*/
        }
    }

    private void ClearShopUI()
    {
        foreach (Transform child in panelContent)
        {
            Destroy(child.gameObject);  
        }
    }

    public void InteractWithPlayer(Inventory playerInventory, GameItem item)
    {
        if (shopInventory.SellItem(item))
        {
            //Successful sale
            playerInventory.BuyItem(item);
        }
        else
        {
            //Sale failed
            Debug.LogWarning("Sale failed.");
        }
    }
}
