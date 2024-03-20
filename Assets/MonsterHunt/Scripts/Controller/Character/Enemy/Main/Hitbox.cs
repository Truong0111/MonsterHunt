using System;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public Enemy enemy;

    public TypeHitBox hitBoxType;

    private void Awake()
    {
        if (!enemy) enemy.GetComponentInParent<Enemy>();
    }

    public enum TypeHitBox
    {
        Head,
        Body,
        Arm,
        Leg
    }

    public void GetDamage(float damage)
    {
        var realDamage = damage;

        realDamage *= hitBoxType switch
        {
            TypeHitBox.Head => 2f,
            TypeHitBox.Body => 1f,
            TypeHitBox.Arm => 0.9f,
            TypeHitBox.Leg => 0.9f,
            _ => 1f
        };
        realDamage -= enemy.currentCharacterData.armor;
        enemy.GetDamage(realDamage);
    }
}