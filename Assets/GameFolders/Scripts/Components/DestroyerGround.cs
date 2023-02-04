using GameFolders.Scripts.Controllers;
using UnityEngine;

namespace GameFolders.Scripts.Components
{
    public class DestroyerGround : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out CarController carController))
            {
                GameManager.Instance.EnemyScore++;
                carController.FallDown();
            }
            
            if (collision.gameObject.TryGetComponent(out AIController aiController))
            {
                GameManager.Instance.PlayerScore++;
                aiController.FallDown();
            }

            if (collision.gameObject.TryGetComponent(out ExplodeObject explodeObject))
            {
                Destroy(collision.gameObject);
            }

        }

    }
}
