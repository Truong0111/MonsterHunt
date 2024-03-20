using UnityEngine;

public class SpawnController : Singleton<SpawnController>
{
    public EnemyDataSO enemyDataSo;
    
    public override void Awake()
    {
        base.Awake();
        // StartCoroutine(Extension.CountDown(1f, SpawnEnemy));
    }

    public void SpawnEnemy()
    {
        var enemy = enemyDataSo.enemyDatas[0].enemy;
        var pos = MapPositionHandler.Instance.GetRandomPos();
        var exampleEnemy = SimplePool.Spawn(enemy, pos, Quaternion.identity);
    }
}