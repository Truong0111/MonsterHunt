using Sirenix.OdinInspector;
using UnityEngine;

public class EnemySetupHit : MonoBehaviour
{
    public Hitbox[] hitBoxes;
    
    [Button]
    public void Remove()
    {
        var cjs = GetComponentsInChildren<CharacterJoint>();
        var rbs = GetComponentsInChildren<Rigidbody>();
        var cols = GetComponentsInChildren<Collider>();
        foreach (var cj in cjs)
        {
            DestroyImmediate(cj);
        }
        foreach (var rb in rbs)
        {
            DestroyImmediate(rb);
        }
        foreach (var col in cols)
        {
            DestroyImmediate(col);
        }
    }

    [Button]
    public void RemoveThis()
    {
        DestroyImmediate(this);
    }

    [Button]
    public void Setup()
    {
        hitBoxes = GetComponentsInChildren<Hitbox>();
        foreach (var hb in hitBoxes)
        {
            hb.gameObject.layer = 16;
        }
    }
}