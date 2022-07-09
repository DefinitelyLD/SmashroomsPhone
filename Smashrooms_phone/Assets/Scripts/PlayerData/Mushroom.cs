using System.Collections.Generic;
using System;

public class Mushroom
{
    public static event Action<int> OnStatsChaged;

    private const float LEVELS_TO_RANK_UP = 5; 
    public const int MAX_RANK = 3;
    public const int MAX_LEVEL = 20;
    public const float STATS_ON_RANK_UP = 0.5f;

    public int rank;
    public int level;
    public int currentXP;

    private List<int> levelXp = new List<int>();

    public float strength, agility, intelligence, endurance;

    private MushroomType mushroomType;

    public Mushroom(MushroomType mushroom, List<int> _levelXp)
    {
        rank = 1;
        level = 1;

        levelXp = _levelXp;

        strength = Storage.GetMushroomStrength(mushroom);
        agility = Storage.GetMushroomAgility(mushroom);
        intelligence = Storage.GetMushroomIntelligence(mushroom);
        endurance = Storage.GetMushroomEndurance(mushroom);
    }

    public void AddXp(int amount)
    {
        currentXP += amount;

        if(level == rank * LEVELS_TO_RANK_UP) 
        currentXP = currentXP > levelXp[level - 1] ? levelXp[level - 1] : currentXP;

        if(currentXP >= levelXp[level - 1] && level < rank * LEVELS_TO_RANK_UP &&  level < MAX_LEVEL)
        {
            currentXP = currentXP - levelXp[level - 1];
            level += 1;
        }
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

    public bool CanRankUp() => level == rank * LEVELS_TO_RANK_UP && currentXP >= levelXp[level - 1];
}
