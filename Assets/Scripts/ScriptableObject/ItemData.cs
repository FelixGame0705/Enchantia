using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ItemConfig", order = 1)]
public class ItemData : ScriptableObject
{
    public Item ItemStats;
    [SerializeField] private Sprite itemImg;
    [SerializeField] private Sprite itemBG;
    [SerializeField] private Color rarityColor;
}
