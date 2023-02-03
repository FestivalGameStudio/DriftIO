using System;
using UnityEngine;

namespace GameFolders.Scripts.Components
{
    public class DestroyWithLifeTime : MonoBehaviour
    {
        [SerializeField] private float lifeTime;
        
        private float _currentTime;

        private void Update()
        {
            _currentTime += Time.deltaTime;

            if (_currentTime >= lifeTime)
            {
                Destroy(gameObject);
            }
        }
    }
}
