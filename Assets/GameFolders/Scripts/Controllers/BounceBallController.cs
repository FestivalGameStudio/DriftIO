using System;
using System.Collections;
using DG.Tweening;
using GameFolders.Scripts.Components;
using GameFolders.Scripts.General;
using UnityEngine;
using Random = UnityEngine.Random;
using UpdateType = GameFolders.Scripts.General.UpdateType;

namespace GameFolders.Scripts.Controllers
{
    public class BounceBallController : MonoBehaviour
    {
        [SerializeField] private BelongsTo belongsTo;
        [SerializeField] private Transform ballReferenceTransform;
        [SerializeField] private Transform carTransform;
        
        [SerializeField] private float baseFollowSpeed;
        [SerializeField] private float maxFollowSpeed;
        [SerializeField] private float speedCoefficientForDistance;
        [SerializeField] private float minDistance;
        [SerializeField] private GameObject trailEffect;

        [Header("Circle Spin Skill")] 
        [SerializeField] private float duration;
        [SerializeField] private float spinSpeed;

        [Header("Explode Variables")] 
        [SerializeField] private Vector2 forceRange;
        [SerializeField] private Vector2 carForceRange;

        private Rigidbody _rigidbody;
        private EventData _eventData;

        private float _followSpeed;
        private bool _canFollow;
        private float _currentTime;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        
        private void Start()
        {
            _canFollow = true;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out ExplodeObject explodeObject))
            {
                float force = Random.Range(forceRange.x, forceRange.y);
                
                explodeObject.Explode(force, transform.position, transform.localScale.x);
               
                if (explodeObject.GiveSpin)
                {
                    Spin();
                }
            }

            if (belongsTo == BelongsTo.Player)
            {
                if (collision.gameObject.TryGetComponent(out AIController aiController))
                {
                    float force = Random.Range(carForceRange.x, carForceRange.y);
                    Vector3 explosionCenterPos = transform.position;

                    aiController.ExplosionForce(force, explosionCenterPos, 5);
                }
            }
            else
            {
                if (collision.gameObject.TryGetComponent(out CarController carController))
                {
                    float force = Random.Range(carForceRange.x, carForceRange.y);
                    Vector3 explosionCenterPos = transform.position;

                    carController.ExplosionForce(force, explosionCenterPos, 5);
                }
            }
        }

        private void FixedUpdate()
        {
            if (!_canFollow) return;

            SpeedUpdate();
            Follow();
        }
        
        private void SpeedUpdate()
        {
            float distanceToPlayer = Vector3.Distance(transform.position, carTransform.position);
            float distanceToTarget = Vector3.Distance(transform.position, ballReferenceTransform.position);

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
            Vector3 newPosition = Vector3.Lerp(transform.position, ballReferenceTransform.position, Time.fixedDeltaTime * _followSpeed);
            _rigidbody.DOMove(newPosition, Time.fixedDeltaTime);
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

                Vector3 angleDegreesPosition = Quaternion.AngleAxis(100 * spinSpeed * Time.deltaTime, Vector3.up) * (transform.position - carTransform.position);
                Vector3 movePosition = carTransform.position + angleDegreesPosition;
                _rigidbody.DOMove(movePosition, Time.fixedDeltaTime);

                yield return new WaitForFixedUpdate();
            }

            while (Vector3.Distance(transform.position, ballReferenceTransform.position) > 1f)
            {
                transform.position = Vector3.Lerp(transform.position, ballReferenceTransform.position, Time.deltaTime * 4);

                yield return null;
            }

            trailEffect.SetActive(false);

            _canFollow = true;
        }
    }
}