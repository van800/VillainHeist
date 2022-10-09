using UnityEngine;

namespace aidan_scripts
{
    public class CameraHelper : MonoBehaviour
    {
        [SerializeField] [Tooltip("Room 1")]
        private Transform camSpot1;

        [SerializeField] [Tooltip("Room 2")]
        private Transform camSpot2;
    
    
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            // Camera's position and rotation
            Transform cameraTransform = transform;
            
            // DEBUG
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                //cameraTransform.SetPositionAndRotation(camSpot1.position, camSpot1.rotation);
                cameraTransform.position = camSpot1.position;
                cameraTransform.rotation = camSpot1.rotation;
            } 
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                cameraTransform.SetPositionAndRotation(camSpot2.position, camSpot2.rotation);
            }
        }
    }
}
