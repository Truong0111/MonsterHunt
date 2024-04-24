using UnityEngine;

public class NonWeaponAttack : MeleeAttack
{
    private float _animationLength;
    
    public override void Attack(float delay)
    {
        base.Attack(delay);
        CanAttack = true;
        AttackAction();
    }

    public override void StopAttack()
    {
        CanAttack = false;
    }

    private void AttackAction()
    {
        // var random = Random.Range(0,2);
        // enemy.Animator.SetInteger(AnimatorConstant.AttackValueHashed, random);
    }
}