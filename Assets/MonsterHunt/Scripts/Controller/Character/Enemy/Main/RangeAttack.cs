using UnityEngine;

public class RangeAttack : EnemyAttack
{
    public ParticleSystem castEffect;

    public Transform skillStartTransform;
    
    public float castTime;
    
    public bool CanAttack { get; set;}
    
    public override void Attack(float delay)
    {
        
    }
}