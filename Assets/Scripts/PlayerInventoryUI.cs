using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class PlayerInventoryUI : MonoBehaviour
{
    public SpriteRenderer spriteBody;
    private SpriteRenderer spriteClothes;
    private SpriteRenderer spriteHair;
    private SpriteRenderer spriteHat;

    public Image imageBody;
    public Image imageClothes;
    public Image imageHair;
    public Image imageHat;

    PlayerController playerController;

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();

            spriteBody = playerController.GetBody().GetComponent<SpriteRenderer>();
            spriteClothes = playerController.GetClothes().GetComponent<SpriteRenderer>();
            spriteHair = playerController.GetHair().GetComponent<SpriteRenderer>();
            spriteHat = playerController.GetHat().GetComponent<SpriteRenderer>();
        }
    }

    void Update()
    {
        UpdateInventoryUI();
    }

    void UpdateInventoryUI()
    {
        imageBody.sprite = spriteBody.sprite;
        imageClothes.sprite = spriteClothes.sprite;
        imageHair.sprite = spriteHair.sprite;
        imageHat.sprite = spriteHat.sprite;
    }
}
