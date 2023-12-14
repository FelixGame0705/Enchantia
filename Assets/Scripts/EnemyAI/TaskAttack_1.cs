using BehaviorTree;
using TMPro;
using UnityEngine;

public class TaskAttack_1 : Node
{
    private Transform _transform;
    private Transform _target;
    private Vector2 _previousTarget;
    private Vector2 des;
    private Vector2 finalDes;
    private NodeState _state;
    private float _attackCounter = 0f;
    private float _delayCounter = 0f;
    private bool isFinished = false;
    private EnemyData _enemyData;
    private BossController _bossController;

    public enum ATTACK_STAGE
    {
        START,
        DELAY,
        LERP,
        FINISHED,
        END
    }

    private ATTACK_STAGE _attackStage = ATTACK_STAGE.START;

    private float _lerpSpeed = 2f;
    private float _delayTime = 0.5f;

    public TaskAttack_1(Transform transform, Transform target)
    {
        _target = target;
        des = new Vector2(_target.position.x, _target.position.y);
        _transform = transform;
        UpdateDestination();
    }

    public override NodeState Evaluate()
    {
            // Target mới, thực hiện lướt tới
            //UpdateDestination();
            AttackMechanism();

        // Cập nhật target trước đó
        //_previousTarget = new Vector2(des.x, des.y);
        return _state;
    }

    private void AttackMechanism()
    {
        switch (_attackStage)
        {
            case ATTACK_STAGE.START:
                state = NodeState.RUNNING;
                UpdateCurrentPosition();
                UpdateDestination();
                UpdateDirection(des);
                _attackStage = ATTACK_STAGE.DELAY;
                break;

            case ATTACK_STAGE.DELAY:
                _delayCounter += Time.deltaTime;
                if (_delayCounter >= _delayTime)
                {
                    _attackStage = ATTACK_STAGE.LERP;
                    _delayCounter = 0f;
                }
                break;

            case ATTACK_STAGE.LERP:
                // Hướng và khoảng cách đã được xác định trước
                Vector2 targetPosition = new Vector2(des.x, des.y);
                float distanceToTarget = 10f; // Đặt khoảng cách mong muốn

                LerpToTarget(des, distanceToTarget);

                if (Vector2.Distance(_transform.position, finalDes) < 1f)
                {
                    _attackStage = ATTACK_STAGE.FINISHED;
                    state = NodeState.SUCCESS;
                }
                break;

            case ATTACK_STAGE.FINISHED:
                // Perform your attack logic here
                isFinished = true;
                _attackCounter = 0f;
                _attackStage = ATTACK_STAGE.END;
                break;

            case ATTACK_STAGE.END:
                // Additional logic or transitions can be handled here
                _attackStage = ATTACK_STAGE.START;
                _state = NodeState.SUCCESS;
                return;
        }
    }

    private void LerpToTarget()
    {
        Vector2 targetPosition = new Vector2(_target.position.x, _target.position.y);
        _transform.position = Vector2.Lerp(_transform.position, targetPosition, _lerpSpeed * Time.deltaTime);
    }

    private void LerpToTarget(Vector2 targetPosition, float distance)
    {

        // Xác định hướng từ vị trí hiện tại đến mục tiêu
        

        // Xác định vị trí mới dựa trên hướng và khoảng cách
        finalDes = currentPos + direction * distance;

        // Lerp tới vị trí mới
        _transform.position = Vector2.MoveTowards(_transform.position, finalDes, 10f * Time.deltaTime);
    }

    private void UpdateDestination()
    {
        des = new Vector2(_target.position.x, _target.position.y);
        
    }

    Vector2 currentPos;

    private void UpdateCurrentPosition()
    {
        currentPos = new Vector2(_transform.position.x, _transform.position.y);
    }

    Vector2 direction;
    private void UpdateDirection(Vector2 targetPosition)
    {
        direction = (targetPosition - currentPos).normalized;
    }
}