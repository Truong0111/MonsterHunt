using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoUI : MonoBehaviour
{
    public Image heathBar;
        
    public static Action<float> UpdateHealthBar;

    private Tween _tween;
    public void Awake()
    {
        UpdateHealthBar += UpdatePlayerHeathBar;
    }

    private void UpdatePlayerHeathBar(float percent)
    {
        _tween?.Kill(true);
        _tween = heathBar.DOFillAmount(percent, 0.1f);
    }
}
