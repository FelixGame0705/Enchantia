using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BasicEnemyFactory : EnemyFactory
{
    [SerializeField] private List<GameObject> _enemyPatternList;
    [SerializeField] private ObjectPool _enemyPool;
    [SerializeField] private ObjectPool _signalPool;
    [SerializeField] private float currentTimeSignal = 2f;
    //[SerializeField] private HashSet<GameObject> _enemies;
    int MIN_LEVEL_ENEMY = 0;
    int MAX_LEVEL_ENEMY = 0;
    public override GameObject CreateEnemy(GameObject target, Vector2 position)
    {
        //_enemyPool.objectPrefab = _enemyPatternList[Random.Range(MIN_LEVEL_ENEMY, MAX_LEVEL_ENEMY)];
        _enemyPool.objectPrefab = _enemyPatternList[1].gameObject;
        GameObject enemy = _enemyPool.GetObjectFromPool();
        enemy.transform.position = position;
        Debug.Log("Target la " + enemy);
        enemy.GetComponent<BasicEnemy>().SetTarget(target);
        return enemy;
    }

    public override List<GameObject> SpawnRandomEnemy(GameObject target)
    {
        List<GameObject> enemyList = new List<GameObject>();
        for (int i = 0; i < 10; i++)
        {
            //enemyList.Add(CreateEnemy(target));
            Enemies.Add(CreateEnemy(target));
            Debug.Log("Spawn enemy");
        }
        return enemyList;
    }

    //Tam thoi dung hardcode
    public void SetPrefabSpawn(int wave)
    {
        if (wave <= 1)
        {
            MAX_LEVEL_ENEMY = 1;
        }
        else if (wave <= 2)
        {
            MAX_LEVEL_ENEMY = 2;
        }
        else if (wave <= 3)
        {
            MAX_LEVEL_ENEMY = 3;
        }
        else if (wave <= 8)
        {
            MAX_LEVEL_ENEMY = 4;
        }
        else if (wave <= 11)
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
        Vector2 spawnPoint = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
        return spawnPoint;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isSpawned)
        {
            currentTime-=Time.deltaTime;
            Debug.Log("Target " + TargetForEnemy);
            if(currentTime <= 0)
            {
                RandomSpawnEnemy(TargetForEnemy);
            }
        }
    }

    public override void ReturnEnemToPool(GameObject gameObject)
    {
        _enemyPool.ReturnObjectToPool(gameObject);
    }

    private float currentTime = 5f;
    private void RandomSpawnEnemy(GameObject target)
    {
        int random = Random.Range(5, 10);
        for (int i = 0; i < random; i++)
        {
            CreateSignal(RandomPositionSpawn());
        }
        currentTime = 5f;
    }

    public IEnumerator Countdown()
    {
        yield return new WaitForSeconds(1f);
        currentTime--;
        RandomSpawnEnemy(TargetForEnemy);
        StartCoroutine(Countdown());
    }
    public void CreateSignal(Vector2 signalPosition)
    {
        GameObject signal = _signalPool.GetObjectFromPool();
        signal.transform.position = signalPosition;
        currentTimeSignal = 2f;
        StartCoroutine(CountdownSignal(signal, signalPosition));
    }

    private IEnumerator CountdownSignal(GameObject signal, Vector2 position)
    {
        while (currentTimeSignal > 0)
        {
            yield return new WaitForSeconds(1f);
            currentTimeSignal--;
        }
        _signalPool.ReturnObjectToPool(signal);
        Enemies.Add(CreateEnemy(TargetForEnemy, position));
    }
}
