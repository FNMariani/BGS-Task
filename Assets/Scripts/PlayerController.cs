using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D body;
    Vector2 direction;

    public float walkSpeed;

    public Inventory playerInventory;

    public GameObject inventoryPanel;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //DEBUG
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventoryPanel();
        }
    }

    private void FixedUpdate()
    {
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        body.velocity = direction * walkSpeed * Time.fixedDeltaTime;
    }

    public Vector2 GetDirection()
    {
        return direction;
    }

    public void InteractWithShop(Inventory shopInventory, GameItem item)
    {
        if (playerInventory.BuyItem(item))
        {
            //Successful purchase
            shopInventory.SellItem(item);
        }
        else
        {
            //Purchase failed
            Debug.LogWarning("Purchase failed.");
        }
    }

    public void ToggleInventoryPanel()
    {
        //inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        if(inventoryPanel.activeSelf)
        {
            inventoryPanel.GetComponent<InventoryUIManager>().HideInventoryPanel();
        }
        else
        {
            inventoryPanel.GetComponent<InventoryUIManager>().ShowInventoryPanel();
        }
    }

    public void Equip(GameItem item)
    {
        Debug.Log(item);

        for (int i = 0; i < transform.childCount; i++)
        {
            // Access each child by index
            Transform childTransform = transform.GetChild(i);

            // Do something with the child
            Debug.Log("Child name: " + childTransform.name);
            if(item.itemPartType == childTransform.GetComponent<SpriteUpdater>().partType)
            {
                childTransform.GetComponent<SpriteUpdater>().UpdateSpritesAtlas(item.itemPartType, item.itemSpriteLocation);
            }
            
        }
    }
}
