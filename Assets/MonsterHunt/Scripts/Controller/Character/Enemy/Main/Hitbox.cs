using System;
using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Hitbox : MonoBehaviour
{
    public Enemy enemy;

    public TypeHitBox hitBoxType;

    private void Awake()
    {
        if (!enemy) enemy.GetComponentInParent<Enemy>();
    }

    public enum TypeHitBox
    {
        Head,
        Body,
        Arm,
        Leg
    }

    public void GetDamage(float damage)
    {
        Debug.Log("call");
        var realDamage = damage;

        realDamage *= hitBoxType switch
        {
            TypeHitBox.Head => 2f,
            TypeHitBox.Body => 1f,
            TypeHitBox.Arm => 0.9f,
            TypeHitBox.Leg => 0.9f,
            _ => 1f
        };
        realDamage -= enemy.currentCharacterData.armor;
        enemy.GetDamage(realDamage);
    }

#if UNITY_EDITOR
    
    [Button]
    public void RemoveThis()
    {
        DestroyImmediate(this);
    }

    #region Collider

    [EnumToggleButtons]
    public enum ColliderType
    {
        Box,
        Capsule,
        Sphere,
        
    }

    [InlineButton(nameof(AddCollider))] [InlineButton(nameof(RemoveCollider))]
    public ColliderType typeCollider;

    [Button]
    public void Setup()
    {
        enemy = GetComponentInParent<Enemy>();
        colorCol = Color.green;
        colorCol.a = 1f;
        
        var col = GetComponent<Collider>();
        col.isTrigger = true;
        var rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    public void AddCollider()
    {
        switch (typeCollider)
        {
            case ColliderType.Box:
                gameObject.AddComponent<BoxCollider>();
                break;
            case ColliderType.Capsule:
                gameObject.AddComponent<CapsuleCollider>();
                break;
            case ColliderType.Sphere:
                gameObject.AddComponent<SphereCollider>();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        var col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    public void RemoveCollider()
    {
        DestroyImmediate(GetComponent<Collider>());
    }

    #endregion
    
    #region Draw

    [InlineButton(nameof(AddDrawCollider))] [InlineButton(nameof(RemoveDrawCollider))]
    public Color colorCol;

    public void AddDrawCollider()
    {
        var draw = gameObject.AddComponent<DrawColliders3D>();
        draw.colliderColor = colorCol;
    }

    public void RemoveDrawCollider()
    {
        DestroyImmediate(gameObject.GetComponent<DrawColliders3D>());
    }

    #endregion

#endif
}