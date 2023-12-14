using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WeaponRanged : WeaponBase
{
    [SerializeField] private Transform _firePoint;

    private GameObject _player;
    protected override void Attack()
    {

    }

    public override void SetTargetForAttack(Transform target)
    {
        Target = target.gameObject;
        if(Target.activeSelf == false) Target = null;
    }

    public override void SetPlayerPosition(Transform player)
    {
        _player = player.gameObject;
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
        if(CanPerformAttack())
        AttackMechanism();
    }

    public bool CanPerformAttack()
    {
        if (Target != null && Target.activeSelf && Vector3.Distance(_player.transform.position, Target.transform.position) <= WeaponDataConfig.WeaponConfig.Range) return true;
        return false;
    }

    public void AttackMechanism()
    {
        switch (PlayerAttackStage)
        {
            case ATTACK_STAGE.START:
                
                PlayerAttackStage = ATTACK_STAGE.DELAY;
                SetStateAttacking(ATTACK_STAGE.DELAY, true);
                break;
            case ATTACK_STAGE.DELAY:
                if (CheckIsAttack(PlayerAttackStage))
                {
                    StartCoroutine(DelayAttack(Target.transform));
                }
                break;
            case ATTACK_STAGE.DURATION:
                GamePlayController.Instance.GetCharacterController().LifeSteal();
                PlayerAttackStage = ATTACK_STAGE.FINISHED;
                SetStateAttacking(ATTACK_STAGE.FINISHED, true);
                break;
            case ATTACK_STAGE.FINISHED:
                if (Target == null) transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                PlayerAttackStage = ATTACK_STAGE.START;
                SetStateAttacking(ATTACK_STAGE.START, true);
                break;
        }
    }

    private IEnumerator DelayAttack(Transform target)
    {
        SetStateAttacking(ATTACK_STAGE.DELAY, false);
        yield return new WaitForSecondsRealtime(WeaponDataConfig.WeaponConfig.AttackSpeed);
        if (target != null) GamePlayController.Instance.GetBulletFactory().CreateBullet(target.position - _firePoint.position, WeaponDataConfig.WeaponConfig.Range, _firePoint.position, DealWithDamage());
        //SetStateAttacking(ATTACK_STAGE.DURATION, true);
        PlayerAttackStage = ATTACK_STAGE.DURATION;
        SetStateAttacking(PlayerAttackStage, true);
        Debug.Log("Nooooo");
    }

    private float DealWithDamage()
    {
        return WeaponDataConfig.WeaponConfig.Damage + GamePlayController.Instance.GetCharacterController().CharacterModStats.RangedDamage.Value;
    }

    public void SpawnBullet()
    {
        //GameObject bullet = 
    }
}
