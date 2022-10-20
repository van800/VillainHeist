using UnityEngine;

namespace movement_and_Camera_Scripts
{
    public class DoorController : MonoBehaviour
    {
        [SerializeField] [Tooltip("Other Side Of the Door")]
        private DoorController otherDoor;

        [SerializeField][Tooltip("Spawn Point")]
        private Transform spawnPoint;

        [SerializeField][Tooltip("Room Door Leaves FROM")]
        private RoomController room;

        
        private void OnTriggerEnter(Collider other)
        {
            otherDoor.Teleport();
        }

        private void Teleport()
        {
            PlayerController player = GameObject.FindObjectOfType<PlayerController>();
            // if (player.isFirstPov) return; // Turn off teleporting for 1st person
            player.transform.position = spawnPoint.position;
            player.SetRoom(room);

        }
    }
}
