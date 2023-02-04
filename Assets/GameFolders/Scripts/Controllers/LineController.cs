using GameFolders.Scripts.General;
using UnityEngine;

namespace GameFolders.Scripts.Controllers
{
    public class LineController : MonoBehaviour
    {
        [SerializeField] private Transform ballTransform;
        
        private LineRenderer _lineRenderer;
    
        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }
        
        void Update()
        {
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, ballTransform.position);
        }
        
    }
}
