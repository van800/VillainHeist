using System;
using System.ComponentModel;
using JetBrains.Annotations;
using movement_and_Camera_Scripts;
using UnityEngine;

namespace areas_and_respawn
{
    public class AreaController : MonoBehaviour
    {
        private RoomController[] _subRooms;

        public CheckPointController spawnPoint;

        private void Start()
        {
            _subRooms = GetComponentsInChildren<RoomController>();
            foreach (RoomController room in _subRooms)
            {
                if (!room.CompareTag($"Start Room")) continue;
                FindObjectOfType<CameraController>().SetRoom(room);
                FindObjectOfType<PlayerController>().checkpoint = spawnPoint;
                FindObjectOfType<PlayerController>().Respawn();
            }

            foreach (RoomController subArea in _subRooms) // call on sub areas
            {
                subArea.Save();
            }
        }

        public void Reset()
        {
           foreach (RoomController subArea in _subRooms) // call on sub areas
           {
               subArea.Reset();
           }
        }
    }
}
