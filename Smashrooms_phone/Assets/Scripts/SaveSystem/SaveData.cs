using System.Collections.Generic;

[System.Serializable]
public class SaveData 
{
    public bool soundOn;

    public int currentLvl = 1;
    public int currentLvlXp;

    public List<int> mushroomLvls = new List<int>();
    public List<int> mushroomXps = new List<int>();

    public int mushroomTrophies;
    public int mushroomTokens;

    public MushroomType selectedMushroom;
}
