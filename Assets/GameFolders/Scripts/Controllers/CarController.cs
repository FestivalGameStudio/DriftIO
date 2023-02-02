using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private float turnSpeed;
    [SerializeField] private float moveSpeed;
    
    private Rigidbody _rigidbody;
    private Vector2 _direction;
    private Vector2 _oldDirection;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void LateUpdate()
    {
        _direction = UIController.Instance.GetJoystick() - _oldDirection;
        _oldDirection = UIController.Instance.GetJoystick();
    }

    private void FixedUpdate()
    {
        _rigidbody.angularVelocity = _direction * turnSpeed;
        _rigidbody.velocity = transform.forward * (UIController.Instance.GetJoystick().magnitude * moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
