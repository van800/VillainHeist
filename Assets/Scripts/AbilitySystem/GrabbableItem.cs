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

        [SerializeField]
        [Tooltip("Normal Material")]
        private Material regular;
    
        [SerializeField]
        [Tooltip("Interactivity Material / Can be picked up material")]
        private Material selectable;

        private bool _isPickedUp;

        // public float playerYRot;
        
        [SerializeField] private Renderer rend;
    
        // Start is called before the first frame update
        void Start()
        {
            rend.enabled = true;
            rend.material = regular;
            _isPickedUp = false;
            this.originalY = this.transform.position.y;
            _player = FindObjectOfType<PlayerController>();
            Save();
        }
        

        void PickUp()
        {
            this.transform.position = this._player.transform.position + new Vector3(0.0f, 20.0f, 0.0f);
            rend.enabled = false;
            this._isPickedUp = true;
        }

        void PutDown()
        {
            float pickUpDistance = _player.interactDistance;
            Transform playerTransform = this._player.transform;
            rend.enabled = true;
            this.transform.position = playerTransform.position + playerTransform.forward * pickUpDistance +
                                      new Vector3(0, -playerTransform.position.y + originalY, 0);
            this._isPickedUp = false;
            
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
            // if (Input.GetKeyDown("y") && CurrentState)
            // {
            //     this.PutDown();
            // }
            //
            // playerYRot = _player.transform.rotation.eulerAngles.y;
            //
            // if ( Vector3.Distance(_player.transform.position, this.transform.position) < _distance
            //      && !CurrentState)
            // {
            //     rend.material = selectable;
            //     if (Input.GetKeyDown("t"))
            //     {
            //         this.PickUp();
            //     }
            // }
            // else
            // {
            //     rend.material = regular;
            // }
        }
    }
}
