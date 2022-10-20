using UnityEngine;

namespace movement_and_Camera_Scripts
{
    public class RoomController : MonoBehaviour
    {
        [SerializeField] [Tooltip("Top Down Camera Boundary")]
        public Collider boundary;
    }
}
