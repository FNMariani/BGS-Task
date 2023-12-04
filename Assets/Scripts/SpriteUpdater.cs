using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SpriteUpdater : MonoBehaviour
{
    private PlayerController playerController;
    private Vector2 playerDirectionRef;
    private SpriteRenderer spriteRenderer;

    public float frameRate;
    float idleTime;

    public string SpriteLocation;
    public Sprite[] spriteArray;

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
    }

    void LoadSpritesWhenReady(AsyncOperationHandle<Sprite[]> handleToCheck, ref Sprite[] spriteArray, int spriteIndex)
    {
        if (handleToCheck.Status == AsyncOperationStatus.Succeeded)
        {
            spriteArray = handleToCheck.Result;

            for (int i = 0; i < WalkSpritesQty; i++)
            {
                northSprites.Add(spriteArray[i + NorthSpritesOffset]);
                eastSprites.Add(spriteArray[i + EastSpritesOffset]);
                southSprites.Add(spriteArray[i + SouthSpritesOffset]);
                westSprites.Add(spriteArray[i + WestSpritesOffset]);
            }
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

        if (direction.y > 0)
        {
            selectedSprites = northSprites;
        }
        else if (direction.y < 0)
        {
            selectedSprites = southSprites;
        }

        return selectedSprites;
    }
}
