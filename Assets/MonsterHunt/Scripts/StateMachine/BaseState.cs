using UnityEngine;

public abstract class BaseState : IState
{
    protected readonly EnemyController EnemyController;
    protected readonly PlayerController PlayerController;
    protected readonly Animator Animator;
    
    // protected static readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    // protected static readonly int JumpHash = Animator.StringToHash("Jump");

    protected BaseState(PlayerController playerController, Animator animator)
    {
        PlayerController = playerController;
        Animator = animator;
    }
    
    protected BaseState(EnemyController enemyController, Animator animator)
    {
        EnemyController = enemyController;
        Animator = animator;
    }

    protected const float CrossFadeDuration = 0.1f;
    
    public virtual void OnEnter()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void FixedUpdate()
    {
    }

    public virtual void OnExit()
    {
    }
}