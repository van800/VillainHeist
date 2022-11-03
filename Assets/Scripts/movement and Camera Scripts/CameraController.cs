using Cinemachine;
using UnityEngine;

namespace movement_and_Camera_Scripts
{
    public class CameraController : MonoBehaviour
    {
        private CinemachineVirtualCamera _povCam;
        
        private CinemachineVirtualCamera _tdCam;

        private Camera _mainCamera;

        // Start is called before the first frame update
        void Start()
        {
            _mainCamera = GetComponentInChildren<Camera>();
            CinemachineVirtualCamera[] cmCameras = FindObjectsOfType<CinemachineVirtualCamera>();
            _povCam = cmCameras[1];
            _tdCam = cmCameras[0];
            SetPerspective(FindObjectOfType<PlayerController>().isFirstPov);
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        //Set the current room to the player's current room
        public void SetRoom(AreaController area)
        {
            CinemachineConfiner roomBoundary = GetComponentInChildren<CinemachineConfiner>();
            roomBoundary.m_BoundingVolume = area.boundary;
        }

        public void SetPerspective(bool isFirstPov)
        {
            _mainCamera.orthographic = !isFirstPov;
            if (isFirstPov)
            {
                _povCam.Priority = 1;
                _tdCam.Priority = 0;
            }
            else
            {
                _povCam.Priority = 0;
                _tdCam.Priority = 1;
            }
        }
    }
}
