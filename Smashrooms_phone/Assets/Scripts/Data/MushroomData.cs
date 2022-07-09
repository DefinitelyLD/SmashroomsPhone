using UnityEngine;

[CreateAssetMenu]
public class MushroomData : ScriptableObject 
{
    public MushroomType type;

    public Sprite mushroomSprite;
    public Sprite islandSprite;

    [Range(1,9)]
    public float strength;

    [Range(1,9)]
    public float agility;

    [Range(1,9)]
    public float intelligence;

    [Range(1,9)]
    public float endurance;
}

public enum MushroomType
{
    basicMushroom,
    fireMushroom,
}