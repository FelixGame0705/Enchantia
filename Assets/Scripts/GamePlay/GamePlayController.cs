using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayController : Singleton<GamePlayController>
{
    public GAME_STATES GameState;
    [SerializeField] private GameObject _character;
    [SerializeField] private GameObject _characterPattern;
    [SerializeField] private EnemyFactory _enemyFactory;
    [SerializeField] private BulletFactory _bulletFactory;
    [SerializeField] private List<GameObject> _enemies;
    [SerializeField] private GameObject TargetCharacter;
    [SerializeField] private int _currentWave;

    // Start is called before the first frame update
    void Start()
    {
        _character = Instantiate(_characterPattern);
        _currentWave = 0;
        
    }

    private void OnEnable()
    {
        //SetState();
        UpdateState(GAME_STATES.START);
        
    }

    // Update is called once per frame
    void Update()
    {
        FindNearestTarget();
        //_character.GetComponent<CharacterController>().SetTarget(TargetCharacter);
    }

    //public void Spawning

    public void SetState(GAME_STATES state)
    {
        GameState = state;
    }

    public void UpdateState(GAME_STATES state)
    {
        SetState(state);
        switch (GameState)
        {
            case GAME_STATES.START:
                StartCoroutine(WaitSpawnPlayer());
                break;
            case GAME_STATES.WAVE_SHOP:
                break;
            case GAME_STATES.PLAYING:
                //GamePlayController
                _enemies = _enemyFactory.SpawnRandomEnemy(_character);
                UpdateState(GAME_STATES.WAVE_SHOP);
                break;
            case GAME_STATES.GAME_OVER:
                break;
        }
    }

    private IEnumerator WaitSpawnPlayer()
    {
        yield return new WaitUntil(() => _character != null);
        CameraFollow.Instance.target = _character.transform;
        UpdateState(GAME_STATES.PLAYING);
        _enemies.Add(_enemyFactory.CreateEnemy(_character));
        Debug.Log("Play");
    }

    public BulletFactory GetBulletFactory()
    {
        return _bulletFactory;
    }

    public EnemyFactory GetEnemyFactory()
    {
        return _enemyFactory;
    }

    private void FindNearestTarget()
    {
        if (_enemies.Count == 0) return;
        GameObject Target = _enemies[0];
        for (int i = 0; i < _enemies.Count; i++)
        {
            if(Vector2.Distance(Target.transform.position, _character.transform.position) >= Vector2.Distance(_enemies[i].transform.position, _character.transform.position))
            {
                TargetCharacter = Target;
            }
        }
        _character.GetComponent<CharacterController>().SetTarget(TargetCharacter);
    }

    public void LevelWave()
    {
        _currentWave += 1;
    }
}
