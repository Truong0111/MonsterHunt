using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

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

        return (T) formatter.Deserialize(ms);
    }
}