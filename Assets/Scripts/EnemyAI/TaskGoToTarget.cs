using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TaskGoToTarget : Node
{
    private Transform _transform;
    private Transform _target;
    private EnemyData _enemyData;
    public TaskGoToTarget(Transform transform, Transform target, EnemyData enemyData)
    {
        _transform = transform;
        _target = target;
        _enemyData = enemyData;
    }

    public override NodeState Evaluate()
    {
        Transform target = _target;
        Debug.Log("moving");
        if (Vector2.Distance(_transform.position, target.position) > 8f)
        {
            _transform.position = Vector2.MoveTowards(_transform.position, target.position, _enemyData.EnemyStats.Speed * Time.deltaTime);
            Flip(target);
        }
        else
        {
            state = NodeState.SUCCESS;
            return state;
        }
        
        state = NodeState.RUNNING;
        return state;
    }

    public void Flip(Transform target)
    {
        _transform.localScale = _transform.position.x < target.position.x ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
    }
}
