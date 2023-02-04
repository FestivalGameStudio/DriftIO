using System;
using System.Collections;
using GameFolders.Scripts.Components;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace GameFolders.Scripts.Controllers
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private Vector2 moveRange;
        [SerializeField] private float defaultForwardSpeed;
        [SerializeField] private float minForwardSpeed;
        [SerializeField] private float acceleration;
        [SerializeField] private float spinBreak;
        [SerializeField] private float turnSpeed;
        [SerializeField] private Vector2 innerDistanceRange;
        [SerializeField] private Vector2 innerRangeChangeTime;
        [SerializeField] private float forcePower;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private BounceBallController bounceBallController;
        
        private RandomTrailEmitter[] _wheelTrails;
        private Rigidbody _rigidbody;

        private float _forwardSpeed;
        private Vector3 _currentMovePoint;

        private float _innerDistance;
        private float _innerRangeTimer;
        private bool _canMove;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _wheelTrails = GetComponentsInChildren<RandomTrailEmitter>();
        }

        private void Start()
        {
            _forwardSpeed = defaultForwardSpeed;
            _innerDistance = 5;
            _currentMovePoint = GetRandomWorldPoint();
            _innerRangeTimer = Random.Range(innerRangeChangeTime.x, innerRangeChangeTime.y);
            _canMove = true;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out ExplodeObject explodeObject))
            {
                
                if (explodeObject.GiveSpin)
                {
                    explodeObject.Explode(500, transform.position, transform.localScale.x);
                    bounceBallController.Spin();
                }
            }
        }
        
        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out CarController carController))
            {
                Vector3 direction = (transform.position - carController.transform.position).normalized;
                
                carController.ForceMove(-direction * forcePower);
            }

            if (collision.gameObject.TryGetComponent(out ExplodeObject explodeObject))
            {
                Vector3 direction = (transform.position - explodeObject.transform.position).normalized;
                explodeObject.Force(-direction * forcePower);
            }
        }

        private void FixedUpdate()
        {
            if (!_canMove) return;

            Move();
        }

        private void Update()
        {
            if (!_canMove) return;

            CheckDistance();
            Look();

            GetRandomWorldPoint();

            // foreach (RandomTrailEmitter wheelTrail in _wheelTrails)
            // {
            //     wheelTrail.SetRunStatus( _magnitude > 0.1f);
            // }

            _innerRangeTimer -= Time.deltaTime;

            if (_innerRangeTimer <= 0)
            {
                _innerDistance = Random.Range(innerDistanceRange.x, innerDistanceRange.y);
                _innerRangeTimer = Random.Range(innerRangeChangeTime.x, innerRangeChangeTime.y);
            }

            SpeedControl();
        }

        private void SpeedControl()
        {
            // if (Mathf.Abs(_horizontal) + Mathf.Abs(_vertical) > 0.5f)
            // {
            //     if (_forwardSpeed > minForwardSpeed)
            //     {
            //         _forwardSpeed -= spinBreak * Time.deltaTime;
            //     }
            // }
            // else
            // {
            //     if (_forwardSpeed < defaultForwardSpeed)
            //     {
            //         _forwardSpeed += acceleration * Time.deltaTime;
            //     }
            // }
        }

        private void CheckDistance()
        {
            if (Vector3.Distance(transform.position, _currentMovePoint) < 3f)
            {
                _currentMovePoint = GetRandomWorldPoint();
            }
        }

        private void Look()
        {
            Vector3 direction = (_currentMovePoint - transform.position).normalized;
            
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation,
                turnSpeed * Time.deltaTime);
        }
        
        private void Move()
        {
            _rigidbody.MovePosition(transform.position + transform.forward * (_forwardSpeed * Time.fixedDeltaTime));
        }
        
        private Vector3 GetRandomWorldPoint()
        {
            float x;
            float z;

            if (transform.position.x > 0)
            {
                x = transform.position.x + _innerDistance < moveRange.x ? Random.Range(transform.position.x, transform.position.x + _innerDistance) : Random.Range(transform.position.x, transform.position.x - _innerDistance);
            }
            else
            {
                x = transform.position.x - _innerDistance > -moveRange.x ? Random.Range(transform.position.x, transform.position.x - _innerDistance) : Random.Range(transform.position.x, transform.position.x + _innerDistance);
            }
            
            if (transform.position.z > 0)
            {
                z = transform.position.z + _innerDistance < moveRange.y ? Random.Range(transform.position.z, transform.position.z + _innerDistance) : Random.Range(transform.position.z, transform.position.z - _innerDistance);
            }
            else
            {
                z = transform.position.z - _innerDistance > -moveRange.y ? Random.Range(transform.position.z, transform.position.z - _innerDistance) : Random.Range(transform.position.z, transform.position.z + _innerDistance);
            }
            
            // x = Random.Range(-moveRange.x, moveRange.x);
            // z = Random.Range(-moveRange.y, moveRange.y);

            return new Vector3(x, 0, z);
        }

        public void ForceMove(Vector3 force)
        {
            _rigidbody.AddForce(force);
        }
        
        public void ExplosionForce(float force, Vector3 explosionPosition, float radius)
        {
            Vector3 direction = (transform.position - explosionPosition);
            _rigidbody.AddExplosionForce(force, transform.position + direction -Vector3.down, radius);

            if (_canMove)
            {
                StartCoroutine(FlyWaitCoroutine());
            }

        }

        public void FallDown()
        {
            SceneManager.LoadScene(sceneBuildIndex: 0);
        }
        
        IEnumerator FlyWaitCoroutine()
        {
            _canMove = false;
            bool onGround = false;

            yield return new WaitForSeconds(1f);
            
            while (!onGround)
            {
                RaycastHit hit;
                // Does the ray intersect any objects excluding the player layer
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1, layerMask))
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
                    Debug.Log("Did Hit");
                    onGround = true;
                }
                else
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1, Color.white);
                    Debug.Log("Did not Hit");
                }
                
                Look();


                yield return new WaitForFixedUpdate();
            }

            _canMove = true;
        }
        
    }
}