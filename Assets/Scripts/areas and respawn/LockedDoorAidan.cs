using System;
using System.Collections.Generic;
using AbilitySystem;
using JetBrains.Annotations;
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

        private bool _isFirst;


        private void Start()
        {
            _isFirst = GameState.Instance.isInFirstPerson;
            gameObject.SetActive(!_isFirst);
        }

        public override int GetCost()
        {
            return 0;
        }
        
        public override string getInteractionName()
        {
            return "";
        }
        
        protected override void Initialize()
        {
            foreach (PlateController trigger in triggers)
            {
                trigger.AddDoor(this);
            }
        }

        public void TryUnlock()
        {
            if (!_isFirst)
            {
                
                bool checkTriggers = true;
                foreach (PlateController trigger in triggers)
                {
                    checkTriggers &= trigger.IsTriggered() ^ inverted;
                }
                _isLocked = !checkTriggers;
                gameObject.SetActive(_isLocked);
                foreach (DoorController door in doors)
                {
                    door.Lock(_isLocked);
                }
            }
        }

        public override void Interact() {} // Locked doors are not interactable

        public override void Save()
        {
            SavedState = _isLocked;
            foreach (PlateController plate in triggers)
            {
                plate.Save();
            }
        }

        public override void Reset()
        {
            _isLocked = SavedState;
            foreach (DoorController door in doors)
            {
                door.Lock(_isLocked);
            }
            foreach (PlateController plate in triggers)
            {
                plate.Reset();
            }
        }

        public override void InRange()
        {
            if (!_displaying)
            {
                _displaying = true;
                //TODO: send message that door is locked
                print("LOCKED");
                Invoke(nameof(OutOfRange), 2f);
            }
        }

        protected override void OutOfRange()
        {
            _displaying = false;
        }

     
    }
}
