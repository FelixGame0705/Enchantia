using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BulletFactory : MonoBehaviour
{
    [SerializeField] protected GameObject BulletPattern;
    [SerializeField] private ObjectPool _bulletPool = new ObjectPool();
    private BulletController bulletController;
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

    public virtual void ReturnObjectToPool(GameObject gameObject)
    {
        _bulletPool.ReturnObjectToPool(gameObject);
    }
}
