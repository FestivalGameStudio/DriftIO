using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class CenterBall : MonoBehaviour
{
    [SerializeField] private float power;
    
    private Rigidbody _rigidbody;
    
    [Button]
    private void RotateRight()
    {
        if (_rigidbody.angularVelocity.y > 0)
        {
            _rigidbody.angularVelocity = Vector3.zero;
        }
        _rigidbody.AddTorque(Vector3.down * power);
    }
    
    [Button]
    private void RotateLeft()
    {
        if (_rigidbody.angularVelocity.y > 0)
        {
            _rigidbody.angularVelocity = Vector3.zero;
        }
        _rigidbody.AddTorque(Vector3.up * power);
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, transform.localScale.x);
    }
}
