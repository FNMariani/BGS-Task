using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D RB;
    Vector2 direction;

    public float walkSpeed;

    public Inventory playerInventory;

    public GameObject inventoryPanel;

    public Transform inventoryPanelContent;
    public GameObject itemPrefab;
    public ShopManager uiManager;

    public List<ItemInstance> shopItems; //DEBUG

    public bool isBusyInventory = false;
    public bool isBusyShopping = false;

    /// <summary>
    /// East | West | North | South
    /// </summary>
    public int lastDirection = -1;

    void Start()
    {
        //Set references
        /*inventoryPanel = GameObject.FindWithTag("InventoryPanel");*/
        if (inventoryPanel == null) { Debug.Log("InventoryPanel null!"); }

        //inventoryPanelContent = GameObject.FindWithTag("ShopPanelContent_Player").transform;
        if (inventoryPanelContent == null) { Debug.Log("InventoryPanelContent null!"); }

        //uiManager = GameObject.FindWithTag("ShopPanel").GetComponent<ShopManager>();
        if (uiManager == null) { Debug.Log("ShopPanel null!"); }

        RB = GetComponent<Rigidbody2D>();
        //playerInventory = GetComponent<Inventory>();
        //playerInventory = gameObject.AddComponent(typeof(Inventory)) as Inventory;

        foreach (ItemInstance item in shopItems)
        {
            playerInventory.AddItem(item);
        }
    }

    public void SetReferences(GameObject canvas)
    {
        //Set references
        inventoryPanel = GameObject.FindWithTag("InventoryPanel");
        if (inventoryPanel == null) { Debug.Log("InventoryPanel null!"); }

        inventoryPanelContent = GameObject.FindWithTag("ShopPanelContent_Player").transform;
        if (inventoryPanelContent == null) { Debug.Log("InventoryPanelContent null!"); }

        uiManager = GameObject.FindWithTag("ShopPanel").GetComponent<ShopManager>();
        if (uiManager == null) { Debug.Log("ShopPanel null!"); }
    }

    void Update()
    {
        //DEBUG
        if (!isBusyShopping && Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventoryPanel();
        }
    }

    private void FixedUpdate()
    {
        if (RB != null)
        {
            if (!isBusyInventory && !isBusyShopping)
            {
                direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
                RB.velocity = direction * walkSpeed * Time.fixedDeltaTime;
            }
            else
            {
                direction = Vector2.zero;
                RB.velocity = Vector2.zero;
            }
        }
    }

    public Vector2 GetDirection()
    {
        return direction;
    }

    public void InteractWithShop(Inventory shopInventory, ItemInstance item)
    {
        if (playerInventory.BuyItem(item))
        {
            //Successful purchase
            shopInventory.SellItem(item);
            GameObject.FindWithTag("MainCamera").GetComponent<AudioController>().PlayCoinSound();
        }
        else
        {
            //Purchase failed
            Debug.LogWarning("Purchase failed.");
        }
    }

    public void ToggleInventoryPanel()
    {
        GameObject.FindWithTag("MainCamera").GetComponent<AudioController>().PlayOpenCloseSound();
        if (inventoryPanel.activeSelf)
        {
            isBusyInventory = false;
            inventoryPanel.GetComponent<InventoryUIManager>().HideInventoryPanel();
        }
        else
        {
            isBusyInventory = true;
            direction = new Vector2(0, -1);
            inventoryPanel.GetComponent<InventoryUIManager>().ShowInventoryPanel();
        }
    }

    public void Equip(GameItem item)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform childTransform = transform.GetChild(i);
            if(item.itemPartType == childTransform.GetComponent<SpriteUpdater>().partType)
            {
                //TODO Refactor this
                if(childTransform.GetComponent<PlayerPartEquipment>().itemEquipped)
                {
                    childTransform.GetComponent<PlayerPartEquipment>().itemEquipped.isEquipped = false;
                }
                childTransform.GetComponent<PlayerPartEquipment>().itemEquipped = item;

                item.isEquipped = true;
                childTransform.GetComponent<SpriteUpdater>().UpdateSpritesAtlas(item.itemPartType, item.itemSpriteLocation);

                inventoryPanel.GetComponent<InventoryUIManager>().PopulateInventoryUI(playerInventory.GetInventory());

                GameObject.FindWithTag("MainCamera").GetComponent<AudioController>().PlayEquipSound();

                return;
            }
        }
    }

    public void Unequip(ItemInstance item)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform childTransform = transform.GetChild(i);
            if (item.itemType.itemPartType == childTransform.GetComponent<SpriteUpdater>().partType)
            {
                item.itemType.isEquipped = false;
                childTransform.GetComponent<SpriteUpdater>().DefaultSpritesAtlas(item.itemType.itemPartType);

                inventoryPanel.GetComponent<InventoryUIManager>().PopulateInventoryUI(playerInventory.GetInventory());

                GameObject.FindWithTag("MainCamera").GetComponent<AudioController>().PlayEquipSound();

                return;
            }
        }
    }

    public void PopulateInventoryUI(List<ItemInstance> inventoryItems)
    {
        ClearInventoryUI();

        foreach (ItemInstance itemInst in inventoryItems)
        {
            GameItem item = itemInst.itemType;

            GameObject itemObject = Instantiate(itemPrefab, inventoryPanelContent);
            ItemUI itemUI = itemObject.GetComponent<ItemUI>();

            if (itemUI != null)
            {
                itemUI.Initialize(uiManager, itemInst, "Sell");
            }
        }
    }

    private void ClearInventoryUI()
    {
        foreach (Transform child in inventoryPanelContent)
        {
            Destroy(child.gameObject);
        }
    }

    public Inventory GetPlayerInventory()
    {
        return playerInventory;
    }

    public GameObject GetBody()
    {
        GameObject body = transform.GetChild(0).gameObject;

        //Check part by name
        if (body == null || body.name != "Body")
        {
            if (body.name != "Body") Debug.LogError("Body is wrong!");

            Debug.LogError("One part not found!");
        }

        return body;
    }

    public GameObject GetClothes()
    {
        GameObject clothes = transform.GetChild(1).gameObject;

        //Check part by name
        if (clothes == null || clothes.name != "Clothes")
        {
            if (clothes.name != "Clothes") Debug.LogError("Clothes are wrong!");

            Debug.LogError("One part not found!");
        }

        return clothes;
    }

    public GameObject GetHair()
    {
        GameObject hair = transform.GetChild(2).gameObject;

        //Check part by name
        if (hair == null || hair.name != "Hair")
        {
            if (hair.name != "Hair") Debug.LogError("Hair is wrong!");

            Debug.LogError("One part not found!");
        }

        return hair;
    }

    public GameObject GetHat()
    {
        GameObject hat = transform.GetChild(3).gameObject;

        //Check part by name
        if (hat == null || hat.name != "Hat")
        {
            if (hat.name != "Hat") Debug.LogError("Hat is wrong!");

            Debug.LogError("One part not found!");
        }

        return hat;
    }
}
