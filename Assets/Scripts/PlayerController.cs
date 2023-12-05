using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D body;
    Vector2 direction;

    public float walkSpeed;

    public Inventory playerInventory;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
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
}
