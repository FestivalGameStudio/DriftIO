using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWireSphere : MonoBehaviour
{
    [SerializeField] private Color color;
    [SerializeField] private float radius;

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
