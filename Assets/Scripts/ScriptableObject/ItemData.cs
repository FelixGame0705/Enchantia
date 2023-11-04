using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ItemConfig", order = 1)]
[Serializable]
public class ItemData : ScriptableObject
{
    public Item ItemStats;
    [SerializeField] private Sprite itemImg;
    [SerializeField] private Sprite itemBG;
    [SerializeField] private Color rarityColor;
    [SerializeField] private string itemName;
    [SerializeField] private string itemDescription;
    [SerializeField] private int itemPrice;
    [SerializeField] private ItemData nextItemWeapon;
    [SerializeField] private int id;
    

    public Sprite ItemImg { get =>  itemImg; set => itemImg = value;}
    public Sprite ItemBG { get => itemBG; set => itemBG = value;}
    public Color RarityColor { get => rarityColor; set => rarityColor = value;}
    public string ItemName { get => itemName; set => itemName = value;}
    public string ItemDescription { get => itemDescription; set => itemDescription = value;}
    public int ItemPrice { get => itemPrice; set => itemPrice = value;}
    public ItemData NextItemWeapon { get => nextItemWeapon; set => nextItemWeapon = value; }
    public int Id { get => id; set => id = value; }
}
