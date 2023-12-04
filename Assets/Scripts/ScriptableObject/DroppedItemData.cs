using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DroppedItemData", menuName = "ScriptableObjects/DroppedItemConfig", order = 1)]
public class DroppedItemData : ScriptableObject
{
    public DROPPED_ITEM_TYPE DroppedItemType;
    public GameObject modelPrefab;

    [SerializeField] private int hpValue;
    [SerializeField] private int goldValue;

    public int HpValue { get => hpValue; set => hpValue = value; }
    public int GoldValue { get => goldValue; set => goldValue = value; }
}
