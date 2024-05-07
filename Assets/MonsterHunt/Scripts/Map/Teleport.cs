using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

public class Teleport : MonoBehaviour
{
    [EnumToggleButtons]
    public enum TypeTeleport
    {
        OpenMap,
        NextMap
    }

    private PlayerInput _playerInput;
    
    private void Awake()
    {
        _playerInput = new PlayerInput();

        _playerInput.Interact.Interact.performed += Interact;
        
        _playerInput.Enable();
    }

    private void OnDestroy()
    {
        _playerInput.Interact.Interact.performed -= Interact;
    }

    public TypeTeleport typeTeleport;

    public bool CanInteract { get; set; } 
        
    [ShowIf(nameof(IsOpenMap))]
    public GameObject areaOpen;
    
    private bool IsOpenMap() => typeTeleport == TypeTeleport.OpenMap;
    private bool IsNextMap() => typeTeleport == TypeTeleport.NextMap;

    private void Interact(InputAction.CallbackContext context)
    {
        if(!CanInteract) return;
        switch (typeTeleport)
        {
            case TypeTeleport.OpenMap:
                OpenMap();
                break;
            case TypeTeleport.NextMap:
                LoadNextMap();
                break;
        }
        InteractUI.Instance.HideInteractText();
    }

    private void OpenMap()
    {
        //TODO: open map
    }

    private void LoadNextMap()
    {
        GameManager.Instance.LoadNextLevel();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            InteractUI.Instance.ShowInteractText();
            CanInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            InteractUI.Instance.HideInteractText();
            CanInteract = false;
        }
    }
}