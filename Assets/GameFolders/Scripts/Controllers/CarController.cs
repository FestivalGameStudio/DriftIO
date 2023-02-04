using System;
using GameFolders.Scripts.Components;
using GameFolders.Scripts.General;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace GameFolders.Scripts.Controllers
{
    public class CarController : MonoBehaviour
    {
        [SerializeField] private float forcePower;
        [SerializeField] private BounceBallController bounceBallController;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
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
            if (collision.gameObject.TryGetComponent(out AIController aiController))
            {
                Vector3 direction = (transform.position - aiController.transform.position).normalized;
                
                aiController.ForceMove(-direction * forcePower);
            }

            if (collision.gameObject.TryGetComponent(out ExplodeObject explodeObject))
            {
                Vector3 direction = (transform.position - explodeObject.transform.position).normalized;
                explodeObject.Force(-direction * forcePower);
            }
        }

        public void FallDown()
        {
            SceneManager.LoadScene(sceneBuildIndex: 0);
        }
        
        public void ForceMove(Vector3 force)
        {
            _rigidbody.AddForce(force);
        }

        public void ExplosionForce(float force, Vector3 explosionPosition, float radius)
        {
            Vector3 direction = (transform.position - explosionPosition).normalized;
            _rigidbody.AddExplosionForce(force, transform.position + direction -Vector3.down, radius);
        }
    }
}
