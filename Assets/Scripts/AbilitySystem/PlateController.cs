using System;
using areas_and_respawn;
using UnityEngine;

namespace AbilitySystem
{
    public class PlateController : Interactable
    {
        [SerializeField] [Tooltip("Block to trigger event when placed.")]
        private GrabbableItem key;
        
        private LockedDoorAidan _lock;

        [SerializeField] [Tooltip("Invert the state for when block is placed.")]
        private bool inverted;

        private bool _triggered;
        
        // Start is called before the first frame update
        void Start()
        { 
            _triggered = inverted;
        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision.transform.TryGetComponent(out GrabbableItem grabbable))
            {
                if (grabbable == key)
                {
                    _triggered = true ^ inverted;
                }
            }
        }
        
        private void OnCollisionExit(Collision collision)
        {
            _triggered = false ^ inverted;
        }

        // Update is called once per frame
        void Update()
        {
            _lock.TryUnlock();
        }

        public void SetDoor(LockedDoorAidan lockedDoor)
        {
            _lock = lockedDoor;
        }

        public bool IsTriggered()
        {
            return _triggered;
        }

        public override void Interact() {} // No interaction

        public override void Save()
        {
            SavedState = _triggered;
        }

        public override void Reset()
        {
            _triggered = SavedState;
        }
    }
}
