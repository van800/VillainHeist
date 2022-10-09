using UnityEngine;

namespace Aidan.scripts
{
    public class TopDownController : MonoBehaviour
    { 
        [SerializeField][Tooltip("Character Controller")]
        private CharacterController controller;

        [SerializeField][Tooltip("Main Camera")]
        private new Camera camera;

        private Vector3 _velocity;
        
        [SerializeField][Tooltip("Gravity")]
        //private float gravity = -35f; 
        //private bool _grounded;
        //private const float GroundCastDist = 0.15f;
        //private bool _hasJumped = false;
        
        public float speed = 5f;
        public float runMultiplier = 3f;
        //public float jumpHeight = 150f;
        
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            // Transform Fields
            Transform playerTransform = transform;
            Transform cameraTransform = camera.transform;
            
            // Grounded
            Vector3 position = playerTransform.position + Vector3.up * 0.01f;

            // Ground movement
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Vector3 movement = ((playerTransform.right * x) + (playerTransform.forward * z)).normalized;
           
            // Walk and run movement
            if (Input.GetKey(KeyCode.LeftShift))
            {
                controller.Move(movement * (speed * runMultiplier * Time.deltaTime));
            }
            else
            {
                controller.Move(movement * (speed * Time.deltaTime));
            }
            
            /*** JUMPING TURNED OFF
        
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
            
            // Gravity and Jumping
            _velocity.y += gravity * Time.deltaTime;
            if (Input.GetButtonDown("Jump") && _grounded)
            {
                _velocity.y = Mathf.Sqrt(jumpHeight);
                _hasJumped = true;
            }
            else if(_grounded && _hasJumped)
            {
                _hasJumped = false;
            }
            ***/
            
            controller.Move(_velocity * Time.deltaTime);
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
