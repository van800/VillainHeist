using Cinemachine;
using UnityEngine;

namespace movement_and_Camera_Scripts
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] [Tooltip("Player")]
        private PlayerController playerController;
        
        private Vector3 _roomTransform;
        private float[] _dims;
        private Camera _camera;

        // Start is called before the first frame update
        void Start()
        {
            _camera = GetComponent<Camera>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        //Set the current room to the player's current room
        public void SetRoom(GameObject room)
        {
            CinemachineConfiner roomBoundary = GetComponentInChildren<CinemachineConfiner>();
            roomBoundary.m_BoundingVolume = room.GetComponent<Collider>();
        }

        public void SwitchPerspective()
        {
            _camera.orthographic = !playerController.isFirstPov;
        }
    }
}
