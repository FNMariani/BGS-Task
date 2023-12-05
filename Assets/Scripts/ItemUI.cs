using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ItemUI : MonoBehaviour
{
    public Image itemIconImage;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemPriceText;
    public TextMeshProUGUI itemDescriptionText;
    public Button actionButton;

    private ShopManager uiManager;
    private GameItem item;

    /*public void UpdateUI(GameItem item)
    {
        itemIconImage.sprite = item.itemIcon;
        itemNameText.text = item.itemName;
        itemPriceText.text = "Price: " + item.itemPrice.ToString();
        itemDescriptionText.text = item.itemDescription;
    }*/

    public void Initialize(ShopManager uiManager, GameItem item, string buttonAction)
    {
        this.uiManager = uiManager;
        this.item = item;

        itemIconImage.sprite = item.itemIcon;
        itemNameText.text = item.itemName;
        itemPriceText.text = "$" + item.itemPrice.ToString();
        itemDescriptionText.text = item.itemDescription;

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
