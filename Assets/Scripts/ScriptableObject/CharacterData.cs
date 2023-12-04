using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerConfig", order = 1)]
public class CharacterData : ScriptableObject
{
    //public Character CharacterStats;
    public Character_Mod Character_Mod;
    [SerializeField] private List<ItemData> firstItems;
    public List<ItemData> FirstItems { get => firstItems; set => firstItems = value; }
}
