using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GameFolders.Scripts.General;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoSingleton<UIController>
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private TextMeshProUGUI playerScoreText;
    [SerializeField] private TextMeshProUGUI enemyScoreText;

    private void Awake()
    {
        Singleton();
    }

    private void Start()
    {
        playerScoreText.transform.parent.DOScale(Vector3.one * 1.1f, 1f).SetEase(Ease.OutElastic)
            .OnComplete(() => playerScoreText.transform.parent.DOScale(Vector3.one * 1, 0.2f));
        enemyScoreText.transform.parent.DOScale(Vector3.one * 1.1f, 1f).SetEase(Ease.OutElastic)
            .OnComplete(() => enemyScoreText.transform.parent.DOScale(Vector3.one * 1, 0.2f));
        playerScoreText.text = $"{GameManager.Instance.PlayerScore}";
        enemyScoreText.text = $"{GameManager.Instance.EnemyScore}";
    }

    public Vector2 GetJoystick()
    {
        return joystick.Direction;
    }

    public float GetHorizontal()
    {
        return joystick.Horizontal;
    }

    public float GetVertical()
    {
        return joystick.Vertical;
    }
}
