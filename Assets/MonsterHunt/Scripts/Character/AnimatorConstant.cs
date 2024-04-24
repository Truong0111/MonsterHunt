using UnityEngine;

public class AnimatorConstant
{
    public static readonly int IdleHash = Animator.StringToHash("Idle");
    public static readonly int Attack1 = Animator.StringToHash("Attack");
    
    public static readonly int AttackValueHashed = Animator.StringToHash("AttackValue");

    public static readonly int IsMoveHash = Animator.StringToHash("IsMove");
    public static readonly int DamageHash = Animator.StringToHash("Damage");
    public static readonly int AttackHash = Animator.StringToHash("Attack");

    public static readonly int ReloadHash = Animator.StringToHash("Reload");
    public static readonly int CanAttackHash = Animator.StringToHash("CanAttack");
    public static readonly int IsAttackingHash = Animator.StringToHash("IsAttacking");

    public static readonly int DieHashed = Animator.StringToHash("Die");
}