using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TaskAttack_1;
using static UnityEngine.GraphicsBuffer;

public class GolemAttack_1 : Node
{
    public float throwForce = 10f;
    public float upwardsModifier = 2f;
    private float _delayCounter;
    private float _delayTime = 0.5f;
    private Animator _animator;
    private ATTACK_STAGE _stage = ATTACK_STAGE.START;

    private Transform _transform;
    private Transform _target;
    private Rigidbody2D _rigidbody2D;

    public GolemAttack_1(Transform transform, Transform target, Animator animator, Rigidbody2D rigidbody2D)
    {
        _transform = transform;
        _target = target;
        _animator = animator;
        _rigidbody2D = rigidbody2D;
    }

    private void AttackMechanism()
    {
        switch (_stage)
        {
            case ATTACK_STAGE.START:
                state = NodeState.RUNNING;
                break;
            case ATTACK_STAGE.DELAY:
                _delayCounter += Time.deltaTime;
                if (_delayCounter >= _delayTime)
                {
                    _stage = ATTACK_STAGE.DURATION;
                    _delayCounter = 0f;
                }
                break;
            case ATTACK_STAGE.DURATION:
                Throw();
                _stage = ATTACK_STAGE.FINISHED;
                break;
            case ATTACK_STAGE.FINISHED:
                _stage = ATTACK_STAGE.START;
                state = NodeState.SUCCESS;
                break;
        }
    }

    private void Throw()
    {
        Vector3 startPosition = _transform.position;
        Vector3 targetPosition = _target.position;

        // Tính toán h??ng và kho?ng cách
        Vector3 direction = targetPosition - startPosition;
        float height = direction.y;
        direction.y = 0;
        float distance = direction.magnitude;

        // Tính toán góc ném
        float angle = Mathf.Atan2(height, distance);
        float velocity = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * angle));

        // T?o Vector c?a l?c
        direction.y = distance * Mathf.Tan(angle);

        // Áp d?ng l?c
        Rigidbody2D rigidbody = _rigidbody2D;
        rigidbody.AddForce(direction.normalized * velocity * throwForce);
        rigidbody.AddForce(Vector3.up * upwardsModifier);
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
