using UnityEngine;

public class InteractUI : Singleton<InteractUI>
{
    public GameObject groupInteract;
    
    public override void Awake()
    {
        base.Awake();
        
        groupInteract.SetActive(false);
    }

    public void ShowInteractText()
    {
        groupInteract.SetActive(true);
    }

    public void HideInteractText()
    {
        groupInteract.SetActive(false);
    }
}