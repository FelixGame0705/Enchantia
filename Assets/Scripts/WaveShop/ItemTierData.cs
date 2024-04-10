using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ItemTierData", order = 0)]
[Serializable]
public class ItemTierData : ScriptableObject {
    [SerializeField] private int waveAvailable;
    [SerializeField] private int minWave;
    [SerializeField] private float baseChance;
    [SerializeField] private float chancePerWave;
    [SerializeField] private float maxChance;
    [SerializeField] private int tier;

    public int WaveAvailable {get => waveAvailable;}
    public int MinWave {get => minWave;}
    public float BaseChance {get => baseChance;}
    public float ChancePerWave {get => chancePerWave;}
    public float MaxChance {get => maxChance;}
    public int Tier {get => tier;}
}

