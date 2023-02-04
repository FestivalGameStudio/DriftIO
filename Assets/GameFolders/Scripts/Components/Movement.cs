using System;
using UnityEngine;

namespace GameFolders.Scripts.Components
{
    public class Movement : MonoSingleton<Movement>
    {
        [SerializeField] private float defaultForwardSpeed;
        [SerializeField] private float minForwardSpeed;
        [SerializeField] private float acceleration;
        [SerializeField] private float spinBreak;
        [SerializeField] private float turnSpeed;

        private RandomTrailEmitter[] _wheelTrails;
        private Rigidbody _rigidbody;

        private float _horizontal;
        private float _vertical;
        private bool _canRotate;
        private float _forwardSpeed;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _wheelTrails = GetComponentsInChildren<RandomTrailEmitter>();
        }

        private void Start()
        {
            Singleton();
            _forwardSpeed = defaultForwardSpeed;
        }

        private void Update()
        {
            _horizontal = UIController.Instance.GetHorizontal();
            _vertical = UIController.Instance.GetVertical();
            
            foreach (RandomTrailEmitter wheelTrail in _wheelTrails)
            {
                wheelTrail.SetRunStatus(Mathf.Abs(_horizontal) + Mathf.Abs(_vertical) > 0.5f);
            }
            
            SpeedControl();
            
            if (Input.GetMouseButtonDown(0)) _canRotate = true;
            if (Input.GetMouseButtonUp(0)) _canRotate = false;
        }


        private void FixedUpdate()
        {
            Vector3 forward = transform.forward;

            _rigidbody.MovePosition(transform.position + forward * (_forwardSpeed * Time.deltaTime));

            if (!_canRotate) return;

            Vector3 direction = Vector3.right * _horizontal + Vector3.forward * _vertical;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),
                turnSpeed * Time.fixedDeltaTime);
        }

        private void SpeedControl()
        {
            if (Mathf.Abs(_horizontal) + Mathf.Abs(_vertical) > 0.5f)
            {
                if (_forwardSpeed > minForwardSpeed)
                {
                    _forwardSpeed -= spinBreak * Time.deltaTime;
                }
            }
            else
            {
                if (_forwardSpeed < defaultForwardSpeed)
                {
                    _forwardSpeed += acceleration * Time.deltaTime;
                }
            }
        }
    }
}