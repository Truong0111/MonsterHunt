using System;
using UnityEngine;
using UnityEngine.AI;
using static NavMeshHelper;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    private StateMachine _stateMachine;
    public Animator animator;

    public float attackRange = 10f;

    private bool _canMove;
    private bool _canAttack;

    public Transform playerTarget;
    private Vector3 _targetMove;

    private Vector3 AgentPosition() => navMeshAgent.transform.position;

    private void Awake()
    {
        //StateMachine
        _stateMachine = new StateMachine();

        var idleState = new IdleState(this, animator);
        var moveState = new MoveState(this, animator);
        var attackState = new AttackState(this, animator);

        At(idleState, moveState, new FuncPredicate(() => _canMove));
        At(moveState, idleState, new FuncPredicate(() => !_canMove));

        At(idleState, attackState, new FuncPredicate(() => _canAttack));
        At(attackState, idleState, new FuncPredicate(() => !_canAttack && !_canMove));

        At(moveState, attackState, new FuncPredicate(() => _canAttack));
        At(attackState, moveState, new FuncPredicate(() => !_canAttack && _canMove));

        _stateMachine.SetState(idleState);
    }
    private void At(IState from, IState to, IPredicate condition) => _stateMachine.AddTransition(from, to, condition);
    private void Any(IState to, IPredicate condition) => _stateMachine.AddAnyTransition(to, condition);
    private void Update()
    {
        _stateMachine.Update();
    }
    private void FixedUpdate()
    {
        _stateMachine.FixedUpdate();
    }
    public void SetTargetMove()
    {
        _targetMove = playerTarget != null
            ? GetPositionToMoveInRange(playerTarget.position, attackRange / 2f, attackRange)
            : MapPositionHandler.Instance.GetPositionToMove();
    }
    public void Move()
    {
        navMeshAgent.destination = _targetMove;
    }
    public void SetMove()
    {
        _canMove = true;
    }
    public void SetStop()
    { 
        navMeshAgent.destination = transform.position;
        _canMove = false;
    }
    public bool ReachTarget()
    {
        var current = new Vector2(AgentPosition().x, AgentPosition().z);
        var target = new Vector2(_targetMove.x, _targetMove.z);
        return Vector2.Distance(current, target) <= Constant.MinCheckReachTarget;
    }
    public void SetAttackPlayer(Player player)
    {
        _canAttack = true;
        _canMove = false;
        navMeshAgent.isStopped = true;
        playerTarget = player.transform;
    }
    public void SetOutRangeAttack()
    {
        navMeshAgent.isStopped = false;
        _canAttack = false;
    }
    public bool IsTrackPlayer() => playerTarget != null;
}