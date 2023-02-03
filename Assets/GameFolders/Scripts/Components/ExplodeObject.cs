using System.Security.Cryptography;
using UnityEngine;

namespace GameFolders.Scripts.Components
{
    public class ExplodeObject : MonoBehaviour
    {
        [SerializeField] private bool giveSpin;
        [SerializeField] private GameObject effectObject;

        public bool GiveSpin => giveSpin;
        
        public void Explode()
        {
            effectObject.SetActive(true);
            effectObject.transform.parent = null;
            Destroy(gameObject);
        }
    }
}
