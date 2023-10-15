using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayController : Singleton<GamePlayController>
{
    public GAME_STATES GameState;
    [SerializeField] private GameObject _character;
    [SerializeField] private GameObject _characterPattern;
    [SerializeField] private EnemyFactory _enemyFactory;
    [SerializeField] private List<GameObject> _enemies;
    [SerializeField] private GameObject TargetCharacter;

    // Start is called before the first frame update
    void Start()
    {
        _character = Instantiate(_characterPattern);
        
    }

    private void OnEnable()
    {
        UpdateState();
    }

    // Update is called once per frame
    void Update()
    {
        FindNearestTarget();
        //_character.GetComponent<CharacterController>().SetTarget(TargetCharacter);
    }

    //public void Spawning

    public void UpdateState()
    {
        switch (GameState)
        {
            case GAME_STATES.START:
                StartCoroutine(SpawnEnemy());
                break;
            case GAME_STATES.WAVE_SHOP:
                break;
            case GAME_STATES.PLAYING:
                break;
            case GAME_STATES.GAME_OVER:
                break;
        }
    }

    private IEnumerator SpawnEnemy()
    {
        yield return new WaitUntil(()=>_character!=null);
        _enemies.Add(_enemyFactory.CreateEnemy(_character));
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
}
