using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BulletFactory : MonoBehaviour
{
    [SerializeField] protected GameObject BulletPattern;
    [SerializeField] protected GameObject BulletMeleePattern;
    [SerializeField] protected GameObject BulletObjectPool;
    [SerializeField] protected Dictionary<int,GameObject> BulletPoolObjects = new Dictionary<int,GameObject>();
    [SerializeField] protected List<GameObject> HitEffectPatterns;
    [SerializeField] protected GameObject BloodEffectPattern;
    [SerializeField] protected GameObject HitEffectPattern;
    [SerializeField] private ObjectPool _bulletPool = new ObjectPool();
    [SerializeField] private ObjectPool _bulletMeleePool = new ObjectPool();
    [SerializeField] private List<GameObject> HitEffectPools;
    [SerializeField] protected GameObject PoolHitEffectModel;
    private BulletController bulletController;

    private void Start()
    {
        SpawnGameObjectPool();
    }

    public void SpawnGameObjectPool()
    {
        for (int i = 0; i < HitEffectPatterns.Count; i++)
        {
            GameObject pool = Instantiate(PoolHitEffectModel);
            HitEffectPools.Add(pool);
        }
    }

    public virtual GameObject CreateBullet(Vector3 target)
    {
        _bulletPool.objectPrefab = BulletPattern;
        GameObject bullet = _bulletPool.GetObjectFromPool();
        //enemy.transform.position = RandomPositionSpawn();
        //enemy.GetComponent<BasicEnemy>().SetTarget(target);
        bullet.GetComponent<BulletController>().SetDirection(target);
        return bullet;
    }

    public virtual GameObject CreateBullet(Vector3 direction, float distance, Vector3 positionBullet, float damage)
    {
        _bulletPool.objectPrefab = BulletPattern;
        GameObject bullet = _bulletPool.GetObjectFromPool();
        bullet.transform.position = positionBullet;
        bulletController = bullet.GetComponent<BulletController>();
        bulletController.SetDistance(distance);
        bulletController.SetDirection(direction);
        bulletController.SetDamage(damage);
        return bullet;
    }

    public GameObject CreateBulletBaseOnPool(int key, Vector3 direction, float distance, Vector3 positionBullet, float damage)
    {
        if (BulletPoolObjects.Count > 0)
        {
            GameObject bullet = BulletPoolObjects[key].GetComponent<ObjectPool>().GetObjectFromPool();
            bullet.transform.position = positionBullet;
            bulletController = bullet.GetComponent<BulletController>();
            bulletController.SetDistance(distance);
            bulletController.SetDirection(direction);
            bulletController.SetDamage(damage);
            return bullet;
        }
        return null;
    }
    
    public virtual GameObject CreateBulletMelee(Vector3 direction, float distance, Vector3 positionBullet, float damage)
    {
        _bulletMeleePool.objectPrefab = BulletMeleePattern;
        GameObject bullet = _bulletMeleePool.GetObjectFromPool();
        bullet.transform.position = positionBullet;
        bulletController = bullet.GetComponent<BulletController>();
        bulletController.IsMeleeWeapon = true;
        bulletController.SetDistance(distance);
        bulletController.SetDirection(direction);
        bulletController.SetDamage(damage);
        return bullet;
    }

    public virtual GameObject CreateHitEffect(Transform target, HIT_EFFECT_TYPE type)
    {
        if ((int)type >= HitEffectPatterns.Count) return null;

        HitEffectPools[(int)type].GetComponent<ObjectPool>().objectPrefab = HitEffectPatterns[(int)type];
        GameObject hitEffect = HitEffectPools[(int)type].GetComponent<ObjectPool>().GetObjectFromPool();
        hitEffect.transform.position = target.position;
        var par = hitEffect.GetComponentInChildren<ParticleSystem>();
        if (par != null)
        {
            par.Play();
            StartCoroutine(DelayHitEffectReturnPool(hitEffect, par.main.startLifetime.constant, type));
        }
        return hitEffect;
    }

    public virtual GameObject CreateHitEffect(Vector3 initPosition, HIT_EFFECT_TYPE type)
    {
        if ((int)type >= HitEffectPatterns.Count) return null;

        HitEffectPools[(int)type].GetComponent<ObjectPool>().objectPrefab = HitEffectPatterns[(int)type];
        GameObject hitEffect = HitEffectPools[(int)type].GetComponent<ObjectPool>().GetObjectFromPool();
        hitEffect.transform.position = initPosition;
        var par = hitEffect.GetComponentInChildren<ParticleSystem>();
        if (par != null)
        {
            par.Play();
            StartCoroutine(DelayHitEffectReturnPool(hitEffect, par.main.startLifetime.constant, type));
        }
        return hitEffect;
    }

    public virtual void ReturnObjectToPool(GameObject gameObject)
    {
        if (gameObject.TryGetComponent(out BulletController bulletController))
        {
            if (bulletController.IsMeleeWeapon)
            {
                bulletController.IsMeleeWeapon = false;
                _bulletMeleePool.ReturnObjectToPool(gameObject);
            }
            else
            {
                _bulletPool.ReturnObjectToPool(gameObject);
            }
        }
    }
    
    private IEnumerator DelayHitEffectReturnPool(GameObject hitEff, float delay, HIT_EFFECT_TYPE type)
    {
        yield return new WaitForSeconds(delay);

        ReturnHitEffToPool(hitEff, type);
    }

    public virtual void ReturnHitEffToPool(GameObject gameObject, HIT_EFFECT_TYPE type)
    {
        HitEffectPools[(int)type].GetComponent<ObjectPool>().ReturnObjectToPool(gameObject);
    }

    public void SetBulletModelPrefab(Dictionary<int,GameObject> bulletModels)
    {
        SpawnBulletObjectPool(bulletModels);
        foreach(KeyValuePair<int, GameObject> objectPool in bulletModels)
        {
            BulletPoolObjects[objectPool.Key].GetComponent<ObjectPool>().objectPrefab = objectPool.Value;
        }
    }

    public void SpawnBulletObjectPool(Dictionary<int, GameObject> bulletModels)
    {
        foreach (KeyValuePair<int, GameObject> objectPool in bulletModels)
        {
            GameObject poolInstance = Instantiate(BulletObjectPool);
            BulletPoolObjects.Add(objectPool.Key, poolInstance);
        }
    }
    
    public void ResetPoolObject()
    {
        foreach (KeyValuePair<int, GameObject> objectPool in BulletPoolObjects)
        {
            Destroy(objectPool.Value);
        }
        
        BulletPoolObjects.Clear();
    }
}
