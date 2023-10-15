using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyFactory : EnemyFactory
{
    [SerializeField] private List<GameObject> _enemyPatternList;
    [SerializeField] private ObjectPool _enemyPool;
    //[SerializeField] private HashSet<GameObject> _enemies;
    int MIN_LEVEL_ENEMY = 0;
    int MAX_LEVEL_ENEMY = 0;
    public override GameObject CreateEnemy(GameObject target)
    {
        //_enemyPool.objectPrefab = _enemyPatternList[Random.Range(MIN_LEVEL_ENEMY, MAX_LEVEL_ENEMY)];
        _enemyPool.objectPrefab = _enemyPatternList[1].gameObject;
        GameObject enemy = _enemyPool.GetObjectFromPool();
        enemy.transform.position = RandomPositionSpawn();
        enemy.GetComponent<BasicEnemy>().SetTarget(target);
        return enemy;
    }

    //Tam thoi dung hardcode
    private void SetPrefabSpawn(int wave)
    {
        if(wave <= 1)
        {
            MAX_LEVEL_ENEMY = 1;
        }
        else if(wave <= 2)
        {
            MAX_LEVEL_ENEMY = 2;
        }
        else if(wave <= 3)
        {
            MAX_LEVEL_ENEMY = 3;
        }
        else if(wave <= 8)
        {
            MAX_LEVEL_ENEMY = 4;
        }
        else if(wave <= 11)
        {
            MAX_LEVEL_ENEMY = 5;
        }
        else
        {
            MAX_LEVEL_ENEMY = 6;
        }
    }

    private Vector2 RandomPositionSpawn()
    {
        Vector2 spawnPoint = new Vector2(Random.Range(-10,10), Random.Range(-10,10));
        return spawnPoint;
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
