using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameFolders.Scripts.Components
{
   public class RandomTrailEmitter : MonoBehaviour
   {
      [SerializeField] private Vector2 timeRange;

      private TrailRenderer _trailRenderer;

      private float _currentTime;
      private bool _runStatus;

      private void Awake()
      {
         _trailRenderer = GetComponent<TrailRenderer>();
      }

      private void Start()
      {
         _currentTime = Random.Range(timeRange.x, timeRange.y);
         _trailRenderer.emitting = false;
      }

      private void Update()
      {
         if (!_runStatus) return;

         _currentTime -= Time.deltaTime;

         if (!(_currentTime < 0)) return;
         
         _trailRenderer.emitting = !_trailRenderer.emitting;
         _currentTime = Random.Range(timeRange.x, timeRange.y);
      }

      public void SetRunStatus(bool status)
      {
         _runStatus = status;

         if (!status)
         {
            _trailRenderer.emitting = false;
         }
      }
   }
}
