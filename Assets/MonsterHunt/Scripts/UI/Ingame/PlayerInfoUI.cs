using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUI : MonoBehaviour
{
    public Image heathBar;

    public FloatEvent updateHealthBarEvent;

    private Tween _tween;
    public void Awake()
    {
        updateHealthBarEvent.Register(UpdatePlayerHeathBar);
    }

    private void OnDestroy()
    {
        updateHealthBarEvent.Unregister(UpdatePlayerHeathBar);
    }

    private void UpdatePlayerHeathBar(float percent)
    {
        heathBar.DOFillAmount(percent, 0.1f);
    }
}
