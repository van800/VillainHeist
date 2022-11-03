using System;
using Unity.VisualScripting;
using UnityEngine;

namespace movement_and_Camera_Scripts
{
    public class DoorController : MonoBehaviour
    {
        [SerializeField] [Tooltip("Other Side Of the Door")]
        private DoorController otherDoor;

        private Transform _enterPosition;

        private AreaController _area;

        private void Start()
        {
            _enterPosition = GetComponentsInChildren<Transform>()[1];
            _area = GetComponentInParent<AreaController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            otherDoor.Teleport();
        }

        private void Teleport()
        {
            PlayerController player = FindObjectOfType<PlayerController>();
            if (player.isFirstPov) return; // Turn off teleporting for 1st person
            player.transform.position = _enterPosition.position;
            player.SetRoom(_area);

        }
    }
}
