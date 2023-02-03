using System;
using System.Collections;
using System.Collections.Generic;
using GameFolders.Scripts.General;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoSingleton<UIController>
{
    private EventData _eventData;

    [SerializeField] private Joystick joystick;

    [Header("Panels")]
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject losePanel;
    
    [Header("Buttons")]
    [SerializeField] Button nextLevelButton;
    [SerializeField] Button tryAgainButton;

    private void Awake()
    {
        Singleton();
        _eventData = Resources.Load("EventData") as EventData;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
