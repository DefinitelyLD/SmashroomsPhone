using System.Collections.Generic;

[System.Serializable]
public class PlayerDataSave
{
    public MushroomType selectedMushroom;
    public bool intelligencePotionUsed, agilityPotionUsed, strengthPotionUsed, endurancePotionUsed;

    public int currentXP;
    public int currentLvl;
    public int trophies;
    public int tokens;

    public List<int> mushroomRanks = new List<int>();
    public List<int> mushroomLvls = new List<int>();
    public List<int> mushroomCurrentXps = new List<int>();

    public List<float> mushroomStrength = new List<float>();
    public List<float> mushroomAgility = new List<float>();
    public List<float> mushroomIntelligence = new List<float>();
    public List<float> mushroomEndurance = new List<float>();

    public List<ItemType> droppedItems = new List<ItemType>();
    public List<int> droppedItemsAmount = new List<int>();
}
