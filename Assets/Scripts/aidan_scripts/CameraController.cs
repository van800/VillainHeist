using UnityEngine;

namespace aidan_scripts
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] [Tooltip("Player")]
        private PlayerController playerController;
        
        private Vector3 _roomTransform;
        private float[] _dims;
        
        // Start is called before the first frame update
        void Start()
        {
            SetRoom();
        }

        // Update is called once per frame
        void Update()
        {
            // Camera's position and rotation
            Transform cameraTransform = transform;

            // Limit Camera Movement when in 3rd person POV
            if (playerController.isFirstPov)
            {
                Vector3 cameraTransformPosition = cameraTransform.position;
                // Lock X
                if (cameraTransformPosition.x < _roomTransform.x)
                {
                    cameraTransformPosition.x = _roomTransform.x;
                }
                else
                {
                    
                }
            }
        }

        //Set the current room to the player's current room
        public void SetRoom()
        {
            //RoomController roomController = playerController.roomController;
            
            // Room's position (left, bottom, front)
            //_roomTransform = roomController.GetTransform().position;
            
            // Room's dimensions
            //_dims = roomController.GetSize();
        }
    }
}
