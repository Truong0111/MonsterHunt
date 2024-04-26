using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public EnemyAttack enemyAttack;
    public NavMeshAgent agent;
    public Animator animator;

    public float AttackRange { get; set; }
    public float NoAttackMove { get; set; }

    private bool _canMove;
    private bool _canAttack;

    private bool _isPlayerInRange;
    private bool _isTrackedPlayer;

    private bool _isDead;

    private Player CurrentPlayerToAttack { get; set; }

    private StateMachine _stateMachine;

    private Vector3 AgentPosition() => agent.transform.position;

    private void Awake()
    {
        _stateMachine = new StateMachine();

        var idleState = new IdleState(this, animator);
        var attackState = new AttackState(this, animator);
        var chaseState = new ChaseState(this, animator);
        var dieState = new DieState(this, animator);

        _stateMachine.At(idleState, attackState, new FuncPredicate(() => _canAttack));
        _stateMachine.At(idleState, chaseState, new FuncPredicate(() => _canAttack && !_isPlayerInRange));

        _stateMachine.At(attackState, chaseState, new FuncPredicate(() => !_isPlayerInRange));
        _stateMachine.At(chaseState, attackState, new FuncPredicate(() => _isPlayerInRange));

        _stateMachine.At(idleState, dieState, new FuncPredicate(() => _isDead));
        _stateMachine.At(attackState, dieState, new FuncPredicate(() => _isDead));
        _stateMachine.At(chaseState, dieState, new FuncPredicate(() => _isDead));

        _stateMachine.SetState(idleState);

        SetDestination(transform.position);

        if (enemyAttack == null) enemyAttack = GetComponent<EnemyAttack>();
    }

    private void Update()
    {
        _stateMachine.Update();
    }

    private void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }

    public void SetDead()
    {
        _isDead = true;
        agent.isStopped = true;
    }
    public void SetSpeed(float speed) => agent.speed = speed;
    
    private void SetDestination(Vector3 target) => agent.destination = target;
    
    public void MoveWhenIdle()
    {
        SetDestination(transform.GetNearRandomPosition(NoAttackMove / 2f, NoAttackMove));
    }
    
    public void MoveWhenAttack()
    {
        SetDestination(AgentPosition().GetPositionToMoveInRange(CurrentPlayerToAttack.Position, AttackRange));
    }
    
    public bool IsReachTarget() => agent.IsAgentReachTarget();
    
    public void SetPlayerToAttack(Player player)
    {
        CurrentPlayerToAttack = player;
        _isTrackedPlayer = true;
        _canAttack = true;
        enemyAttack.player = player;
    }
    
    public void SetPlayerInRange(bool isInRange) => _isPlayerInRange = isInRange;
    
    public bool IsPlayerInRange() => AgentPosition().IsPlayerInRange(CurrentPlayerToAttack.Position, AttackRange);
    
    public Player GetPlayerToAttack() => CurrentPlayerToAttack;
    
    public bool IsTrackPlayer() => _isTrackedPlayer;

    public void Attack(float delay)
    {
        enemyAttack.Attack(delay);
        agent.isStopped = true;
    }

    public void StopAttack()
    {
        enemyAttack.StopAttack();
        agent.isStopped = false;
    }
}