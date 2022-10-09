using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Block", menuName = "Assets/Block")]
public class ScriptableBlock : ScriptableObject
{
    public Faction faction;
    public Block prefab;
}

public enum Faction
{
    Bishop,
    Dragon,
    Knight,
    Rock
}
