using UnityEngine;

public class AttackState : BaseState
{
    public AttackState(EnemyController enemyController, Animator animator) 
        : base(enemyController, animator)
    {
    }

    private bool _isAttacking;
    private float _lengthAttackAnimation;
    
    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void Update()
    {
        base.Update();
        if(_isAttacking) return;

        _lengthAttackAnimation -= Time.deltaTime;
        if(_lengthAttackAnimation > 0) return;
        
        if (!EnemyController.IsPlayerInRange())
        {
            EnemyController.SetPlayerInRange(false);
            return;
        }
        _isAttacking = true;
        Animator.SetTrigger(AnimatorConstant.AttackHash);
        EnemyController.Attack(_lengthAttackAnimation);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void OnExit()
    {
        base.OnExit();
        _isAttacking = false;
        EnemyController.StopAttack();
    }
}