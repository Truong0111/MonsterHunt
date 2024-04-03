using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Character character;
    
    protected CharacterController CharacterController => character.CharacterController;
    public Animator Animator => character.Animator;
    
    protected float Speed => character.Speed;
    protected float JumpForce => character.currentCharacterData.jumpForce;

    #region MonoBehaviour

    public virtual void Awake()
    {
        character = GetComponent<Character>();
    }

    public virtual void Update()
    {
    }

    #endregion
}