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

    private void Awake()
    {
        GameManager.Instance.PlayerInput.Interact.Interact.performed += Interact;
    }

    private void OnDestroy()
    {
        GameManager.Instance.PlayerInput.Interact.Interact.performed -= Interact;
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