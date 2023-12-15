using System.Collections.Generic;
using UnityEngine;
using System;

public class Utils : Singleton<Utils>
{
    public float GetAnglesFromVector(Vector3 vectorToCheck)
    {
        float angleInRadians = Mathf.Atan2(vectorToCheck.y, vectorToCheck.x);
        float angleInDegrees = angleInRadians * Mathf.Rad2Deg;
        return angleInDegrees;
    }
// Min reroll is 0, Min currentWave = 1
    public int GetWaveShopRerollPrice(int currentWave, int rerollTime){
        //Reroll Increase = Rounddown(max(0.5 * wave,1))
        //First Reroll Price = Wave + Reroll Increase
        var increasement = (int)Math.Floor(Math.Max(0.5 * currentWave, 1));
        return currentWave + increasement + increasement * rerollTime;
    }

    public int GetFinalPrice(int basePrice, int currentWave, int shopPrice = 1){
        return (int) (basePrice + currentWave + (basePrice * 0.1 * currentWave)) * shopPrice;
    }

    public float GetChanceRateTierPerWave(float chanceWave, int currentWave, int minWave, float luck , float baseChance){
        return ((chanceWave * (currentWave - minWave)) + baseChance) * (1 + luck);
    }

    public void ShuffleList<T>(ref List<T> list)
    {
        // Implement a list shuffling algorithm (e.g., Fisher-Yates shuffle)
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
