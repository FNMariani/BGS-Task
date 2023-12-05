using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{
    public GameObject inventoryPanel;
    public Transform panelContent;
    public GameObject shopItemPrefab;

    public TextMeshProUGUI playerCurrencyText;

    public PlayerController playerController;

    public InventoryUIManager uiManager;
    private GameItem item;

    public Image itemIconImage;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemPriceText;
    public TextMeshProUGUI itemDescriptionText;
    public Button buyButton;

    void Start()
    {
        HideInventoryPanel();

        UpdateUI();
    }

    public void UpdateUI()
    {
        playerCurrencyText.text = "Player Money: " + playerController.playerInventory.currency.ToString();

        PopulateInventoryUI(playerController.playerInventory.GetInventory());
    }

    public void PopulateInventoryUI(List<GameItem> inventoryItems)
    {
        ClearInventoryUI();

        foreach (GameItem item in inventoryItems)
        {
            GameObject inventoryItemObject = Instantiate(shopItemPrefab, panelContent);
            ItemUI shopItemUI = inventoryItemObject.GetComponent<ItemUI>();

            if (shopItemUI != null)
            {
                //shopItemUI.Initialize(this, item);
                this.item = item;

                inventoryItemObject.GetComponent<ItemUI>().itemIconImage.sprite = item.itemIcon;
                inventoryItemObject.GetComponent<ItemUI>().itemNameText.text = item.itemName;
                inventoryItemObject.GetComponent<ItemUI>().itemPriceText.text = "Price: " + item.itemPrice.ToString();
                inventoryItemObject.GetComponent<ItemUI>().itemDescriptionText.text = item.itemDescription;

                inventoryItemObject.GetComponent<ItemUI>().buyButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Equip"; //TODO: replace GetChild
                inventoryItemObject.GetComponent<ItemUI>().buyButton.onClick.AddListener(() => playerController.Equip(item));
            }
        }
    }

    private void ClearInventoryUI()
    {
        foreach (Transform child in panelContent)
        {
            Destroy(child.gameObject);
        }
    }

    public void ShowInventoryPanel()
    {
        UpdateUI();

        inventoryPanel.SetActive(true);
    }

    public void HideInventoryPanel()
    {
        inventoryPanel.SetActive(false);
    }
}