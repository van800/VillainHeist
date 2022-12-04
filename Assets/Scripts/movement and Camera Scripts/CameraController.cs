using areas_and_respawn;
using Cinemachine;
using UnityEngine;

namespace movement_and_Camera_Scripts
{
    enum CameraMode{TopDown, First}
    public class CameraController : MonoBehaviour
    {
        private CinemachineVirtualCamera _povCam;
        
        private CinemachineVirtualCamera _tdCam;

        private Camera _mainCamera;
        [SerializeField] private Camera uiCamera;
        private CameraMode _cameraMode;
        private CinemachineConfiner _roomBoundary;

        // Start is called before the first frame update
        void Start()
        {
            _mainCamera = GetComponentInChildren<Camera>();
            CinemachineVirtualCamera[] cmCameras = FindObjectsOfType<CinemachineVirtualCamera>();
            _povCam = cmCameras[1];
            _tdCam = cmCameras[0];
            SetPerspective(FindObjectOfType<PlayerController>().isFirstPov);
        }

        //Set the current room to the player's current room
        public void SetRoom(RoomController room)
        {
            _roomBoundary = GetComponentInChildren<CinemachineConfiner>();
            _roomBoundary.m_BoundingVolume = room.GetCameraBoundary();
        }

        public void SetPerspective(bool isFirstPov)
        {
            _mainCamera.orthographic = !isFirstPov;
            uiCamera.orthographic = !isFirstPov;
            if (isFirstPov)
            {
                _povCam.Priority = 1;
                _tdCam.Priority = 0;
                _cameraMode = CameraMode.First;
            }
            else
            {
                _povCam.Priority = 0;
                _tdCam.Priority = 1;
                _cameraMode = CameraMode.TopDown;
            }
        }

        public Transform GetCameraTransform()
        {
            return _mainCamera.transform;
        }
    }
}
