using UnityEngine;

public class ChaseState : BaseState
{
    public ChaseState(EnemyController enemyController, Animator animator) 
        : base(enemyController, animator)
    {
    }
    
    public override void OnEnter()
    {
        base.OnEnter();
        Animator.SetBool(AnimatorConstant.IsMoveHash, true);
    }

    public override void Update()
    {
        base.Update();
        if (EnemyController.IsPlayerInRange())
        {
            EnemyController.SetPlayerInRange(true);
            return;
        }
        EnemyController.MoveWhenAttack();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void OnExit()
    {
        base.OnExit();
        Animator.SetBool(AnimatorConstant.IsMoveHash, false);
    }
}