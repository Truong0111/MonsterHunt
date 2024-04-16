using UnityEngine;

public class MagicAttackObject : MonoBehaviour
{
    public float radius = 0.3f;
    public LayerMask layerMask;

    public Vector3 Direction { get; set; }

    public float Damage { get; set; }

    public float Speed { get; set; }

    public bool CanMove { get; set; }
}