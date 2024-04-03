using UnityEngine;

public class IdleState : BaseState
{
    public IdleState(EnemyController enemyController, Animator animator)
        : base(enemyController, animator)
    {
    }

    private const float TimeToNextMove = 4f;
    private float _timeToNextMove;
    private bool _isMoving;

    public override void OnEnter()
    {
        base.OnEnter();
        _timeToNextMove = 0f;
    }

    public override void Update()
    {
        base.Update();
        Animator.SetBool(AnimatorConstant.IsMoveHash, _isMoving);
        if(!EnemyController.IsReachTarget()) return;
        _timeToNextMove -= Time.deltaTime;
        if (_timeToNextMove > 0)
        {
            _isMoving = false;
            return;
        }
        _timeToNextMove = TimeToNextMove;
        EnemyController.MoveWhenIdle();
        _isMoving = true;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}