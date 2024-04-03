using System;
using UnityEngine;
using static Model;

public class PlayerController : CharacterMovement
{
    private CharacterController _characterController;

    private PlayerInput _playerInput;
    [HideInInspector] public Vector2 inputMovement;
    [HideInInspector] public Vector2 inputView;

    private Vector3 _newCameraRotation;
    private Vector3 _newPlayerRotation;

    [Header("Ref")] public Transform cameraHolder;
    public Transform rootMotionTransform;
    public Transform playerCameraTransform;
    public Transform feetTransform;

    [Header("Setting")] public PlayerSettingModel playerSettingModel;
    public float viewClampMin = -80f;
    public float viewClampMax = 90f;
    public LayerMask playerMask;

    [Header("Gravity")] public float gravityAmount;
    public float gravityMin;
    private float _playerGravity;

    [Header("Jump")] public Vector3 jumpForce;
    private Vector3 _jumpForceVelocity;

    [Header("Stance")] public PlayerStance playerStance;
    public float playerStanceSmoothing;
    public CharacterStance playerStandStance;
    public CharacterStance playerCrouchStance;

    private const float StanceCheckErrorMargin = 0.05f;
    private float _cameraHeight;
    private float _cameraHeightVelocity;

    private Vector3 _stanceCapsuleCenter;
    private Vector3 _stanceCapsuleCenterVelocity;

    private float _stanceCapsuleHeight;
    private float _stanceCapsuleHeightVelocity;

    public bool isSprinting;

    private Vector3 _newMovementSpeed;
    private Vector3 _newMovementSpeedVelocity;

    public override void Awake()
    {
        _playerInput = new PlayerInput();

        _playerInput.CharacterControl.Move.performed += e => inputMovement = e.ReadValue<Vector2>();
        _playerInput.CharacterControl.View.performed += e => inputView = e.ReadValue<Vector2>();
        _playerInput.CharacterControl.Jump.performed += e => Jump();
        _playerInput.CharacterControl.Crouch.performed += e => Crouch();
        _playerInput.CharacterControl.Sprint.performed += e => ToggleSprint();
        _playerInput.CharacterControl.SprintReleased.performed += e => StopSprint();
        
        _playerInput.Enable();

        // _newCameraRotation = cameraHolder.localRotation.eulerAngles;
        _newPlayerRotation = transform.localRotation.eulerAngles;

        _cameraHeight = cameraHolder.localPosition.y;

        _characterController = GetComponent<CharacterController>();
    }

    public override void Update()
    {
        CalculateView();
        CalculateMove();
        CalculateJump();
        // CalculateStance();
    }

    private void CalculateView()
    {
        _newPlayerRotation.y +=
            playerSettingModel.viewXSensitivity *
            (playerSettingModel.viewXInverted ? -1f : 1f) *
            inputView.x * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(_newPlayerRotation);

        _newCameraRotation.x +=
            playerSettingModel.viewYSensitivity *
            (playerSettingModel.viewYInverted ? -1f : 1f) *
            -inputView.y * Time.deltaTime;

        _newCameraRotation.x = Mathf.Clamp(_newCameraRotation.x, viewClampMin, viewClampMax);

        // playerCameraTransform.localRotation = Quaternion.Euler(_newCameraRotation / 2f);
        rootMotionTransform.localRotation = Quaternion.Euler(_newCameraRotation);
        // handWeapon.localRotation = Quaternion.Euler(_newCameraRotation);
    }

    private void CalculateMove()
    {
        if (inputMovement.y <= 0.2f)
        {
            isSprinting = false;
        }

        if (playerStance == PlayerStance.Crouch)
        {
            playerSettingModel.speedEffector = playerSettingModel.crouchSpeedEffector;
        }
        else
        {
            playerSettingModel.speedEffector = 1;
        }
        
        var verticalSpeed = isSprinting
            ? playerSettingModel.runningForwardSpeed
            : playerSettingModel.walkingForwardSpeed;
        var horizontalSpeed = isSprinting
            ? playerSettingModel.runningStrafeSpeed
            : playerSettingModel.walkingStrafeSpeed;
        
        verticalSpeed *= playerSettingModel.speedEffector;
        horizontalSpeed *= playerSettingModel.speedEffector;

        _newMovementSpeed = Vector3.SmoothDamp(_newMovementSpeed, new Vector3(
                horizontalSpeed * inputMovement.x * Time.deltaTime,
                0f,
                verticalSpeed * inputMovement.y * Time.deltaTime), ref _newMovementSpeedVelocity,
            playerSettingModel.movementSmoothing);

        var movementSpeed = transform.TransformDirection(_newMovementSpeed);

        if (_playerGravity > gravityMin)
        {
            _playerGravity -= gravityAmount * Time.deltaTime;
        }

        if (_playerGravity < -0.1f && _characterController.isGrounded)
        {
            _playerGravity = -0.1f;
        }

        movementSpeed.y += _playerGravity;

        movementSpeed += jumpForce * Time.deltaTime;

        _characterController.Move(movementSpeed);
    }

    private void CalculateJump()
    {
        jumpForce = Vector3.SmoothDamp(jumpForce, Vector3.zero, ref _jumpForceVelocity, playerSettingModel.jumpFalloff);
    }

    private void CalculateStance()
    {
        var currentStance = playerStandStance;
        
        if (playerStance == PlayerStance.Crouch)
        {
            currentStance = playerCrouchStance;
        }
        
        var cameraHolderLocalPosition = cameraHolder.localPosition;
        _cameraHeight = Mathf.SmoothDamp(cameraHolderLocalPosition.y, currentStance.cameraHeight,
            ref _cameraHeightVelocity, playerStanceSmoothing);
        
        cameraHolderLocalPosition = new Vector3(
            x: cameraHolderLocalPosition.x,
            y: _cameraHeight,
            z: cameraHolderLocalPosition.z);
        cameraHolder.localPosition = cameraHolderLocalPosition;
        
        _characterController.height = Mathf.SmoothDamp(_characterController.height, currentStance.stanceCollider.height,
            ref _stanceCapsuleHeightVelocity, playerStanceSmoothing);
        
        _characterController.center = Vector3.SmoothDamp(_characterController.center,
            currentStance.stanceCollider.center,
            ref _stanceCapsuleCenterVelocity, playerStanceSmoothing);
    }

    private void Jump()
    {
        if (!_characterController.isGrounded) return;

        jumpForce = Vector3.up * playerSettingModel.jumpHeight;
        _playerGravity = 0;

        if (playerStance == PlayerStance.Crouch)
        {
            if (StanceCheck(playerStandStance.stanceCollider.height)) return;

            playerStance = PlayerStance.Stand;
        }
    }

    private void Crouch()
    {
        if (playerStance == PlayerStance.Crouch)
        {
            if (StanceCheck(playerStandStance.stanceCollider.height)) return;

            playerStance = PlayerStance.Stand;
            return;
        }

        if (StanceCheck(playerCrouchStance.stanceCollider.height)) return;

        playerStance = PlayerStance.Crouch;
    }

    private bool StanceCheck(float stanceCheckHeight)
    {
        var feetPosition = feetTransform.position;
        var radius = _characterController.radius;

        var start = new Vector3(
            x: feetPosition.x,
            y: feetPosition.y + radius + StanceCheckErrorMargin + stanceCheckHeight,
            z: feetPosition.z);
        var end = new Vector3(
            x: feetPosition.x,
            y: feetPosition.y - radius - StanceCheckErrorMargin + stanceCheckHeight,
            z: feetPosition.z);


        return Physics.CheckCapsule(start, end, radius, playerMask);
    }

    private void ToggleSprint()
    {
        if (inputMovement.y <= 0.2f)
        {
            isSprinting = false;
            return;
        }

        isSprinting = !isSprinting;
    }

    private void StopSprint()
    {
        if (playerSettingModel.sprintingHold)
        {
            isSprinting = false;
        }
    }
}