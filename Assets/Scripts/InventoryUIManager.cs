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

    private PlayerController playerController;

    private InventoryUIManager uiManager;
    private GameItem item;

    void Start()
    {
        //Set references
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        if (playerController == null) { Debug.Log("Player Controller null!"); }

        uiManager = GameObject.FindWithTag("InventoryPanel").GetComponent<InventoryUIManager>();
        if (uiManager == null) { Debug.Log("InventoryPanel null!"); }

        HideInventoryPanel();
    }

    public void UpdateUI()
    {
        playerCurrencyText.text = "Player Money: $" + playerController.GetPlayerInventory().currency.ToString();

        PopulateInventoryUI(playerController.GetPlayerInventory().GetInventory());
    }

    public void PopulateInventoryUI(List<ItemInstance> inventoryItems)
    {
        ClearInventoryUI();

        foreach (ItemInstance itemIns in inventoryItems)
        {
            GameItem item = itemIns.itemType;

            GameObject inventoryItemObject = Instantiate(shopItemPrefab, panelContent);
            ItemUI shopItemUI = inventoryItemObject.GetComponent<ItemUI>();

            if (shopItemUI != null)
            {
                this.item = item;

                inventoryItemObject.GetComponent<ItemUI>().itemIconImage.sprite = item.itemIcon;
                inventoryItemObject.GetComponent<ItemUI>().itemNameText.text = item.itemName;
                inventoryItemObject.GetComponent<ItemUI>().itemPriceText.text = "$" + item.itemPrice.ToString();
                inventoryItemObject.GetComponent<ItemUI>().itemDescriptionText.text = item.itemDescription;

                if (item.isEquipped)
                {
                    inventoryItemObject.GetComponent<ItemUI>().actionButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Unequip"; //TODO: replace GetChild
                    inventoryItemObject.GetComponent<ItemUI>().actionButton.onClick.AddListener(() => playerController.Unequip(itemIns));
                }
                else
                {
                    inventoryItemObject.GetComponent<ItemUI>().actionButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Equip"; //TODO: replace GetChild
                    inventoryItemObject.GetComponent<ItemUI>().actionButton.onClick.AddListener(() => playerController.Equip(item));
                }
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

        GameObject.FindWithTag("MainCamera").GetComponent<AudioController>().PlayOpenCloseSound();

        playerController.isBusyInventory = true;
    }

    public void HideInventoryPanel()
    {
        inventoryPanel.SetActive(false);

        GameObject.FindWithTag("MainCamera").GetComponent<AudioController>().PlayOpenCloseSound();

        playerController.isBusyInventory = false;
    }
}
