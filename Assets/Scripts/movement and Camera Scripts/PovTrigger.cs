using UnityEngine;

namespace movement_and_Camera_Scripts
{
    public class PovTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            FindObjectOfType<PlayerController>().ToPov();
            
        }
    }
}
