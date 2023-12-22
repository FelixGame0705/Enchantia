using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Skills/FireBall")]
public class FireBall : Skill
{
    [SerializeField] private float skillEffectDuration = 5f;
    [SerializeField] private float attackSpeed = 2f;
    [SerializeField] private GameObject fireBallPrefab;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private GameObject _fireBallPool;
    private GameObject fireBallPoolInstance;
    private float attackSpeedCooldown = 0;
    private HashSet<GameObject> fireBalls = new HashSet<GameObject>();
    public override void Execute(GameObject player, ref bool isUsing, ref ATTACK_STAGE stage)
    {

        Debug.Log("Skill execute");
        switch (stage)
        {
            case ATTACK_STAGE.START:
                _characterController = player.GetComponent<CharacterController>();
                if (fireBallPoolInstance == null) {
                    fireBallPoolInstance = Instantiate(_fireBallPool);
                }
                fireBallPoolInstance.GetComponent<ObjectPool>().objectPrefab = fireBallPrefab;
                stage = ATTACK_STAGE.DELAY;
                break;
            case ATTACK_STAGE.DELAY:
                stage = ATTACK_STAGE.DURATION;
                break;
            case ATTACK_STAGE.DURATION:
                timeInUsing += Time.deltaTime;
                
                attackSpeedCooldown += Time.deltaTime;
                if(attackSpeedCooldown > attackSpeed && _characterController.GetTarget()!=null)
                {
                    fireBalls.Add(SpawnFireBall(player.transform));
                    attackSpeedCooldown = 0;
                }

                if (timeInUsing >= skillEffectDuration)
                {
                    timeInUsing = 0f;
                    stage = ATTACK_STAGE.FINISHED;
                }
               
                break;
            case ATTACK_STAGE.FINISHED:
                foreach(var fireBall in fireBalls)
                {
                    fireBallPoolInstance.GetComponent<ObjectPool>().ReturnObjectToPool(fireBall);
                }
                isUsing = false;
                stage = ATTACK_STAGE.START;
                break;
        }
    }

    public GameObject SpawnFireBall(Transform playerTransform)
    {
        GameObject fireBallInstance = fireBallPoolInstance.GetComponent<ObjectPool>().GetObjectFromPool();
        fireBallInstance.GetComponent<BallBulletSkill>().SetDistance(200f);
        fireBallInstance.GetComponent<BallBulletSkill>().SetFirstPosition(playerTransform.position);
        fireBallInstance.GetComponent<BallBulletSkill>().SetDirection(_characterController.GetTarget().transform.position - playerTransform.position);
        fireBallInstance.GetComponent<BallBulletSkill>().SetDamage(damage);
        return fireBallInstance;
    }
}
