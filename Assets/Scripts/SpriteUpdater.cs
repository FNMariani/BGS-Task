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

    public Sprite idleSprite;
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
        AsyncOperationHandle<Sprite[]> spriteHandle = Addressables.LoadAssetAsync<Sprite[]>(SpriteLocation);
        spriteHandle.Completed += handle => LoadSpritesWhenReady(handle, ref spriteArray, 0);

        spriteRenderer = GetComponent<SpriteRenderer>();

        //Get player reference
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

        //DEBUG
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UpdateSpritesAtlas(PartType.Clothes, "Assets/Sprites/Player/char_a_p1/1out/char_a_p1_1out_fstr_v04.png");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UpdateSpritesAtlas(PartType.Hat, "Assets/Sprites/Player/char_a_p1/5hat/char_a_p1_5hat_pnty_v04.png");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UpdateSpritesAtlas(PartType.Clothes, "Assets/Sprites/Player/char_a_p1/1out/char_a_p1_1out_boxr_v01.png");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UpdateSpritesAtlas(PartType.Hat, "Assets/Sprites/Player/char_a_p1/5hat/char_a_p1_5hat_pfht_v04.png");
        }
    }

    void LoadSpritesWhenReady(AsyncOperationHandle<Sprite[]> handleToCheck, ref Sprite[] spriteArray, int spriteIndex)
    {
        if (handleToCheck.Status == AsyncOperationStatus.Succeeded)
        {
            spriteArray = handleToCheck.Result;

            northSprites.Clear();
            eastSprites.Clear();
            southSprites.Clear();
            westSprites.Clear();

            for (int i = 0; i < WalkSpritesQty; i++)
            {
                northSprites.Add(spriteArray[i + NorthSpritesOffset]);
                eastSprites.Add(spriteArray[i + EastSpritesOffset]);
                southSprites.Add(spriteArray[i + SouthSpritesOffset]);
                westSprites.Add(spriteArray[i + WestSpritesOffset]);
            }

            idleSprite = spriteArray[0];
        }
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
        }
        else if (direction.x < 0)
        {
            selectedSprites = westSprites;
        }
        else if (direction.y > 0)
        {
            selectedSprites = northSprites;
        }
        else if (direction.y < 0)
        {
            selectedSprites = southSprites;
        }
        else
        {
            spriteRenderer.sprite = idleSprite;
        }

        return selectedSprites;
    }

    void UpdateSpritesAtlas(PartType _partType, string SpriteLocation)
    {

        if(partType == _partType)
        {
            AsyncOperationHandle<Sprite[]> spriteHandle = Addressables.LoadAssetAsync<Sprite[]>(SpriteLocation);
            spriteHandle.Completed += handle => LoadSpritesWhenReady(handle, ref spriteArray, 0);
        }
    }
}
