using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Image itemIconImage;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemPriceText;
    public TextMeshProUGUI itemDescriptionText;
    public Button actionButton;

    private ShopManager uiManager;
    private ItemInstance item;

    public void Initialize(ShopManager uiManager, ItemInstance item, string buttonAction)
    {
        this.uiManager = uiManager;
        this.item = item;

        itemIconImage.sprite = item.itemType.itemIcon;
        itemNameText.text = item.itemType.itemName;
        itemPriceText.text = "$" + item.itemType.itemPrice.ToString();
        itemDescriptionText.text = item.itemType.itemDescription;

        actionButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = buttonAction;

        if(buttonAction == "Buy") 
        {
            actionButton.onClick.AddListener(BuyButtonClicked);
        }
        else if (buttonAction == "Sell")
        {
            actionButton.onClick.AddListener(SellButtonClicked);
        }
    }

    public void BuyButtonClicked()
    {
        uiManager.BuyButtonClicked(item);
    }

    public void SellButtonClicked()
    {
        uiManager.SellButtonClicked(item);
    }
}
