using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : Singleton<Utils>
{
    public float GetAnglesFromVector(Vector3 vectorToCheck)
    {
        float angleInRadians = Mathf.Atan2(vectorToCheck.y, vectorToCheck.x);
        float angleInDegrees = angleInRadians * Mathf.Rad2Deg;
        return angleInDegrees;
    }
}
