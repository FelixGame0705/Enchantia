using CarterGames.Assets.AudioManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStaffController : BulletController
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();
            if (enemy != null)
                enemy.TakeDamage(Damage);
            GamePlayController.Instance.GetBulletFactory().CreateHitEffect(transform.position, HIT_EFFECT_TYPE.DAMAGE_EFFECT);
            //GamePlayController.Instance.GetBulletFactory().ReturnObjectToPool(gameObject);
            AudioManager.instance.Play("Hit", GameData.Instance.GetVolumeAudioGame());

            DynamicTextManager.CreateText2D(collision.transform.position, Damage.ToString(), DynamicTextManager.defaultData);
        }
    }
}
