using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.AI;

public static class Extension
{
    // public static Vector3 Set(this Vector3 vector, float? x = null, float? y = null, float? z = null)
    // {
    //     vector = new Vector3(x ?? vector.x, y ?? vector.y, z ?? vector.z);
    //     return vector;
    // }

    public static IEnumerator CountDown(float time, Action action)
    {
        var currentTime = time;
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            yield return Time.deltaTime;
        }

        action?.Invoke();
    }

    public static T DeepClone<T>(this T obj)
    {
        using var ms = new MemoryStream();
        var formatter = new BinaryFormatter();
        formatter.Serialize(ms, obj);
        ms.Position = 0;

        return (T)formatter.Deserialize(ms);
    }

    public static bool IsReachTarget(this Vector3 pos, Vector3 target)
    {
        var currentVt2 = new Vector2(pos.x, pos.z);
        var targetVt3 = new Vector2(target.x, target.z);
        return Vector2.Distance(currentVt2, targetVt3) <= Constant.MinCheckReachTarget;
    }

    public static bool IsAgentReachTarget(this NavMeshAgent agent)
    {
        return !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance;
    }

    public static bool IsPlayerInRange(this Vector3 agentPos, Vector3 target, float range)
    {
        return Vector3.Distance(agentPos, target) < range;
    }
    
    public static void At(this StateMachine stateMachine, IState from, IState to, IPredicate condition) =>
        stateMachine.AddTransition(from, to, condition);

    public static void Any(this StateMachine stateMachine, IState to, IPredicate condition) =>
        stateMachine.AddAnyTransition(to, condition);
}