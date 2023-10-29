using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalController : MonoBehaviour
{
    [SerializeField]float duration = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        duration -= Time.deltaTime;
        if(duration <= 0)
        {
            GameObject ob = GamePlayController.Instance.GetEnemyFactory().CreateEnemy(GamePlayController.Instance.GetEnemyFactory().GetTarget(), transform.position);
            GamePlayController.Instance.GetEnemyFactory().GetEnemies().Add(ob);
            duration = 2;
            Debug.Log("Check" + ob);
            GamePlayController.Instance.GetEnemyFactory().ReturnSignalToPool(gameObject);
            //Destroy(gameObject);
        }
    }
}
