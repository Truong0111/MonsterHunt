using System;
using UnityEngine;

public static class Model
{
    #region Player

    public enum PlayerStance
    {
        Stand,
        Crouch
    }
    
    [Serializable]
    public class PlayerSettingModel
    {
        [Header("View Setting")]
        public float viewXSensitivity;
        public float viewYSensitivity;
        
        public bool viewXInverted;
        public bool viewYInverted;

        [Header("Movement Settings")] 
        public bool sprintingHold;
        public float movementSmoothing;
        
        [Header("Movement Walking Setting")] 
        public float walkingForwardSpeed;
        public float walkingStrafeSpeed;

        [Header("Movement Running Setting")] 
        public float runningForwardSpeed;
        public float runningStrafeSpeed;
        
        [Header("Jumping Setting")] 
        public float jumpHeight;
        public float jumpFalloff;

        [Header("Speed Effectors Setting")]
        public float speedEffector = 1;
        public float crouchSpeedEffector;
    }

    [Serializable]
    public class CharacterStance
    {
        public float cameraHeight;
        public CapsuleCollider stanceCollider;
    }
    
    #endregion
}