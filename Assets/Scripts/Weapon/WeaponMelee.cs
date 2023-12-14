using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WeaponMelee : WeaponBase
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Animator _weaponAnimator;

    private GameObject _player;
    private Vector3 initWeaponPosition;

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
        initWeaponPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Rotate();
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
                transform.position = Target.transform.position;
                break;
            case ATTACK_STAGE.DELAY:
                if (CheckIsAttack(PlayerAttackStage))
                {
                    StartCoroutine(DelayAttack(Target.transform));
                }
                break;
            case ATTACK_STAGE.DURATION:
                PlayerAttackStage = ATTACK_STAGE.FINISHED;
                SetStateAttacking(ATTACK_STAGE.FINISHED, true);
                transform.position = initWeaponPosition;
                break;
            case ATTACK_STAGE.FINISHED:
                if (Target == null) transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                PlayerAttackStage = ATTACK_STAGE.START;
                SetStateAttacking(ATTACK_STAGE.START, true);
                transform.position = initWeaponPosition;
                break;
        }
    }

    private IEnumerator DelayAttack(Transform target)
    {
        SetStateAttacking(ATTACK_STAGE.DELAY, false);
        _weaponAnimator.SetTrigger("attack");

        yield return new WaitForSecondsRealtime(WeaponDataConfig.WeaponConfig.AttackSpeed);
        GameObject bullet = GamePlayController.Instance.GetBulletFactory().CreateBulletMelee(target.position - _firePoint.position, WeaponDataConfig.WeaponConfig.Range, _firePoint.position, WeaponDataConfig.WeaponConfig.Damage);
        //SetStateAttacking(ATTACK_STAGE.DURATION, true);
        PlayerAttackStage = ATTACK_STAGE.DURATION;
        SetStateAttacking(PlayerAttackStage, true);
        Debug.Log("Nooooo");
    }

    public void SpawnBullet()
    {
        //GameObject bullet = 
    }
}
