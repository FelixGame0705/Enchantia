using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerConfig", order = 1)]
public class CharacterData : ScriptableObject
{
    public Character CharacterStats;
}
