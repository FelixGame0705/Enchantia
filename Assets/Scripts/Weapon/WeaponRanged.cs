using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WeaponRanged : WeaponBase
{
    protected override void Attack()
    {

    }

    protected override void Rotate()
    {
        if (Target == null) return;
        Vector2 direction = Target.transform.position - transform.position;
        direction.Normalize(); 
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public override void SetTargetForAttack(Transform target)
    {
        Target = target.gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }
}
