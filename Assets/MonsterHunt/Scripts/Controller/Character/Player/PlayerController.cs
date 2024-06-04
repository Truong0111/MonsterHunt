using System;
using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.InputSystem;
using static Model;

public class PlayerController : CharacterMovement
{
    public BoolEvent pauseEvent;
    public IntEvent winGameEvent;
    public IntEvent loseGameEvent;

    private CharacterController _characterController;

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
    [ShowInInspector] private float _playerGravity;

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

    private bool _isPause;
    private bool _isWin;
    private bool _isLose;
    
    public override void Awake()
    {
        Application.targetFrameRate = 60;

        _characterController = GetComponent<CharacterController>();

        pauseEvent.Register(Pause);
        winGameEvent.Register(Win);
        loseGameEvent.Register(Lose);
        
        SubscribeInputEvent();
    }

    private void OnEnable()
    {
        _newPlayerRotation = transform.localRotation.eulerAngles;
        _cameraHeight = cameraHolder.localPosition.y;
    }

    private void OnDestroy()
    {
        pauseEvent.Unregister(Pause);
        winGameEvent.Register(Win);
        loseGameEvent.Register(Lose);

        UnSubscribeInputEvent();
    }

    private void SubscribeInputEvent()
    {
        GameManager.Instance.PlayerInput.CharacterControl.Move.performed += UpdateMove;
        GameManager.Instance.PlayerInput.CharacterControl.View.performed += UpdateView;
        GameManager.Instance.PlayerInput.CharacterControl.Jump.performed += Jump;
        GameManager.Instance.PlayerInput.CharacterControl.Crouch.performed += Crouch;
        GameManager.Instance.PlayerInput.CharacterControl.Sprint.performed += ToggleSprint;
        GameManager.Instance.PlayerInput.CharacterControl.SprintReleased.performed += StopSprint;
    }

    private void UnSubscribeInputEvent()
    {
        GameManager.Instance.PlayerInput.CharacterControl.Move.performed -= UpdateMove;
        GameManager.Instance.PlayerInput.CharacterControl.View.performed -= UpdateView;
        GameManager.Instance.PlayerInput.CharacterControl.Jump.performed -= Jump;
        GameManager.Instance.PlayerInput.CharacterControl.Crouch.performed -= Crouch;
        GameManager.Instance.PlayerInput.CharacterControl.Sprint.performed -= ToggleSprint;
        GameManager.Instance.PlayerInput.CharacterControl.SprintReleased.performed -= StopSprint;
    }

    public override void Update()
    {
        if (_isWin) return;
        if (_isLose) return;
        if (_isPause) return;
        CalculateView();
        CalculateMove();
        CalculateJump();
        // CalculateStance();
    }

    private void Win()
    {
        _isWin = !_isWin;
    }
    
    private void Lose()
    {
        _isLose = !_isLose;
    }

    private void Pause(bool isPause)
    {
        _isPause = isPause;
        _characterController.Move(Vector3.zero);
    }

    private void UpdateMove(InputAction.CallbackContext context)
    {
        inputMovement = context.ReadValue<Vector2>();
    }

    private void UpdateView(InputAction.CallbackContext context)
    {
        inputView = context.ReadValue<Vector2>();
    }


    private void Jump(InputAction.CallbackContext context)
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

    private void Crouch(InputAction.CallbackContext context)
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

    private void ToggleSprint(InputAction.CallbackContext context)
    {
        if (inputMovement.y <= 0.2f)
        {
            isSprinting = false;
            return;
        }

        isSprinting = !isSprinting;
    }

    private void StopSprint(InputAction.CallbackContext context)
    {
        if (playerSettingModel.sprintingHold)
        {
            isSprinting = false;
        }
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

        rootMotionTransform.localRotation = Quaternion.Euler(_newCameraRotation);
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
}