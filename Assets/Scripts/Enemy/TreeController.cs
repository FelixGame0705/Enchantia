using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : EnemyBase
{
    [SerializeField] private List<GameObject> awardObject;
    public override void TakeDamage(float health)
    {
        if (CurrentHealth <= 0)
        {

        }
    }

    private void SpawnAwardObject()
    {
        for(int i = 0; i < awardObject.Count; i++)
        {
            
        }
    }

    protected override void Attack()
    {
    }

    protected override void Move()
    {
    }

    protected override void Rotate()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
