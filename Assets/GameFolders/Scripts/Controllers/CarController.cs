using GameFolders.Scripts.General;
using UnityEngine;

namespace GameFolders.Scripts.Controllers
{
    public class CarController : MonoBehaviour
    {
        [SerializeField] private Transform ballReferenceTransform;
        
        private EventData _eventData;
    
        private void Awake()
        {
            _eventData = Resources.Load("EventData") as EventData;
        }

        private void Start()
        {
            _eventData.OnAssignPlayerTransform?.Invoke(transform);
            _eventData.OnAssignBallReferenceTransform?.Invoke(ballReferenceTransform);
        }
    }
}
