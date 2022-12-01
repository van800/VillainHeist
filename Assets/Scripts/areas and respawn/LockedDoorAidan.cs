using System;
using AbilitySystem;
using UnityEngine;

namespace areas_and_respawn
{
    public class LockedDoorAidan : Interactable
    {
        private bool _isLocked = true;
        
        [SerializeField] [Tooltip("Invert the state for when block is placed.")]
        private bool inverted;

        [SerializeField] private DoorController[] doors;

        [SerializeField] [Tooltip("Plates that need to be active to unlock.")] 
        private PlateController[] triggers;
        
        private bool _displaying;

        private void Start()
        {
            foreach (PlateController trigger in triggers)
            {
                trigger.SetDoor(this);
            }
            TryUnlock();
        }

        public void TryUnlock()
        {
            bool checkTriggers = true;
            foreach (PlateController trigger in triggers)
            {
                checkTriggers &= trigger.IsTriggered();
            }
            _isLocked = !checkTriggers ^ inverted;
            gameObject.SetActive(_isLocked);
            foreach (DoorController door in doors)
            {
                door.Lock(_isLocked);
            }
        }

        public override void Interact() {} // Locked doors are not interactable

        public override void Save()
        {
            print("SAVED");
            SavedState = _isLocked;
        }

        public override void Reset()
        {
            _isLocked = SavedState;
        }

        public override void InRange()
        {
            if (!_displaying)
            {
                _displaying = true;
                //TODO: send message that door is locked
                Debug.Log("LOCKED");
                Invoke(nameof(OutOfRange), 2f);
            }
        }

        protected override void OutOfRange()
        {
            _displaying = false;
        }
    }
}
