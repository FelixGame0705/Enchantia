using System.Collections;
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
}
