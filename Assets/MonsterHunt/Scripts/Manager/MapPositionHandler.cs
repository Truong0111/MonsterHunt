using System;
using UnityEngine;
using Random = UnityEngine.Random;
using static NavMeshHelper;

public class MapPositionHandler : Singleton<MapPositionHandler>
{
    public BoxCollider[] movementArea;

    public Vector3 GetPositionToMove()
    {
        var moveArea = movementArea[Random.Range(0, movementArea.Length)];
        return GetRandomPosition(moveArea);
    }
    
    public Vector3 GetRandomPos()
    {
        var moveArea = movementArea[Random.Range(0, movementArea.Length)];
        return GetRandomPosition(moveArea);
    }
}