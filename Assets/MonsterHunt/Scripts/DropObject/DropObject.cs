using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class DropObject : MonoBehaviour
{
    public float moveSpeed;
    public TextMeshPro textCollect;
    
    public bool ReadyToMove { get; set; }
    public Droppable droppable;

    private Transform _target;

    public virtual void OnDisable()
    {
        ReadyToMove = false;
        _target = null;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Magnet>(out var magnet))
        {
            if (droppable is Droppable.Gold or Droppable.HealthItem or Droppable.None)
            {
                ReadyToMove = true;
                _target = magnet.transform;
            }
        }

        if (other.TryGetComponent<Player>(out var player))
        {
            if (droppable is Droppable.BulletBox or Droppable.WeaponDrop or Droppable.None)
            {
                return;
            }
            SimplePool.Despawn(gameObject);
        }
    }

    public virtual void Update()
    {
        if (!ReadyToMove) return;
        if (_target == null) return;
        transform.position =
            Vector3.MoveTowards(transform.position, _target.position, moveSpeed * Time.deltaTime);
    }
}

[Serializable]
public enum Droppable
{
    None = 0,
    Gold = 1 << 2,
    BulletBox = 1 << 3,
    HealthItem = 1 << 4,
    WeaponDrop = 4 << 5,
}