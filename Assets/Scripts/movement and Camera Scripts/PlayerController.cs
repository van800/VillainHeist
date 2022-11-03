using System;
using UnityEngine;

namespace movement_and_Camera_Scripts
{
    public class PlayerController : MonoBehaviour
    { 
        private CharacterController _characterController;

        private CameraController _cameraController;
        [SerializeField][Tooltip("Is First Person Mode")]
        public bool isFirstPov;

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
        private bool _hasJumped;
        private const float GroundCastDist = 0.15f;

        public GameObject pickedUpItem;
        [SerializeField][Tooltip("Distance to interact with items")]
        private float pickupDistance = 2f;
        public CheckPointController checkpoint;

        
        // Start is called before the first frame update
        void Start()
        {
            _characterController = GetComponent<CharacterController>();
            _cameraController = FindObjectOfType<CameraController>();
        }
        

        // Update is called once per frame
        void Update()
        {
            // Transform Fields
            Transform playerTransform = transform;
            Transform cameraTransform = _cameraController.transform;
            
            Vector3 position = playerTransform.position + Vector3.up * 0.01f;

            // Movement Inputs
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Vector3 movement;

            // Gravity
            _grounded = Physics.Raycast(position, Vector3.down, GroundCastDist);
            if (!_grounded)
            {
                _velocity.y += gravity * Time.deltaTime;
            }
            
            // First Person only
            if (isFirstPov)
            {
                // Position movement
                movement = ((playerTransform.forward * z) + (playerTransform.right * x)).normalized;

                // Rotation movement
                playerTransform.rotation = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up);
                
                // Jumping
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
            else
            {
                // Position movement
                movement = ((Vector3.forward * z) + (Vector3.right * x)).normalized;

                if (movement.magnitude > 0)
                {
                    playerTransform.rotation = Quaternion.AngleAxis(
                        Vector3.SignedAngle(Vector3.forward, movement, Vector3.up), Vector3.up);
                }
                else
                {
                    playerTransform.rotation = Quaternion.AngleAxis(180, Vector3.up);
                }
            }

            _characterController.Move(_velocity * Time.deltaTime);

            
            // Walk and run movement
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _characterController.Move(movement * (speed * runMultiplier * Time.deltaTime));
            }
            else
            {
                _characterController.Move(movement * (speed * Time.deltaTime));
            }
            
            // Pickup Item
            if (Input.GetKeyDown(KeyCode.E))
            {
                //TODO: @Joseph add interactable object code here
                //use pickupDistance field
            }
            
            // Respawn on R key press
            if (Input.GetKeyDown(KeyCode.R))
            {
                Respawn();
            }
            
            // DEBUG change perspective
            if (Input.GetKeyDown(KeyCode.Return))
            {
                ToPov();
            }
        }

        public void Respawn()
        {
            checkpoint.Respawn(this);
        }

        // Set the player and camera's room
        public void SetRoom(AreaController area)
        {
            _cameraController.SetRoom(area);
        }
        
        // Switch Pov of game
        public void ToPov(bool toFirst = true)
        {
            isFirstPov = toFirst;
            _cameraController.SetPerspective(toFirst);
        }
        
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
        }
    }
}
