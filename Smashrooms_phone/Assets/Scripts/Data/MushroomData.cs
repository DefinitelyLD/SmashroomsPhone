using UnityEngine;

[CreateAssetMenu]
public class MushroomData : ScriptableObject 
{
    public MushroomType type;

    public Sprite mushroomSprite;
    public Sprite islandSprite;

    [Range(1,9)]
    public int strength;

    [Range(1,9)]
    public int agility;

    [Range(1,9)]
    public int intelligence;

    [Range(1,9)]
    public int endurance;
}

public enum MushroomType
{
    basicMushroom,
    fireMushroom,
}