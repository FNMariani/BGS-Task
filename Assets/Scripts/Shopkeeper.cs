using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopkeeper : InteractableObject
{
    public Transform shopPanelContent;
    public GameObject shopItemPrefab;

    public List<ItemInstance> shopItems = new ();

    public Inventory shopInventory;

    public ShopManager uiManager;

    private void Start()
    {
        //Set references
        //shopPanelContent = GameObject.FindWithTag("ShopPanelContent_Shopkeeper").transform;
        if (shopPanelContent == null) { Debug.Log("ShopPanelContent_Shopkeeper null!"); }

        //uiManager = GameObject.FindWithTag("ShopPanel").GetComponent<ShopManager>();
        if (uiManager == null) { Debug.Log("ShopPanel null!"); }

        //Initialize shop inventory
        foreach (ItemInstance item in shopItems)
        {
            shopInventory.AddItem(item);
        }
    }

    public void Initialize()
    {
        
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

    public void PopulateShopUI(List<ItemInstance> shopItems)
    {
        ClearShopUI();

        foreach (ItemInstance itemInst in shopItems)
        {
            GameItem item = itemInst.itemType;

            GameObject shopItemObject = Instantiate(shopItemPrefab, shopPanelContent);
            ItemUI shopItemUI = shopItemObject.GetComponent<ItemUI>();

            if (shopItemUI != null)
            {
                shopItemUI.Initialize(uiManager, itemInst, "Buy");
            }
        }
    }

    private void ClearShopUI()
    {
        foreach (Transform child in shopPanelContent)
        {
            Destroy(child.gameObject);  
        }
    }

    public void InteractWithPlayer(Inventory playerInventory, ItemInstance item)
    {
        if (playerInventory.SellItem(item))
        {
            //Successful sale
            if (item.itemType.isEquipped)
            {
                GameObject player = GameObject.FindWithTag("Player");
                if (player != null)
                {
                    player.GetComponent<PlayerController>().Unequip(item);
                }
            }
            shopInventory.BuyItem(item);
            GameObject.FindWithTag("MainCamera").GetComponent<AudioController>().PlayCoinSound();
        }
        else
        {
            //Sale failed
            Debug.LogWarning("Sale failed.");
        }
    }
}
