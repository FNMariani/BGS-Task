using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class SpriteUpdater : MonoBehaviour
{
    public PartType partType;

    private PlayerController playerController;
    private Vector2 playerDirectionRef;
    private SpriteRenderer spriteRenderer;

    public float frameRate;
    float idleTime;

    public string SpriteLocation;
    public Sprite[] spriteArray;

    public int idleNorthSprite;
    public int idleEastSprite;
    public int idleSouthSprite;
    public int idleWestSprite;
    public List<Sprite> northSprites;
    public List<Sprite> eastSprites;
    public List<Sprite> southSprites;
    public List<Sprite> westSprites;

    public int WalkSpritesQty;

    public int NorthSpritesOffset;
    public int EastSpritesOffset;
    public int SouthSpritesOffset;
    public int WestSpritesOffset;

    void Start()
    {
        LoadSpritesWhenReady(SpriteLocation, ref spriteArray, 0);

        spriteRenderer = GetComponent<SpriteRenderer>();

        // Get player reference
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerController = player.GetComponent<PlayerController>();
        }
    }

    void Update()
    {
        playerDirectionRef = playerController.GetDirection();
        SetSprites();
    }

    void LoadSpritesWhenReady(string path, ref Sprite[] sprites, int spriteIndex)
    {
        sprites = Resources.LoadAll<Sprite>(path);

        northSprites.Clear();
        eastSprites.Clear();
        southSprites.Clear();
        westSprites.Clear();

        for (int i = 0; i < WalkSpritesQty; i++)
        {
            northSprites.Add(sprites[i + NorthSpritesOffset]);
            eastSprites.Add(sprites[i + EastSpritesOffset]);
            southSprites.Add(sprites[i + SouthSpritesOffset]);
            westSprites.Add(sprites[i + WestSpritesOffset]);
        }

        southSprites[idleSouthSprite] = sprites[0];
    }

    void SetSprites()
    {
        List<Sprite> directionSprites = GetSpritesFromDirection(playerDirectionRef);

        if (directionSprites != null)
        {
            float playTime = Time.time - idleTime;
            int totalFrames = (int)(playTime * frameRate);
            int frame = totalFrames % directionSprites.Count;

            spriteRenderer.sprite = directionSprites[frame];
        }
        else
        {
            idleTime = Time.time;
        }
    }

    List<Sprite> GetSpritesFromDirection(Vector2 direction)
    {
        List<Sprite> selectedSprites = null;

        // Update sprite based on movement direction
        if (direction.x > 0)
        {
            selectedSprites = eastSprites;
            playerController.lastDirection = 1; //East
        }
        else if (direction.x < 0)
        {
            selectedSprites = westSprites;
            playerController.lastDirection = 2; //West
        }
        else if (direction.y > 0)
        {
            selectedSprites = northSprites;
            playerController.lastDirection = 3; //North
        }
        else if (direction.y < 0)
        {
            selectedSprites = southSprites;
            playerController.lastDirection = 4; //South
        }
        else
        {
            if(playerController.lastDirection == 1) spriteRenderer.sprite = spriteArray[idleEastSprite];
            if(playerController.lastDirection == 2) spriteRenderer.sprite = spriteArray[idleWestSprite];
            if(playerController.lastDirection == 3) spriteRenderer.sprite = spriteArray[idleNorthSprite];
            if(playerController.lastDirection == 4) spriteRenderer.sprite = spriteArray[idleSouthSprite];
        }

        return selectedSprites;
    }

    public void UpdateSpritesAtlas(PartType _partType, string path)
    {
        if (partType == _partType)
        {
            LoadSpritesWhenReady(path, ref spriteArray, 0);
        }
    }

    public void DefaultSpritesAtlas(PartType _partType)
    {
        string path = "";

        switch (_partType)
        {
            case PartType.Body:
                path = "Sprites/char_a_p1/char_a_p1_0bas_humn_v01";
                break;
            case PartType.Hair:
                path = "Sprites/char_a_p1/4har/char_a_p1_4har_bob1_v01";
                break;
            case PartType.Hat:
                path = "Sprites/char_a_p1/4har/char_a_p1_4har_bob1_v01";
                break;
            case PartType.Clothes:
                path = "Sprites/char_a_p1/1out/char_a_p1_1out_boxr_v01";
                break;
        }

        LoadSpritesWhenReady(path, ref spriteArray, 0);
    }
}