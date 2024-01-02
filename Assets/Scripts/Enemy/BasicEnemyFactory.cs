using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BasicEnemyFactory : EnemyFactory
{
    [SerializeField] private List<WaveGameData> _waveGameDatas;
    [SerializeField] private List<GameObject> _enemyPatternList;
    [SerializeField] private List<GameObject> _enemyPools = new List<GameObject>();
    [SerializeField] private GameObject _poolModel;
    [SerializeField] private GameObject _bossPrefab;
    [SerializeField] private ObjectPool _signalPool;

    private float _timeAppear = 0f;
    private int _currentWave = 0;
    public override GameObject CreateEnemyBaseOnPool(GameObject target, Vector2 position)
    {
        return CreateEnemyBaseOnPool(GetRandomEnemyType(), target, position);
    }

    public GameObject CreateEnemyBaseOnPool(int enemyModel, GameObject target, Vector2 position)
    {
        _enemyPools[enemyModel].GetComponent<ObjectPool>().objectPrefab = _waveGameDatas[_currentWave].EnemiesConfig[enemyModel].EnemyPrefab;
        
        GameObject enemy = _enemyPools[(int)enemyModel].GetComponent<ObjectPool>().GetObjectFromPool();
        enemy.transform.position = position;
        if (enemy.GetComponent<EnemyBase>() != null)
        {
            enemy.GetComponent<EnemyBase>().SetEnemyConfigStats(_currentWave);
            enemy.GetComponent<EnemyBase>().SetTarget(target);
        }
            
        return enemy;
    }

    public override GameObject CreateBoss(Transform target)
    {
        GameObject boss = Instantiate(_bossPrefab);
        boss.GetComponent<EnemyBase>().SetTarget(target.gameObject);
        return boss;
    }
    public int GetRandomEnemyType()
    {

        float randomRate = Random.Range(0, _waveGameDatas[_currentWave].SumRateAppear); 

        for(int i = 0; i < _waveGameDatas[_currentWave].EnemiesConfig.Count; i++)
        {
            if(randomRate <= _waveGameDatas[_currentWave].EnemiesConfig[i].RateAppear)
            {
                return i;
            }
            randomRate -= _waveGameDatas[_currentWave].EnemiesConfig[i].RateAppear;
        }
        return 0;
    }

    public void SpawnGameObjectPool()
    {
        GameObject pool = Instantiate(_poolModel);
        _enemyPools.Add(pool);
    }

    public override void SetEnemyModelPool()
    {
        for(int i = 0; i < _waveGameDatas[_currentWave].EnemiesConfig.Count; i++)
        {
            SpawnGameObjectPool();
            _enemyPools[i].GetComponent<ObjectPool>().objectPrefab = _waveGameDatas[_currentWave].EnemiesConfig[i].EnemyPrefab;
        }
    }

    public override void ResetEnemiesPool()
    {
        for(int i = _enemyPools.Count-1; i >= 0; i--)
        {
            _enemyPools[i].GetComponent<ObjectPool>().ResetQueue();
            Destroy(_enemyPools[i]);
            

        }
        _enemyPools.Clear();
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
            //Debug.Log("Target " + TargetForEnemy);
            if(currentTime <= 0)
            {
                RandomSpawnEnemy();
            }
        }
    }

    public override void ReturnEnemToPool(GameObject gameObject)
    {
        ObjectPool objectPool = gameObject.GetComponentInParent<ObjectPool>();
        objectPool.ReturnObjectToPool(gameObject);
        Debug.Log("Object queue ");
    }

    private float currentTime = 0;
    private void RandomSpawnEnemy()
    {
        int random = Random.Range(_waveGameDatas[_currentWave].MinEnemies, _waveGameDatas[_currentWave].MaxEnemies);
        for (int i = 0; i < random; i++)
        {
            CreateSignal(RandomPositionSpawn());
        }
        currentTime = _timeAppear;
    }
    public void CreateSignal(Vector2 signalPosition)
    {
        GameObject signal = _signalPool.GetObjectFromPool();
        signal.transform.position = signalPosition;
        //StartCoroutine(CountdownSignal(signal, signalPosition));
    }

    public override void ReturnSignalToPool(GameObject gameObject)
    {
        _signalPool.ReturnObjectToPool(gameObject);
    }

    public override void SetCurrentWave(int currentWave)
    {
        _currentWave = currentWave;
    }

    public override void SetTimeAppearEnemies()
    {
        _timeAppear = _waveGameDatas[_currentWave].TimeAppearEnemies;
    }

    public override WaveGameData GetWaveGameData()
    {
        return _waveGameDatas[_currentWave];
    }
}
