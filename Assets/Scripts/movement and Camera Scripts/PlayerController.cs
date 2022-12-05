using System;
using AbilitySystem;
using areas_and_respawn;
using Unity.VisualScripting;
using UnityEngine;

namespace movement_and_Camera_Scripts
{
    public class PlayerController : MonoBehaviour
    {
        private CharacterController _characterController;

        private RoomController _currentRoom;

        [SerializeField]
        private CameraController _cameraController;
        [SerializeField][Tooltip("Is First Person Mode")]
        public bool isFirstPov;

        [SerializeField][Tooltip("Speed")]
        public float speed = 5f;
        private Vector3 _velocity;
        
        [SerializeField][Tooltip("Gravity")]
        private float gravity = -9.8f; 
        [SerializeField][Tooltip("Jump height")]
        public float jumpHeight = 5f;
        private bool _grounded;
        private bool _hasJumped;
        private const float GroundCastDist = 0.15f;

        public int maxBattery = 6;
        public int currentBattery = 6;
        [SerializeField] private int shootBatCost = 6;

        private GameUI _gameUI;

        public GrabbableItem pickedUpItem;
        [Tooltip("Distance to interact with items")]
        public float interactDistance = 1f;
        public CheckPointController checkpoint;

        // If false, player is frozen
        private bool canMove;
        
        [SerializeField] private AudioClip tasedSound;
        [SerializeField] private AudioClip pickupSound;
        [SerializeField] private AudioClip topDownMusic;
        [SerializeField] private AudioClip firstPersonMusic;
        
        // AS1 is for sound Effects
        private AudioSource playerAS1;
        
        //AS2 is for Music
        private AudioSource playerAS2;

        private float restartHoldStart = -1; 
        
        private Animator _animator;
        
        [SerializeField]
        private ParticleSystem tazeFlash;
        private Transform playerTransform;
        private Vector3 position;

        [SerializeField]
        private GameObject listenerChild;


        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("Player start");
            isFirstPov = GameState.Instance.isInFirstPerson;

            _characterController = GetComponent<CharacterController>();
            _cameraController = FindObjectOfType<CameraController>();
            _gameUI = FindObjectOfType<GameUI>();
            _gameUI.RegisterPlayer(this);
            canMove = true;
            playerAS1 = GetComponents<AudioSource>()[0];
            playerAS1.volume = 1f;
            playerAS1.spatialBlend = 0f;

            _animator = GetComponent<Animator>();

            playerAS2 = GetComponents<AudioSource>()[1];
            playerAS2.volume = 2f;
            playerAS2.spatialBlend = 0f;

            SetMusic(isFirstPov);
            playerAS2.Play();


            GameState.Instance.setPlayer(this);
            if (GameState.Instance.totalBattery < 1)
            {
                GameState.Instance.totalBattery = maxBattery;
            }
            else
            {
                maxBattery = GameState.Instance.totalBattery;
                RechargeBattery();
            }
        }
        

        // Update is called once per frame
        void Update()
        {
            // Transform Fields
            playerTransform = transform;
            
            position = playerTransform.position + Vector3.up * 0.01f;

            // Movement Inputs
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");
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
                Transform cameraTransform = _cameraController.GetCameraTransform();

                listenerChild.transform.rotation = transform.rotation;

                if (canMove)
                {
                    // Position movement
                    movement = ((playerTransform.forward * z) + (playerTransform.right * x)).normalized;
                }
                else
                {
                    movement = Vector3.zero;
                }

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
                
                
                listenerChild.transform.rotation = new Quaternion(0.707106829f,0,0,0.707106829f);
                

                if (movement.magnitude > 0)
                {
                    _animator.SetBool("Walking", true);
                    playerTransform.rotation = Quaternion.AngleAxis(
                        Vector3.SignedAngle(Vector3.forward, movement, Vector3.up), Vector3.up);
                }
                else
                {
                    _animator.SetBool("Walking", false);
                    float angle = Vector3.SignedAngle(Vector3.forward, playerTransform.forward, Vector3.up);
                    angle = (int)(angle % 360 / 90) * 90;
                    playerTransform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
                }
            }

            _characterController.Move(_velocity * Time.deltaTime);

            
            // Walk and run movement
            _characterController.Move(movement * (speed * Time.deltaTime));

            if (_characterController.transform.position.y < -10)
            {
                Respawn();
            }
            
            // Interact with objects
            Interactable interactable = GetNearestInteractableObj();
            if (interactable is not null)
            {
                interactable.InRange();
                GameUI.AbilityPrompts? prompt = interactable.getInteractionName() switch
                {
                    "Freeze" => GameUI.AbilityPrompts.Freeze,
                    "Pickup" => GameUI.AbilityPrompts.Pickup,
                    "MoveWall" => GameUI.AbilityPrompts.MoveWall,
                    "Light" => GameUI.AbilityPrompts.Light,
                    _ => null
                };
                if (prompt.HasValue)
                {
                    _gameUI.ShowAbilityPrompts(prompt.Value);
                }
            }
            else
            {
                _gameUI.HideAllAbilityPrompts();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (interactable is not null)
                {
                    if (interactable.GetCost() <= currentBattery)
                    {
                        interactable.Interact();
                        currentBattery -= interactable.GetCost();
                        _gameUI.SetBattery(currentBattery, maxBattery);
                    }
                }
                else if (pickedUpItem is not null)
                {
                    pickedUpItem.Interact();
                }
            }
            
            // Respawn on R key press
            if (Input.GetKey(KeyCode.R))
            {
                
                if (restartHoldStart < 0)
                {
                    restartHoldStart = Time.time;
                }
                else if (Time.time - restartHoldStart > 1f)
                {
                    Respawn();
                    restartHoldStart = -1;
                }
            }
            else
            {
                restartHoldStart = -1;
            }
            
            // DEBUG change perspective
            if (Input.GetKeyDown(KeyCode.Return))
            {
                // ToPov();
            }
        }

        public void Respawn()
        {
            GrabbableItem pickedUp = pickedUpItem;
            pickedUpItem = null;
            if (pickedUp is not null) pickedUp.Reset();
            checkpoint.Respawn(this);
            RechargeBattery();
        }

        // Set the player and camera's room
        public void SetRoom(RoomController room)
        {
            _currentRoom = room;
            _cameraController.SetRoom(room); 
        }

        public void AddMaxBattery(int amount)
        {
            currentBattery += amount;
            maxBattery += amount;
            UpdateBatteryUI();
        }

        public bool CanShoot()
        {
            return currentBattery == shootBatCost;
        }

        public void RemoveBatteryShoot()
        {
            currentBattery -= shootBatCost;
            UpdateBatteryUI();
            //_gameUI.SetBattery(currentBattery, maxBattery);
        }


        public void RechargeBattery(int amount)
        {
            currentBattery = Math.Min(currentBattery + amount, maxBattery);
            UpdateBatteryUI();
        }
        
        public void RechargeBattery()
        {
            currentBattery = maxBattery;
            UpdateBatteryUI();
        }

        private void UpdateBatteryUI()
        {
            _gameUI.SetBattery(currentBattery, maxBattery);
        }
        
        // Set the player's checkpoint
        public void SetCheckpoint(CheckPointController cp)
        {
            RechargeBattery();
            if (checkpoint != null)
            {
                checkpoint.DeselectCheckpoint();
            }
            checkpoint = cp;
        }
        
        // Switch Pov of game
        public void ToPov(bool toFirst = true)
        {
            isFirstPov = toFirst;
            GameState.Instance.isInFirstPerson = toFirst;
            _cameraController.SetPerspective(toFirst);
            SetMusic(isFirstPov);
            if (EscapeTimer.Instance != null)
            {
                Debug.Log("StartTimer");
                EscapeTimer.Instance.startTimer();
            }
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

        // Freeze player
        public void Tased()
        {
            canMove = false;
            PlayTaseSound();
            Instantiate(tazeFlash, position, Quaternion.identity);
            Invoke(nameof(Unfreeze), 2f);
        }

        private void Unfreeze()
        {
            canMove = true;
        }

        private void PlayTaseSound()
        {
            playerAS1.clip = tasedSound;
            playerAS1.Play();
        }
        
        public void PlayPickupSound()
        {
            playerAS1.clip = pickupSound;
            playerAS1.Play();
        }

        private void SetMusic(bool firstPerson)
        {
            playerAS2.clip = firstPerson ? firstPersonMusic : topDownMusic;
            playerAS2.Play();
        }
        
        private Interactable GetNearestInteractableObj()
        {
            Vector3 playerPos = transform.position;
            Collider[] nearestColliders = Physics.OverlapSphere(playerPos, interactDistance);
            Collider closest = null;
            foreach (Collider c in nearestColliders)
            {
                //Debug.Log("c = " + c.name);
                if (closest is null && c.CompareTag("Interactable"))
                {
                    closest = c;
                }
                else if (c.CompareTag("Interactable") && 
                         (c.transform.position - playerPos).magnitude < 
                         (closest.transform.position - playerPos).magnitude)
                {
                    closest = c;
                }
            }
            if (closest is not null)
            {
                return closest.GetComponentInChildren<Interactable>();
            }
            return null;
        }
    }
}
