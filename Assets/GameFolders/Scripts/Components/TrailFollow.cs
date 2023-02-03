using GameFolders.Scripts.General;
using UnityEngine;

namespace GameFolders.Scripts.Components
{
    public class TrailFollow : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private UpdateType updateType;
        [SerializeField] private float followSpeed;
        private void FixedUpdate()
        {
            if (updateType == UpdateType.FixedUpdate)
            {
                Follow();
            }
        }

        private void Update()
        {
            if (updateType == UpdateType.Update)
            {
                Follow();
            }
        }

        private void LateUpdate()
        {
            if (updateType == UpdateType.LateUpdate)
            {
                Follow();
            }
        }
    
        private void Follow()
        {
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * followSpeed);
        }

    }
}
