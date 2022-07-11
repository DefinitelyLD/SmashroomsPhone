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

    intelligencePotion = 1,
    strengthPotion = 2,
    agilityPotion = 3,
    endurancePotion = 4,
    crystal_lvl_1 = 5,
    crystal_lvl_2 = 6,
    crystal_lvl_3 = 7,
    crystal_lvl_4 = 8,
    crystal_lvl_5 = 9,
    goldClover = 10,
    gloomDust = 11,
    fireEssence = 12,
    airEssence = 13,
    relic = 14,
}
