using System;
using System.Collections.Generic;
using System.Linq;
using areas_and_respawn;
using Unity.VisualScripting;
using UnityEngine;

namespace AbilitySystem
{
    public class PlateController : MonoBehaviour
    {
        [SerializeField] [Tooltip("Material when pressed.")]
        private Material pressedMaterial;

        private Renderer _renderer;

        private Material _defaultMaterial;
        
        [SerializeField] [Tooltip("Block to trigger event when placed.")]
        private GrabbableItem key;
        
        private readonly List<LockedDoorAidan> _locks = new();

        [SerializeField] [Tooltip("Invert the state for when block is placed.")]
        private bool inverted;

        private bool _triggered;

        private void Start()
        {
            _triggered = inverted;
            _renderer = GetComponent<Renderer>();
            _defaultMaterial = _renderer.material;
        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision.transform.TryGetComponent(out GrabbableItem grabbable))
            {
                if (grabbable == key)
                {
                    _triggered = true ^ inverted;
                    _renderer.material = pressedMaterial;
                }
            }
        }
        
        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag("Interactable"))
            {
                _triggered = false ^ inverted;
                _renderer.material = _defaultMaterial;
            }
        }

        // Update is called once per frame
        void Update()
        {
            foreach (LockedDoorAidan lockedDoor in _locks)
            {
                lockedDoor.TryUnlock();
            }
        }

        public void AddDoor(LockedDoorAidan lockedDoor)
        {
            _locks.Add(lockedDoor);
        }

        public bool IsTriggered()
        {
            return _triggered;
        }

        public void SetTriggered(bool triggered)
        {
            _triggered = triggered;
        }
    }
}
