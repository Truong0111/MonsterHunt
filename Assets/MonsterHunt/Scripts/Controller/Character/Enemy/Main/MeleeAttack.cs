using UnityEngine;


public class MeleeAttack : EnemyAttack
{
    public ParticleSystem castEffect;

    public bool CanAttack { get; set; }

    public override void Attack(float delay)
    {
    }

    public override void StopAttack()
    {
        CanAttack = false;
    }
}