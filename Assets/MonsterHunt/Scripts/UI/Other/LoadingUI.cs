using System;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class LoadingUI : MonoBehaviour
{
    public BoolEvent loadingEvent;

    private void Awake()
    {
        loadingEvent.Register(ShowLoadingUI);
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        loadingEvent.Unregister(ShowLoadingUI);
    }

    private void ShowLoadingUI(bool isShow)
    {
        gameObject.SetActive(isShow);
    }
}