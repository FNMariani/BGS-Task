using UnityEngine;

[CreateAssetMenu(fileName = "GameItem", menuName = "Item")]
public class GameItem : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public int itemPrice;
    public string itemDescription;
    public string itemSpriteLocation;
    public PartType itemPartType;
    public bool isEquipped;
}