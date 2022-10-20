using Cinemachine;
using UnityEngine;

namespace movement_and_Camera_Scripts
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] [Tooltip("Player")]
        private PlayerController playerController;

        [SerializeField] [Tooltip("First Person Camera")]
        private CinemachineVirtualCamera povCam;
        
        [SerializeField] [Tooltip("Top Down Camera")]
        private CinemachineVirtualCamera TDCam;
        
        private Vector3 _roomTransform;
        private Camera _camera;

        // Start is called before the first frame update
        void Start()
        {
            _camera = GetComponent<Camera>();
            SwitchPerspective();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        //Set the current room to the player's current room
        public void SetRoom(RoomController room)
        {
            CinemachineConfiner roomBoundary = GetComponentInChildren<CinemachineConfiner>();
            roomBoundary.m_BoundingVolume = room.boundary;
        }

        public void SwitchPerspective()
        {
            bool isFirstPov = playerController.isFirstPov; // True if switching TO first person
            _camera.orthographic = !isFirstPov;
            if (isFirstPov)
            {
                povCam.Priority = 1;
                TDCam.Priority = 0;
            }
            else
            {
                povCam.Priority = 0;
                TDCam.Priority = 1;
            }
        }
    }
}
