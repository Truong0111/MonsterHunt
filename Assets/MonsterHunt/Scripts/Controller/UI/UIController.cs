using UnityEngine;

public class UIController : Singleton<UIController>
{
    public GameObject menuUI;

    public GameState currentState;

    public override void Awake()
    {
        base.Awake();
        currentState = GameState.Menu;
    }

    public void Pause(bool isShow)
    {
        if (isShow && currentState != GameState.InGame)
        {
            return;
        }
        currentState = isShow ? GameState.Pause : GameState.InGame;
        Cursor.lockState = isShow ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isShow;
    }

    public void ShowMenu(bool isShow)
    {
        if (isShow)
        {
            ChangState(GameState.Menu);
        }
        menuUI.gameObject.SetActive(isShow);
    }

    public void ChangState(GameState newState)
    {
        if (currentState == newState)
        {
            return;
        }

        currentState = newState;
    }
}