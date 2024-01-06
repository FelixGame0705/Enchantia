using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemThrowRock : MonoBehaviour
{
    [SerializeField] GolemBoss golemBoss;
    
    public void ThrowRock()
    {
        golemBoss.SpawnBullet();
    }

    public void ThrowBigRock()
    {
        golemBoss.SpawnBigBullet();
    }
}
