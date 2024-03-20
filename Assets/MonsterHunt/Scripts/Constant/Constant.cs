using System;
using UnityEngine;

public static class Constant
{
    public const float TimeWaitToNextMove = 5f;
    public const float MinCheckReachTarget = 0.1f;

    public static class PlayerAnim
    {
        public static readonly int IdleHash = Animator.StringToHash("Idle");
        public static readonly int MoveHash = Animator.StringToHash("Move");
        public static readonly int AttackHash = Animator.StringToHash("Attack");
        public static readonly int DieHash = Animator.StringToHash("Die");
    }
    
    public static class EnemyAnim
    {
        public static readonly int IdleHash = Animator.StringToHash("Idle");
        public static readonly int MoveHash = Animator.StringToHash("Move");
        public static readonly int AttackHash = Animator.StringToHash("Attack");
        public static readonly int DieHash = Animator.StringToHash("Die");
    }
}