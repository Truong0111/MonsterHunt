using UnityEngine;

public static class NavMeshHelper
{
    public static Vector3 GetRandomPosition(BoxCollider boxCollider)
    {
        var center = boxCollider.bounds.center;
        var size = boxCollider.bounds.size;

        var randomX = Random.Range(center.x - size.x / 2f, center.x + size.x / 2f);
        var randomY = Random.Range(center.y - size.y / 2f, center.y + size.y / 2f);
        var randomZ = Random.Range(center.z - size.z / 2f, center.z + size.z / 2f);

        return new Vector3(randomX, randomY, randomZ);
    }

    public static Vector3 GetPositionToMoveInRange(Vector3 target, float minRange, float maxRange)
    {
        var randomDirection = Random.insideUnitSphere.normalized;
        var randomDistance = Random.Range(minRange, maxRange);

        return target + randomDirection * randomDistance;
    }

    public static Vector3 GetPositionToMoveInRange(this Vector3 agent, Vector3 target, float range)
    {
        var direction = (agent - target).normalized;
        
        return target + direction * (range * 0.97f);
    }

    // public static Vector3 GetPositionToAttackTarget(this Transform transform, Vector3 target, float range)
    // {
    //     
    // }

    public static Vector3 GetNearRandomPosition(this Transform trans, float minRange, float maxRange)
    {
        var randomDirection = Random.insideUnitSphere.normalized;
        var randomDistance = Random.Range(minRange, maxRange);

        return trans.position + randomDirection * randomDistance;
    }
}