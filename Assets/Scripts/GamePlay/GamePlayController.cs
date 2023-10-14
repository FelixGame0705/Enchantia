using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayController : Singleton<GamePlayController>
{
    public GAME_STATES GameState;
    [SerializeField] private GameObject _character;
    [SerializeField] private GameObject _characterPattern;
    [SerializeField] private EnemyFactory _enemyFactory;
    [SerializeField] private HashSet<GameObject> _enemies;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void Spawning

    public void UpdateState()
    {
        switch (GameState)
        {
            case GAME_STATES.START:
                break;
            case GAME_STATES.WAVE_SHOP:
                break;
            case GAME_STATES.PLAYING:
                break;
            case GAME_STATES.GAME_OVER:
                break;
        }
    }
}
