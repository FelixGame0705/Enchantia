using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WeaponRanged : WeaponBase
{
    [SerializeField] private Transform _firePoint;

    protected override void Attack()
    {

    }

    public override void SetTargetForAttack(Transform target)
    {
        Target = target.gameObject;
        if(Target.activeSelf == false) Target = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetStateAttacking(ATTACK_STAGE.START, true);
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        AttackMechanism();
    }

    public void AttackMechanism()
    {
        switch (PlayerAttackStage)
        {
            case ATTACK_STAGE.START:
                
                PlayerAttackStage = ATTACK_STAGE.DURATION;
                SetStateAttacking(ATTACK_STAGE.DURATION, true);
                break;
            case ATTACK_STAGE.DURATION:
                if (CheckIsAttack(PlayerAttackStage) && Target != null)
                {
                    StartCoroutine(DelayAttack(Target.transform));
                    //PlayerAttackStage = ATTACK_STAGE.FINISHED;
                    //SetStateAttacking(ATTACK_STAGE.FINISHED, true);
                }
                break;
            case ATTACK_STAGE.FINISHED:
                PlayerAttackStage = ATTACK_STAGE.END;
                SetStateAttacking(ATTACK_STAGE.END, true);
                break;
            case ATTACK_STAGE.END:
                if (Target == null) transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                PlayerAttackStage = ATTACK_STAGE.START;
                SetStateAttacking(ATTACK_STAGE.START, true);
                break;
        }
    }

    private IEnumerator DelayAttack(Transform target)
    {
        SetStateAttacking(ATTACK_STAGE.DURATION, false);
        yield return new WaitForSecondsRealtime(WeaponDataConfig.WeaponConfig.AttackSpeed);
        GameObject bullet = GamePlayController.Instance.GetBulletFactory().CreateBullet(target.position - _firePoint.position, WeaponDataConfig.WeaponConfig.Range, _firePoint.position, WeaponDataConfig.WeaponConfig.Damage);
        //SetStateAttacking(ATTACK_STAGE.DURATION, true);
        PlayerAttackStage = ATTACK_STAGE.FINISHED;
        SetStateAttacking(PlayerAttackStage, true);
        Debug.Log("Nooooo");
    }

    public void SpawnBullet()
    {
        //GameObject bullet = 
    }
}
