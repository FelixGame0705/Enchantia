using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
[CreateAssetMenu(fileName = "WaveRatio", menuName = "ScriptableObjects/WaveRatio", order = 3)]
public class WaveRatio : ScriptableObject
{
    [SerializeField] private int[] wave; // if wave < 0 that will be default ratio
    [SerializeField] private int minWeapon;
    [SerializeField] private int minItem;

    public int[] Wave {get => wave;}
    public int MinWeapon {get => minWeapon;}
    public int MinItem {get => minItem;}

    public bool IsWaveValid(int currentWave){
        foreach(var i in wave){
            if(i == currentWave) return  true;
        }
        return false;
    }    
}
