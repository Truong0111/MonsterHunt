using System;

[Serializable]
public struct EnemyAttackValue
{
    public EnemyAttackType attackType;
    public float damage;
    public float attackRange;
    public float attackDistance;
    public float attackRate;
    public float attackDetect;
    public float speedBullet;
}

[Serializable]
public enum EnemyAttackType
{
    Suicide,
    Melee,
    Range,
}