using System;
using UnityEngine;

namespace movement_and_Camera_Scripts
{
    public class PlayerController : MonoBehaviour
    { 
        [SerializeField][Tooltip("Character Controller")]
        private CharacterController characterController;

        [SerializeField][Tooltip("Main Camera")]
        private CameraController cameraController;
        [SerializeField][Tooltip("Is First Person Mode")]
        public bool isFirstPov = false;

        [SerializeField][Tooltip("Speed")]
        public float speed = 5f;
        [SerializeField][Tooltip("Run Speed Multiplier")]
        public float runMultiplier = 2f;
        private Vector3 _velocity;
        
        [SerializeField][Tooltip("Gravity")]
        private float gravity = -9.8f; 
        [SerializeField][Tooltip("Jump height")]
        public float jumpHeight = 5f;
        private bool _grounded;
        private bool _hasJumped = false;
        private const float GroundCastDist = 0.15f;

        public GameObject pickedUpItem;

        
        // Start is called before the first frame update
        void Start()
        {
        }
        

        // Update is called once per frame
        void Update()
        {
            // Transform Fields
            Transform playerTransform = transform;
            Transform cameraTransform = cameraController.transform;
            
            // Grounded
            Vector3 position = playerTransform.position + Vector3.up * 0.01f;

            // Ground movement
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Vector3 movement = ((playerTransform.right * x) + (playerTransform.forward * z)).normalized;
            
           
            // Walk and run movement
            if (Input.GetKey(KeyCode.LeftShift))
            {
                characterController.Move(movement * (speed * runMultiplier * Time.deltaTime));
            }
            else
            {
                characterController.Move(movement * (speed * Time.deltaTime));
            }
            
            _grounded = Physics.Raycast(position, Vector3.down, GroundCastDist);

            // DEBUG - Grounded
            if (_grounded)
            {
                Debug.DrawRay(position, Vector3.down, Color.magenta);  
            }
            else
            {
                Debug.DrawRay(position, Vector3.down, Color.blue);
            }
            
            // Gravity
            _velocity.y += gravity * Time.deltaTime;
            
            // First Person only
            if (isFirstPov)
            {
                // Rotation movement
                playerTransform.rotation = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up);
                
                // Jumping
                if (Input.GetButtonDown("Jump"))
                {
                    Debug.Log(_grounded);
                }
                if (Input.GetButtonDown("Jump") && _grounded)
                {
                    _velocity.y = Mathf.Sqrt(jumpHeight);
                    _hasJumped = true;
                }
                else if (_grounded && _hasJumped)
                {
                    _hasJumped = false;
                }
            }
            
            // DEBUG change perspective
            if (Input.GetKeyDown(KeyCode.Return))
            {
                isFirstPov = !isFirstPov;
                cameraController.SwitchPerspective();
            }

            characterController.Move(_velocity * Time.deltaTime);
        }
        
        // Set the player and camera's room
        public void SetRoom(GameObject room)
        {
            cameraController.SetRoom(room);
        }

        /*
        // Hide mouse
        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }*/
    }
}
