using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RankUpPressets : ScriptableObject
{
    public int rankId;
    public List<ItemWithAmount> itemsToRankUp = new List<ItemWithAmount>();
}
[System.Serializable]
public struct ItemWithAmount
{
    public ItemType item;
    public int amount;

    public ItemWithAmount (ItemType _item, int _amout)
    {
        item = _item;
        amount = _amout;
    }
}