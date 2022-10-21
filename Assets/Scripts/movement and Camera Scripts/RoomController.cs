using UnityEngine;

namespace movement_and_Camera_Scripts
{
    public class RoomController : MonoBehaviour
    {
        [SerializeField] [Tooltip("Left, Bottom, Front Coordinate of Room")]
        private Transform position;

        [SerializeField] [Tooltip("X-Width")]
        private float width;

        [SerializeField] [Tooltip("Z-Length")]
        private float length;

        [SerializeField] [Tooltip("Y-Height")]
        private float height;

        // Returns the position of the room
        public Transform GetTransform()
        {
            return position;
        }
        
        // Returns the [width, length, height] of the room
        public float[] GetSize()
        {
            return new[] { width, length, height };
        }
    }
}
