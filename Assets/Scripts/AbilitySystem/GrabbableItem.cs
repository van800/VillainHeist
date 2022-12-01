using areas_and_respawn;
using movement_and_Camera_Scripts;
using Unity.Mathematics;
using UnityEngine;

namespace AbilitySystem
{
    public class GrabbableItem : Interactable
    {
        private PlayerController _player;

        private float originalY;

        // [SerializeField]
        // [Tooltip("Normal Material")]
        // private Material regular;

        private bool _isPickedUp;

        private Rigidbody _rigidbody;
        
        // public float playerYRot;
        
        // [SerializeField] private Renderer rend;
    
        // Start is called before the first frame update
        void Start()
        {
            // rend = GetComponent<MeshRenderer>();
            // regular = Renderer.material;
            // rend.enabled = true;
            _isPickedUp = false;
            this.originalY = this.transform.position.y;
            _player = FindObjectOfType<PlayerController>();
            _rigidbody = GetComponent<Rigidbody>();
            
            _rigidbody.constraints = RigidbodyConstraints.FreezePositionX | 
                                     RigidbodyConstraints.FreezePositionZ |
                                     RigidbodyConstraints.FreezeRotation;
        }
        

        void PickUp()
        {
            this.transform.position = this._player.transform.position + new Vector3(0.0f, 20.0f, 0.0f);
            Renderer.enabled = false;
            this._isPickedUp = true;
            _rigidbody.isKinematic = true;
        }

        void PutDown()
        {
            float pickUpDistance = _player.interactDistance;
            Transform playerTransform = this._player.transform;
            Renderer.enabled = true;
            this.transform.position = playerTransform.position + playerTransform.forward * pickUpDistance +
                                      new Vector3(0, -playerTransform.position.y + originalY + 0.5f, 0);
            this._isPickedUp = false;
            _rigidbody.isKinematic = false;
        }

        public override void Interact()
        {
            if (_isPickedUp)
            {
                if (_player.pickedUpItem == this)
                {
                    _player.pickedUpItem = null;
                    PutDown();
                }
            }
            else
            {
                if (_player.pickedUpItem is null)
                {
                    _player.pickedUpItem = this;
                    PickUp();
                }
            }
        }

        public override void Save()
        {
            Transform t = transform;
            SavedPosition = t.position;
            SavedRotation = t.rotation;
            SavedState = _isPickedUp;
        }

        public override void Reset()
        {
            _isPickedUp = SavedState;
            if (_isPickedUp)
            {
                _player.pickedUpItem = this;
                PickUp();
            }
            else
            {
                PutDown();
                Transform t = transform;
                t.position = SavedPosition;
                t.rotation = SavedRotation;
            }
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}
