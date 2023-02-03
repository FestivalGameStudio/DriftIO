using System;
using UnityEngine;

namespace GameFolders.Scripts.General
{
    [CreateAssetMenu(fileName = "EventData", menuName = "Data/Event Data")]
    public class EventData : ScriptableObject
    {
        public Action OnPlay;
        public Action<bool> OnFinish;
        public Action<Transform> OnAssignPlayerTransform;
        public Action<Transform> OnAssignBallTransform;
        public Action<Transform> OnAssignBallReferenceTransform;
    }
}
