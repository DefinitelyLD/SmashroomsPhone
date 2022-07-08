using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public ItemType type;
    public string itemName;
    public Sprite itemIcon;
}
public enum ItemType
{
    NONE = 0,

    gloomDust = 1,
    crystal = 2,
    goldClover = 3,
}
