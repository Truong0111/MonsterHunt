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
        var animInfo = Animator.GetCurrentAnimatorStateInfo(0);
        if (!animInfo.IsName("Attack"))
        {
            _isAttacking = false;
        }
        if(_isAttacking) return;

        _lengthAttackAnimation -= Time.deltaTime;
        if(_lengthAttackAnimation > 0) return;
        if (!EnemyController.IsPlayerInRange())
        {
            EnemyController.SetPlayerInRange(false);
            return;
        }
        Animator.SetTrigger(AnimatorConstant.AttackHash);
        _lengthAttackAnimation = animInfo.length;
        EnemyController.Attack(_lengthAttackAnimation * 0.96f);
        _isAttacking = true;
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