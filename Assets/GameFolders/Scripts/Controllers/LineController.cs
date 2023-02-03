using GameFolders.Scripts.General;
using UnityEngine;

namespace GameFolders.Scripts.Controllers
{
    public class LineController : MonoBehaviour
    {
        [SerializeField] private Transform startTransform;
        
        private Transform _ballTransform;
        private LineRenderer _lineRenderer;
        private EventData _eventData;
    
        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _eventData = Resources.Load("EventData") as EventData;
        }

        private void OnEnable()
        {
            _eventData.OnAssignBallTransform += AssignBallTransform;
        }

        void Update()
        {
            _lineRenderer.SetPosition(0, startTransform.position);
            _lineRenderer.SetPosition(1, _ballTransform.position);
        }

        private void OnDisable()
        {
            _eventData.OnAssignBallTransform -= AssignBallTransform;
        }

        private void AssignBallTransform(Transform ballTransform)
        {
            _ballTransform = ballTransform;
        }
    }
}
