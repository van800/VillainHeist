using System;
using UnityEngine;

namespace aidan_scripts
{
    public class DoorController : MonoBehaviour
    {
        [SerializeField] [Tooltip("Player")]
        public PlayerController playerController;

        [SerializeField] [Tooltip("Other Side Of the Door")]
        private DoorController otherDoor;

        [SerializeField][Tooltip("Spawn Point")]
        private Transform spawnPoint;

        [SerializeField][Tooltip("Room Door Leaves FROM")]
        private GameObject room;

        
        private void OnTriggerEnter(Collider other)
        {
            otherDoor.Teleport();
        }

        private void Teleport()
        {
            playerController.transform.position = spawnPoint.position;
            playerController.SetRoom(room);
        }
    }
}
