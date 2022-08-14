using System;

public class Mushroom
{
    public static event Action<int> OnStatsChaged;
    public static event Action<int, int> OnRankChanged;

    private const float LEVELS_TO_RANK_UP = 5; 
    public const int MAX_RANK = 3;
    public const int MAX_LEVEL = 20;
    public const float STATS_ON_RANK_UP = 0.5f;

    private int rank = 1;
    public int Rank
    {
        get => rank;
        set
        {
            rank = value;
            OnRankChanged?.Invoke((int)mushroomType, rank);
        }
    }
    public int level;
    private int currentXP;

    public int CurrentXP
    {
        get => currentXP;

        set
        {
            currentXP = value;
            if(level == rank * LEVELS_TO_RANK_UP) 
            currentXP = currentXP > Utility.levelXp[level - 1] ? Utility.levelXp[level - 1] : currentXP;

            if(currentXP >= Utility.levelXp[level - 1] && level < rank * LEVELS_TO_RANK_UP &&  level < MAX_LEVEL)
            {
                currentXP = currentXP - Utility.levelXp[level - 1];
                level += 1;
            }
        }
    }

    public float strength, agility, intelligence, endurance;

    private MushroomType mushroomType;

    public Mushroom(MushroomType mushroom)
    {
        rank = 1;
        level = 1;

        mushroomType = mushroom;
        strength = Storage.GetMushroomStrength(mushroom);
        agility = Storage.GetMushroomAgility(mushroom);
        intelligence = Storage.GetMushroomIntelligence(mushroom);
        endurance = Storage.GetMushroomEndurance(mushroom);
    }


    public void RankUP()
    {
        if(rank == MAX_RANK) return;

        strength += 0.5f;
        agility += 0.5f;
        intelligence += 0.5f;
        endurance += 0.5f;

        rank += 1;
        OnStatsChaged.Invoke((int) mushroomType);
    }

    public bool CanRankUp() => level == rank * LEVELS_TO_RANK_UP && currentXP >= Utility.levelXp[level - 1];
}
