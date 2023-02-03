using System;
using System.Collections;
using GameFolders.Scripts.Components;
using GameFolders.Scripts.General;
using UnityEngine;

namespace GameFolders.Scripts.Controllers
{
    public class BounceBallController : MonoBehaviour
    {
        [SerializeField] private UpdateType updateType;
        [SerializeField] private float baseFollowSpeed;
        [SerializeField] private float maxFollowSpeed;
        [SerializeField] private float speedCoefficientForDistance;
        [SerializeField] private float minDistance;
        [SerializeField] private GameObject trailEffect;

        [Header("Circle Spin Skill")] [SerializeField]
        private float duration;

        [SerializeField] private float spinSpeed;

        private Transform _target;
        private Transform _playerTransform;
        private Rigidbody _rigidbody;
        private EventData _eventData;

        private float _followSpeed;
        private bool _canFollow;
        private float _currentTime;

        private void Awake()
        {
            _eventData = Resources.Load("EventData") as EventData;
            //_rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _eventData.OnAssignPlayerTransform += AssignPlayerTransform;
            _eventData.OnAssignBallReferenceTransform += AssignTarget;
        }

        private void Start()
        {
            _eventData.OnAssignBallTransform?.Invoke(transform);
            _canFollow = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ExplodeObject explodeObject))
            {
                explodeObject.Explode();
                Spin();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out ExplodeObject explodeObject))
            {
                explodeObject.Explode();
                if (explodeObject.GiveSpin)
                {
                    Spin();
                }
            }
        }

        private void FixedUpdate()
        {
            if (!_canFollow) return;
            
            if (updateType == UpdateType.FixedUpdate)
            {
                SpeedUpdate();
                Follow();
            }
        }

        private void Update()
        {
            if (!_canFollow) return;

            if (updateType == UpdateType.Update)
            {
                SpeedUpdate();
                Follow();
            }
        }

        private void LateUpdate()
        {
            if (!_canFollow) return;

            if (updateType == UpdateType.LateUpdate)
            {
                SpeedUpdate();
                Follow();
            }
        }

        private void OnDisable()
        {
            _eventData.OnAssignPlayerTransform -= AssignPlayerTransform;
            _eventData.OnAssignBallReferenceTransform -= AssignTarget;
        }

        private void AssignTarget(Transform target)
        {
            _target = target;
        }

        private void AssignPlayerTransform(Transform playerTransform)
        {
            _playerTransform = playerTransform;
        }

        private void SpeedUpdate()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, _playerTransform.position);
            float distanceToTarget = Vector3.Distance(transform.position, _target.position);

            if (distanceToPlayer <= minDistance)
            {
                _followSpeed = maxFollowSpeed;
            }
            else
            {
                _followSpeed = baseFollowSpeed + distanceToTarget * speedCoefficientForDistance;
            }
        }

        private void Follow()
        {
            transform.position = Vector3.Lerp(transform.position, _target.position, Time.deltaTime * _followSpeed);
        }


        private void Spin()
        {
            if (!_canFollow)
            {
                _currentTime = 0;
                return;
            }

            StartCoroutine(SpinCoroutine());
        }

        private IEnumerator SpinCoroutine()
        {
            _canFollow = false;

            trailEffect.transform.position = transform.position;
            trailEffect.SetActive(true);
            
            _currentTime = 0;

            while (_currentTime < duration)
            {
                _currentTime += Time.fixedDeltaTime;

                transform.RotateAround(_playerTransform.position, Vector3.up, 100 * spinSpeed * Time.deltaTime);

                yield return new WaitForFixedUpdate();
            }

            while (Vector3.Distance(transform.position, _target.position) > 1f)
            {
                transform.position = Vector3.Lerp(transform.position, _target.position, Time.deltaTime * 4);

                yield return null;
            }

            trailEffect.SetActive(false);

            _canFollow = true;
        }
    }
}