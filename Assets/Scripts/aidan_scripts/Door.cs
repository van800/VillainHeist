using System;
using UnityEngine;

namespace aidan_scripts
{
    public class Door : MonoBehaviour
    {
        [SerializeField] [Tooltip("Player")]
        public GameObject player;
        
        [SerializeField] [Tooltip("Camera")]
        public new Camera camera;

        [SerializeField] [Tooltip("Other Door")]
        private Door otherDoor;

        [SerializeField][Tooltip("Spawn Point")]
        private Transform spawnPoint;
        
        [SerializeField][Tooltip("Camera Point")]
        private Transform cameraPoint;

        
        private void OnTriggerEnter(Collider other)
        {
            otherDoor.Teleport(player);
        }

        private void Teleport(GameObject playerObject)
        {
            playerObject.transform.position = spawnPoint.position;
            camera.transform.position = cameraPoint.position;
        }
    }
}
