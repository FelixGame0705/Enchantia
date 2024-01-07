using CarterGames.Assets.AudioManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationEvent : MonoBehaviour
{
    [SerializeField] private Collider2D _colAttack;
    [SerializeField] private LayerMask _layerMaskAttack;
    [SerializeField] private WeaponData _weaponData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();
            if (enemy != null)
                enemy.TakeDamage(_weaponData.WeaponConfig.Damage);
            GamePlayController.Instance.GetBulletFactory().CreateHitEffect(transform.position, HIT_EFFECT_TYPE.DAMAGE_EFFECT);
           // GamePlayController.Instance.GetBulletFactory().ReturnObjectToPool(gameObject);
            AudioManager.instance.Play("Hit", GameData.Instance.GetVolumeAudioGame());

            DynamicTextManager.CreateText2D(collision.transform.position, _weaponData.WeaponConfig.Damage.ToString(), DynamicTextManager.defaultData);
        }
    }

    public void SetWeaponData(WeaponData weaponData)
    {
        _weaponData = weaponData;
    }

    public void ActivableColliderAttack()
    {
        _colAttack.enabled = true;
    }
    public void DisableColliderAttack()
    {
        _colAttack.enabled = false;
    }
}
