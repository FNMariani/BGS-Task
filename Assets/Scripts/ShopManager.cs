using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public GameObject shopPanel;

    public TextMeshProUGUI playerCurrencyText;
    public TextMeshProUGUI shopCurrencyText;
    //public InventoryUI playerInventoryUI;
    //public InventoryUI shopInventoryUI;

    public PlayerController playerController;
    public Shopkeeper shopkeeper;

    void Start()
    {
        HideShopPanel();

        UpdateUI();
    }

    public void UpdateUI()
    {
        // Update currency balances
        playerCurrencyText.text = "Player Money: " + playerController.playerInventory.currency.ToString();
        shopCurrencyText.text = "Shop Money: " + shopkeeper.shopInventory.currency.ToString();

        // Update player and shop inventories
        //playerInventoryUI.DisplayInventory(playerController.playerInventory.GetInventory());
        //shopInventoryUI.DisplayInventory(shopkeeperController.shopInventory.GetInventory());
        shopkeeper.PopulateShopUI(shopkeeper.shopInventory.GetInventory());
    }

    public void BuyButtonClicked(GameItem item)
    {
        playerController.InteractWithShop(shopkeeper.shopInventory, item);
        UpdateUI();
    }

    public void SellButtonClicked(GameItem item)
    {
        shopkeeper.InteractWithPlayer(playerController.playerInventory, item);
        UpdateUI();
    }

    public void ShowShopPanel()
    {
        shopPanel.SetActive(true);
    }

    public void HideShopPanel()
    {
        shopPanel.SetActive(false);
    }
}
