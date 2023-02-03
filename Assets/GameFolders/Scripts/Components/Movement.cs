using UnityEngine;

namespace GameFolders.Scripts.Components
{
    public class Movement : MonoSingleton<Movement>
    {
        [SerializeField] private float forwardSpeed;
        [SerializeField] private float turnSpeed;

        private Rigidbody _rigidbody;

        private float _horizontal;
        private float _vertical;
        private bool _canRotate;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            Singleton();
        }

        private void Update()
        {
            _horizontal = UIController.Instance.GetHorizontal();
            _vertical = UIController.Instance.GetVertical();

            if (Input.GetMouseButtonDown(0)) _canRotate = true;
            if (Input.GetMouseButtonUp(0)) _canRotate = false;
        }


        private void FixedUpdate()
        {
            Vector3 forward = transform.forward;
            _rigidbody.velocity = new Vector3(forward.x * forwardSpeed, _rigidbody.velocity.y,
                forward.z * forwardSpeed);

            if (!_canRotate) return;

            Vector3 direction = Vector3.right * _horizontal + Vector3.forward * _vertical;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),
                turnSpeed * Time.fixedDeltaTime);
        }
    }
}