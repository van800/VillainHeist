using System;
using movement_and_Camera_Scripts;
using UnityEngine;

namespace areas_and_respawn
{
    public class DoorController : MonoBehaviour
    {
        [SerializeField] [Tooltip("Other Side Of the Door")]
        private DoorController otherDoor;

        private Transform _enterPosition;

        private AreaController _area;

        private PlayerController _player;

        private BoxCollider _collider;

        private void Start()
        {
            _enterPosition = GetComponentsInChildren<Transform>()[1];
            _area = GetComponentInParent<AreaController>();
            _player = FindObjectOfType<PlayerController>();
            _collider = GetComponent<BoxCollider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _area.Save();
                otherDoor.Teleport();
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            //TODO update to match in game message system
            Debug.Log("You cannot leave while carrying a block");
        }

        private void Teleport()
        {
            if (_player.isFirstPov) return; // Turn off teleporting for 1st person
            _player.transform.position = _enterPosition.position;
            _player.SetRoom(_area);
        }

        private void Update()
        {
            _collider.isTrigger = _player.pickedUpItem is null;
        }
    }
}
