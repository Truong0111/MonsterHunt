using Sirenix.OdinInspector;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    public Collider[] ragdollColliders;
    public Rigidbody[] ragdollRigidbodies;
    public CharacterJoint[] ragdollCharacterJoints;

    [Button]
    public void GetRagdollComponent()
    {
        ragdollColliders = GetComponentsInChildren<Collider>();
        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        ragdollCharacterJoints = GetComponentsInChildren<CharacterJoint>();
    }

    [Button]
    public void ClearRagdollComponent()
    {
        GetRagdollComponent();
        foreach (var cj in ragdollCharacterJoints)
        {
            DestroyImmediate(cj);
        }
        foreach (var col in ragdollColliders)
        {
            DestroyImmediate(col);
        }
        foreach (var rb in ragdollRigidbodies)
        {
            DestroyImmediate(rb);
        }

        ragdollColliders = null;
        ragdollCharacterJoints = null;
        ragdollRigidbodies = null;
    }
    
}