using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class BodySelector : MonoBehaviour
{
    [SerializeField] private GameObject[] bodies;
    [SerializeField] private int activeBodyIndex;

    private int _activeBodyIndex;

    private void Start()
    {
        _activeBodyIndex = activeBodyIndex;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ChangeBody();
        }
    }

    private void ChangeBody()
    {
        int randomIndex = _activeBodyIndex;

        while (randomIndex == _activeBodyIndex)
        {
            randomIndex = Random.Range(0, bodies.Length);
        }

        bodies[activeBodyIndex].SetActive(false);
        activeBodyIndex = randomIndex;
        bodies[activeBodyIndex].SetActive(false);
    }

    private void OnValidate()
    {
        if (bodies.Length == 0 || activeBodyIndex >= bodies.Length) return;

        foreach (GameObject body in bodies)
        {
            body.SetActive(false);
        }
        
        bodies[activeBodyIndex].SetActive(true);
    }
}