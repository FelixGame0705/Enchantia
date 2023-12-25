using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
[CreateAssetMenu(fileName = "SpriteTier_", menuName = "ScriptableObjects/SpriteTier", order = 5)]
public class TierCardSpriteInfo : ScriptableObject
{
    [SerializeField] public int tier;
    [SerializeField] private Sprite background;
    [SerializeField] private Sprite iconBackground;
    [SerializeField] private Color tierColor;
    [SerializeField] private Color nameColor;

    public int Tier { get => tier; set => tier = value; }
    public Sprite Background { get => background; set => background = value; }
    public Sprite IconBackground { get => iconBackground; set => iconBackground = value; }
    public Color TierColor { get => tierColor; set => tierColor = value; }
    public Color NameColor { get => nameColor; set => nameColor = value; }
}
