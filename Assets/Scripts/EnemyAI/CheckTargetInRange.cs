using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;
using UnityEngine.UIElements;

public class CheckTargetInRange : Node
{
    private Transform _target;
    private Transform _transform;
    private float _range;
    private NodeState _state;
    public CheckTargetInRange(Transform transform, Transform target, float range)
    {
        _transform = transform;
        _target = target;
        _range = range;
    }

    public override NodeState Evaluate()
    {
        if (Vector2.Distance(_transform.position, _target.position) < 8f)
        {
            _state = NodeState.SUCCESS;
            return _state;
        }
        _state = NodeState.RUNNING;
        return _state;
    }

}
