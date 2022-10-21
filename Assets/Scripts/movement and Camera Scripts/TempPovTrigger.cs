using UnityEngine;

namespace movement_and_Camera_Scripts
{
    public class TempPovTrigger : MonoBehaviour
    {
        [SerializeField] [Tooltip("Player")] 
        private PlayerController player;
        
        private void OnTriggerEnter(Collider other)
        {
            player.ToPov();
        }
    }
}
