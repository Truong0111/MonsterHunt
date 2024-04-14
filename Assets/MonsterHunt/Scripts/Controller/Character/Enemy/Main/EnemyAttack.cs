using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Enemy enemy;

    public Player player;
    
    public LayerMask attackMask;
    public EnemyData EnemyData() => enemy.enemyData;

    public virtual void Attack(float delay)
    {
    }
}