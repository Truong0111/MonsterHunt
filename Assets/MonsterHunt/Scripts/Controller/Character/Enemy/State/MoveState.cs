using UnityEngine;
using static Constant;

public class MoveState : BaseState
{
    public MoveState(EnemyController enemyController, Animator animator) :
        base(enemyController, animator)
    {
    }

    private bool _isMoving;

    public override void OnEnter()
    {
        base.OnEnter();
        _isMoving = false;
        EnemyController.SetTargetMove();
    }

    public override void Update()
    {
        base.Update();
        if (EnemyController.ReachTarget()) EnemyController.SetStop();
        if (_isMoving) return;
        _isMoving = true;
        EnemyController.Move();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void OnExit()
    {
        base.OnExit();
        _isMoving = false;
    }
}