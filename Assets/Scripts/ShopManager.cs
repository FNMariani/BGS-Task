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

    private PlayerController playerController;
    private Shopkeeper shopkeeper;

    void Start()
    {
        //Set references
        shopkeeper = GameObject.FindWithTag("Shopkeeper").GetComponent<Shopkeeper>();
        if (shopkeeper == null) { Debug.Log("Shopkeeper null!"); }

        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        if (playerController == null) { Debug.Log("Player Controller null!"); }

        HideShopPanel();

        UpdateUI();
    }

    public void UpdateUI()
    {
        // Update currency balances
        playerCurrencyText.text = "Player Money: " + playerController.playerInventory.currency.ToString();
        shopCurrencyText.text = "Shop Money: " + shopkeeper.shopInventory.currency.ToString();

        // Update player and shop inventories
        shopkeeper.PopulateShopUI(shopkeeper.shopInventory.GetInventory());
        playerController.PopulateInventoryUI(playerController.playerInventory.GetInventory());
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
        UpdateUI();
        shopPanel.SetActive(true);
    }

    public void HideShopPanel()
    {
        shopPanel.SetActive(false);
    }
}
