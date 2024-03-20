using UnityEngine;

public class IdleState : BaseState
{
    public IdleState(EnemyController enemyController, Animator animator)
        : base(enemyController, animator)
    {
    }
    
    private float _timeWaitToNextMove;
    
    public override void OnEnter()
    {
        base.OnEnter();
        if (EnemyController.IsTrackPlayer())
        {
            _timeWaitToNextMove = 0;
        }
    }

    public override void Update()
    {
        base.Update();
        _timeWaitToNextMove -= Time.deltaTime;
        if (_timeWaitToNextMove < 0)
        {
            EnemyController.SetMove();
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void OnExit()
    {
        base.OnExit();
        _timeWaitToNextMove = Constant.TimeWaitToNextMove;
    }
}