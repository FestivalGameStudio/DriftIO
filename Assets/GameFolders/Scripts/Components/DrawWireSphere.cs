using UnityEngine;

namespace GameFolders.Scripts.Components
{
    public class DrawWireSphere : MonoBehaviour
    {
        [SerializeField] private Color color;
        [SerializeField] private float radius;

        private void OnDrawGizmos()
        {
            Gizmos.color = color;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
