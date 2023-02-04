using System;
using System.Security.Cryptography;
using UnityEngine;
using Sirenix.OdinInspector;

namespace GameFolders.Scripts.Components
{
    [RequireComponent(typeof(Rigidbody))]
    public class ExplodeObject : MonoBehaviour
    {
        [SerializeField] private bool giveSpin;
        [SerializeField] private bool useBlowUp;
        [SerializeField] private bool useEffect;
        [SerializeField] [ShowIf("useEffect")] private GameObject effectObject;

        private Rigidbody _rigidbody;
        
        public bool GiveSpin => giveSpin;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Explode(float force, Vector3 explosionPosition, float radius)
        {
            if (useEffect)
            {
                effectObject.SetActive(true);
                effectObject.transform.parent = null;
            }

            if (useBlowUp)
            {
                _rigidbody.AddExplosionForce(force, explosionPosition, radius);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void Force(Vector3 force)
        {
            _rigidbody.AddForce(force);
        }
    }
}
