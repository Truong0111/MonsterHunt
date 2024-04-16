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
        
        _lengthAttackAnimation -= Time.deltaTime;
        if(_lengthAttackAnimation > 0) return;
        if (!EnemyController.IsPlayerInRange())
        {
            EnemyController.SetPlayerInRange(false);
            return;
        }
        Animator.SetTrigger(AnimatorConstant.AttackHash);
        // Animator.SetBool(AnimatorConstant.IsAttackingHash, true);
        _lengthAttackAnimation = Animator.GetCurrentAnimatorStateInfo(0).length;
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