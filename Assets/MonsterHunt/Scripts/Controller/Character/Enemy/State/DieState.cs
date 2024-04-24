using UnityEngine;
using UnityEngine.UI;

public class DieState : BaseState
{
    public DieState(EnemyController enemyController, Animator animator) 
        : base(enemyController, animator)
    {
    }
    
    public override void OnEnter()
    {
        base.OnEnter();
        Animator.SetTrigger(AnimatorConstant.DieHashed);
    }

    public override void Update()
    {
        base.Update();
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